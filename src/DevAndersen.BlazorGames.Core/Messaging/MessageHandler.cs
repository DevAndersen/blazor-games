﻿using DevAndersen.BlazorGames.Core.Players;

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
