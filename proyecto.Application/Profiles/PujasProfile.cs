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
    public class PujasProfile : Profile
    {
        public PujasProfile() 
        {
            CreateMap<Pujas, PujasDTO>()
           .ForMember(dest => dest.PujaId, opt => opt.MapFrom(src => src.PujaId))
       .ForMember(dest => dest.SubastaId, opt => opt.MapFrom(src => src.SubastaId))
       .ForMember(dest => dest.CompradorId, opt => opt.MapFrom(src => src.CompradorId))
       .ForMember(dest => dest.MontoOfertado, opt => opt.MapFrom(src => src.MontoOfertado))
       .ForMember(dest => dest.FechaHora, opt => opt.MapFrom(src => src.FechaHora))
       .ForMember(dest => dest.Notificado, opt => opt.MapFrom(src => src.Notificado))
       .ForMember(dest => dest.Comprador, opt => opt.MapFrom(src => src.Comprador))
       .ForMember(dest => dest.Subasta, opt => opt.MapFrom(src => src.Subasta));
        }

    }
}
