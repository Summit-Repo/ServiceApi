using AutoMapper;
using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using ModelLayer.ViewModel;
using System.Linq;

namespace BusinessLayer
{
    public class Mapper : Profile
    {
        public Mapper()
        {
           
                    CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<Users, UsersDto>()
                .ForMember(dto=>dto.IsLocked,act=>act.Ignore())
                .ForMember(dto => dto.Password, act => act.Ignore())
                .ForMember(dto => dto.Id, act => act.Ignore())
                .ForMember(dto => dto.UserRoles, act => act.Ignore())
                .ForMember(dto => dto.UserClaims, act => act.Ignore()).ReverseMap();
                    CreateMap<UserRole, UserRoleDto>().ReverseMap();
                    CreateMap<UserClaims, UserClaimsDto>().ReverseMap();
                    CreateMap<Role, RoleDto>().ReverseMap();
               
           
        }

       

    }
}
