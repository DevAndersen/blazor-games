using DevAndersen.BlazorGames.Core.Games;
using DevAndersen.BlazorGames.Core.Games.GameHandlers;
using DevAndersen.BlazorGames.Core.Players;
using Microsoft.AspNetCore.Components;

namespace DevAndersen.BlazorGames.Site.ViewExtensions;

public class GamePage<T> : ComponentBase where T : GameHandler
{
    [Parameter]
    public T Handler { get; set; } = default!;

    [Parameter]
    public PlayerIdentity Identity { get; set; } = default!;
}
