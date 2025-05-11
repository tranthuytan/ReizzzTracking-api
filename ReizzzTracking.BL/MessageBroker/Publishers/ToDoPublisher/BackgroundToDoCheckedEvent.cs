namespace ReizzzTracking.BL.MessageBroker.Publishers.ToDoPublisher
{
    public record BackgroundToDoCheckedEvent
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? StartAt => StartAtUtc.GetValueOrDefault().AddHours(7);
        public DateTime? StartAtUtc { get; set; }
        public int? EstimatedTime { get; set; }
        public string TimeUnitString { get; set; } = string.Empty;
    }
}
