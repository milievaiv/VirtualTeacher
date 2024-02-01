using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VirtualTeacher.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeApiUsersAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedRoles;

        public AuthorizeApiUsersAttribute(params string[] roles)
        {
            allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the token from the Authorization header
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                // Return a 401 Unauthorized response if the token is missing
                context.Result = new UnauthorizedResult();
                return;
            }

            // Perform token validation and decoding (using a JWT library like System.IdentityModel.Tokens.Jwt)
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Retrieve the roles claim from the token
            var rolesClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Check if the user has any of the allowed roles
            if (!IsAuthorized(rolesClaim))
            {
                // Return a 403 Forbidden response if the user is not authorized
                context.Result = new ForbidResult();
            }
        }

        private bool IsAuthorized(string rolesClaim)
        {
            // Check if the user has any of the allowed roles based on the roles claim
            return !string.IsNullOrEmpty(rolesClaim) && allowedRoles.Any(role => role == rolesClaim);
        }
    }
}