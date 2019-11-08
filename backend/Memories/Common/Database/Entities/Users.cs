using System;
using System.Collections.Generic;

namespace Boundless_Memories.Common.Database.Entities
{
    public partial class Users
    {
        public Users()
        {
            ImageAssociations = new HashSet<ImageAssociations>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Pw { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<ImageAssociations> ImageAssociations { get; set; }
    }
}
