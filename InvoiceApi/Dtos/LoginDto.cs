using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
