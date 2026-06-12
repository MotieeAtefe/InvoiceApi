using InvoiceApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Dtos
{
    public class InvoiceItemDto
    {

        public int InvoiceId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
