using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public int? CareTypeId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxDependents { get; set; }
        public string SessionTimeString => $"{StartTime} - {EndTime}";

        // Navigation property to represent the session dependents associated with a session
        public ICollection<SessionDependent>? SessionDependents { get; set; }
    }
}