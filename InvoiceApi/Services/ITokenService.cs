namespace InvoiceApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, int userId);

    }
}
