using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrienteeringPlanner.Data;
using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Net.Http.Formatting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrienteeringPlanner.Services
{
    public class RunService : IRunService
    {
        private HttpClient _httpClient { get; }
        private IClubService _clubService { get; } 

        public RunService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _clubService = new ClubService(_httpClient);
        }

        public async Task<IEnumerable<Run>> GetUpcomingRuns(int searchDaysAhead)
        {
            return await _httpClient.GetJsonAsync<IEnumerable<Run>>("api/runs/GetUpcomingRuns/" + searchDaysAhead.ToString());
        }

        public async Task<HttpResponseMessage> CreateRun(Run run, Club club)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            var isValidClub = await _clubService.ValidClubCredidentials(club);

            if (isValidClub)
            {
                responseMessage = await _httpClient.PostAsJsonAsync("api/runs/CreateRun", run);
            }
            else
            {
                responseMessage.StatusCode = (HttpStatusCode)500;
            }

            return responseMessage;
        }

        public async Task<IEnumerable<Run>> GetUpcomingRunsForClub(Club club)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/Runs/GetUpcomingRunsForClub");
                request.Content = new StringContent(JsonConvert.SerializeObject(club), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Run>>(new[] { new JsonMediaTypeFormatter() });
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

        public async Task<HttpResponseMessage> DeleteRun(Run run, Club club)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            var isValidClub = await _clubService.ValidClubCredidentials(club);

            if (isValidClub)
            {
                responseMessage = await _httpClient.PostAsJsonAsync("api/runs/DeleteRun", run);
            }
            else
            {
                responseMessage.StatusCode = (HttpStatusCode)500;
            }

            return responseMessage;
        }

        public async Task<HttpResponseMessage> EditRun(Run run, Club club)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            var isValidClub = await _clubService.ValidClubCredidentials(club);

            if (isValidClub)
            {
                responseMessage = await _httpClient.PutAsJsonAsync("api/runs/EditRun", run);
            }
            else
            {
                responseMessage.StatusCode = (HttpStatusCode)500;
            }

            return responseMessage;
        }
    }
}
