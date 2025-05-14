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

        public async Task<List<PlantResult>> IdentifyPlantFromBytesAsync(byte[] imageBytes)
        {
            string requestUri = $"https://my-api.plantnet.org/v2/identify/all?api-key={_apiKey}&include-related-images=true";
            string organ = "auto";

            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            ByteArrayContent imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            form.Add(imageContent, "images", "photo.jpg");

            form.Add(new StringContent(organ), "organs");

            HttpResponseMessage response = await client.PostAsync(requestUri, form);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            PlantNetResponse plantNetResponse = JsonSerializer.Deserialize<PlantNetResponse>(json);

            return plantNetResponse?.Results ?? new List<PlantResult>();
        }
    }
}
