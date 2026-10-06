namespace KBank_Web_API.DTOs
{
    public class CompraParceladaResponseDTO
    {
        public int CompraParceladaId { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataCompra { get; set; } = DateTime.Now;
    }
}
