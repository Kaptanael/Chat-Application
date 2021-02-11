using AutoMapper;
using AutoMapper.Configuration;
using ChatApp.Data;
using ChatApp.Dtos;
using ChatApp.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task<MessageForListDto> InsertMessage(Guid senderId, MessageForCreateDto messageForCreateDto, CancellationToken cancellationToken = default(CancellationToken))
        {
            var messageToCreate = new Message
            {
                Text = messageForCreateDto.Text,                
                SenderId = senderId,
                ReceiverId = messageForCreateDto.ReceiverId
            };

            var messageFromRepo = await _messageRepository.InsertMessage(messageToCreate, cancellationToken);
            var messageToReturn = _mapper.Map<MessageForListDto>(messageFromRepo);


            return messageToReturn;
        }

        public async Task<List<MessageForListDto>> GetAllMessageAsync(CancellationToken cancellationToken = default)
        {
            var messagesFromRepo = await _messageRepository.GetAllMessageAsync();
            var messagesToReturn = _mapper.Map<List<MessageForListDto>>(messagesFromRepo);

            return messagesToReturn;
        }

        public async Task<List<MessageForListDto>> GetAllMessageAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var messagesFromRepo = await _messageRepository.GetAllMessageAsync(senderId, receiverId, cancellationToken);
            var messagesToReturn = _mapper.Map<List<MessageForListDto>>(messagesFromRepo);

            return messagesToReturn;
        }
    }
}
