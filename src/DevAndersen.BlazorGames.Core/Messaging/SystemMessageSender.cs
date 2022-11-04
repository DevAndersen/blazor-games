namespace DevAndersen.BlazorGames.Core.Messaging;

public record SystemMessageSender : IMessageSender
{
    internal readonly MessageLevel messageLevel;

    public SystemMessageSender(MessageLevel messageLevel)
    {
        this.messageLevel = messageLevel;
    }

    public bool Equals(IMessageSender? other)
    {
        return other is SystemMessageSender sender
            && sender.messageLevel == messageLevel;
    }

    public string GetSenderIdentity() => messageLevel.ToString();

    public enum MessageLevel
    {
        Information,
        Debug
    }
}
