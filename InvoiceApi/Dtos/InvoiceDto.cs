using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InvoiceApi.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        public DateTime Date { get; set; }
        [MinLength(1,ErrorMessage = "Invoice must contain at least one item")]
        public List<InvoiceItemDto> Items { get; set; }
        [Range(0.01,double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}
