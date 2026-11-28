using System;
using Microsoft.AspNetCore.Http;

namespace InvoiceApi.Services
{
    public interface ICurrentUser
    {
        public int GetUserId();
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int GetUserId()
        {
            var claim = _accessor.HttpContext?.User?.FindFirst("UserId");

            if (claim != null &&
                int.TryParse(claim.Value, out int result) &&
                result > 0)
            {
                return result;
            }

            throw new Exception("Invalid User Identity");
        }
    }
}
