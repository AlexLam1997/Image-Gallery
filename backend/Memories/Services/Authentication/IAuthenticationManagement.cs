using Memories.Models.Authentication;
using Memories.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Services.Authentication
{
    public interface IAuthenticationManagement
    {
        Task<BaseBodyResponse<LoginResponse>> LoginAsync(LoginRequest request);
    }
}
