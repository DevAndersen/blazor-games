using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Site.Services.Abstractions;

public interface IIdentityService
{
    public Task<PlayerIdentity> LoadIdentityAsync();

    public Task SaveIdentityAsync(PlayerIdentity identity);
}
