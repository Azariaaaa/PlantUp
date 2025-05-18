using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantUp.Services;

namespace PlantUp.ViewModels
{
    class PlantDetailsViewModel
    {
        private readonly ApiService _apiService;
        public PlantDetailsViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }
    }
}
