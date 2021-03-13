using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrienteeringPlanner.Models;

namespace CreateUser.Data
{
    public class ClubContext : DbContext
    {
        public ClubContext (DbContextOptions<ClubContext> options)
            : base(options)
        {
        }

        public DbSet<OrienteeringPlanner.Models.Club> Club { get; set; }
        public DbSet<OrienteeringPlanner.Models.ClubExtended> ClubExtended { get; set; }
    }
}
