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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrienteeringPlanner.Services
{
    public class RunService : IRunService
    {
        private readonly HttpClient httpClient;
        private readonly IClubService clubService; 

        public RunService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("http://localhost:65419/");
        }

        public async Task<IEnumerable<Run>> GetUpcomingRuns()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Run>>("/api/runs/GetUpcomingRuns");
        }
        public async Task<HttpResponseMessage> CreateRun(Run run, Club club)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            if (await clubService.ValidClubCredidentials(club)){
                responseMessage = await httpClient.PostAsJsonAsync("api/runs/CreateRun", run);
            }
            else
            {
                responseMessage.StatusCode = (System.Net.HttpStatusCode)500;
            }

            return responseMessage;

        }

    }
}
