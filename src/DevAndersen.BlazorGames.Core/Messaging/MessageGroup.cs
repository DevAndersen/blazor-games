namespace DevAndersen.BlazorGames.Core.Messaging;

public record MessageGroup(IMessageSender Sender, string[] Messages, DateTime Timestamp);
