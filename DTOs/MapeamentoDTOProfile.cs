using AutoMapper;
using KBank_Web_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KBank_Web_API.DTOs
{
    public class MapeamentoDTOProfile : Profile
    {
        public MapeamentoDTOProfile()
        {
            
            CreateMap<CompraParcelada, CompraParceladaResponseDTO>()
            .ForMember(
                dest => dest.DataCompra,
                opt => opt.MapFrom(src => src.DataCompra.ToString("dd/MM/yyyy"))
            );

            CreateMap<Parcela, ParcelaResponseDTO>()
            .ForMember(
                dest => dest.DataParcela,
                opt => opt.MapFrom(src => src.DataParcela.ToString("dd/MM/yyyy"))
            );
        }
    }
}
