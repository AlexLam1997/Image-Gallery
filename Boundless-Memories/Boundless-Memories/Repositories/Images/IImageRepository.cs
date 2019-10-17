using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boundless_Memories.Common.Database.Entities;

namespace Boundless_Memories.Repositories.ImageRepository
{
	public interface IImageRepository
	{
		Task<List<Images>> UploadImages(List<Images> images);
		Task<Images> GetImage(Guid guid);
		Task<Images> GetImageById(int id);
	}
}
