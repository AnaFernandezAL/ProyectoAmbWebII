using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using proyecto.Application.DTOs;
using proyecto.Infraestructure.Models;

namespace proyecto.Application.Profiles
{
    public class EstadoSubastaProfile : Profile
    {
        public EstadoSubastaProfile()
        {
            CreateMap<EstadosSubasta, EstadoSubastaDTO>()
                .ForMember(dest => dest.EstadoSubastaId, opt => opt.MapFrom(src => src.EstadoSubastaId))
                .ForMember(dest => dest.NombreEstado, opt => opt.MapFrom(src => src.NombreEstado))
                .ReverseMap();
        }
    }
}

