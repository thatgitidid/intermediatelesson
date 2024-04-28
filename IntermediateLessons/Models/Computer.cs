using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntermediateLessons.Models
{
    public class Computer
    {
        // Some things will look for properties and ignore fields.  
        // Auto-implement properties.

        // JsonPropertyName is used to match the property name to the JSON key.
        // Applies to the property directly below.
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; }
        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = ""; // Short way to guarantee no null.
        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; }
        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }
        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("video_card")]
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
