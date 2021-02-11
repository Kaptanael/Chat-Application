using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class User
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string Email { get; set; }                
        public ICollection<Message> SenderMessages { get; set; }
        public ICollection<Message> ReceiverMessages { get; set; }
    }
}
