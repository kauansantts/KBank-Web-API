using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.DTOs
{
    public class ParcelaResponseDTO
    {
        public int ParcelaId { get; set; }
        public int NumeroParcela { get; set; }
        public double ValorParcela { get; set; }
        public string DataParcela { get; set; }
        public int CompraParceladaId { get; set; }
        public TipoEnum TipoParcela { get; set; }
        public CategoriaEnum CategoriaParcela { get; set; }
    }
}
