using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models.Images
{
    public class UploadImageResponse
	{
        public List<int> ImageIds { get; set; }
		public List<Guid> ImageGuids { get; set; }
    }
}
