using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Dtos;

namespace ChatApp.Services
{
    public interface IMessageService
    {
        Task<MessageForListDto> InsertMessage(Guid senderId, MessageForCreateDto messageForCreateDto, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<MessageForListDto>> GetAllMessageAsync(CancellationToken cancellationToken = default);
        Task<List<MessageForListDto>> GetAllMessageAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken = default);
    }
}