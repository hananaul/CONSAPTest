using CONSAPTest.DatabaseSettings;
using CONSAPTest.Dto;
using CONSAPTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CONSAPTest.Services.Accounts
{
    public class AccountService
    {
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Role> _roles;
        private readonly IPasswordHasher<User> _passwordHasher;
        private IConfiguration _config;

        public AccountService(IDatabaseSetting settings, IPasswordHasher<User> passwordHasher, IConfiguration config)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _roles = database.GetCollection<Role>(settings.RolesCollectionName);
            _passwordHasher = passwordHasher;
            _config = config;
        }

        public User RegisterUser(RegisterUserDto input)
        {
            //checkRole
            var dataRole = _roles.Find(x => x.roleName == input.RoleName).FirstOrDefault();

            if (dataRole == null)
            {
                dataRole = new Role
                {
                    roleName = input.RoleName
                };

                _roles.InsertOne(dataRole);
            }

            //checkUser
            var dataUser = _user.Find(x => x.userName == input.userName).FirstOrDefault();

            if(dataUser != null)
            {
                throw new Exception("Username Already Exist");
            }

            dataUser = new User
            {
                userName = input.userName,
                email = input.email,
                phoneNumber = input.phoneNumber,
                RoleId = dataRole.Id
            };

            dataUser.password = _passwordHasher.HashPassword(dataUser, input.password);

            _user.InsertOne(dataUser);

            return dataUser;
        }

        private User GetUser(string userName)
        {
            return _user.Find(x => x.userName == userName).FirstOrDefault();
        }

        public LoginResultDto Login(LoginInputDto input)
        {
            var user = GetUser(input.userName);

            if (user == null)
            {
                throw new Exception("Invalid username");
            }

            var checkPassword = _passwordHasher.VerifyHashedPassword(user, user.password, input.password);

            if (checkPassword != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid Password");
            }

            var tokenStr = GenerateJSONWebToken(user);

            return new LoginResultDto
            {
                UserId = user.Id,
                userName = user.userName,
                email = user.email,
                phoneNumber = user.phoneNumber,
                RoleId = user.RoleId,
                token = tokenStr
            };
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.userName),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodeToken;
        }

        public List<GetListUserResultDto> GetListUser()
        {
            var user = _user.Find(x => true).ToList();

            var Role = _roles.Find(x => true).ToList();

            var result = (from a in user
                          join b in Role on a.RoleId equals b.Id
                          select new GetListUserResultDto
                          {
                              UserId = a.Id,
                              userName = a.userName,
                              email = a.email,
                              phoneNumber = a.phoneNumber,
                              RoleId = a.RoleId,
                              roleName = b.roleName
                          }).ToList();

            return result;
        }


    }
}
