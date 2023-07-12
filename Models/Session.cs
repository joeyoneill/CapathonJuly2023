using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models
{
    public class Session
    {
        public guid Id { get; set; }
        public int? CareTypeId { get; set; }
        public int? LocationId { get; set; }
        public datetime? StartTime { get; set; }
        public datetime? EndTime { get; set; }
        public int? MaxDependents { get; set; }
    }
}