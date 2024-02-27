using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		public readonly FundoContext context;
		public readonly IConfiguration config;

		public UserRepository(FundoContext context, IConfiguration config)
		{
			this.context = context;
			this.config = config;
		}

        public UserEntity UserRegistration(RegisterModel model)
		{
			if (context.UserTable.FirstOrDefault(a => a.UserEmail == model.UserEmail)==null)
			{
                //making object of UserEntity 
                UserEntity entity = new UserEntity();
                //set the value to entity that coming from User(Postman or Swagger)
                entity.FName = model.FName;
				entity.LName = model.LName;
                entity.UserEmail = model.UserEmail;
                entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.UserPassword);

                context.UserTable.Add(entity);
				context.SaveChanges();
				return entity;


            }
			else{
				throw new Exception("User Already Exists ,Enter another id for Registration");
			}
		}

		public string UserLogin(LoginModel model)
		{
			var user = context.UserTable.FirstOrDefault(a => a.UserEmail == model.UserEmail);

			if (user != null)
			{
				UserEntity userEntity = new UserEntity();
				if (BCrypt.Net.BCrypt.Verify(model.UserPassword, user.UserPassword))
				{
					//userEntity.UserId = user.UserId;
					//userEntity.FName = user.FName;
					//userEntity.LName = user.LName;
					//userEntity.UserEmail = user.UserEmail;
					//return userEntity;

					var token = GenerateToken(user.UserEmail, user.UserId);
					return token;
				}
				else
				{
					throw new Exception("Invalid Password");
				}
			}
			else
			{
				throw new Exception("User Not Found");
			}
		}

        private string GenerateToken(string Email, int UserId)
        {
            //Defining a Security Key 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

		public ForgetPasswordModel ForgetPassword(string Email)
		{
			UserEntity user = context.UserTable.FirstOrDefault(a => a.UserEmail == Email);

			ForgetPasswordModel forgetPassword = new ForgetPasswordModel();

			forgetPassword.UserEmail = user.UserEmail;
			forgetPassword.UserId = user.UserId;
			forgetPassword.token = GenerateToken(user.UserEmail,user.UserId);

			return forgetPassword;
		}

		public bool checker(string Email)
		{
			if(context.UserTable.FirstOrDefault(a => a.UserEmail == Email) != null)
			{
				return true;
			}
			return false;
		}

		public bool ResetPassword(string Email, ResetPasswordModel resetPassword)
		{
			UserEntity user = context.UserTable.FirstOrDefault(a => a.UserEmail == Email);

			if (user != null)
			{
				user.UserPassword= BCrypt.Net.BCrypt.HashPassword(resetPassword.UserPassword);
                context.SaveChanges();
				return true;

            }
			return false;
		}

    }
}

