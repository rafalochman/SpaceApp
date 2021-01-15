using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public decimal RelativeVelocity { get; set; }
        public decimal MissDistance { get; set; }
        public string Date { get; set; }
        public string OrbitingBody { get; set; }
    }
}
