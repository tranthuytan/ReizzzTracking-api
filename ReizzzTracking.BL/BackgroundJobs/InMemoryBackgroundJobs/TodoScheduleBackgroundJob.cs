using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using ReizzzTracking.BL.MessageBroker.Publishers.ToDoPublisher;
using ReizzzTracking.DAL.Entities;
using ReizzzTracking.DAL.Repositories.TodoScheduleRepository;
using ReizzzTracking.DAL.Repositories.UserRepository;

namespace ReizzzTracking.BL.BackgroundJobs.InMemoryBackgroundJobs
{
    public class TodoScheduleBackgroundJob : IJob
    {
        private readonly ITodoScheduleRepository _todoScheduleRepository;
        private readonly ILogger<TodoScheduleBackgroundJob> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ToDoPublisher _toDoPublisher;

        public TodoScheduleBackgroundJob(ITodoScheduleRepository todoScheduleRepository,
                                            ILogger<TodoScheduleBackgroundJob> logger,
                                            IUserRepository userRepository,
                                            ToDoPublisher toDoPublisher)
        {
            _todoScheduleRepository = todoScheduleRepository;
            _logger = logger;
            _userRepository = userRepository;
            _toDoPublisher = toDoPublisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start of {nameof(TodoScheduleBackgroundJob)}");
            var dataMap = context.JobDetail.JobDataMap;
            var toDoJson = dataMap.GetString("toDo");
            if (toDoJson is not null)
            {
                var toDo = JsonConvert.DeserializeObject<TodoSchedule>(toDoJson);
                if (toDo is not null)
                {

                    var toDoUser = await _userRepository.Find(toDo.AppliedFor);
                    if (toDo is not null && toDo.IsDone == false)
                    {
                        BackgroundToDoCheckedEvent backgroundToDoCheckedEvent = new BackgroundToDoCheckedEvent
                        {
                            Id = toDo.Id,
                            UserName = toDoUser!.Name!,
                            UserEmail = toDoUser!.Email!,
                            StartAtUtc = toDo.StartAtUtc,
                            Name = toDo.Name,
                            EstimatedTime = toDo.EstimatedTime,
                            TimeUnitString = TimeUnit.FromValue((int)toDo.TimeUnitId.GetValueOrDefault())!.Name
                        };
                        await _toDoPublisher.PublishToDo(backgroundToDoCheckedEvent);

                    }

                    _logger.LogInformation($"{nameof(TodoScheduleBackgroundJob)} completed of toDoId = {toDo!.Id}, startTime = {toDo.StartAtUtc.AddHours(7)}");
                }
            }
        }
    }
}
