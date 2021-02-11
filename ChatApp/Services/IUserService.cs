using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Dtos;
using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IUserService
    {
        Task<List<UserForListDto>> GetAllUserAsync(CancellationToken cancellationToken = default);

        Task<List<UserForListDto>> GetAllUserExceptByIdAsync(Guid id, CancellationToken cancellationToken = default);        

        Task<UserForListDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<UserForListDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<User> InsertUser(User user, CancellationToken cancellationToken = default);

        string GenerateToken(UserForListDto userForListDto, string secret, string issuer, string audience);
    }
}