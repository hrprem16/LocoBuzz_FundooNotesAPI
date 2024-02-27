using System;
using Common_Layer.RequestModels;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
    public interface IUserManager
    {
        public UserEntity UserRegistration(RegisterModel model);

        public string UserLogin(LoginModel model);

        public ForgetPasswordModel ForgetPassword(string Email);

        public bool checker(string Email);

        public bool ResetPassword(string Email, ResetPasswordModel resetPassword);
    }
}

