using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application.Activities.DTOs
{
    public class BaseActivityDto
    {
        public string Title { get; set; } = "";
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "";

        // location props
        public string City { get; set; } = "";
        public string Venue { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}