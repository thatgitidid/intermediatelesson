using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateLessons.Models
{
    public class ComputerSnake
    {
        // Some things will look for properties and ignore fields.  
        // Auto-implement properties.

        public int computer_id { get; set; }
        public string motherboard { get; set; } = ""; // Short way to guarantee no null.
        public int? cpu_cores { get; set; }
        public bool has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string video_card { get; set; } = "";

        // Lengthy way to avoid null
        public ComputerSnake()
        {
            if (cpu_cores == null) 
            {
                cpu_cores = 0;
            }
        }
    }
}
