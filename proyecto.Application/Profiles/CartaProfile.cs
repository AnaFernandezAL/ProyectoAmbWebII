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
    public class CartaProfile : Profile
    {
        public CartaProfile()
        {
            CreateMap<Cartas, CartaDTO>()
                .ForMember(dest => dest.NombreCarta, opt => opt.MapFrom(src => src.NombreCarta))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Condicion, opt => opt.MapFrom(src => src.Condicion))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.FechaRegistro))
                .ForMember(dest => dest.Edicion, opt => opt.MapFrom(src => src.Edicion))
                .ForMember(dest => dest.Rareza, opt => opt.MapFrom(src => src.Rareza))
                .ForMember(dest => dest.EstadoCarta, opt => opt.MapFrom(src => src.EstadoCarta))
                .ForMember(dest => dest.Vendedor, opt => opt.MapFrom(src => src.Vendedor))
                .ForMember(dest => dest.CartaCategoria, opt => opt.MapFrom(src => src.CartaCategoria))
                .ForMember(dest => dest.ImagenesCarta, opt => opt.MapFrom(src => src.ImagenesCarta))
                .ForMember(dest => dest.Subastas, opt => opt.MapFrom(src => src.Subastas))
                .ReverseMap();
        }
    }
}

