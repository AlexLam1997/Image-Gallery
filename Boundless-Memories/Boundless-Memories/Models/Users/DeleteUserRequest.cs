using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class DeleteUserRequest
    {
        public string Username { get; set; }
        //TODO: change this when we have login implemented; remove request for JWT check
    }
}
