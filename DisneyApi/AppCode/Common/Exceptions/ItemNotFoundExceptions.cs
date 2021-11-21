using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Common
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.NotFound;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}