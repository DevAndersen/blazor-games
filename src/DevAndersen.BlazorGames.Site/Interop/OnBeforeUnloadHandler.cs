using Microsoft.JSInterop;

namespace DevAndersen.BlazorGames.Site.Interop;

public class OnBeforeUnloadHandler
{
    private readonly IJSRuntime jsRuntime;
    private readonly Action onBeforeUnloadTask;

    public OnBeforeUnloadHandler(IJSRuntime jsRuntime, Action onBeforeUnloadTask)
    {
        this.jsRuntime = jsRuntime;
        this.onBeforeUnloadTask = onBeforeUnloadTask;
    }

    public async Task Init()
    {
        await jsRuntime.InvokeVoidAsync("setBeforeUnloadObject", DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public void OnBeforeUnload()
    {
        onBeforeUnloadTask.Invoke();
    }
}
