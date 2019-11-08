using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Services.Errors
{
    public enum EnumManagementError
    {
        NO_ERROR = 0,

        BAD_REQUEST = 400,

        NO_PERMISSION = 403,

        UNKNOWN_USER = 450,

        NOT_AN_IMAGE = 998,

        UNKNOWN_ERROR = 999
    }
}
