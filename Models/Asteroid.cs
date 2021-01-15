using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class Asteroid
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EstimatedDiameterMin { get; set; }
        public string EstimatedDiameterMax { get; set; }
        public string Hazardous { get; set; }
        public string ApproachDate { get; set; }
        public string RelativeVelocity { get; set; }
        public string MissDistance { get; set; }
    }
}
