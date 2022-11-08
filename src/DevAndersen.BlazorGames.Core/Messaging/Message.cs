using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Messaging;

public class Message
{
    public string Text { get; }

    public IMessageSender Sender { get; }

    public bool IsSystemMessage => Sender is SystemMessageSender;

    public DateTime Timestamp { get; }

    public Message(string text, IMessageSender sender)
    {
        Text = text;
        Sender = sender;
        Timestamp = DateTime.Now;
    }
}
