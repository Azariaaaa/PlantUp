using System.Collections.ObjectModel;
using System.Reflection;
using PlantUp.Models;
using PlantUp.Services;
using PlantUp.ViewModels;

namespace PlantUp;

public partial class MainPage : ContentPage
{
    //private readonly ApiService _apiService;
    //public ObservableCollection<PlantResult> PlantResults { get; set; } = new();
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        //_apiService = apiService;
        //BindingContext = this;
        _viewModel = viewModel;
        BindingContext = _viewModel;
        //InitAsync();
    }

    public async Task<string> TakePhotoAsync()
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    return photo.FullPath; // ← pas besoin de copier
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur photo : {ex.Message}");
        }

        return null;
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        var photo = await TakePhotoAsync();
        if (photo != null)
        {
           await _viewModel.SendPhotoAsync(photo);
        }
    }

    //private async void InitAsync()
    //{
    //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
    //Stream stream = assembly.GetManifestResourceStream("PlantUp.Assets.mauve.jpg");
    //MemoryStream ms = new MemoryStream();
    //await stream.CopyToAsync(ms);
    //byte[] imageBytes = ms.ToArray();
    //var identification = await _apiService.IdentifyPlantFromBytesAsync(imageBytes, "flower");

    //PlantResults.Clear();
    //foreach (var plant in identification)
    //    PlantResults.Add(plant);
    //}

    //public async Task<List<PlantResult>> Identify(byte[] imageBytes, string type)
    //{
    //    return await _apiService.IdentifyPlantFromBytesAsync(imageBytes, type);
    //}
}

