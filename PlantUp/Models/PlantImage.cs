using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class PlantImage
    {
        [JsonPropertyName("organ")]
        public string Organ { get; set; }

        [JsonPropertyName("url")]
        public PlantImageUrl Url { get; set; }
    }
}
