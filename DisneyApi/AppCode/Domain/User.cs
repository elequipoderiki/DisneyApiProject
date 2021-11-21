using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DisneyApi.AppCode.Domain
{
    public class User
    {
      public int UserId {get; set; }  

      public string email { get; set; }
 
      public string password { get; set; }
    }
}