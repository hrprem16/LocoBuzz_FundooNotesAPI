﻿using System;
using Common_Layer.RequestModels;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class UserManager:IUserManager
	{
		private readonly IUserRepository repository;

		public UserManager(IUserRepository repository)
		{
			this.repository = repository;
		}
        public UserEntity UserRegistration(RegisterModel model)
		{
			return repository.UserRegistration(model);

		}

		public string UserLogin(LoginModel model)
		{
			return repository.UserLogin(model);
		}
        public ForgetPasswordModel ForgetPassword(string Email)
		{
			return repository.ForgetPassword(Email);
;		}

        public bool checker(string Email)
		{
			return repository.checker(Email);
		}

        public bool ResetPassword(string Email, ResetPasswordModel resetPassword)
		{
			return repository.ResetPassword(Email, resetPassword);
		}

    }
}

