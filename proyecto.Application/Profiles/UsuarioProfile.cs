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
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        { CreateMap<Usuarios, UsuarioDTO>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol.NombreRol))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoUsuario.NombreEstado)); 
        }
    }
}
