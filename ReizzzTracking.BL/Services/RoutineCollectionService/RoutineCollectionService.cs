using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.RoutineCollectionViewModels;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Repositories.RoutineCollectionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.RoutineCollectionService
{
    internal class RoutineCollectionService : IRoutineCollectionService
    {
        private readonly IRoutineCollectionRepository _routineCollectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoutineCollectionService(IRoutineCollectionRepository routineCollectionRepository, IUnitOfWork unitOfWork)
        {
            _routineCollectionRepository = routineCollectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> AddRoutineCollection(RoutineCollectionAddViewModel routineCollectionVM)
        {
            var result = new ResultViewModel();
            try
            {
                var routineCollection = routineCollectionVM.ToRoutineCollection(routineCollectionVM);
                _routineCollectionRepository.Add(routineCollection);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                throw;
            }
            return result;
        }
    }
}
