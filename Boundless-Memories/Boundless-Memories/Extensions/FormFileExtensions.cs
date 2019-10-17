using Boundless_Memories.Common.Database.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boundless_Memories.Extensions
{
	public static class FormFileExtensions
	{
		public static Images ToImage(this IFormFile file)
		{
			MemoryStream ms = new MemoryStream();
			file.OpenReadStream().CopyTo(ms);

			//Image image = Image.FromStream(ms);

			Images dbImage = new Images()
			{
				Guid = Guid.NewGuid(),
				Name = file.Name,
				Data = ms.ToArray(),
				//Width = image.Width,
				//Height = image.Height,
				ContentType = file.ContentType
			};

			return dbImage;
		}
	}
}
