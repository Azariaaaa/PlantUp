using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class PlantImageUrl
    {
        [JsonPropertyName("o")]
        public string Original { get; set; }

        [JsonPropertyName("m")]
        public string Medium { get; set; }

        [JsonPropertyName("s")]
        public string Small { get; set; }
    }
}
