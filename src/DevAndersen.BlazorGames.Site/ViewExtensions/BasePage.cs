using DevAndersen.BlazorGames.Site.Shared;
using Microsoft.AspNetCore.Components;

namespace DevAndersen.BlazorGames.Site.ViewExtensions;

public abstract class BasePage : ComponentBase
{
    public virtual string PageTitle { get; } = "Blazor Games";

    [CascadingParameter]
    public MainLayout MainLayout { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        MainLayout?.UpdatePageTitle(PageTitle);
    }
}
