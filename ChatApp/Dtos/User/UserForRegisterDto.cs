using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Dtos
{
    public class UserForRegisterDto
    {
        [Required, MaxLength(64)]
        public string FirstName { get; set; }

        [Required, MaxLength(64)]
        public string LastName { get; set; }

        [Required, MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
