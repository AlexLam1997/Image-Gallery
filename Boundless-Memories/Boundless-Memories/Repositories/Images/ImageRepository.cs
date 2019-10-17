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

		public async Task<bool> CreateImagesAsync(List<Images> images, int userId)
		{
			var user = await m_MemoriesContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
			await m_MemoriesContext.Images.AddRangeAsync(images);
			return await m_MemoriesContext.SaveChangesAsync() >= 1;
		}

	}
}
