
using System.ComponentModel.DataAnnotations;

namespace DisneyApi.AppCode.Users
{
    public class RegisterData
    {
        [Required]
        public string email { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}