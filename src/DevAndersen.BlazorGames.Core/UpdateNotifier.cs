namespace DevAndersen.BlazorGames.Core;

public class UpdateNotifier
{
    public event Action? OnUpdate;

    public void Update()
    {
        OnUpdate?.Invoke();
    }
}
