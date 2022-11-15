using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DevAndersen.BlazorGames.Core.Players;

public class PlayerIdentity : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public Guid Id { get; set; } = default!;

    private string? username;
    public string? Username
    {
        get => username;
        set => SetOnPropertyChanged(ref username, value);
    }

    /// <summary>
    /// Necessary in order for deserialization to work.
    /// </summary>
    public PlayerIdentity()
    {
    }

    public PlayerIdentity(Guid id)
    {
        Id = id;
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetOnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
    {
        property = value;
        OnPropertyChanged(propertyName);
    }
}
