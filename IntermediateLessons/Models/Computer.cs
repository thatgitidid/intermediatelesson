using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateLessons.Models
{
    public class Computer
    {
        // Some things will look for properties and ignore fields.  
        // Auto-implement properties.

        public int ComputerId { get; set; }
        public string Motherboard { get; set; } = ""; // Short way to guarantee no null.
        public int? CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";

        // Lengthy way to avoid null
        public Computer()
        {
            if (CPUCores == null) 
            {
                CPUCores = 0;
            }
        }
    }
}
