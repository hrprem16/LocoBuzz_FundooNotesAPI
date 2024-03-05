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
	[Route("[controller]")]
	public class CollabController : ControllerBase
	{
		public readonly ICollabManager collabManager;

		public CollabController(ICollabManager collabManager)
		{
			this.collabManager = collabManager;
		}

		[Authorize]
		[HttpPost]
		[Route("AddCollaborator")]

		public ActionResult AddCollab(string collabEmailId, int noteId)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = collabManager.AddCollab(collabEmailId, userId, noteId);
				if (response != null)
				{
					return Ok(new ResModel<CollaboratorEntity> { Success = true, Message = "Collaborator added Sucessfully", Data = response });
				}
				else
				{
					return BadRequest(new ResModel<CollaboratorEntity> { Success = true, Message = "Collaborator added is failed", Data = response });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new ResModel<CollaboratorEntity> { Success = true, Message = ex.Message, Data = null });
			}

		}

		[Authorize]
		[HttpPut]
		[Route("UpdateNoteByCollaborator")]

		public ActionResult UpdateCollab(UpdateCollabModel model, int noteId)
		{
			try
			{
				string collabEmail = User.FindFirst("Email").Value;
				var response = collabManager.UpdateCollab(model, collabEmail, noteId);
				if (response != null)
				{
					return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note title and description update by Collaborator", Data = response });
				}
				else
				{
					return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Updated notes details by collaborstor is failed", Data = response });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
			}
		}

		[Authorize]
		[HttpDelete]
		[Route("Remove Collaborator")]
		public ActionResult RemoveCollab(int collabId)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = collabManager.RemoveCollab(userId, collabId);
				if (response != null)
				{
					return Ok(new ResModel<string> { Success = true, Message = $"{response} has been removed", Data = response });
				}
				else
				{
                    return BadRequest(new ResModel<string> { Success = false, Message = $"{response} hasn't removed", Data = response });
                }
			}
			catch(Exception ex)
			{
                return Ok(new ResModel<string> { Success = false, Message =ex.Message, Data =null});
            }
		}

	}
}

