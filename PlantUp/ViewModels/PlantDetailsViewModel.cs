using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PlantUp.Models;
using PlantUp.Services;

namespace PlantUp.ViewModels
{
    public partial class PlantDetailsViewModel : ObservableObject
    {
        private readonly TrefleApiService _trefleApiService;
        [ObservableProperty]
        public string plantName;
        [ObservableProperty]
        public DetailedPlant plant;

        public PlantDetailsViewModel(TrefleApiService trefleApiService)
        {
            _trefleApiService = trefleApiService;
        }

        public async Task GetPlantInformations()
        {
            Plant = await _trefleApiService.GetPlantDetails(PlantName);
        }
    }
}
