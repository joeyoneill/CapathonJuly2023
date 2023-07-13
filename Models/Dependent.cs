using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models
{
    public class Dependent
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public DateTime? Birthday { get; set; }
        public string? AdditionalNotes { get; set; }
        public string ClientId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}