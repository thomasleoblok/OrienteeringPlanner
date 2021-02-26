using Microsoft.IdentityModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrienteeringPlanner.Data;
using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components;

namespace OrienteeringPlanner.Services
{
    public class ClubService : IClubService
    {
        public HttpClient _httpClient { get; }

        public ClubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:65419/");
        }

        public async Task<Club> ClubLogin(LoginRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.SendJsonAsync<Club>(HttpMethod.Get, "api/Clubs/Login", loginRequest);

                return response;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> ValidClubCredidentials(Club club)
        {
            return await _httpClient.SendJsonAsync<bool>(HttpMethod.Get, "api/Clubs/ValidClubCredidentials", club);
        }

    }
}
