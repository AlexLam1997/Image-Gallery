﻿using Memories.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boundless_Memories.Extensions;
using Boundless_Memories.Repositories.ImageRepository;
using Memories.Services.Errors;
using Memories.Models.Images;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Memories.Services.ImageManagement
{
	public class ImageManagement : IImageManagement
    {
		private readonly IImageRepository m_ImageRepository;
		private readonly IHostingEnvironment m_HostingEnvironment;

		public ImageManagement(IImageRepository imageRepository, IHostingEnvironment hostingEnvironment)
		{
			m_ImageRepository = imageRepository;
			m_HostingEnvironment = hostingEnvironment;
		}

        public async Task<BaseBodyResponse<UploadImageResponse>> UploadImageAsync(List<IFormFile> files)
        {
			if (files.Count == 0)
			{
				return new BaseBodyResponse<UploadImageResponse>(new ManagementError(EnumManagementError.NOT_AN_IMAGE));
			}

			// TODO Check if not an image

			try
			{
				var images = files.Select(x => x.ToImage()).ToList();

				var imagesUploaded = await m_ImageRepository.UploadImages(images);

				if (imagesUploaded == null)
				{
					return new BaseBodyResponse<UploadImageResponse>(new ManagementError(EnumManagementError.UNKNOWN_ERROR));
				}

				var imageIds = imagesUploaded.Select(x => x.Id).ToList();
				var imageGuids = imagesUploaded.Select(x => (Guid) x.Guid).ToList();


				var response = new UploadImageResponse
				{
					ImageIds = imageIds,
					ImageGuids = imageGuids
				};

				return new BaseBodyResponse<UploadImageResponse>(response);

			}
			catch(Exception e)
			{
				// TODO 
				Console.Out.WriteLine(e);
				return new BaseBodyResponse<UploadImageResponse>(new ManagementError(EnumManagementError.UNKNOWN_ERROR));
			}
		}

		public async Task<BaseBodyResponse<GetImageResponse>> GetImage(Guid guid)
		{
			// error check
			var image = await m_ImageRepository.GetImage(guid);
			var response = new GetImageResponse
			{
				ContentType = image.ContentType,
				Data = image.Data
			};
			return new BaseBodyResponse<GetImageResponse>(response);
		}

		public async Task<BaseBodyResponse<GetImageResponse>> GetImageById(int id)
		{
			// error check
			var image = await m_ImageRepository.GetImageById(id);
			var response = new GetImageResponse
			{
				ContentType = image.ContentType,
				Data = image.Data
			};
			return new BaseBodyResponse<GetImageResponse>(response);
		}

		public async Task<BaseBodyResponse<bool>> UploadImageToDisk(List<IFormFile> images)
		{
			var uploads = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));
			foreach (var image in images)
			{
				if (image.Length > 0)
				{
					var filePath = Path.Combine(uploads, image.FileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await image.CopyToAsync(fileStream);
					}
				}
			}

			return new BaseBodyResponse<bool>(true);
		}
	}
}