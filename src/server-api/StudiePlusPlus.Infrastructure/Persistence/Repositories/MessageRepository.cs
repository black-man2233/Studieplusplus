using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class MessageRepository : Repository<Message, Guid>, IMessageRepository
{
    private readonly AppDbContext _db;

    public MessageRepository(AppDbContext db, ILoggerFactory loggerFactory) : base(db, loggerFactory)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Message>> GetConversationAsync(Guid userId1, Guid userId2, CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetConversation between userId1={UserId1} and userId2={UserId2}", userId1, userId2);

        var messages = await _db.Messages
            .AsNoTracking()
            .Where(m =>
                (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                (m.SenderId == userId2 && m.ReceiverId == userId1))
            .OrderBy(m => m.SentAt)
            .ToListAsync(ct);

        Logger.LogDebug("GetConversation returned {Count} messages between {UserId1} and {UserId2}",
            messages.Count, userId1, userId2);

        return messages;
    }

    public async Task<IReadOnlyList<Message>> GetByUserAsync(Guid userId, CancellationToken ct = default)
    {
        Logger.LogDebug("DB GetByUser messages for userId={UserId}", userId);

        var messages = await _db.Messages
            .AsNoTracking()
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(ct);

        Logger.LogDebug("GetByUser returned {Count} messages for userId={UserId}", messages.Count, userId);

        return messages;
    }
}
