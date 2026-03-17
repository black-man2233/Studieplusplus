using System;
using StudiePlusPlus.Domain.Common;

namespace StudiePlusPlus.Domain.Messaging;

public sealed class Message : Entity<Guid>
{
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime SentAt { get; private set; }
    public bool IsRead { get; private set; }

    private Message() { }

    public Message(Guid id, Guid senderId, Guid receiverId, string content)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
