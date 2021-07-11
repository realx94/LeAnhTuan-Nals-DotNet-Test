using Core.Entities.Enums;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Authorize
{
    public class JwtAuthAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private List<UserTypes> allowUserTypes { get; set; }
        public JwtAuthAttribute(params UserTypes[] allowUserTypes)
        {
            this.allowUserTypes = allowUserTypes.ToList();
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var auth = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationManager>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString()?.Split(' ').ElementAtOrDefault(1);
            if (string.IsNullOrEmpty(token))
                context.Result = new UnauthorizedResult();
            else
            {
                var claims = auth.ValidateToken(token);
                if (claims == null)
                    context.Result = new UnauthorizedResult();
                else
                {
                    var role = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
                    if (string.IsNullOrEmpty(role) || (allowUserTypes.Any() && !allowUserTypes.Any(p => p.ToString().Equals(role))))
                        context.Result = new UnauthorizedResult();
                    else
                        context.HttpContext.User = claims;
                }
            }
            return Task.CompletedTask;
        }
    }
}
