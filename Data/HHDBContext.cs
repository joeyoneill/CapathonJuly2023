using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace CAPATHON.Data
{
    public partial class HHDBContext : DbContext
    {
        // constructor
        public HHDBContext(DbContextOptions<HHDBContext> options) : base(options)
        {
        }

        // tables
        // EX: public virtual DbSet<Book>? Books { get; set; }
        public virtual DbSet<Client>? Clients { get; set; }
        public virtual DbSet<Dependent>? Dependents { get; set; }
    }
}