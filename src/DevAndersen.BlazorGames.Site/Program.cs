using Blazored.LocalStorage;
using DevAndersen.BlazorGames.Core.Games;
using DevAndersen.BlazorGames.Site.Services;
using DevAndersen.BlazorGames.Site.Services.Abstractions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<IIdentityService, LocalStorageIdentityService>();
builder.Services.AddSingleton<GameLobby>();

WebApplication app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
