using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs
{
    public class CompraParceladaResponseDTO
    {
        public int CompraParceladaId { get; set; }
        public string NomeProduto { get; set; }
        public string DataCompra { get; set; }
        public int? QuantidadeParcelada { get; set; }
        public CategoriaEnum CategoriaParcela { get; set; }
        public TipoEnum TipoCompra { get; set; }
        public IEnumerable<ParcelaResponseDTO> Parcelas { get; set; }
    }
}
