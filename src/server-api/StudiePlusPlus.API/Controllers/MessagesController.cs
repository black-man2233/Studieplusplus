using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudiePlusPlus.Application.Abstractions.Mapping;
using StudiePlusPlus.Application.Abstractions.Persistence;
using StudiePlusPlus.Application.Common.Handlers;
using StudiePlusPlus.Application.Features.Messages.Contracts;
using StudiePlusPlus.Application.Features.Messages.Dtos;
using StudiePlusPlus.Domain.Messaging;

namespace StudiePlusPlus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : CrudController<Message, Guid, MessageDto, CreateMessageRequest, UpdateMessageRequest>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper<Message, MessageDto> _mapper;

    public MessagesController(
        ReadHandler<Message, Guid, MessageDto> read,
        WriteHandler<Message, Guid, CreateMessageRequest, UpdateMessageRequest, MessageDto> write,
        IMessageRepository messageRepository,
        IMapper<Message, MessageDto> mapper)
        : base(read, write)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    [HttpGet("conversation/{userId1}/{userId2}")]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetConversation(
        [FromRoute] Guid userId1,
        [FromRoute] Guid userId2,
        CancellationToken ct)
    {
        var messages = await _messageRepository.GetConversationAsync(userId1, userId2, ct);
        return Ok(_mapper.Map(messages));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetByUser(
        [FromRoute] Guid userId,
        CancellationToken ct)
    {
        var messages = await _messageRepository.GetByUserAsync(userId, ct);
        return Ok(_mapper.Map(messages));
    }
}
