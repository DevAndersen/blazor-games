namespace DevAndersen.BlazorGames.Core.Messaging;

public record SystemMessageSender : IMessageSender
{
    public MessageLevel MessageLevel { get; }

    public SystemMessageSender(MessageLevel messageLevel)
    {
        this.MessageLevel = messageLevel;
    }

    public bool Equals(IMessageSender? other)
    {
        return other is SystemMessageSender sender
            && sender.MessageLevel == MessageLevel;
    }

    public string GetSenderIdentity() => MessageLevel.ToString();
}
