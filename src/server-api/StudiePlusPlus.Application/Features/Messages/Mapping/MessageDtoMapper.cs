using StudiePlusPlus.Application.Abstractions.Security;
using StudiePlusPlus.Application.Common.Mapping;
using StudiePlusPlus.Application.Features.Messages.Dtos;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.Application.Features.Messages.Mapping;

public sealed class MessageDtoMapper : BaseMapper<Message, MessageDto>
{
    private readonly IEncryptionService _encryption;

    public MessageDtoMapper(IEncryptionService encryption)
    {
        _encryption = encryption;
    }

    public override MessageDto Map(Message source)
    {
        return new MessageDto(
            source.Id,
            source.SenderId,
            source.ReceiverId,
            _encryption.Decrypt(source.Content),
            source.SentAt,
            source.IsRead);
    }

    public override void Update(Message source, MessageDto destination)
    {
    }
}
