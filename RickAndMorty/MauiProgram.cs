using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RickAndMorty.Services;
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

            // Registro de todas las páginas y ViewModels usando reflección
            // Obtiene el ensamblado actual y registra todas las clases derivadas de ViewModelGlobal o Page
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => (type.IsSubclassOf(typeof(ViewModelGlobal)) || type.IsSubclassOf(typeof(Page))) && !type.IsAbstract)
                .ToList()
                .ForEach(type => builder.Services.AddTransient(type));


            // inyecto servicio
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<INavegacionService, NavegacionService>();

            // registro las rutas
            Routing.RegisterRoute(nameof(CharacterDetailPage), typeof(CharacterDetailPage));
            Routing.RegisterRoute(nameof(LocationDetailPage), typeof(LocationDetailPage));
            Routing.RegisterRoute(nameof(EpisodeDetailPage), typeof(EpisodeDetailPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
