using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IUserService userService)
        {
            _configuration = configuration;
            _secret = _configuration.GetSection("AppSettings:Token").Value;
            _issuer = _configuration.GetSection("AppSettings:Issuer").Value;
            _audience = _configuration.GetSection("AppSettings:Audience").Value;
            _logger = logger;
            _userService = userService;
        }
        
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userForRegisterDto);
            }

            try
            {
                var isEmailExist = await _userService.GetUserByEmailAsync(userForRegisterDto.Email.ToLower());
                if (isEmailExist != null)
                {
                    return BadRequest("Email already exists");
                }

                var userToCreate = new User
                {
                    FirstName = userForRegisterDto.FirstName,
                    LastName = userForRegisterDto.LastName,
                    Email = userForRegisterDto.Email
                };

                var createdUser = await _userService.InsertUser(userToCreate);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userForLoginDto);
            }

            try
            {
                var userToLogin = await _userService.GetUserByEmailAsync(userForLoginDto.Email);

                if (userToLogin == null)
                {
                    return Unauthorized();
                }


                var token = _userService.GenerateToken(userToLogin, _secret, _issuer, _audience);

                return Ok(new { Token = token.ToString(), Id= userToLogin.Id, Name = userToLogin.FirstName + " " + userToLogin.LastName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [AllowAnonymous]
        [Route("user-email-exist")]
        [HttpGet]
        public async Task<IActionResult> IsEmailExist(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest();
                }

                var isEmailExist = await _userService.GetUserByEmailAsync(email.ToLower());

                if (isEmailExist != null)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}