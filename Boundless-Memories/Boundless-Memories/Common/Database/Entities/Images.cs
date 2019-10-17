using System;
using System.Collections.Generic;

namespace Boundless_Memories.Common.Database.Entities
{
    public partial class Images
    {
        public int Id { get; set; }
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentType { get; set; }
    }
}
