namespace DevAndersen.BlazorGames.Core;

public class GameDefinition
{
    public GameIdentity GameIdentity { get; }

    public string Name { get; }

    public string Page => $"/games/{(int)GameIdentity.RockPaperScissors}";

    public string TextIcon { get; set; }

    public int PlayersNeeded { get; }

    public GameDefinition(GameIdentity identity, string name, int playersNeeded, string textIcon)
    {
        GameIdentity = identity;
        Name = name;
        PlayersNeeded = playersNeeded;
        TextIcon = textIcon;
    }

    public static readonly IEnumerable<GameDefinition> GameDefinitions = new GameDefinition[]
    {
        new GameDefinition(GameIdentity.RockPaperScissors, "Rock paper scissors", 2, new string(new char[]
        {
            (char)55358, (char)57000, // Rock emoji
            (char)55357, (char)56540, // Scroll emoji
            (char)9986, (char)65039, // Scissors emoji
        }))
    };

    public static GameDefinition? GetGameDefinition(GameIdentity identity)
    {
        return GameDefinitions.FirstOrDefault(x => x.GameIdentity == identity);
    }
}
