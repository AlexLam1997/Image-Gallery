using Memories.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Services.Errors
{
    public class ManagementError : ErrorBase
    {
        public ManagementError(EnumManagementError error) : base((int)error)
        {
            ErrorMessage = error.ToString();
        }

        public ManagementError(EnumManagementError error, string errorMessage) : base((int)error)
        {
            ErrorMessage = errorMessage;
        }

    }
}
