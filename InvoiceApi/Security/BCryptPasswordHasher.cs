namespace InvoiceApi.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        string IPasswordHasher.HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        bool IPasswordHasher.VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
