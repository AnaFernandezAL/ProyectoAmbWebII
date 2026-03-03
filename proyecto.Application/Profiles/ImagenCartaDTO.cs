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
    public class ImagenCartaProfile : Profile
    {
        public ImagenCartaProfile()
        {
            CreateMap<ImagenesCarta, ImagenCartaDTO>()
                .ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.Urlimagen))
                .ForMember(dest => dest.EsPrincipal, opt => opt.MapFrom(src => src.EsPrincipal))
                .ReverseMap();
        }
    }
}
