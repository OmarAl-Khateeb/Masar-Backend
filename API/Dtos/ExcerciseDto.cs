using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ExcerciseDto
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Duration { get; set; }
    }
}