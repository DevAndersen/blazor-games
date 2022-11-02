using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.GameHandlers;

public abstract class GameHandler
{
	public GameIdentity GameIdentity { get;  }

	public Guid GameId { get; }

	public IEnumerable<Guid> PlayerIds { get; }

	public Dictionary<Guid, PlayerIdentity> Players { get; }

	public List<(string Message, PlayerIdentity Sender)> Chat { get; }

    public UpdateNotifier UpdateNotifier { get; }

	public GameDefinition GameDefinition => GameDefinition.GetDefinition(GameIdentity);

    public GameHandler(GameIdentity gameIdentity, IEnumerable<Guid> playerIds)
	{
		GameIdentity = gameIdentity;
		PlayerIds = playerIds;
        GameId = Guid.NewGuid();
		Players = new Dictionary<Guid, PlayerIdentity>();
		Chat = new List<(string Message, PlayerIdentity Sender)>();
		UpdateNotifier = new UpdateNotifier();
    }

	public void JoinGame(PlayerIdentity playerIdentity)
	{
		Players[playerIdentity.Id] = playerIdentity;
	}

	public virtual void StopGame()
	{
	}

	public void SendChatMessage(string message, PlayerIdentity? playerIdentity)
	{
		if (!string.IsNullOrWhiteSpace(message) && playerIdentity != null)
		{
			Chat.Add((message, playerIdentity));
		}
	}
}
