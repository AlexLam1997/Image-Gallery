using Microsoft.AspNetCore.Http;

namespace Memories.Models.Images
{
	public class ImageWithFile
	{
		public Boundless_Memories.Common.Database.Entities.Images Image { get; set; }
		public IFormFile File {get; set;}
    }
}
