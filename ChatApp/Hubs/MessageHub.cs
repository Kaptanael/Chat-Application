using ChatApp.Dtos;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class MessageHub : Hub
    {
        public Task SendMessage(MessageForCreateDto messageForCreateDto)
        {
            return Clients.Client(this.Context.ConnectionId).SendAsync("BroadcastMessage", messageForCreateDto);
        }
    }
}
