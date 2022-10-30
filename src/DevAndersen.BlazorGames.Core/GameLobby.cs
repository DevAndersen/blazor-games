namespace DevAndersen.BlazorGames.Core
{
    public class GameLobby
    {
        public event Action<Guid, GameDefinition> JoinGame = default!;

        private readonly Dictionary<Guid, GameDefinition> queue;

        public GameLobby()
        {
            queue = new Dictionary<Guid, GameDefinition>();
        }

        public void AddPlayerToQueue(Guid playerId, GameIdentity gameIdentity)
        {
            GameDefinition? gameDefinition = GameDefinition.GetGameDefinition(gameIdentity);
            if (gameDefinition != null)
            {
                queue[playerId] = gameDefinition;
                Update();
            }
        }

        public void RemovePlayerFromQueue(Guid playerId)
        {
            queue.Remove(playerId);
        }

        public void Update()
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
                    var gameIdentity = queue[group.First()];

                    foreach (Guid playerId in group)
                    {
                        JoinGame.Invoke(playerId, gameIdentity);
                    }
                }
            }
        }
    }
}
