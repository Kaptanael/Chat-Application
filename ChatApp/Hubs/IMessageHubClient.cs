using ChatApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public interface IMessageHubClient
    {
        Task BroadcastMessage(MessageForListDto message);
    }
}
