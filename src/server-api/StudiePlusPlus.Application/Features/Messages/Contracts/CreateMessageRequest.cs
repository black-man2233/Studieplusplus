using System;

namespace StudiePlusPlus.Application.Features.Messages.Contracts;

public sealed record CreateMessageRequest(
    Guid SenderId,
    Guid ReceiverId,
    string Content
);
