using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class PlantNetResponse
    {
        [JsonPropertyName("results")]
        public List<PlantResult> Results { get; set; }
    }
}
