using DevAndersen.BlazorGames.Core.Games.GameHandlers;
using System.Collections.ObjectModel;

namespace DevAndersen.BlazorGames.Core.Games;

public class GameLobby
{
    public event Action<Guid, IEnumerable<Guid>> JoinGameEvent = default!;
    private readonly Dictionary<Guid, GameDefinition> queue;
    private readonly Dictionary<Guid, GameHandler> gameHandlers;

    public GameLobby()
    {
        queue = new Dictionary<Guid, GameDefinition>();
        gameHandlers = new Dictionary<Guid, GameHandler>();
    }

    public ReadOnlyDictionary<Guid, GameDefinition> GetQueue() => queue.AsReadOnly();

    public ReadOnlyDictionary<Guid, GameHandler> GetGames() => gameHandlers.AsReadOnly();

    #region Queue

    public void AddPlayerToQueue(Guid playerId, GameIdentity gameIdentity)
    {
        GameDefinition? gameDefinition = GameDefinition.GetDefinition(gameIdentity);
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
                if (TryStartGame(group.Key, playerIds, out Guid guid))
                {
                    JoinGameEvent.Invoke(guid, playerIds);
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

    private bool TryStartGame(GameDefinition gameDefinition, IEnumerable<Guid> playerIds, out Guid guid)
    {
        GameHandler? handler = gameDefinition.Identity switch
        {
            GameIdentity.RockPaperScissors => new RockPaperScissorsHandler(playerIds),
            _ => null,
        };

        if (handler == null)
        {
            guid = Guid.Empty;
            return false;
        }

        guid = handler.GameId;
        gameHandlers[guid] = handler;
        return true;
    }

    public GameHandler? GetGameHandler(Guid playerId)
    {
        gameHandlers.TryGetValue(playerId, out GameHandler? handler);
        return handler;
    }

    public void StopGame(GameHandler handler)
    {
        gameHandlers.Remove(handler.GameId);
    }

    #endregion
}
