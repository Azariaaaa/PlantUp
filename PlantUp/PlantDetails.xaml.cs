using PlantUp.Models;

namespace PlantUp;

public partial class PlantDetails : ContentPage
{
	private PlantResult _plant;
	public PlantDetails(PlantResult plantResult)
	{
		_plant = plantResult;
		InitializeComponent();
	}
}