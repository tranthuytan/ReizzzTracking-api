using ReizzzTracking.BL.MessageBroker.EventBus;
using ReizzzTracking.BL.MessageBroker.Publishers.BasePublishers;

namespace ReizzzTracking.BL.MessageBroker.Publishers.ToDoPublisher
{
    public class ToDoPublisher : BasePublisher
    {
        public ToDoPublisher(IEventBus eventBus) : base(eventBus)
        {
        }
        public async Task PublishToDo(BackgroundToDoCheckedEvent backgroundToDoCheckedEvent)
        {
            await _eventBus.PublishAsync(backgroundToDoCheckedEvent);
        }
    }
}