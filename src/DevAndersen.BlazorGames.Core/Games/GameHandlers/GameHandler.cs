using DevAndersen.BlazorGames.Core.Messaging;
using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Games.GameHandlers;

public abstract class GameHandler
{
	public GameIdentity GameIdentity { get; }

	public Guid GameId { get; }

	public IEnumerable<Guid> PlayerIds { get; }

	private Dictionary<Guid, PlayerIdentity> Players { get; }

	public UpdateNotifier UpdateNotifier { get; }

	public MessageHandler Chat { get; }

	public GameDefinition GameDefinition => GameDefinition.GetDefinition(GameIdentity);

	public GameHandler(GameIdentity gameIdentity, IEnumerable<Guid> playerIds)
	{
		GameIdentity = gameIdentity;
		PlayerIds = playerIds;
		GameId = Guid.NewGuid();
		Players = new Dictionary<Guid, PlayerIdentity>();
		UpdateNotifier = new UpdateNotifier();
		Chat = new MessageHandler(UpdateNotifier, true);
		Chat.SendSystemMessage("Game starting...");
	}

	public abstract void StartGame();

	public void JoinGame(PlayerIdentity playerIdentity)
	{
		Players[playerIdentity.Id] = playerIdentity;
	}

	public virtual void StopGame()
	{
		Chat.SendSystemMessage("The game has ended.");
	}

	public void SendChatMessage(string message, PlayerIdentity? playerIdentity)
	{
		if (!string.IsNullOrWhiteSpace(message) && playerIdentity != null)
		{
			Chat.SendChatMessage(message, playerIdentity);
		}
	}
}
