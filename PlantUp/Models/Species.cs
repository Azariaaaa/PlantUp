using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class Species
    {
        [JsonPropertyName("scientificNameWithoutAuthor")]
        public string ScientificName { get; set; }

        [JsonPropertyName("commonNames")]
        public List<string> CommonNames { get; set; }
    }
}
