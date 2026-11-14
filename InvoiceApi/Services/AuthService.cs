using InvoiceApi.Data;
using InvoiceApi.Dtos;
using InvoiceApi.Helpers;
using InvoiceApi.Models;
using InvoiceApi.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InvoiceApi.Security;

namespace InvoiceApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher _passwordHasher;
        public AuthService(AppDbContext appDbContext, IPasswordHasher passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }
        public async Task<LoginResult> Login(LoginDto dto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                return new LoginResult
                {
                    IsSuccess = false,
                    Message = "نام کاربری وجود ندارد",
                    Token = null
                };
            
            var resultVerifyPassword = _passwordHasher.VerifyPassword(dto.Password ,user.Password);

            if (resultVerifyPassword == true)
            {
                var token = JwtHelper.GenerateToken(user.UserName, user.Id);
                return new LoginResult
                {
                    IsSuccess = true,
                    Message = "با موفقیت وارد شدید!",
                    Token = token
                };
            }

                return new LoginResult
                {
                    IsSuccess = false,
                    Message = "رمز عبور اشتباه است! مجدد امتحان کنید.",
                    Token = null
                };
            
               

            
          
        }

        public async Task<RegisterResult> Register(RegisterDto dto)
        {

            var userExists = await _appDbContext.Users.AnyAsync(u => u.UserName == dto.UserName);

            if (userExists)
            {
                return new RegisterResult
                {
                    IsSuccess = false,
                    Message = $"کاربر با {dto.UserName} نام وجود دارد این"
                };
            }
         
                var user = new User
                {
                    UserName = dto.UserName,
                    Password = _passwordHasher.HashPassword(dto.Password)
                };
                _appDbContext.Users.Add(user);
                await _appDbContext.SaveChangesAsync();
                return new RegisterResult
                {
                    IsSuccess = true,
                    Message = "حساب کاربری با موفقیت ایجاد شد!"
                };

            

           
        }
    }
}
