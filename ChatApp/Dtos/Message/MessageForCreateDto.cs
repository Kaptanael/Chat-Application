using ChatApp.Models;
using System;

namespace ChatApp.Dtos
{
    public class MessageForCreateDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }        
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
