using Blazored.LocalStorage;
using DevAndersen.BlazorGames.Core;
using DevAndersen.BlazorGames.Site.Services;
using DevAndersen.BlazorGames.Site.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<IIdentityService, LocalStorageIdentityService>();
builder.Services.AddSingleton<GameLobby>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
