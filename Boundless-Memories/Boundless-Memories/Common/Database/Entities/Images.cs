using System;
using System.Collections.Generic;

namespace Boundless_Memories.Common.Database.Entities
{
    public partial class Images
    {
        public Images()
        {
            ImageAssociations = new HashSet<ImageAssociations>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public Guid StorageName { get; set; }
        public DateTime Uploaded { get; set; }

        public virtual ICollection<ImageAssociations> ImageAssociations { get; set; }
    }
}
