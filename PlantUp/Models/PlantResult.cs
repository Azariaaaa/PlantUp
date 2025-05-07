using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class PlantResult
    {
        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("species")]
        public Species Species { get; set; }

        [JsonPropertyName("images")]
        public List<PlantImage> Images { get; set; }
    }
}
