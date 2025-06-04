using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlantUp.Services
{
    public class TrefleApiService
    {
        private readonly string _apiKey = "hWgzOGeXGK_IvaQsaeC7y-r288zuptLPr60QPYubuP4";

        public async Task<int?> GetPlantIdByNameAsync(string plantName)
        {
            string requestUri = $"https://trefle.io/api/v1/plants/search?token={_apiKey}&q={Uri.EscapeDataString(plantName)}";

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
                    Console.WriteLine("Aucune plante trouvée.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return null;
            }
        }
    }
}
