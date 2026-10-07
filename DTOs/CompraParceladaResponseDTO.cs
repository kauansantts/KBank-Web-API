using KBank_Web_API.Models;

namespace KBank_Web_API.DTOs
{
    public class CompraParceladaResponseDTO
    {
        public int CompraParceladaId { get; set; }
        public string NomeProduto { get; set; }
        public string DataCompra { get; set; }
        public IEnumerable<ParcelaResponseDTO> Parcelas { get; set; }
    }
}
