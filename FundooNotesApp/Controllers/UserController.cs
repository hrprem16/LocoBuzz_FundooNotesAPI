using System;
using Azure;
using Common_Layer.RequestModels;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundooNotesApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
		private readonly IUserManager userManager;

		public UserController(IUserManager userManager)
		{
			this.userManager = userManager;
		}
		[HttpPost]
		[Route("Reg")]
		public ActionResult Register(RegisterModel model)
		{
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
					return Ok(new ResModel<UserEntity> { Success = true, Message = "Login Succuessfully", Data = response });
				}
				else{
					return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Login Failed", Data = response });
				}
			}
			catch(Exception ex)
			{
				return BadRequest(new ResModel<UserEntity> { Success = false ,Message=ex.Message,Data=null});
			}
		}

	}

}

