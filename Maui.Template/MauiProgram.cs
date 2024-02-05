using $safeprojectname$.Services;
using $safeprojectname$.ViewModel;
using $safeprojectname$.Views;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$
{
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
#if DEBUG

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7252/") });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000") });

#elif RELEASE
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tech.capricom.co.za:9657"), Timeout = TimeSpan.FromSeconds(10)});
#endif

            builder.Services.AddTransient<Home>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<LoadingPage>();

            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<Logout>();

            builder.Services.AddTransient<AuthService>();

            return builder.Build();
        }
    }
}
