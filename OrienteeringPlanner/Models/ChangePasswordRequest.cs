using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrienteeringPlanner.Models
{
    public class ChangePasswordRequest
    {
        public int ClubId { get; set; }
        public Guid ClubToken { get; set; }
        public string NewPassword { get; set; }
    }
}
