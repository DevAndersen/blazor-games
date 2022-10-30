using DevAndersen.BlazorGames.Core.GameHandlers;

namespace DevAndersen.BlazorGames.Core
{
    public class GameLobby
    {
        public event Action<IEnumerable<Guid>, GameDefinition> JoinGame = default!;

        private readonly Dictionary<Guid, GameDefinition> queue;
        private readonly List<GameHandler> gameHandlers;

        public GameLobby()
        {
            queue = new Dictionary<Guid, GameDefinition>();
            gameHandlers = new List<GameHandler>();
        }

        public void AddPlayerToQueue(Guid playerId, GameIdentity gameIdentity)
        {
            GameDefinition? gameDefinition = GameDefinition.GetGameDefinition(gameIdentity);
            if (gameDefinition != null)
            {
                queue[playerId] = gameDefinition;
                UpdateQueue();
            }
        }

        public void RemovePlayerFromQueue(Guid playerId)
        {
            queue.Remove(playerId);
        }

        public bool StartGame(GameDefinition gameDefinition, IEnumerable<Guid> playerIds)
        {
            GameHandler? handler = gameDefinition.GameIdentity switch
            {
                GameIdentity.RockPaperScissors => new RockPaperScissorsHandler(playerIds),
                _ => null,
            };

            if (handler == null)
            {
                return false;
            }

            gameHandlers.Add(handler);
            return true;
        }

        public void StopGame(GameHandler handler)
        {
            handler.StopGame();
            gameHandlers.Remove(handler);
        }

        public void UpdateQueue()
        {
            // Groups players based on the games they want to play, ensuring there are enough in each group to start the game.
            IEnumerable<IEnumerable<Guid>> groups = queue
                .GroupBy(x => x.Value)
                .SelectMany(y => y.Chunk(y.Key.PlayersNeeded)
                    .Where(z => z.Length == y.Key.PlayersNeeded))
                .Select(a => a.Select(b => b.Key));

            foreach (IEnumerable<Guid> group in groups)
            {
                if (group.Any())
                {
                    GameDefinition gameDefinition = queue[group.First()];

                    bool gameCreated = StartGame(gameDefinition, group);

                    if (gameCreated)
                    {
                        JoinGame.Invoke(group, gameDefinition);
                    }
                }
            }
        }
    }
}
