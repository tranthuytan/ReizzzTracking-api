using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.DAL.Common.DateTimeToUtc;
using ReizzzTracking.DAL.Common.UnitOfWork;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class RoutineIsUsedCheckBackgroundJob : IJob
    {
        private readonly ITodoScheduleRepository _todoScheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoutineIsUsedCheckBackgroundJob(ITodoScheduleRepository todoScheduleRepository,
                                                IUnitOfWork unitOfWork)
        {
            _todoScheduleRepository = todoScheduleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("RoutineIsUsedCheckBackgroundJob starting");
            var dataMap = context.JobDetail.JobDataMap;
            var routineJson = dataMap.GetString("routine");
            if (routineJson is not null)
            {
                var routine = JsonConvert.DeserializeObject<Routine>(routineJson!);

                if (routine != null && routine.CreatedBy != null)
                {
                    var routineStartAtTime = TimeMapper.FromTimeStringUtc7ToUtc(routine!.StartTime!);
                    DateTime startAt = new DateTime(DateOnly.FromDateTime(DateTime.UtcNow), routineStartAtTime);

                    TodoSchedule todoScheduleToAdd = new TodoSchedule
                    {
                        Name = routine!.Name!,
                        StartAt = startAt,
                        AppliedFor = routine.CreatedBy,
                        IsDone = false,
                        CategoryType = 1,
                    };
                    _todoScheduleRepository.Add(todoScheduleToAdd);
                    await _unitOfWork.SaveChangesAsync();

                }
                Console.WriteLine("RoutineIsUsedCheckBackgroundJob completed");
            }
        }
    }
}
