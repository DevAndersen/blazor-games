namespace DevAndersen.BlazorGames.Core.Players;

public class PlayerIdentity
{
    public Guid Id { get; set; } = default!;

    public string? Username { get; set; }

    /// <summary>
    /// Necessary for deserialization to work.
    /// </summary>
    public PlayerIdentity()
    {
    }

    public PlayerIdentity(Guid id)
    {
        Id = id;
    }
}
