using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlantUp.Models;

namespace PlantUp.Services
{
    public class ApiService
    {
        private readonly string _apiKey = "2b10ZrH2blcEM5bf3sJXKhh8e";

        public async Task<List<PlantResult>> IdentifyPlantFromBytesAsync(byte[] imageBytes, string organ = "leaf")
        {
            string requestUri = $"https://my-api.plantnet.org/v2/identify/all?api-key={_apiKey}";

            using var client = new HttpClient();
            using var form = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            form.Add(imageContent, "images", "tournesol.jpg");

            form.Add(new StringContent(organ), "organs");

            var response = await client.PostAsync(requestUri, form);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            string resultsJson = doc.RootElement.GetProperty("results").GetRawText();

            return JsonSerializer.Deserialize<List<PlantResult>>(resultsJson) ?? new List<PlantResult>();
        }
    }
}
