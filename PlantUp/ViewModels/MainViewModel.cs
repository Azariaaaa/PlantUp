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
        private readonly ApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<PlantResult> plantResults;
        [ObservableProperty]
        private bool isWaitingResult;

        public MainViewModel(ApiService apiService) 
        {
            _apiService = apiService;
        }

        public async Task SendPhotoAsync(string photoPath)
        {
            if(PlantResults != null)
                PlantResults.Clear();

            byte[] imageBytes = await File.ReadAllBytesAsync(photoPath);
            IsWaitingResult = true;
            List<PlantResult> results = await _apiService.IdentifyPlantFromBytesAsync(imageBytes, "leaf");
            PlantResults = new ObservableCollection<PlantResult>(results);
            IsWaitingResult = false;
        }
    }
}
