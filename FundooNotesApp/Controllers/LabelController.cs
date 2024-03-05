using System;
using Common_Layer.RequestModels;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using Serilog;

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
            //Log.Information("AddingLabel Starts");
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

        [Authorize]
        [HttpGet]
        [Route("GetAllLabels")]
        public ActionResult GetAllLabel()
        {
            var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            try
            {

                var response = labelManager.GetAllLabels(userId);
                if (response != null)
                {
                    return Ok(new ResModel<HashSet<string>> { Success = true, Message = "Display all Label Successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<HashSet<string>> { Success = true,Message="Display all label failed",Data=response });
                }

            }
            catch
            {
                return BadRequest(new ResModel<HashSet<string>> { Success = true, Message = "Display all label failed", Data = null });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteLabel")]
        public ActionResult DeleteLabel(int labelId)
        {
            var userId = Convert.ToInt32(User.FindFirst("UserId").Value);

            try
            {
                var response = labelManager.DeleteLabel(userId, labelId);
                if (response != null)
                {
                    return Ok(new ResModel<bool> { Success = true ,Message="Delete Label Successfully",Data=true});
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Delete Label Failed!", Data = false });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
        
	}
}
           

