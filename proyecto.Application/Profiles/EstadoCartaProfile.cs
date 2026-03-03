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
    public class EstadoCartaProfile : Profile
    {
        public EstadoCartaProfile()
        {
            CreateMap<EstadosCarta, EstadoCartaDTO>()
                .ForMember(dest => dest.NombreEstado, opt => opt.MapFrom(src => src.NombreEstado))
                .ReverseMap();
        }
    }
}

