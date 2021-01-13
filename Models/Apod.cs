using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class Apod
    {
        public DateTime Date { get; set; }
        public string Explanation { get; set; }
        public string MediaType { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}