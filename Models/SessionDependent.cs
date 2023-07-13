using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models
{
    public class SessionDependent
    {
        public int Id { get; set; }
        public Guid DependentId { get; set; }
        public Guid SessionId { get; set; }

        public Session? session { get; set; }
        public Dependent? dependent { get; set; }
    }
}