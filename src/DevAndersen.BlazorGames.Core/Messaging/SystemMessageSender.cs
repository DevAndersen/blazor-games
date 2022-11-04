namespace DevAndersen.BlazorGames.Core.Messaging;

public record SystemMessageSender : IMessageSender
{
    private readonly MessageLevel messageLevel;

    public SystemMessageSender(MessageLevel messageLevel)
    {
        this.messageLevel = messageLevel;
    }

    public string GetSenderIdentity() => messageLevel.ToString();

    public enum MessageLevel
    {
        Information,
        Debug
    }
}
