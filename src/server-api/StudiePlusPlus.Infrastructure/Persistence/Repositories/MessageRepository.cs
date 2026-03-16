using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.Infrastructure.Persistence.Repositories;

public sealed class MessageRepository : Repository<Message, Guid>, IMessageRepository
{
    private readonly AppDbContext _db;

    public MessageRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Message>> GetConversationAsync(Guid userId1, Guid userId2, CancellationToken ct = default)
    {
        return await _db.Messages
            .AsNoTracking()
            .Where(m =>
                (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                (m.SenderId == userId2 && m.ReceiverId == userId1))
            .OrderBy(m => m.SentAt)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Message>> GetByUserAsync(Guid userId, CancellationToken ct = default)
    {
        return await _db.Messages
            .AsNoTracking()
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(ct);
    }
}
