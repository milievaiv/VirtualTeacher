using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Org.BouncyCastle.Asn1.Ocsp;

namespace VirtualTeacher.Attributes
{
    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //public class AuthorizeUsersAttribute : Attribute, IAuthorizationFilter
    //{
    //    private readonly string[] allowedRoles;

    //    public AuthorizeUsersAttribute(params string[] roles)
    //    {
    //        allowedRoles = roles;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var user = context.HttpContext.User;

    //        if (!user.Identity.IsAuthenticated)
    //        {
    //            context.Result = new RedirectToRouteResult(new { controller = "Auth", action = "SignIn" });
    //            return;
    //        }

    //        if (!allowedRoles.Any(role => user.IsInRole(role)))
    //        {
    //            context.Result = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
    //        }
    //    }
    //}
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUsersAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedRoles;

        public AuthorizeUsersAttribute(params string[] roles)
        {
            allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var user = context.HttpContext.User;
            var tokenAsStr = context.HttpContext.Request.Cookies["Authorization"];
            var handler = new JwtSecurityTokenHandler();
            var role = handler.ReadJwtToken(tokenAsStr).Claims.First(claim=>claim.Type==ClaimTypes.Role).Value;

            if (tokenAsStr==null)
            {
                // Redirect to the login page if the token is missing
                context.Result = new RedirectToRouteResult(new { controller = "Auth", action = "SignIn" });
                return;
            }

            if (!allowedRoles.Any(r=>r==role))
            // Perform token validation and decoding (using a JWT library like System.IdentityModel.Tokens.Jwt)
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Retrieve the roles claim from the token
            var rolesClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(jsonToken?.Claims, "jwt"));

            // Check if the user has any of the allowed roles
            //if (!allowedRoles.Any(r=>r==role))
            if (!IsAuthorized(rolesClaim))
            {
                context.Result = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
            }
        }

        private bool IsAuthorized(string rolesClaim)
        {
            // Check if the user has any of the allowed roles based on the roles claim
            return !string.IsNullOrEmpty(rolesClaim) && allowedRoles.Any(role => role == rolesClaim);
        }
    }
}
