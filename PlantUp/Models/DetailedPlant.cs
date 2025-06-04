using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantUp.Models
{
    public class DetailedPlant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public int DiscoveryYear { get; set; }
        public string FamilyName { get; set; }
        public string[] Duration { get; set; }
        public bool LeafRetention { get; set; }
        public float HarvestTimeAverage { get; set; }
        public float MaxPH { get; set; }
        public float MinPH { get; set; }
        public int Light { get; set; }
        public int Humidity { get; set; }
        public string[] FruitMonths { get; set; }
        public float Spread { get; set; }
        public float MinimumRootDepth { get; set; }
        public float MinimumTemperature { get; set; }
        public float MaximumTemperature { get; set; }
    }
}
