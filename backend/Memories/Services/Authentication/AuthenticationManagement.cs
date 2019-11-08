using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Memories.Models.Authentication;
using Memories.Repositories.User;
using Memories.Services.Base;
using Memories.Services.Errors;
using Microsoft.IdentityModel.Tokens;

namespace Memories.Services.Authentication
{
    public class AuthenticationManagement : IAuthenticationManagement
    {
        private readonly IUsersRepository m_UsersRepository;

        public AuthenticationManagement(IUsersRepository UsersRepository)
        {
            m_UsersRepository = UsersRepository;
        }
        public async Task<BaseBodyResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new BaseBodyResponse<LoginResponse>(new ManagementError(EnumManagementError.BAD_REQUEST));
                }

                var user = await m_UsersRepository.GetUserAsync(request.Username);

                if (user == null)
                {
                    return new BaseBodyResponse<LoginResponse>(new ManagementError(EnumManagementError.UNKNOWN_USER));
                }
                
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(request.Password + user.Salt);

                SHA256Managed hash = new SHA256Managed();

                byte[] tHashBytes = hash.ComputeHash(plainTextBytes);

                var rehashedPassword = Convert.ToBase64String(tHashBytes); 

                if (rehashedPassword == user.Pw)
                {
                    var expirationTime = DateTime.UtcNow.AddMinutes(20);

                    var claims = new[] {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Username", user.Username)
                    };
                    
                    var key = new SymmetricSecurityKey(Convert.FromBase64String("Zm9vZG9tZXRlcjIwMTgyMDE5"));   //TODO: store these in a config file
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //generate token
                    var token = new JwtSecurityToken(
                        issuer: "Memories",
                        audience: "Memories",
                        claims: claims,
                        expires: expirationTime,
                        signingCredentials: signingCredentials
                     );

                    var handler = new JwtSecurityTokenHandler();

                    return new BaseBodyResponse<LoginResponse>(new LoginResponse { AccessToken = handler.WriteToken(token) });

                }

                return new BaseBodyResponse<LoginResponse>(new ManagementError(EnumManagementError.UNKNOWN_USER));
            }
            catch (Exception e)
            {
                return new BaseBodyResponse<LoginResponse>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.Message));
            }
        }
    }
}
