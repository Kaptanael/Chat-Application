using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{    
    public class Message
    {
        public Guid Id { get; set; }            
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }        
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
