using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrienteeringPlanner.Models
{
    public class ClubExtended
    {
        [Key]
        public int Id { get; set; }
        public int ClubId { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool HasChangedPassword { get; set; }
    }
}
