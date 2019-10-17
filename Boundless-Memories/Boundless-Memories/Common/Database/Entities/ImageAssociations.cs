using System;
using System.Collections.Generic;

namespace Boundless_Memories.Common.Database.Entities
{
    public partial class ImageAssociations
    {
        public int UserId { get; set; }
        public int ImageId { get; set; }

        public virtual Images Image { get; set; }
        public virtual Users User { get; set; }
    }
}
