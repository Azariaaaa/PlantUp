using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using PlantUp.Models;
using PlantUp.Services;
using PlantUp.ViewModels;

namespace PlantUp;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async Task<string> TakePhotoAsync()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            return photo.FullPath;
        }

        return null;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        string photo = await TakePhotoAsync();
        if (photo != null)
        {
            await _viewModel.SendPhotoAsync(photo);
        }
    }

    private async void OnUploadPhotoClicked(object sender, EventArgs e)
    {
        FileResult photo = await MediaPicker.PickPhotoAsync();
        if (photo != null)
        {
            await _viewModel.SendPhotoAsync(photo);
        }
    }

    private async void OnPlantSelected(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is PlantResult selectedPlant)
        {
            var encodedName = Uri.EscapeDataString(selectedPlant.Species.ScientificName);
            await Shell.Current.GoToAsync($"///PlantDetails?PlantName={encodedName}");
        }
    }
}

