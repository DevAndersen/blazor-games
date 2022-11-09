namespace DevAndersen.BlazorGames.Core.Games;

public class GameDefinition
{
    public GameIdentity Identity { get; }

    public required int PlayersNeeded { get; init; }

    public required string Name { get; init; }

    public required string TextIcon { get; init; }

    public string Page => $"/games/{(int)Identity}";

    private GameDefinition(GameIdentity identity)
    {
        Identity = identity;
    }

    public static GameDefinition GetDefinition(GameIdentity identity)
    {
        return GameDefinitions[identity];
    }

    public static IEnumerable<GameDefinition> GetDefinitions()
    {
        return GameDefinitions.Values;
    }

    public static IReadOnlyDictionary<GameIdentity, GameDefinition> GameDefinitions { get; } = Enum.GetValues<GameIdentity>()
        .ToDictionary(
        k => k,
        CreateDefinition);

    private static GameDefinition CreateDefinition(GameIdentity identity) => identity switch
    {
        GameIdentity.RockPaperScissors => new GameDefinition(identity)
        {
            PlayersNeeded = 2,
            Name = "Rock paper scissors",
            TextIcon = new string(new char[]
            {
                (char)55358, (char)57000, // Rock emoji
                (char)55357, (char)56540, // Scroll emoji
                (char)9986, (char)65039   // Scissors emoji
            })
        },
        _ => throw new ArgumentOutOfRangeException($"No game matches identity '{identity}'.")
    };
}
