using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		public readonly FundoContext context;

		public UserRepository(FundoContext context)
		{
			this.context = context;
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

		public UserEntity UserLogin(LoginModel model)
		{
			var user = context.UserTable.FirstOrDefault(a => a.UserEmail == model.UserEmail);

			if (user != null)
			{
				UserEntity userEntity = new UserEntity();
				if (BCrypt.Net.BCrypt.Verify(model.UserPassword, user.UserPassword))
				{
					userEntity.UserId = user.UserId;
					userEntity.FName = user.FName;
					userEntity.LName = user.LName;
					userEntity.UserEmail = user.UserEmail;
					return userEntity;
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

    }
}

