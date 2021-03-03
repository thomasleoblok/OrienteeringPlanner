using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrienteeringPlanner.Models;

namespace OrienteeringPlanner.Data
{
    public class OrienteeringPlannerContext : DbContext
    {
        public OrienteeringPlannerContext(DbContextOptions<OrienteeringPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<Run> Run { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<ClubExtended> ClubExtended { get; set; }


    }
}