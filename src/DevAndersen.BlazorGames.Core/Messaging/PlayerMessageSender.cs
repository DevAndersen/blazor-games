using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Messaging;

public record PlayerMessageSender : IMessageSender
{
    private readonly PlayerIdentity identity;

    public Guid PlayerId => identity.Id;

    public PlayerMessageSender(PlayerIdentity identity)
    {
        this.identity = identity;
    }

    public string GetSenderIdentity() => identity.Username ?? identity.Id.ToString();
}
