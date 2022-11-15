using DevAndersen.BlazorGames.Site.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DevAndersen.BlazorGames.Site.ViewExtensions;

public abstract class BasePage : ComponentBase, IDisposable
{
    public virtual string PageTitle { get; } = "Blazor Games";

    [CascadingParameter]
    public required MainLayout MainLayout { get; set; }

    [Inject]
    public required NavigationManager Navigation { get; set; }

    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    private OnBeforeUnloadHandler? onBeforeUnloadHandler;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        MainLayout?.UpdatePageTitle(PageTitle);
        onBeforeUnloadHandler = new OnBeforeUnloadHandler(JsRuntime, () => OnLeavePage());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            if (onBeforeUnloadHandler != null)
            {
                await onBeforeUnloadHandler.Init();
            }
        }
    }

    public virtual void OnLeavePage()
    {
    }

    public virtual void Dispose()
    {
    }
}
