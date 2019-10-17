using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Memories.Common.Security
{
    public class AuthorizationContext : IAuthorizationContext
    {
        private readonly IPrincipal m_principal;
        private readonly IHttpContextAccessor m_HttpContextAccessor;


        public AuthorizationContext(IPrincipal principal, IHttpContextAccessor HttpContextAccessor)
        {
            m_principal = principal;
            m_HttpContextAccessor = HttpContextAccessor;
        }

        public ClaimsPrincipal User
        {
            get
            {
                return m_principal as ClaimsPrincipal;
            }
        }

        public int getCurrentUserId()
        {
            string stream = m_HttpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(stream))
            {
                return -1;  //Not a signed in user
            }
            var jwt = stream.Split(' ')[1];
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(jwt);
            
            var userId = token.Claims.First(claim => claim.Type == "UserId").Value;
            return Int32.Parse(userId);
        }

        public int UserId => GetClaimIntValue("UserId");

        public string GetClaimRawValue(string claimsType)
        {
            return (User.Claims.FirstOrDefault(f => f.Type.ToLower() == claimsType.ToLower())?.Value) ?? "";
        }

        private int GetClaimIntValue(string claimsType)
        {
            if (!string.IsNullOrEmpty(GetClaimRawValue(claimsType)))
            {
                if (int.TryParse(GetClaimRawValue(claimsType), out int result))
                {
                    return result;
                }
            }
            return 0;
        }
        public bool RequireUserId(int userId)
        {
            return userId == getCurrentUserId();
        }
    }
}
