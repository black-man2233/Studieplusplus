using System;

namespace StudiePlusPlus.Application.Features.Messages.Dtos;

public sealed record MessageDto(
    Guid Id,
    Guid SenderId,
    Guid ReceiverId,
    string Content,
    DateTime SentAt,
    bool IsRead
);
