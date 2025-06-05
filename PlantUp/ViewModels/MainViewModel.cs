using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PlantUp.Models;
using PlantUp.Services;

namespace PlantUp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly PlantNetApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<PlantResult> plantResults;
        [ObservableProperty]
        private bool isWaitingResult;

        public MainViewModel(PlantNetApiService apiService) 
        {
            _apiService = apiService;
        }

        public async Task SendPhotoAsync(string photoPath)
        {
            byte[] imageBytes = await File.ReadAllBytesAsync(photoPath);
            await IdentifyAndSetResultsAsync(imageBytes);
        }

        public async Task SendPhotoAsync(FileResult photo)
        {
            using Stream stream = await photo.OpenReadAsync();
            using MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();

            await IdentifyAndSetResultsAsync(imageBytes);
        }

        private async Task IdentifyAndSetResultsAsync(byte[] imageBytes)
        {
            PlantResults?.Clear();
            IsWaitingResult = true;

            List<PlantResult> results = await _apiService.IdentifyPlantFromBytesAsync(imageBytes);
            PlantResults = new ObservableCollection<PlantResult>(results);
            IsWaitingResult = false;
        }
    }
}
