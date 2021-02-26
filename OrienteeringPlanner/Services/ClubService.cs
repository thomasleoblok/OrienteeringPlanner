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
        }

        public async Task<Club> ClubLogin(LoginRequest loginRequest)
        {
            //club.Password = Utility.Encrypt(club.Password);
            string serializedClub = JsonConvert.SerializeObject(loginRequest);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Clubs/Login");
            requestMessage.Content = new StringContent(serializedClub);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedClub = JsonConvert.DeserializeObject<Club>(responseBody);

            return await Task.FromResult(returnedClub);

        }

        public async Task<bool> ValidClubCredidentials(Club club)
        {
            return await _httpClient.SendJsonAsync<bool>(HttpMethod.Get, "/api/Clubs/ValidClubCredidentials", club);
        }

    }
}
