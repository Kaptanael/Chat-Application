using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _dbContext;

        public UserRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(CancellationToken cancellationToken = default)
        {
            var users = await _dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
            return users;
        }

        public async Task<IEnumerable<User>> GetAllUserExceptByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var users = await _dbContext.Users.AsNoTracking().Where(u=>u.Id != id).ToListAsync(cancellationToken);
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
            return user;
        }        

        public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            return user;
        }        

        public async Task<User> InsertUser(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
