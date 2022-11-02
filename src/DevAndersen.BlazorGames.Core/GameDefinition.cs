namespace DevAndersen.BlazorGames.Core;

public class GameDefinition
{
    public GameIdentity Identity { get; }

    // Todo: Mark as required.
    public int PlayersNeeded { get; init; }

    // Todo: Mark as required.
    public string Name { get; init; }

    // Todo: Mark as required.
    public string TextIcon { get; init; }

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
        v => CreateDefinition(v));

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
