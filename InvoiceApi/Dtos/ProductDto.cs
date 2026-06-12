using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "قیمت باید بیشتر از صفر باشد")]
        public decimal Price { get; set; }
    }
}
