using OrienteeringPlanner.Data;
using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrienteeringPlanner.Services
{
    public interface IClubService
    {
        public Task<Club> LoginAsync(Club club);

    }
}
