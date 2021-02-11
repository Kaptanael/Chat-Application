using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UserForListDto GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            UserForListDto userForListDto = null;

            if (identityClaims != null && identityClaims.Claims.Count() > 0)
            {
                userForListDto = new UserForListDto();                
                userForListDto.Id = new Guid(identityClaims.FindFirst("UserId").Value);
                userForListDto.Email = identityClaims.FindFirst("UserName").Value;                
            }

            return userForListDto;
        }
    }
}