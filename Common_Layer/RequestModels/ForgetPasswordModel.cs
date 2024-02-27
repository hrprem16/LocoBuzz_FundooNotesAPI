using System;
namespace Common_Layer.RequestModels
{
	public class ForgetPasswordModel
	{
		public int UserId { get; set; }

		public string UserEmail { get; set; }

		public string token { get; set; }
		
	}
}

