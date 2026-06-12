using InvoiceApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
namespace InvoiceApi.Filters
{
    public class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(header) || !header.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var token = header.Substring(7);
            var user = JwtHelper.ValidateToken(token);

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
             
            }
            context.HttpContext.User = user; 
        }
    }
}
