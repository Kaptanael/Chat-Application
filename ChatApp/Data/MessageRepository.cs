using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _dbContext;

        public MessageRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Message> InsertMessage(Message message, CancellationToken cancellationToken = default)
        {
            await _dbContext.Messages.AddAsync(message, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return message;
        }

        public async Task<IEnumerable<Message>> GetAllMessageAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var messages = await _dbContext.Messages.AsNoTracking().ToListAsync(cancellationToken);
            return messages;
        }

        public async Task<IEnumerable<Message>> GetAllMessageAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var messages = await _dbContext.Messages.AsNoTracking().Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId)).ToListAsync(cancellationToken);
            return messages;
        }
    }
}
