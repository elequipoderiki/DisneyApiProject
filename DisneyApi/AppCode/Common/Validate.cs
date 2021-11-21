using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;

namespace DisneyApi.AppCode.Common
{
    public static class Validate
    {
        public static void ValidateNotNull<T>(T val, string message)
        {
            if(val == null)
                throw new ItemNotFoundException(message);
        }

        public static void ValidateIsFalse(bool val, string message)
        {
            if(val)
                throw new ItemAlreadyExistsException(message);
        }

    }
}