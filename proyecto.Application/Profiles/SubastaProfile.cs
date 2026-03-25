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
    public class SubastaProfile : Profile
    {
        public SubastaProfile()
        {
            CreateMap<Subastas, SubastaDTO>()
                .ForMember(dest => dest.CantidadPujas, opt => opt.MapFrom(src => src.Pujas.Count))
                .ForMember(dest => dest.EstadoSubasta, opt => opt.MapFrom(src => src.EstadoSubasta))
                .ForMember(dest => dest.Carta, opt => opt.MapFrom(src => src.Carta))
                .ForMember(dest => dest.Vendedor, opt => opt.MapFrom(src => src.Vendedor))
                .ReverseMap()
                .ForMember(dest => dest.Carta, opt => opt.Ignore())
                .ForMember(dest => dest.Vendedor, opt => opt.Ignore())
                .ForMember(dest => dest.EstadoSubasta, opt => opt.Ignore());
        }
    }
}
