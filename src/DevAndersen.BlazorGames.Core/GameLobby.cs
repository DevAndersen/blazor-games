using DevAndersen.BlazorGames.Core.GameHandlers;

namespace DevAndersen.BlazorGames.Core;

public class GameLobby
{
    public event Action<GameDefinition, IEnumerable<Guid>> JoinGameEvent = default!;

    private readonly Dictionary<Guid, GameDefinition> queue;
    private readonly List<GameHandler> gameHandlers;

    public GameLobby()
    {
        queue = new Dictionary<Guid, GameDefinition>();
        gameHandlers = new List<GameHandler>();
    }

    #region Queue

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

    public void UpdateQueue()
    {
        // Groups players based on the games they want to play, ensuring there are enough in each group to start the game.
        Dictionary<GameDefinition, IEnumerable<Guid[]>> groups = queue
            .GroupBy(
                groupKey => groupKey.Value,
                groupVal => groupVal.Key)
            .ToDictionary(
                dictionaryKey => dictionaryKey.Key,
                dictionaryVal => dictionaryVal.Chunk(dictionaryVal.Key.PlayersNeeded)
            .Where(playerIds => playerIds.Length == dictionaryVal.Key.PlayersNeeded));

        foreach (KeyValuePair<GameDefinition, IEnumerable<Guid[]>> group in groups)
        {
            foreach (Guid[] playerIds in group.Value)
            {
                if (StartGame(group.Key, playerIds))
                {
                    JoinGameEvent.Invoke(group.Key, playerIds);
                    foreach (Guid playerId in playerIds)
                    {
                        queue.Remove(playerId);
                    }
                }
            }
        }
    }

    #endregion

    #region Game

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

    public GameHandler? GetGameHandler(Guid playerId)
    {
        return gameHandlers.FirstOrDefault(x => x.PlayerIds.Contains(playerId));
    }

    public void StopGame(GameHandler handler)
    {
        gameHandlers.Remove(handler);
    }

    #endregion
}
