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
using Memories.Services.Errors;

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

		/// <summary>
		/// Gets all the images the user has access to
		/// </summary>
		/// <returns></returns>
		public async Task<BaseBodyResponse<List<Images>>> GetImagesAsync()
		{
			try
			{
				var userId = m_AuthorizationContext.getCurrentUserId();

				if (userId < 0)
				{
					// User is not logged in 
					return new BaseBodyResponse<List<Images>>(new ManagementError(EnumManagementError.NO_PERMISSION));
				}

				var userImages = await m_ImageRepository.GetImagesAsync(userId);
				return new BaseBodyResponse<List<Images>>(userImages);
			}catch(Exception e)
			{
				return new BaseBodyResponse<List<Images>>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.ToString()));

			}
		}

		/// <summary>
		/// Retrieves the byte array image from the filesystem
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public async Task<byte[]> GetImageBytesByGuidAsync(Guid guid)
		{
			var userId = m_AuthorizationContext.getCurrentUserId();
			var image = await m_ImageRepository.GetImageByGuidAsync(guid, userId);
			var imageExtension = image.FileName.Split('.')[1];
			var imagePath = Path.Combine(imageBucketPath, $"{image.StorageName.ToString()}.{imageExtension}");
			Byte[] imageByteArray = File.ReadAllBytes(imagePath);
			return imageByteArray; 
		}

		/// <summary>
		/// Saves the images to the server and stores the image information to the database
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		public async Task<BaseBodyResponse<bool>> UploadImageAsync(List<IFormFile> files)
		{
			try
			{
				var userId = m_AuthorizationContext.getCurrentUserId();
				
				if(userId < 0)
				{
					// User is not logged in 
					return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.NO_PERMISSION));
				}

				// Using guids as the image file name when storing on the server 
				var fileImages = files.Select(x => new ImageWithFile
				{
					Image = new Images
					{
						FileName = x.FileName,
						StorageName = Guid.NewGuid()
					},
					File = x
				}).ToList();

				var uploads = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));

				foreach (var fileImage in fileImages)
				{
					// Save images to filesystem
					if (fileImage.File.Length > 0)
					{
						var fileExtention = fileImage.File.FileName.Split('.')[1];
						var filePath = Path.Combine(uploads, $"{fileImage.Image.StorageName.ToString()}.{fileExtention}");
						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							await fileImage.File.CopyToAsync(fileStream);
						}
					}
				}

				var images = fileImages.Select(x => x.Image).ToList();

				var response = await m_ImageRepository.CreateImagesAsync(images, userId);
				return new BaseBodyResponse<bool>(response);

			}catch(Exception e)
			{
				return new BaseBodyResponse<bool>(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.ToString()));
			}
		}

		public async Task<BaseResponse> DeleteImagesAsync(List<Guid> imageGuids)
		{
			try
			{
				var userId = m_AuthorizationContext.getCurrentUserId();

				if (!await IsUserOwner(userId, imageGuids))
				{
					return new BaseResponse(new ManagementError(EnumManagementError.NO_PERMISSION));
				}

				await m_ImageRepository.DeleteImagesAsync(imageGuids);
				return new BaseResponse();
			}catch(Exception e)
			{
				return new BaseResponse(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.ToString()));
			}
		}

		public async Task<BaseResponse> ShareImagesAsync(List<Guid> imageGuids)
		{
			try
			{
				var userId = m_AuthorizationContext.getCurrentUserId();

				if (!await IsUserOwner(userId, imageGuids))
				{
					return new BaseResponse(new ManagementError(EnumManagementError.NO_PERMISSION));
				}

				await m_ImageRepository.AddImageAssociationAsync(userId, imageGuids);
				return new BaseResponse();
			}catch(Exception e)
			{
				return new BaseResponse(new ManagementError(EnumManagementError.UNKNOWN_ERROR, e.ToString()));
			}
		}

		// Checks to see if the current user owns all the images in the list
		private async Task<bool> IsUserOwner(int userId, List<Guid> imageGuids)
		{
			var userOwnedImages = await m_ImageRepository.GetOwnedImages(userId);
			var userImageGuids = userOwnedImages.Select(x => x.StorageName);

			var unownedImages = imageGuids.Except(userImageGuids);

			// user is trying to delete images that he does not own
			if (unownedImages.Any())
			{
				return false;
			}
			return true;
		}
	}
}
