namespace DevAndersen.BlazorGames.Core.GameHandlers;

public class RockPaperScissorsHandler : GameHandler
{
    public Dictionary<Guid, RockPaperScissorsChoice> Choices = new Dictionary<Guid, RockPaperScissorsChoice>();

    public Dictionary<Guid, RockPaperScissorsResult> Results = new Dictionary<Guid, RockPaperScissorsResult>();

    public RockPaperScissorsHandler(IEnumerable<Guid> players) : base(GameIdentity.RockPaperScissors, players)
    {
    }

    public override void StartGame()
    {
        throw new NotImplementedException();
    }

    public enum RockPaperScissorsChoice
    {
        Rock,
        Paper,
        Scissors
    }

    public enum RockPaperScissorsResult
    {
        Win,
        Loss,
        Tie
    }

    public void MakeChoice(Guid playerId, RockPaperScissorsChoice choice)
    {
        if (Choices.Keys.Contains(playerId))
        {
            return;
        }

        Choices[playerId] = choice;

        if (Choices.Keys.Count == 2)
        {
            KeyValuePair<Guid, RockPaperScissorsChoice> playerA = Choices.First();
            KeyValuePair<Guid, RockPaperScissorsChoice> playerB = Choices.Skip(1).First();

            Results[playerA.Key] = IsWinner(playerA.Value, playerB.Value);
            Results[playerB.Key] = IsWinner(playerB.Value, playerA.Value);

            UpdateNotifier.Update();
        }
    }

    public static RockPaperScissorsResult IsWinner(RockPaperScissorsChoice choice, RockPaperScissorsChoice otherChoice)
    {
        return (choice, otherChoice) switch
        {
            (RockPaperScissorsChoice.Rock, RockPaperScissorsChoice.Rock) => RockPaperScissorsResult.Tie,
            (RockPaperScissorsChoice.Paper, RockPaperScissorsChoice.Paper) => RockPaperScissorsResult.Tie,
            (RockPaperScissorsChoice.Scissors, RockPaperScissorsChoice.Scissors) => RockPaperScissorsResult.Tie,

            (RockPaperScissorsChoice.Rock, RockPaperScissorsChoice.Scissors) => RockPaperScissorsResult.Win,
            (RockPaperScissorsChoice.Rock, RockPaperScissorsChoice.Paper) => RockPaperScissorsResult.Loss,

            (RockPaperScissorsChoice.Paper, RockPaperScissorsChoice.Rock) => RockPaperScissorsResult.Win,
            (RockPaperScissorsChoice.Paper, RockPaperScissorsChoice.Scissors) => RockPaperScissorsResult.Loss,

            (RockPaperScissorsChoice.Scissors, RockPaperScissorsChoice.Paper) => RockPaperScissorsResult.Win,
            (RockPaperScissorsChoice.Scissors, RockPaperScissorsChoice.Rock) => RockPaperScissorsResult.Loss,

            _ => RockPaperScissorsResult.Loss
        };
    }
}
