using System;
using StudiePlusPlus.Application.Abstractions.Security;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Messages.Contracts;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.Application.Features.Messages.Mapping;

public sealed class CreateMessageRequestMapper : BaseMapper<CreateMessageRequest, Message>
{
    private readonly IEncryptionService _encryption;

    public CreateMessageRequestMapper(IEncryptionService encryption)
    {
        _encryption = encryption;
    }

    public override Message Map(CreateMessageRequest source)
    {
        return new Message(
            Guid.NewGuid(),
            source.SenderId,
            source.ReceiverId,
            _encryption.Encrypt(source.Content));
    }

    public override void Update(CreateMessageRequest source, Message destination)
    {
    }
}

public sealed class UpdateMessageRequestMapper : BaseMapper<UpdateMessageRequest, Message>
{
    public override Message Map(UpdateMessageRequest source)
    {
        return null!;
    }

    public override void Update(UpdateMessageRequest source, Message destination)
    {
        if (source.IsRead)
            destination.MarkAsRead();
    }
}
