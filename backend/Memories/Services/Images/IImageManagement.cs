﻿using Boundless_Memories.Common.Database.Entities;
using Memories.Models.Images;
using Memories.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Services.ImageManagement
{
    public interface IImageManagement
    {
		Task<BaseBodyResponse<List<Images>>> GetImagesAsync();
		Task<BaseBodyResponse<bool>> UploadImageAsync(List<IFormFile> images);
		Task<Byte[]> GetImageBytesByGuidAsync(Guid guid);
		Task<BaseResponse> ShareImagesAsync(List<Guid> imageGuids);
		Task<BaseResponse> DeleteImagesAsync(List<Guid> imageGuids);
	}
}
