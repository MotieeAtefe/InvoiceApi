using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }=string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
