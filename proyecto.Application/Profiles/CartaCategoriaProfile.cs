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
    public class CartaCategoriaProfile : Profile
    {
        public CartaCategoriaProfile()
        {
            CreateMap<CartaCategoria, CartaCategoriaDTO>()
                .ForMember(dest => dest.CartaId, opt => opt.MapFrom(src => src.CartaId))
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
                .ReverseMap();
        }
    }
}

