using System.ComponentModel.DataAnnotations;

namespace InvoiceApi.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "طول نام باید بین {2} تا {1} کاراکتر باشد")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "وارد کردن پسورد اجباری است")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "طول پسورد باید بین {2} تا {1} کاراکتر باشد")]
        public string Password { get; set; }
    }
}
