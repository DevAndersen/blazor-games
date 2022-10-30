using Blazored.LocalStorage;
using DevAndersen.BlazorGames.Core;
using DevAndersen.BlazorGames.Core.Players;
using DevAndersen.BlazorGames.Site.Services.Abstractions;

namespace DevAndersen.BlazorGames.Site.Services;

public class LocalStorageIdentityService : IIdentityService
{
    private readonly ILocalStorageService localStorage;

    public LocalStorageIdentityService(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public async Task<PlayerIdentity> LoadIdentityAsync()
    {
        PlayerIdentity? identity = await localStorage.GetItemAsync<PlayerIdentity>(Constants.Site.LocalStoragePlayerIdentityKey);

        if (identity != null && identity.Id != null)
        {
            return identity;
        }

        PlayerIdentity newIdentity = new PlayerIdentity(Guid.NewGuid());
        await SaveIdentityAsync(newIdentity);
        return newIdentity;
    }

    public async Task SaveIdentityAsync(PlayerIdentity identity)
    {
        await localStorage.SetItemAsync(Constants.Site.LocalStoragePlayerIdentityKey, identity);
    }
}
