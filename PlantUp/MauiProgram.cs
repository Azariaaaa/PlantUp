using Microsoft.Extensions.Logging;
using PlantUp.Services;
using PlantUp.ViewModels;

namespace PlantUp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        //Pages DI
        builder.Services.AddTransient<MainPage>();

		//ViewModels DI
		builder.Services.AddTransient<MainViewModel>();

        //Services DI
        builder.Services.AddTransient<ApiService>();

		return builder.Build();
	}
}
