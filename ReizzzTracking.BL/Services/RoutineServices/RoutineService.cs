using Microsoft.AspNetCore.Http;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineViewModel;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.RoutineServices
{
    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly IRoutineCollectionRepository _routineCollectionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public RoutineService(IRoutineRepository routineRepository,
            IRoutineCollectionRepository routineCollectionRepository,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _routineRepository = routineRepository;
            _routineCollectionRepository = routineCollectionRepository;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> AddRoutine(RoutineAddViewModel routineVM)
        {
            var result = new ResultViewModel();
            try
            {
                string? creatorIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (creatorIdString == null)
                {
                    throw new Exception(AuthError.UserClaimsAccessFailed);
                }
                Routine addRoutine = routineVM.ToRoutine(routineVM);
                if (routineVM.RoutineCollectionId != null)
                {
                    _routineRepository.Add(addRoutine);
                }
                else
                {
                    RoutineCollection addRoutineCollection = new RoutineCollection
                    {
                        Name = "New Routine Collection",
                        CreatedBy = long.Parse(creatorIdString),
                        IsPublic = false
                    };
                    _routineCollectionRepository.Add(addRoutineCollection);
                    await _unitOfWork.SaveChangesAsync();
                    addRoutine.RoutineCollectionId = addRoutineCollection.Id;
                    _routineRepository.Add(addRoutine);
                }
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
            return result;
        }

        public Task<ResultViewModel> UpdateRoutine(RoutineAddViewModel routineVM)
        {
            throw new NotImplementedException();
        }
    }
}
