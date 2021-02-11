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
    public class UserService : IUserService
    {        
        private readonly IMapper _mapper;        
        private readonly IUserRepository _userRepository;        

        public UserService(IMapper mapper, IUserRepository userRepository)
        {            
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserForListDto>> GetAllUserAsync(CancellationToken cancellationToken = default)
        {
            var usersFromRepo = await _userRepository.GetAllUserAsync(cancellationToken);
            var usersToReturn = _mapper.Map<List<UserForListDto>>(usersFromRepo);

            return usersToReturn;
        }

        public async Task<List<UserForListDto>> GetAllUserExceptByIdAsync(Guid id, CancellationToken cancellationToken = default) 
        {
            var usersFromRepo = await _userRepository.GetAllUserExceptByIdAsync(id,cancellationToken);
            var usersToReturn = _mapper.Map<List<UserForListDto>>(usersFromRepo);

            return usersToReturn;
        }       

        public async Task<UserForListDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var userFromRepo = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
            var userToReturn = _mapper.Map<UserForListDto>(userFromRepo);
            return userToReturn;
        }

        public async Task<UserForListDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userFromRepo = await _userRepository.GetUserByIdAsync(id, cancellationToken);
            var userToReturn = _mapper.Map<UserForListDto>(userFromRepo);
            return userToReturn;
        }

        public async Task<User> InsertUser(User user, CancellationToken cancellationToken = default) 
        {
            var createdUser =  await _userRepository.InsertUser(user, cancellationToken);
            return createdUser;
        }

        public string GenerateToken(UserForListDto userForListDto, string secret, string issuer, string audience)
        {            

            var claims = new[]
                {
                    new Claim("UserId", userForListDto.Id.ToString()),
                    new Claim("UserName",userForListDto.FirstName + " " + userForListDto.LastName)                    
                };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); ;
        }        
    }
}
