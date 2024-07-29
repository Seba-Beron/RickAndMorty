using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RickAndMorty.Services;
using RickAndMorty.ViewModels;
using RickAndMorty.Views;
using ShopApp.Services;
using System.Reflection;

namespace RickAndMorty
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // obtengo la configuracion del archivo
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("RickAndMorty.appsettings.json");
            
            // construyo la configuracion
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // Extension MAUI toolkit
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // inyecto la configuracion
            builder.Configuration.AddConfiguration(config);
            
            // registro las paginas
            builder.Services.AddTransient<CharactersListViewModel>();
            builder.Services.AddTransient<CharactersListPage>();

            builder.Services.AddTransient<CharacterDetailViewModel>();
            builder.Services.AddTransient<CharacterDetailPage>();

            // inyecto servicio
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<INavegacionService, NavegacionService>();

            // registro las rutas
            Routing.RegisterRoute(nameof(CharacterDetailPage), typeof(CharacterDetailPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
