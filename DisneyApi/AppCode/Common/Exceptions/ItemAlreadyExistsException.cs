using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Common
{
    public class ItemAlreadyExistsException : Exception
    {
         public ItemAlreadyExistsException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.Conflict;
        }
        public HttpStatusCode StatusCode { get; set; }

    }

}