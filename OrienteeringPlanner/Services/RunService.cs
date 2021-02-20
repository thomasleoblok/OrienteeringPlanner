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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrienteeringPlanner.Services
{
    public class RunService : IRunService
    {
        private readonly HttpClient httpClient;

        public RunService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Run>> GetUpcomingRuns()
        {
            return await httpClient.GetJsonAsync<IEnumerable<Run>>("http://localhost:65419/api/runs/GetUpcomingRuns");
        }

    }
}
