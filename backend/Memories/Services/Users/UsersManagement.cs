using Memories.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Memories.Repositories.User;
using Memories.Services.Errors;
using Memories.Models;
using System.Security.Cryptography;
using System.Text;
using Memories.Common.Security;
using Microsoft.AspNetCore.Http;
using Boundless_Memories.Common.Database.Entities;

namespace Memories.Services.UserManagement
{
	public class UsersManagement : IUsersManagement
    {
        private readonly IUsersRepository m_UsersRepository;
        private readonly IAuthorizationContext m_AuthorizationContext;

        public UsersManagement(IUsersRepository UsersRepository, IAuthorizationContext AuthorizationContext, IHttpContextAccessor httpContextAccessor)
        {
            m_UsersRepository = UsersRepository;
            m_AuthorizationContext = AuthorizationContext;
        }

        public async Task<BaseBodyResponse<List<Users>>> QueryUsersAsync()
        {
            try
            {
                var users = await m_UsersRepository.GetUsersAsync();
                return new BaseBodyResponse<List<Users>>(users);
            }
            catch (Exception e)
            {
                return new BaseBodyResponse<List<Users>>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.Message));
            }

        }

        public async Task<BaseBodyResponse<bool>> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST));
                }

                var user = await m_UsersRepository.GetUserAsync(request.Username);

                if (user != null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST, "Username taken."));
                }

                var salt = Guid.NewGuid().ToString("N");

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(request.Password + salt);

                SHA256Managed hash = new SHA256Managed();

                byte[] tHashBytes = hash.ComputeHash(plainTextBytes);

                var hashPassword = Convert.ToBase64String(tHashBytes);

                var dbUser = new CreateUserRequest { Username = request.Username, Password = hashPassword };

                var result = await m_UsersRepository.CreateUserAsync(dbUser, salt);

                return new BaseBodyResponse<bool>(result);
            }
            catch (Exception e)
            {
                return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.Message));
            }
        }

        public async Task<BaseBodyResponse<bool>> DeleteUserAsync(DeleteUserRequest request)
        {
            try
            {
                if (request == null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST));
                }

                var user = await m_UsersRepository.GetUserAsync(request.Username);

                if (user == null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST, "User does not exist"));
                }

                var result = await m_UsersRepository.DeleteUserAsync(request);

                return new BaseBodyResponse<bool>(result);
            }
            catch (Exception e)
            {
                return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.Message));
            }
        }

        public async Task<BaseBodyResponse<bool>> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            try
            {

                if (request == null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST));
                }
                var user = await m_UsersRepository.GetUserAsync(id);

                if (!m_AuthorizationContext.RequireUserId(id))
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.NO_PERMISSION));
                }

                if (user == null)
                {
                    return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.BAD_REQUEST, "User does not exist"));
                }

                var result = await m_UsersRepository.UpdateUserAsync(id, request);

                return new BaseBodyResponse<bool>(result);

            }
            catch (Exception e)
            {
                return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.Message));

            }
        }
    }
}
