using Boundless_Memories.Common.Database.Entities;
using Memories.Models;
using Memories.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memories.Services.UserManagement
{
	public interface IUsersManagement
    {
        Task<BaseBodyResponse<List<Users>>> QueryUsersAsync();
        Task<BaseBodyResponse<bool>> CreateUserAsync(CreateUserRequest request);
        Task<BaseBodyResponse<bool>> DeleteUserAsync(DeleteUserRequest request);
        Task<BaseBodyResponse<bool>> UpdateUserAsync(int id, UpdateUserRequest request);
    }
}
