using AutoMapper;
using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    // Esta clase hereda de Profile, que es una clase de AutoMapper que permite configurar mapeos de objetos.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Este mapeo copia el valor de la propiedad NombreServicio de un objeto ServicioDto a otro objeto ServicioDto.
            CreateMap<ServicioDto, ServicioDto>()
                .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.NombreServicio));

            // Este mapeo copia los valores de las propiedades Email y Token de un objeto UsuarioDto a otro objeto UsuarioDto.
            CreateMap<UsuarioDto, UsuarioDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));

            // Este mapeo copia los valores de las propiedades Email y Password de un objeto LoginDto a otro objeto LoginDto.
            CreateMap<LoginDto, LoginDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            // Este mapeo copia los valores de las propiedades Email y Password de un objeto LoginPaseadorDto a otro objeto LoginPaseadorDto.
            CreateMap<LoginPaseadorDto, LoginPaseadorDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }


}
