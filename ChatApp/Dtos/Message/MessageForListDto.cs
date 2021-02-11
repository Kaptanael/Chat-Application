using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dtos
{
    public class MessageForListDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }        
        public Guid SenderId { get; set; }       
        public Guid ReceiverId { get; set; }        
    }
}
