using DevAndersen.BlazorGames.Core.Messaging;
using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Games.GameHandlers;

public abstract class GameHandler
{
	public GameIdentity GameIdentity { get; }

	public GameState State { get; protected set; }

	public Guid GameId { get; }

	public IEnumerable<Guid> PlayerIds { get; }

	private Dictionary<Guid, PlayerIdentity> Players { get; }

	public UpdateNotifier UpdateNotifier { get; }

	public MessageHandler Chat { get; }

	public GameDefinition GameDefinition => GameDefinition.GetDefinition(GameIdentity);

	public GameHandler(GameIdentity gameIdentity, IEnumerable<Guid> playerIds)
	{
		GameIdentity = gameIdentity;
		State = GameState.Starting;
		PlayerIds = playerIds;
		GameId = Guid.NewGuid();
		Players = new Dictionary<Guid, PlayerIdentity>();
		UpdateNotifier = new UpdateNotifier();
		Chat = new MessageHandler(UpdateNotifier, GameDefinition.PlayersNeeded > 1);
		Chat.SendSystemMessage("Game starting...");
	}

	public abstract void StartGame();

	public void JoinGame(PlayerIdentity playerIdentity)
	{
		if (PlayerIds.Contains(playerIdentity.Id) && !Players.ContainsKey(playerIdentity.Id))
        {
			lock(this)
            {
                Players[playerIdentity.Id] = playerIdentity;
                Chat.SendSystemMessage($"{playerIdentity.Username} joined the game.");

                if (PlayerIds.SequenceEqual(Players.Keys))
                {
                    State = GameState.Running;
                    Chat.SendSystemMessage("Game started.");
                    StartGame();
                }
            }
        }
		else
        {
            Chat.SendSystemMessage($"{playerIdentity.Username} started spectating.");
        }
    }

	public bool IsInputFromUserInvalid(Guid playerId) => !PlayerIds.Contains(playerId) || State != GameState.Running;

	public virtual void StopGame()
	{
		if (State != GameState.Ended)
        {
			State = GameState.Ended;
            Chat.SendSystemMessage("The game has ended.");
        }
    }
}
