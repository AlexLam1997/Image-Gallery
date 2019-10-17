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

		public async Task<List<Images>> UploadImages(List<Images> images)
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

		public async Task<Images> GetImage(Guid guid)
		{
			Images image = await m_MemoriesContext.Images.SingleOrDefaultAsync(m => m.Guid == guid);

			return image;
		}

		public async Task<Images> GetImageById(int id)
		{
			Images image = await m_MemoriesContext.Images.SingleOrDefaultAsync(m => m.Id == id);

			return image;
		}
	}
}
