using System;
using Common_Layer.RequestModels;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundooNotesApp.Controllers
{
    [ApiController]
    [Route("label/[controller]")]

    public class LabelController:ControllerBase
	{
        public readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]

        public ActionResult AddingLabel(string LabelName,int noteId)
        {

            try
            {
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = labelManager.AddLabel(LabelName, userId, noteId);
                if (response != null)
                {
                    return Ok(new ResModel<LabelEntity> { Success = true, Message = "Adding Label Successfully", Data =response});
                }
                else
                {
                    return BadRequest(new ResModel<LabelEntity> { Success = false, Message = "Label not added", Data = response });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = ex.Message, Data = null});
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public ActionResult UpdateLabel(int noteId, int labelId, string NewLabelName)
        {
            try
            {
                var response = labelManager.LabelUpdate(NewLabelName, noteId, labelId);

                if (response != null)
                {
                    return Ok(new ResModel<string> { Success = true, Message = "Label name has been Updated", Data = $"Label Changed to {NewLabelName}"});
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = true,Message="Label Update Failed",Data=null });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = true, Message = ex.Message, Data = null });
            }
        }


	}
}
           

