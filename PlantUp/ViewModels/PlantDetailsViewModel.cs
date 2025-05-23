﻿using System;
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
        private readonly ApiService _apiService;
        [ObservableProperty]
        public string plantName;
        public PlantDetailsViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }
    }
}
