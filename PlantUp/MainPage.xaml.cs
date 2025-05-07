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

    public MainPage()
    {
        InitializeComponent();
        //_apiService = apiService;
        //BindingContext = this;
        BindingContext = new MainViewModel();
        //InitAsync();
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

