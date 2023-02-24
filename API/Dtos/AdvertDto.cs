using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GymId { get; set; }
    }
    public class AdvertCDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GymId { get; set; }
    }
}