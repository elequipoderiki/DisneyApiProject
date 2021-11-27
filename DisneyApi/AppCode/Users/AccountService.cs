using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using DisneyApi.AppCode.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DisneyApi.AppCode.Domain;
using DisneyApi.AppCode.Common;

namespace DisneyApi.AppCode.Users
{
    public interface IAccountService
    {
         bool ValidateUser(string email, string password);
        string GetToken();
    }

    public class AccountService : IAccountService
    {
        private readonly IDbContext _context;
        private User _user;
        private IConfiguration _config;

        public AccountService(IDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool ValidateUser(string email, string password)
        {
            string encryptPass = EncriptService.EncryptPass(password);
            _user = _context.RegisteredUsers().Where(u => u.email == email && u.password == encryptPass).FirstOrDefault();
            if (_user == null)
                    return false;                
            return true;
        }        

        public string GetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}