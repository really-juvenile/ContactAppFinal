using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ContactAppFinal.Models;
using ContactAppFinal.ViewModels;

namespace ContactAppFinal.Mappers
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<Role, RoleDTO>();

            CreateMap<RoleDTO, Role>()
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}