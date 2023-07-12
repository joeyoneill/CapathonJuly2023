using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPATHON.Models.ViewModels
{
    public class AddDependentViewModel
    {
        public Guid Id = new Guid();
        public string ClientId;
        public Dependent dependent;
    }
}