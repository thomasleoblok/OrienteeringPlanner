using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;


namespace OrienteeringPlanner.Services
{
    public interface IRunService
    {
        Task<IEnumerable<Run>> GetUpcomingRuns(int searchDaysAhead);
        Task<HttpResponseMessage> CreateRun(Run run, Club club);
        Task<IEnumerable<Run>> GetUpcomingRunsForClub(Club club);
        Task<HttpResponseMessage> DeleteRun(Run run, Club club);
        Task<HttpResponseMessage> EditRun(Run run, Club club);
    }
}
