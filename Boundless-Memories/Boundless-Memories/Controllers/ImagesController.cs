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


        [HttpPost("upload")]
        public async Task<IActionResult> UploadImageAsync(List<IFormFile> files)
        {
			var result = await m_ImageManagement.UploadImageAsync(files);
            return ProcessResponse(result);
        }

		[HttpGet("get")]
		public IActionResult GetImage()
		{
			var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));

			Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(path, "test.jpg"));   // You can use your own method over here.         
			return File(b, "image/jpeg");
		}

	}
}
