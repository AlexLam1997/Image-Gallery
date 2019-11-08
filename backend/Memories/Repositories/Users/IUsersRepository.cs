using System.Collections.Generic;
using System.Threading.Tasks;
using Boundless_Memories.Common.Database.Entities;
using Memories.Models;

namespace Memories.Repositories.User
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetUsersAsync();
        Task<Users> GetUserAsync(int id);
        Task<Users> GetUserAsync(string username);
        Task<bool> CreateUserAsync(CreateUserRequest request, string salt);
        Task<bool> DeleteUserAsync(DeleteUserRequest request);
        Task<bool> UpdateUserAsync(int id, UpdateUserRequest request);
    }
}
