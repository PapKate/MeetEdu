using MeetBase;
using MeetBase.Blazor;
using MeetBase.Web;

using MeetCore;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor;
using MudBlazor.Services;

using Newtonsoft.Json;

JsonConvert.DefaultSettings = () =>
{
    var settings = new JsonSerializerSettings();

    settings.Converters.Add(new TimeOnlyToStringJsonConverter());
    settings.Converters.Add(new DateOnlyToStringJsonConverter());

    return settings;
};

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<SessionStorageManager>();
builder.Services.AddSingleton<StateManagerCore>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 400;
    config.SnackbarConfiguration.ShowTransitionDuration = 400;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddSingleton<HeaderUserManager>();

builder.Services.AddSingleton<MeetCoreClient>();
builder.Services.AddSingleton<MeetCoreHubClient>();

await builder.Build().RunAsync();
