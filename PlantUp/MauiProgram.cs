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
		//AppShell DI
        builder.Services.AddSingleton<AppShell>();

        //Pages DI
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<PlantDetails>();

        //ViewModels DI
        builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<PlantDetailsViewModel>();

        //Services DI
        builder.Services.AddTransient<PlantNetApiService>();
		builder.Services.AddTransient<TrefleApiService>();

        var app = builder.Build();
        return app;
    }
}
