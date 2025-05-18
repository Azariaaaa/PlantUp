using PlantUp.Models;
using PlantUp.ViewModels;

namespace PlantUp;

[QueryProperty(nameof(IncomingPlantName), "PlantName")]
public partial class PlantDetails : ContentPage
{
    private readonly PlantDetailsViewModel _viewModel;

    public string IncomingPlantName
    {
        get => _viewModel.PlantName;
        set => _viewModel.PlantName = value;
    }

    public PlantDetails(PlantDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}