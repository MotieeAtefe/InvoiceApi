using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos
{
    public class CreateInvoiceItemDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
