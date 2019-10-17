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
			var result = await m_ImageManagement.UploadImageToDisk(files);
            return ProcessResponse(result);
        }

		[HttpGet("get")]
		public IActionResult GetImage(Guid guid)
		{
			var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\UserImages"));

			Byte[] b = System.IO.File.ReadAllBytes(Path.Combine(path, "test.jpg"));   // You can use your own method over here.         
			return File(b, "image/jpeg");
		}

		[HttpGet("{imageId:int}")]
		public IActionResult GetImageById(int imageId)
		{
			var result = m_ImageManagement.GetImageById(imageId).Result;

			MemoryStream ms = new MemoryStream(result.Payload.Data);

			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StreamContent(ms);
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

			return ProcessResponse(result);
		}

	}
}
