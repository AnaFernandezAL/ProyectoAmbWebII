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
        {
            CreateMap<Usuarios, UsuarioDTO>()
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.CorreoElectronico))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.NombreCompleto))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol))
                .ForMember(dest => dest.EstadoUsuario, opt => opt.MapFrom(src => src.EstadoUsuario))
                .ReverseMap();
        }
    }
}