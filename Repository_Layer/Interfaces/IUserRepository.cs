using System;
using Repository_Layer.Entity;
using Common_Layer.RequestModels;

namespace Repository_Layer.Interfaces
{
	public interface IUserRepository
	{
		public UserEntity UserRegistration(RegisterModel model);

		//public UserEntity UserLogin(LoginModel model); -- due to token we chnage UserEntity to String

		public string UserLogin(LoginModel model);

		public ForgetPasswordModel ForgetPassword(string Email);
		public bool checker(string Email);

		public bool ResetPassword(string Email, ResetPasswordModel resetPassword);


    }
}

