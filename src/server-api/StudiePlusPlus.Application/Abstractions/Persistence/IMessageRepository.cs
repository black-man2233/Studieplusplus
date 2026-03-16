using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.Application.Abstractions.Persistence;

public interface IMessageRepository : IRepository<Message, Guid>
{
    Task<IReadOnlyList<Message>> GetConversationAsync(Guid userId1, Guid userId2, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetByUserAsync(Guid userId, CancellationToken ct = default);
}
