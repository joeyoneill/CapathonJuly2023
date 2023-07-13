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

        // Front Facing Tables
        public virtual DbSet<Client>? Clients { get; set; }
        public virtual DbSet<Dependent>? Dependents { get; set; }

        // Back-end Facing Tables
        public virtual DbSet<CareType>? CareTypes { get; set; }
        public virtual DbSet<Business>? Businesses { get; set; }
        public virtual DbSet<Location>? Locations { get; set; }
        public virtual DbSet<Session>? Sessions { get; set; }

        // Connector Tables
        public virtual DbSet<SessionDependent>? SessionDependents { get; set; }
    }
}