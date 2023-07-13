using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zipcode { get; set; }
        public int? BusinessId { get; set; }
        public string FullAddress => $"{Address}, {City}, {State}, {Country}, {Zipcode}";
    }
}