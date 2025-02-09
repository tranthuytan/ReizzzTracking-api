using Microsoft.AspNetCore.Http;
using ReizzzTracking.BL.Errors.Auth;
using ReizzzTracking.BL.Errors.Common;
using ReizzzTracking.BL.Services.TodoScheduleServices;
using ReizzzTracking.BL.ViewModels.ResultViewModels;
using ReizzzTracking.BL.ViewModels.TodoScheduleViewModels;
using ReizzzTracking.BL.ViewModels.ToDoScheduleViewModelsToDoSchedule;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.RoutineRepository;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.BL.Services.TodoScheduleServices
{
    public class TodoScheduleService : ITodoScheduleService
    {
        private readonly ITodoScheduleRepository _todoScheduleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TodoScheduleService(ITodoScheduleRepository todoScheduleRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _todoScheduleRepository = todoScheduleRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResultViewModel> UserAddToDoSchedule(TodoScheduleAddViewModel todoVM)
        {
            ResultViewModel result = new();
            try
            {
                string? creatorIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (creatorIdString == null)
                {
                    throw new Exception(AuthError.UserClaimsAccessFailed);
                }

                TodoSchedule toDo = todoVM.ToToDoSchedule(todoVM);
                toDo.AppliedFor = long.Parse(creatorIdString);

                _todoScheduleRepository.Add(toDo);
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> DeleteToDoSchedules(long[] ids)
        {
            ResultViewModel result = new();
            try
            {
                foreach (var id in ids)
                {
                    TodoSchedule? toDoScheduleForDelete = await _todoScheduleRepository.Find(id);
                    if (toDoScheduleForDelete == null)
                    {
                        throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(TodoSchedule), id));
                    }
                    _todoScheduleRepository.Remove(toDoScheduleForDelete);
                }
                await _unitOfWork.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<TodoScheduleGetResultViewModel> GetToDoScheduleById(long id)
        {
            TodoScheduleGetResultViewModel result = new TodoScheduleGetResultViewModel();
            try
            {
                TodoSchedule? todo = await _todoScheduleRepository.Find(id);
                if (todo == null)
                {
                    throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(TodoSchedule), id));
                }
                TodoScheduleGetViewModel todoScheduleGetViewModel = new TodoScheduleGetViewModel();
                todoScheduleGetViewModel = todoScheduleGetViewModel.FromToDoSchedule(todo);
                result.PaginatedResult.Data.Add(todoScheduleGetViewModel);
                result.PaginatedResult.IsPaginated = false;
                result.PaginatedResult.TotalRecord = 1;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<TodoScheduleGetResultViewModel> GetToDoSchedules(GetTodoScheduleRequestViewModel request)
        {
            TodoScheduleGetResultViewModel result = new TodoScheduleGetResultViewModel();
            result.PaginatedResult.IsPaginated = request.IsPaginated;
            result.PaginatedResult.CurrentPage = request.CurrentPage;
            result.PaginatedResult.PageSize = request.PageSize;
            try
            {
                var todoSchedules = await _todoScheduleRepository.Pagination(request.CurrentPage,
                                                                                request.PageSize,
                                                                                td=> td.AppliedFor==request.UserId,
                                                                                null,
                                                                                null,
                                                                                [nameof(TodoSchedule.StartAt)],
                                                                                [false]);
                result.PaginatedResult.TotalRecord = todoSchedules.Item1;
                foreach (var todoSchedule in todoSchedules.Item2)
                {
                    TodoScheduleGetViewModel todoVM = new();
                    result.PaginatedResult.Data.Add(todoVM.FromToDoSchedule(todoSchedule));
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        public async Task<ResultViewModel> UpdateOrAddToDoSchedule(TodoScheduleUpdateViewModel[] todoVMs)
        {
            ResultViewModel result = new();
            foreach (var todoVM in todoVMs)
            {
                //Complete Todo
                if (todoVM.IsDone == true)
                {
                    todoVM.EndAt = DateTime.UtcNow;
                    todoVM.ActualTime = TimeDifferenceBetweenStartAtAndEndAtByTimeUnit((long)todoVM.TimeUnitId, (DateTime)todoVM.StartAt, (DateTime)todoVM.EndAt);
                }
                //Update Todo

                TodoSchedule todoToUpdate = todoVM.ToToDoSchedule(todoVM);
                try
                {

                    //Update existing Todo
                    if (todoVM.Id != null)
                    {
                        //Check if todo is existed
                        var todo = await _todoScheduleRepository.Find((long)todoVM.Id);
                        if (todo == null)
                        {
                            throw new Exception(string.Format(CommonError.NotFoundWithId, nameof(TodoSchedule), todoToUpdate.Id));
                        }
                        _todoScheduleRepository.Update(todoToUpdate, td => td.CategoryType);
                    }
                    //Create new routine if not exist
                    else
                    {
                        _todoScheduleRepository.Add(todoToUpdate);
                    }
                    await _unitOfWork.SaveChangesAsync();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Errors.Add(ex.Message);
                }
            }
            return result;
        }
        public decimal TimeDifferenceBetweenStartAtAndEndAtByTimeUnit(long id, DateTime startAt, DateTime endAt)
        {
            //TODO: room for improvement
            var timeUnits = TimeUnit.GetValues();
            TimeSpan timeDifference = endAt- startAt;
            decimal result = id switch
            {
                1 => (decimal)timeDifference.TotalSeconds,
                2 => (decimal)timeDifference.TotalMinutes,
                3 => (decimal)timeDifference.TotalHours,
                _ => throw new Exception("TimeUnit doesn't belong to any existed case")
            };
            return result;
        }
    }
}
