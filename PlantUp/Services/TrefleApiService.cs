using System;
using System.Linq;
using System.Net.Http;
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

            if (plantId == null)
                return new DetailedPlant();

            return await GetDetailedPlantByIdAsync(plantId);
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

                JsonElement dataArray = doc.RootElement.GetProperty("data");

                if (dataArray.GetArrayLength() > 0)
                {
                    int id = dataArray[0].GetProperty("id").GetInt32();
                    return id;
                }

                Console.WriteLine("Aucune plante trouvée avec ce nom scientifique.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return null;
            }
        }

        public async Task<DetailedPlant> GetDetailedPlantByIdAsync(int? plantId)
        {
            if (plantId == null)
                return new DetailedPlant();

            string requestUri = $"https://trefle.io/api/v1/plants/{plantId}?token={_apiKey}";

            using HttpClient httpClient = new();
            try
            {
                var response = await httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);

                var root = doc.RootElement.GetProperty("data");
                var main = root.GetProperty("main_species");
                var growth = main.GetProperty("growth");
                var foliage = main.GetProperty("foliage");

                return new DetailedPlant
                {
                    Id = root.GetProperty("id").GetRawText(),
                    Name = root.GetProperty("common_name").GetString() ?? "Inconnu",
                    ScientificName = root.GetProperty("scientific_name").GetString() ?? "Inconnu",
                    DiscoveryYear = TryGetInt(root, "year"),
                    FamilyName = main.GetProperty("family").GetString() ?? "Inconnu",
                    Duration = main.TryGetProperty("duration", out var duration) && duration.ValueKind == JsonValueKind.Array
                        ? duration.EnumerateArray().Select(x => x.GetString() ?? "Inconnu").ToArray()
                        : new[] { "Inconnu" },
                    LeafRetention = TryGetBool(foliage, "leaf_retention"),
                    HarvestTimeAverage = TryGetFloat(growth, "days_to_harvest"),
                    MaxPH = TryGetFloat(growth, "ph_maximum"),
                    MinPH = TryGetFloat(growth, "ph_minimum"),
                    Light = TryGetInt(growth, "light"),
                    Humidity = TryGetInt(growth, "atmospheric_humidity"),
                    FruitMonths = growth.TryGetProperty("fruit_months", out var months) && months.ValueKind == JsonValueKind.Array
                        ? months.EnumerateArray().Select(m => m.GetString() ?? "Inconnu").ToArray()
                        : new[] { "Inconnu" },
                    Spread = TryGetNestedFloat(growth, "spread", "cm"),
                    MinimumRootDepth = TryGetNestedFloat(growth, "minimum_root_depth", "cm"),
                    MinimumTemperature = TryGetNestedFloat(growth, "minimum_temperature", "deg_c"),
                    MaximumTemperature = TryGetNestedFloat(growth, "maximum_temperature", "deg_c")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return null;
            }
        }

        private float TryGetFloat(JsonElement element, string propertyName)
        {
            return element.TryGetProperty(propertyName, out var prop) &&
                   prop.ValueKind == JsonValueKind.Number &&
                   prop.TryGetSingle(out var value)
                   ? value : 0f;
        }

        private int TryGetInt(JsonElement element, string propertyName)
        {
            return element.TryGetProperty(propertyName, out var prop) &&
                   prop.ValueKind == JsonValueKind.Number &&
                   prop.TryGetInt32(out var value)
                   ? value : 0;
        }

        private bool TryGetBool(JsonElement element, string propertyName)
        {
            return element.TryGetProperty(propertyName, out var prop) &&
                   prop.ValueKind == JsonValueKind.True;
        }

        private float TryGetNestedFloat(JsonElement parent, string nestedProperty, string innerProperty)
        {
            return parent.TryGetProperty(nestedProperty, out var nested) &&
                   nested.TryGetProperty(innerProperty, out var inner) &&
                   inner.ValueKind == JsonValueKind.Number &&
                   inner.TryGetSingle(out var value)
                   ? value : 0f;
        }
    }
}
