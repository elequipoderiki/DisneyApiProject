using System.Linq;
using System.Threading.Tasks;
using DisneyApi.AppCode.Common;
using DisneyApi.AppCode.Db;
using DisneyApi.AppCode.Domain;
using DisneyApi.AppCode.Services;
using Microsoft.Extensions.Configuration;

namespace DisneyApi.AppCode.Users
{
    public interface IUserCommandService
    {
        bool UserNameExists(string userName);
        Task<int> RegisterUserAsync(RegisterData model);
    }


    public class UserCommandService : IUserCommandService
    {
        private readonly IDbContext _context;
        private readonly IEMailService _emailService;
    
        public UserCommandService(IDbContext context, IEMailService emailService, IConfiguration config)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<int> RegisterUserAsync(RegisterData model)
        {
            string encryptPass = EncriptService.EncryptPass(model.password);
            User user = new User()
            {
                email = model.email,
                password = encryptPass,
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            var response = await _emailService.SendMail(user.email, "Bienvenido a DisneyApi");
            return user.UserId;
        }

        public bool UserNameExists(string userName)
        {
            return _context.RegisteredUsers().Where(u => u.email == userName).Count() > 0;
        }

    }
}