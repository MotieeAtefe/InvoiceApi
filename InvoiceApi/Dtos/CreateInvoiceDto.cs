using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos
{
    public class CreateInvoiceDto
    {
        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [MinLength(1,ErrorMessage = "Invoice must contain at least one item")]

        public List<CreateInvoiceItemDto> Items { get; set; }
    }
}
