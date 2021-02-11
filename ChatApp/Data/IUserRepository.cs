using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Models;

namespace ChatApp.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllUserExceptByIdAsync(Guid id, CancellationToken cancellationToken = default);        
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User> InsertUser(User user, CancellationToken cancellationToken = default);
    }
}