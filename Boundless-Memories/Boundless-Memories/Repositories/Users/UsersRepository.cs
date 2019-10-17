using Boundless_Memories.Common.Database.Entities;
using Memories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memories.Repositories.User
{
	public class UsersRepository : IUsersRepository
    {
        private readonly MemoriesContext m_MemoriesContext;

        public UsersRepository(MemoriesContext FoodometerContext)
        {
            m_MemoriesContext = FoodometerContext;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            var list = await m_MemoriesContext.Users.ToListAsync();

            return list;
        }

        public async Task<bool> CreateUserAsync(CreateUserRequest request, string salt)
        {
            var dbUser = new Users
            {
                Username = request.Username,
                Pw = request.Password,
                Salt = salt
            };

            m_MemoriesContext.Users.Add(dbUser);

            return await m_MemoriesContext.SaveChangesAsync() == 1;
        }

        public async Task<Users> GetUserAsync(string username)
        {
            var dbUser = await m_MemoriesContext.Users.SingleOrDefaultAsync(x => x.Username == username);

            return dbUser;
        }

        public async Task<Users> GetUserAsync(int id)
        {
            var dbUser = await m_MemoriesContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            return dbUser;
        }

        public async Task<bool> DeleteUserAsync(DeleteUserRequest request)
        {
            var dbUser = await m_MemoriesContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            m_MemoriesContext.Users.Remove(dbUser);

            return await m_MemoriesContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            //temporary update feature for now, but a user should not be able to change their username or same username as another
            //update should modify other stuff in the future, not just username
            var dbUser = await m_MemoriesContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            dbUser.Username = request.Username;

            return await m_MemoriesContext.SaveChangesAsync() > 0;
        }
    }
}
