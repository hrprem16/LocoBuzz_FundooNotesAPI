using System;
using Azure;
using Common_Layer.RequestModels;
using Common_Layer.ResponseModel;
using Common_Layer.Utility;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using Serilog;

namespace FundooNotesApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
		private readonly IUserManager userManager;
		private readonly IBus bus;

		public UserController(IUserManager userManager,IBus bus)
		{
			this.userManager = userManager;
			this.bus = bus;
		}
		[HttpPost]
		[Route("Reg")]
		public ActionResult Register(RegisterModel model)
		{
			//Log.Information("Regiter User");
			try
			{
				var response = userManager.UserRegistration(model);

				if (response != null)
				{
					return Ok(new ResModel<UserEntity> { Success = true, Message = "Registered Successfully", Data = response });
				}
				else
				{
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Registration Failed", Data = response });
                }
			}
            catch (Exception ex)
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });
            }
		}

		[HttpPost]
		[Route("Login")]
		public ActionResult Login(LoginModel model)
		{
			try
			{
				var response = userManager.UserLogin(model);
				if (response != null)
				{
					return Ok(new ResModel<string> { Success = true, Message = "Login Succuessfully", Data = response });
				}
				else{
					return BadRequest(new ResModel<string> { Success = false, Message = "Login Failed", Data = response });
				}
			}
			catch(Exception ex)
			{
				return BadRequest(new ResModel<string> { Success = false ,Message=ex.Message,Data=null});
			}
		}

		[HttpPost]
		[Route("Forget Password")]
        public async Task<ActionResult> ForgetPassword(string Email)
        {
            try
            {
                if (userManager.checker(Email))
                {
                    Send send = new Send();
                    ForgetPasswordModel model = userManager.ForgetPassword(Email);
                    string str = send.SendMail(model.UserEmail, model.token);
                    Uri uri = new Uri("rabbitmq://localhost/FundooNotesEmailQueue");
                    var endpoint = await bus.GetSendEndpoint(uri);
                    return Ok(new ResModel<string> { Success = true, Message = "Forget password successful", Data = model.token });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = true, Message = ex.Message, Data = null });
            }

        }
		[Authorize]
		[HttpPost]
		[Route("ResetPassword")]

		public ActionResult ResetPassword(ResetPasswordModel resetPassword)
		{
			try
			{
				string Email = User.FindFirst("Email").Value;
                if (userManager.ResetPassword(Email,resetPassword))
                {

                    return Ok(new ResModel<bool> { Success = true, Message = "Password Reset successfully", Data = true });


                }
                else
                {
					return BadRequest(new ResModel<bool> { Success = false, Message = "Failed to reset", Data = false });

                }
            }

			catch(Exception ex)
			{
                return BadRequest(new ResModel<bool> { Success = true, Message = ex.Message, Data = false });

            }
		}


    }

}
		



