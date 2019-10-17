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
		Task<BaseBodyResponse<UploadImageResponse>> UploadImageAsync(List<IFormFile> files);
		Task<BaseBodyResponse<GetImageResponse>> GetImage(Guid guid);
		Task<BaseBodyResponse<GetImageResponse>> GetImageById(int id);
		Task<BaseBodyResponse<bool>> UploadImageToDisk(List<IFormFile> images);


	}
}
