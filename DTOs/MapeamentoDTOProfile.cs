using AutoMapper;
using KBank_Web_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KBank_Web_API.DTOs
{
    public class MapeamentoDTOProfile : Profile
    {
        public MapeamentoDTOProfile()
        {
            CreateMap<CompraParcelada, CompraParcelaProvisorioDTO>()
                .ForMember(
                dest => dest.DataCompra,
                opt => opt.MapFrom(src => src.DataCompra.ToString("dd/MM/yyyy"))
            ).ReverseMap();

            CreateMap<CompraParcelada, CompraParceladaRequestDTO>().ReverseMap();
            CreateMap<CompraParcelada, CompraParceladaResponseDTO>()
            .ForMember(
                dest => dest.DataCompra,
                opt => opt.MapFrom(src => src.DataCompra.ToString("dd/MM/yyyy"))
            ).ReverseMap();

            CreateMap<Parcela, ParcelaResponseDTO>()
            .ForMember(
                dest => dest.DataParcela,
                opt => opt.MapFrom(src => src.DataParcela.ToString("dd/MM/yyyy"))
            ).ReverseMap();

            CreateMap<Transacao, TransacaoRequestDTO>().ReverseMap();
            CreateMap<Transacao, TransacaoResponseDTO>().ForMember(
                dest => dest.DataTransacao,
                opt => opt.MapFrom(src => src.DataTransacao.ToString("dd/MM/yyyy"))
            ).ReverseMap();
        }
    }
}
