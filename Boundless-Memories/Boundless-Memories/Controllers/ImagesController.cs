using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Memories.Services.Base;
using Memories.Services.ImageManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Memories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IImageManagement m_ImageManagement;

        public ImagesController(IImageManagement ImageHandler)
        {
            m_ImageManagement = ImageHandler;
        }

		/// <summary>
		/// Uploads the list of image files
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImageAsync(List<IFormFile> files)
        {
			var uploadSuccess = await m_ImageManagement.UploadImageAsync(files);
            return ProcessResponse(uploadSuccess);
        }

		/// <summary>
		/// Used to get the list of images the user has access to. 
		/// This includes both the url of the image and additional image information.
		/// The image url can be used to seperately retrieve the actual image file.
		/// </summary>
		/// <returns></returns>
		[HttpGet("get")]
		public async Task<IActionResult> GetImageAsync()
		{
			var userImages = await m_ImageManagement.GetImagesAsync();
			return ProcessResponse(userImages);
		}

		/// <summary>
		/// Gets the image file associated with the Guid if the currently logged in user has access to it
		/// </summary>
		/// <param name="imageGuid"></param>
		/// <returns></returns>
		[HttpGet("get/{imageGuid:Guid}")]
		public async Task<IActionResult> GetImage(Guid imageGuid)
		{
			var imageByteArray = await m_ImageManagement.GetImageBytesByGuidAsync(imageGuid);
			return File(imageByteArray, "image/png");
		}
	}
}
