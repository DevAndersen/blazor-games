using DevAndersen.BlazorGames.Core.Messaging;
using DevAndersen.BlazorGames.Core.Players;

namespace DevAndersen.BlazorGames.Core.Chat;

public class MessageHandler
{
    public bool HasChatSupport { get; }

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
        SendMessage(message, new SystemMessageSender(SystemMessageSender.MessageLevel.Information));
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

    public IEnumerable<KeyValuePair<IMessageSender, string[]>> GetGroupedChatMessages()
    {
        IEnumerable<Message> chatMessages = GetChatMessages();

        if (chatMessages.Any())
        {
            int index = 0;
            bool keepGoing = true;

            while (keepGoing)
            {
                IMessageSender sender = chatMessages.Skip(index).First().Sender;

                string[] messages = chatMessages
                    .Skip(index)
                    .TakeWhile(x => x.Sender.Equals(sender))
                    .Select(y => y.Text)
                    .ToArray();

                index += messages.Length;
                if (index >= chatMessages.Count())
                {
                    keepGoing = false;
                }

                yield return new KeyValuePair<IMessageSender, string[]>(sender, messages);
            }
        }
    }
}
