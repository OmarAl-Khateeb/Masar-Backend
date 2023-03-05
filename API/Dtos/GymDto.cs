using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GymDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Id { get; set; }
    }
    
    public class GymCDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}