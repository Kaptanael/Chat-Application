using ChatApp.Dtos;
using ChatApp.Hubs;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IMessageService _messageService;
        private readonly ILogger<AuthController> _logger;

        public MessagesController(ILogger<AuthController> logger,IHubContext<MessageHub> hubContext,IMessageService messageService)
        {
            _logger = logger;
            _hubContext = hubContext;
            _messageService = messageService;
        }        
        
        [Route("send-message")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] MessageForCreateDto messageForCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messageForCreateDto);            
            }

            try
            {
                var messageToReturn = await _messageService.InsertMessage(base.GetUserClaims().Id, messageForCreateDto);
                await _hubContext.Clients.All.SendAsync("BroadcastMessage", messageToReturn);
                //await _hubContext.Clients.All.BroadcastMessage(messageToReturn);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Route("get-message-by-receiver-id/{receiverId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(Guid receiverId)
        {
            try
            {                
                var messagesToReturn = await _messageService.GetAllMessageAsync(base.GetUserClaims().Id, receiverId);

                if (messagesToReturn == null)
                {
                    return NotFound();
                }

                return Ok(messagesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}