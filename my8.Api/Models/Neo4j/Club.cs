using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models.Neo4j
{
    public class Club
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }
        public double Rate { get; set; }
        public int Joins { get; set; }
        public int ClubIPoint { get; set; } //Club interaction point
        public string Title { get; set; }
    }
}
