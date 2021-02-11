using AutoMapper;
using ChatApp.Dtos;
using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<Message, MessageForListDto>().ReverseMap();            
        }
    }
}
