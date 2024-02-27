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
                entity.UserPassword = model.UserPassword;

				context.UserTable.Add(entity);
				context.SaveChanges();
				return entity;


            }
			else{
				throw new Exception("User Already Exists ,Enter another id for Registration");
			}
		}

    }
}

