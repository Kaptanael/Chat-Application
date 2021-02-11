using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Models;

namespace ChatApp.Data
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessage(Message message, CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> GetAllMessageAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> GetAllMessageAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken = default);
    }
}