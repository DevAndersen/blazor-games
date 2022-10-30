using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.GameHandlers;

public abstract class GameHandler
{
	public Guid GameId { get; }

	public IEnumerable<Guid> PlayerIds { get; }

	public Dictionary<Guid, PlayerIdentity> Players { get; }

	public List<(string Message, PlayerIdentity Sender)> Chat { get; }

	public GameHandler(IEnumerable<Guid> playerIds)
	{
		PlayerIds = playerIds;
		GameId = Guid.NewGuid();
		Players = new Dictionary<Guid, PlayerIdentity>();
		Chat = new List<(string Message, PlayerIdentity Sender)>();
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
