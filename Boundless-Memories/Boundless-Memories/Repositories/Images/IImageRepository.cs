using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boundless_Memories.Common.Database.Entities;

namespace Boundless_Memories.Repositories.ImageRepository
{
	public interface IImageRepository
	{
		Task<List<Images>> UploadImagesAsync(List<Images> images);
		Task<List<Images>> GetImagesAsync(int userId);
		Task<bool> CreateImagesAsync(List<Images> images, int userId);
	}
}
