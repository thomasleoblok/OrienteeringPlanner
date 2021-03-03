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
        public Task<ClubWithExtendedData> ClubLogin(LoginRequest loginRequest);
        public Task<bool> ValidClubCredidentials(Club club);
        public Task<bool> ChangeClubPassword(ChangePasswordRequest requestData);
    }
}
