namespace DevAndersen.BlazorGames.Core.GameHandlers;

public abstract class GameHandler
{
	public Guid GameId { get; }

	public IEnumerable<Guid> PlayerIds { get; init; }

	public GameHandler(IEnumerable<Guid> playerIds)
	{
		PlayerIds = playerIds;
		GameId = Guid.NewGuid();
	}

	public void StopGame()
	{

	}
}
