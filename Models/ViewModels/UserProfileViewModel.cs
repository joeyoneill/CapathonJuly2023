using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string? UserId { get; internal set; }
        public string? Name { get; internal set; }
        public string? Email { get; internal set; }
        public Client? Client_Obj { get; internal set; }
        public List<Dependent>? client_dependents { get; set; }
    }
}