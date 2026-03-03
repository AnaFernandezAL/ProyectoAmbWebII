using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Roles, RolDTO>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreRol))
                .ReverseMap();
        }
    }
}
