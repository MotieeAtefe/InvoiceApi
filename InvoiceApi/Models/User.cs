using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(60)]
        public string Password { get; set; }

    }
}
