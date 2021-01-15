using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class Asteroid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double EstimatedDiameterMin { get; set; }
        public double EstimatedDiameterMax { get; set; }
        public string Hazardous { get; set; }
        public string ApproachDate { get; set; }
        public double RelativeVelocity { get; set; }
        public double MissDistance { get; set; }
    }
}
