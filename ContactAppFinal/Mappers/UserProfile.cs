using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ContactAppFinal.Models;
using ContactAppFinal.ViewModels;

namespace ContactAppFinal.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // We'll handle Role mapping separately
        }
    }
}