using System;
using System.ComponentModel.DataAnnotations;

namespace OrienteeringPlanner.Models
{
    public class Run
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string GotoLink { get; set; }
        public int ClubId { get; set; }
    }
}
