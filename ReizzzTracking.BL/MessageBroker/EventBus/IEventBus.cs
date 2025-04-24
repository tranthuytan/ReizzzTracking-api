namespace ReizzzTracking.BL.MessageBroker.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}
