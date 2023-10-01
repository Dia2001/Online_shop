using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.ExceptionHandle
{
    public enum ExceptionType
    {
        DEFAULT = 0,
        NOT_FOUND = 1,
        NO_PERMISSION = 2,
        BAD_REQUEST = 3,
        UNAUTHORIZED = 4
    }
}
