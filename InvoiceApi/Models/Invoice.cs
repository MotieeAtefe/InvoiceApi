using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }= string.Empty;
        [Required]
        public DateTime Date { get; set; }= DateTime.Now;

        public  decimal TotalAmount { get; set; }
        public int UserId { get; set; }

        #region relation
        public List<InvoiceItem> Items { get; set; } = new();
        #endregion
    }
}
