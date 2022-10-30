using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.GameHandlers
{
    public class RockPaperScissorsHandler : GameHandler
    {
        public RockPaperScissorsHandler(IEnumerable<Guid> players) : base(players)
        {
        }
    }
}
