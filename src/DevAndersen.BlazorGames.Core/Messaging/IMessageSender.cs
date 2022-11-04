namespace DevAndersen.BlazorGames.Core.Messaging;

public interface IMessageSender : IEquatable<IMessageSender>
{
    public string GetSenderIdentity();
}
