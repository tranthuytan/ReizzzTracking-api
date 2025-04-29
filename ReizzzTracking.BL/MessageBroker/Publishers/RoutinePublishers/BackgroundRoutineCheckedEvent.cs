using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReizzzTracking.DAL.Entities;

namespace ReizzzTracking.BL.MessageBroker.Publishers.RoutinePublishers
{
    public record BackgroundRoutineCheckedEvent
    {
        public long Id { get; set; }
        public string UserName {get;set;} = string.Empty;
        public string UserEmail {get;set;} = string.Empty;

        public string? StartTime { get; set; }

        public string? Name { get; set; }

        public bool? IsPublic { get; set; }
        public bool? IsActive { get; set; }
        public long? CategoryType { get; set; }
        public long? RoutineCollectionId { get; set; }
    }
}
