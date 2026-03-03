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
                .ForMember(d => d.NombreCarta, opt => opt.MapFrom(s => s.Carta.NombreCarta))
                .ForMember(d => d.ImagenPrincipal, opt => opt.MapFrom(s =>
                    s.Carta.ImagenesCarta.FirstOrDefault(i => i.EsPrincipal).Urlimagen))
                .ForMember(d => d.CantidadPujas, opt => opt.MapFrom(s => s.Pujas.Count))
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.EstadoSubasta.NombreEstado));
        }
    }
}
