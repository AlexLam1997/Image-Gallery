using Memories.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boundless_Memories.Repositories.ImageRepository;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Memories.Common.Security;
using Boundless_Memories.Common.Database.Entities;
using System.Linq;
using Memories.Models.Images;

namespace Memories.Services.ImageManagement
{
	public class ImageManagement : IImageManagement
	{
		private readonly IImageRepository m_ImageRepository;
		private readonly IAuthorizationContext m_AuthorizationContext;

		private readonly string imageBucketPath;

		public ImageManagement(IImageRepository imageRepository, IAuthorizationContext authorizationContext)
		{
			m_ImageRepository = imageRepository;
			m_AuthorizationContext = authorizationContext;
			imageBucketPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));
		}

		public async Task<BaseBodyResponse<List<Images>>> GetImagesAsync()
		{
			var userId = m_AuthorizationContext.getCurrentUserId();

			// error check
			var userImages = await m_ImageRepository.GetImagesAsync(userId);
			return new BaseBodyResponse<List<Images>>(userImages);
		}

		public async Task<byte[]> GetImageBytesByGuidAsync(Guid guid)
		{
			var userId = m_AuthorizationContext.getCurrentUserId();
			var image = await m_ImageRepository.GetImageByGuidAsync(guid, userId);

			var imagePath = Path.Combine(imageBucketPath, image.StorageName.ToString());
			Byte[] imageByteArray = File.ReadAllBytes(imagePath);
			return imageByteArray; 
		}

		public async Task<BaseBodyResponse<bool>> UploadImageAsync(List<IFormFile> files)
		{
			// Using guids as the image file name when storing on the server 
			var fileImages = files.Select(x => new ImageWithFile
			{
				Image = new Images
				{
					FileName = x.FileName,
					StorageName = new Guid()
				},
				File = x
			}).ToList();

			var uploads = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));

			foreach (var fileImage in fileImages)
			{
				// Save images to filesystem
				if (fileImage.File.Length > 0)
				{
					var filePath = Path.Combine(uploads, fileImage.Image.StorageName.ToString());
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await fileImage.File.CopyToAsync(fileStream);
					}
				}
			}

			var images = fileImages.Select(x => x.Image).ToList();
			var userId = m_AuthorizationContext.getCurrentUserId();

			var response = await m_ImageRepository.CreateImagesAsync(images, userId);

			return new BaseBodyResponse<bool>(response);
		}
	}
}
