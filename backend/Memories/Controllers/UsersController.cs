using System.Threading.Tasks;
using Memories.Models;
using Memories.Services.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace Memories.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : BaseController
	{
		private readonly IUsersManagement m_UsersManagement;

		public UsersController(IUsersManagement UsersManagement)
		{
			m_UsersManagement = UsersManagement;
		}

		/// <summary>
		/// Creates a new user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
		{
			var result = await m_UsersManagement.CreateUserAsync(request);
			return ProcessResponse(result);
		}

		/// <summary>
		/// Deletes an old user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteUser([FromBody]DeleteUserRequest request)
		{
			var result = await m_UsersManagement.DeleteUserAsync(request);
			return ProcessResponse(result);
		}

		/// <summary>
		/// Updates a user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody]UpdateUserRequest request)
		{
			var result = await m_UsersManagement.UpdateUserAsync(id, request);
			return ProcessResponse(result);
		}
	}
}