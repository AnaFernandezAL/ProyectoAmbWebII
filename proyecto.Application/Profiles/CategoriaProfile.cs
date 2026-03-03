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
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categorias, CategoriaDTO>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.NombreCategoria))
                .ReverseMap();
        }
    }
}
