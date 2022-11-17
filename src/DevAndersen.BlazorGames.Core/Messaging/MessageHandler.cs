using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Messaging;

public class MessageHandler
{
    public bool HasChatSupport { get; init; }

    private readonly UpdateNotifier updateNotifier;
    private readonly List<Message> chatMessages;

    public MessageHandler(UpdateNotifier updateNotifier, bool hasChatSupport)
    {
        this.updateNotifier = updateNotifier;
        HasChatSupport = hasChatSupport;
        chatMessages = new List<Message>();
    }

    public void SendChatMessage(string message, PlayerIdentity identity)
    {
        if (HasChatSupport)
        {
            SendMessage(message, new PlayerMessageSender(identity));
        }
    }

    public void SendSystemMessage(string message)
    {
        SendMessage(message, new SystemMessageSender(MessageLevel.Information));
    }

    public void SendSystemMessage(string message, MessageLevel messageLevel)
    {
        SendMessage(message, new SystemMessageSender(messageLevel));
    }

    private void SendMessage(string message, IMessageSender sender)
    {
        chatMessages.Add(new Message(message, sender));
        updateNotifier.Update();
    }

    public IEnumerable<Message> GetChatMessages()
    {
        return chatMessages;
    }

    public IEnumerable<MessageGroup> GetGroupedChatMessages()
    {
        IEnumerable<Message> chatMessages = GetChatMessages();

        if (chatMessages.Any())
        {
            int index = 0;
            bool keepGoing = true;

            while (keepGoing)
            {
                var timeOfFirstMessageInBlock = chatMessages.Skip(index).First().Timestamp;
                IMessageSender sender = chatMessages.Skip(index).First().Sender;

                if (sender is SystemMessageSender)
                {
                    string[] message = chatMessages
                        .Skip(index)
                        .Take(1)
                        .Select(x => x.Text)
                        .ToArray();

                    index++;
                    yield return new MessageGroup(sender, message, timeOfFirstMessageInBlock);
                }
                else
                {
                    string[] messages = chatMessages
                        .Skip(index)
                        .TakeWhile(x => x.Sender.Equals(sender) && (x.Timestamp - timeOfFirstMessageInBlock) < TimeSpan.FromSeconds(5))
                        .Select(y => y.Text)
                        .ToArray();

                    index += messages.Length;
                    yield return new MessageGroup(sender, messages, timeOfFirstMessageInBlock);
                }

                if (index >= chatMessages.Count())
                {
                    keepGoing = false;
                }

            }
        }
    }
}
