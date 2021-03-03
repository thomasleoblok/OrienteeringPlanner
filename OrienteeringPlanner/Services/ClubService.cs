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
using System.Text;
using System.Net;
using System.Net.Http.Formatting;

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
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Clubs/Login");
                request.Content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsAsync<Club>(new[] { new JsonMediaTypeFormatter() });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> ValidClubCredidentials(Club club)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Clubs/ValidClubCredidentials");
                request.Content = new StringContent(JsonConvert.SerializeObject(club), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsAsync<bool>(new[] { new JsonMediaTypeFormatter() });
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
