using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Memories.Common.Security
{
    public interface IAuthorizationContext
    {
        ClaimsPrincipal User { get; }
        string GetClaimRawValue(string claimsType);

        bool RequireUserId(int userId);

        int UserId { get; }

        int getCurrentUserId(); 
    }
}
