using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlantUp.Models;

namespace PlantUp.Services
{
    public class TrefleApiService
    {
        private readonly string _apiKey = "hWgzOGeXGK_IvaQsaeC7y-r288zuptLPr60QPYubuP4";

        public async Task<DetailedPlant> GetPlantDetails(string plantScientifName)
        {
            int? plantId = await GetPlantIdByNameAsync(plantScientifName);

            if(plantId == null)
            {
                return new DetailedPlant();
            }

            DetailedPlant plant = await GetDetailedPlantByIdAsync(plantId);
            return plant;
        }
        public async Task<int?> GetPlantIdByNameAsync(string plantScientificName)
        {
            string requestUri = $"https://trefle.io/api/v1/plants?token={_apiKey}&filter[scientific_name]={Uri.EscapeDataString(plantScientificName)}";

            using HttpClient httpClient = new();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                JsonElement dataArray = root.GetProperty("data");
                if (dataArray.GetArrayLength() > 0)
                {
                    JsonElement firstPlant = dataArray[0];
                    int id = firstPlant.GetProperty("id").GetInt32();
                    return id;
                }
                else
                {
                    Console.WriteLine("Aucune plante trouvée avec ce nom scientifique.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return null;
            }
        }

        public async Task<DetailedPlant> GetDetailedPlantByIdAsync(int? plantId)
        {
            if(plantId == null)
                return new DetailedPlant();

            string requestUri = $"https://trefle.io/api/v1/plants/{plantId}?token={_apiKey}";

            using HttpClient httpClient = new();
            try
            {
                var response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var main = root.GetProperty("main_species");
                var growth = main.GetProperty("growth");
                var foliage = main.GetProperty("foliage");

                return new DetailedPlant
                {
                    Id = root.GetProperty("id").GetRawText(),
                    Name = root.GetProperty("common_name").GetString() ?? "Inconnu",
                    ScientificName = root.GetProperty("scientific_name").GetString() ?? "Inconnu",
                    DiscoveryYear = root.TryGetProperty("year", out var year) && year.TryGetInt32(out var y) ? y : 0,
                    FamilyName = main.GetProperty("family").GetString() ?? "Inconnu",
                    Duration = main.TryGetProperty("duration", out var duration) && duration.ValueKind == JsonValueKind.Array
                        ? duration.EnumerateArray().Select(x => x.GetString() ?? "Inconnu").ToArray()
                        : new[] { "Inconnu" },
                    LeafRetention = foliage.TryGetProperty("leaf_retention", out var lr) && lr.ValueKind == JsonValueKind.True,
                    HarvestTimeAverage = growth.TryGetProperty("days_to_harvest", out var harvest) && harvest.TryGetSingle(out var h) ? h : 0f,
                    MaxPH = growth.TryGetProperty("ph_maximum", out var phMax) && phMax.TryGetSingle(out var phM) ? phM : 0f,
                    MinPH = growth.TryGetProperty("ph_minimum", out var phMin) && phMin.TryGetSingle(out var phm) ? phm : 0f,
                    Light = growth.TryGetProperty("light", out var light) && light.TryGetInt32(out var l) ? l : 0,
                    Humidity = growth.TryGetProperty("atmospheric_humidity", out var hum) && hum.TryGetInt32(out var hmd) ? hmd : 0,
                    FruitMonths = growth.TryGetProperty("fruit_months", out var months) && months.ValueKind == JsonValueKind.Array
                        ? months.EnumerateArray().Select(m => m.GetString() ?? "Inconnu").ToArray()
                        : new[] { "Inconnu" },
                    Spread = growth.TryGetProperty("spread", out var spread) && spread.TryGetProperty("cm", out var s) && s.TryGetSingle(out var sp) ? sp : 0f,
                    MinimumRootDepth = growth.TryGetProperty("minimum_root_depth", out var rd) && rd.TryGetProperty("cm", out var rdc) && rdc.TryGetSingle(out var rdv) ? rdv : 0f,
                    MinimumTemperature = growth.TryGetProperty("minimum_temperature", out var tmin) && tmin.TryGetProperty("deg_c", out var tminc) && tminc.TryGetSingle(out var tmc) ? tmc : 0f,
                    MaximumTemperature = growth.TryGetProperty("maximum_temperature", out var tmax) && tmax.TryGetProperty("deg_c", out var tmaxc) && tmaxc.TryGetSingle(out var tM) ? tM : 0f
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return null;
            }
        }
    }
}
