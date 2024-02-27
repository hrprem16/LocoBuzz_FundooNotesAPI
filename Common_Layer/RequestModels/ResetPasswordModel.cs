using System;
namespace Common_Layer.RequestModels
{
	public class ResetPasswordModel
	{
		public string UserEmail { get; set; }
		public string UserPassword { get; set; }
	}
}

