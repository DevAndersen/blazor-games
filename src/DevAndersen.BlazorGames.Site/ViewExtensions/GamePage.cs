using DevAndersen.BlazorGames.Core.GameHandlers;
using Microsoft.AspNetCore.Components;

namespace DevAndersen.BlazorGames.Site.ViewExtensions;

public class GamePage<T> : ComponentBase where T : GameHandler
{
    [Parameter]
    public T? Handler { get; set; }

    public string DebugString { get; set; } = typeof(T).Name;
}
