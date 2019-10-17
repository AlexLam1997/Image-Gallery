using System.Collections.Generic;
using System.Threading.Tasks;
using Memories.Models.Authentication;
using Memories.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationManagement m_AuthenticationManagement;

        public AuthenticationController(IAuthenticationManagement AuthenticationManagement)
        {
            m_AuthenticationManagement = AuthenticationManagement;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await m_AuthenticationManagement.LoginAsync(request);
            return ProcessResponse(result);
        }
    }
}
