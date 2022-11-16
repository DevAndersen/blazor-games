namespace DevAndersen.BlazorGames.Core.Games;

public class GameDefinition
{
    public GameIdentity Identity { get; init; }

    public string Name { get; init; }

    public int PlayersNeeded { get; init; }

    public GameDefinition(GameIdentity identity, string name, int playersNeeded)
    {
        Identity = identity;
        Name = name;
        PlayersNeeded = playersNeeded;
    }

    public static GameDefinition GetDefinition(GameIdentity identity)
    {
        return GameDefinitions[identity];
    }

    public static IEnumerable<GameDefinition> GetDefinitions()
    {
        return GameDefinitions.Values;
    }

    public static IReadOnlyDictionary<GameIdentity, GameDefinition> GameDefinitions { get; } = Enum.GetValues<GameIdentity>().ToDictionary(
        k => k,
        CreateDefinition);

    private static GameDefinition CreateDefinition(GameIdentity identity) => identity switch
    {
        GameIdentity.RockPaperScissors => new GameDefinition(identity, "Rock paper scissors", 2),
        _ => throw new ArgumentOutOfRangeException($"No game matches identity '{identity}'.")
    };
}
