using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

    }
}
