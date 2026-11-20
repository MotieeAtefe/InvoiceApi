using InvoiceApi.Dtos;
using InvoiceApi.Results;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterDto dto);
        Task<LoginResult> Login(LoginDto dto);


    }
}
