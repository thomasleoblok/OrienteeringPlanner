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

namespace OrienteeringPlanner.Services
{
    public class ClubService : IClubService
    {
        public HttpClient _httpClient { get; }

        public ClubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Club> LoginAsync(Club club)
        {
            //club.Password = Utility.Encrypt(club.Password);
            string serializedClub = JsonConvert.SerializeObject(club);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Clubs/Login");
            requestMessage.Content = new StringContent(serializedClub);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedClub = JsonConvert.DeserializeObject<Club>(responseBody);

            return await Task.FromResult(returnedClub);

        }

    }
}
