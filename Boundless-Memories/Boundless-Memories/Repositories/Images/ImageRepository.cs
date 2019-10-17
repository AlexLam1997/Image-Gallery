using Boundless_Memories.Common.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boundless_Memories.Repositories.ImageRepository
{
	public class ImageRepository : IImageRepository
	{
		private readonly MemoriesContext m_MemoriesContext;

		public ImageRepository(MemoriesContext FoodometerContext)
		{
			m_MemoriesContext = FoodometerContext;
		}

		/// <summary>
		/// Saves the list of images to the database
		/// </summary>
		/// <param name="images"></param>
		/// <returns></returns>
		public async Task<List<Images>> UploadImagesAsync(List<Images> images)
		{
			await m_MemoriesContext.Images.AddRangeAsync(images);
			try
			{
				await m_MemoriesContext.SaveChangesAsync();
				return images;
			}catch(Exception e)
			{
				//TODO
				Console.WriteLine(e);
				return null;
			}
		}

		/// <summary>
		/// Get all user images
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<List<Images>> GetImagesAsync(int userId)
		{
			var images = await m_MemoriesContext.ImageAssociations.AsNoTracking()
				.Include(x => x.Image)
				.Include(x => x.User)
				.Where(x => x.UserId == userId)
				.Select(x => x.Image)
				.ToListAsync();

			return images;
		}

		/// <summary>
		/// Retrieves image info if user has access to is
		/// </summary>
		/// <param name="guid"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Images> GetImageByGuidAsync(Guid guid, int userId)
		{
			var images = (await m_MemoriesContext.ImageAssociations.AsNoTracking()
				.Include(x => x.Image)
				.Include(x => x.User)
				.Where(x => x.UserId == userId)
				.SingleOrDefaultAsync(x => x.Image.StorageName.Equals(guid)))
				.Image;

			return images;
		}

		/// <summary>
		/// Creates the list of images in the database and the corresponding associations to the specified user
		/// </summary>
		/// <param name="images"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<bool> CreateImagesAsync(List<Images> images, int userId)
		{
			var user = await m_MemoriesContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

			//Create links between user and the images he has added 
			var userImageLinks = images.Select(x => new ImageAssociations
			{
				User = user, 
				Image = x
			});

			await m_MemoriesContext.Images.AddRangeAsync(images);
			await m_MemoriesContext.ImageAssociations.AddRangeAsync(userImageLinks);

			return await m_MemoriesContext.SaveChangesAsync() >= 1;
		}
	}
}
