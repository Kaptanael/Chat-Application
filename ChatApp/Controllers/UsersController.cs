using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatApp.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {        
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {            
            _logger = logger;
            _userService = userService;
        }

        [Route("get-all-user")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var usersToReturn = await _userService.GetAllUserAsync();

                if (usersToReturn == null)
                {
                    return NotFound();
                }

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("get-all-user-except-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetUserExceptById()
        {
            try
            {
                var usersToReturn = await _userService.GetAllUserExceptByIdAsync(base.GetUserClaims().Id);                

                if (usersToReturn == null)
                {
                    return NotFound();
                }

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("get-user-by-email/{email}")]
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var userToReturn = await _userService.GetUserByEmailAsync(email);

                if (userToReturn == null)
                {
                    return NotFound();
                }

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("get-user-by-id/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var userToReturn = await _userService.GetUserByIdAsync(id);

                if (userToReturn == null)
                {
                    return NotFound();
                }

                return Ok(userToReturn);
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