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
	[Route("note/[controller]")]
	public class NoteController:ControllerBase
	{
		public readonly INoteManager noteManager;
		


        public NoteController(INoteManager noteManager)
		{
			this.noteManager = noteManager;
		}
		[Authorize]
		[HttpPost]
		[Route("AddNote")]

		public ActionResult AddNotes(AddNoteModel addNotes)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = noteManager.NoteCreation(userId,addNotes);
				if (response != null)
				{
					return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Added SuccessFully", Data = response });
				}
				else
				{
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Added UnSuccessfull", Data = response});
                }
            }
			
			catch(Exception ex)
			{
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null});
            }
		}

		[Authorize]
		[HttpDelete]
		[Route("DeleteNote")]
		public ActionResult DeleteNote(int NoteId)
		{
			try
			{
				var response = noteManager.DeleteNote(NoteId);
				if (response != null)
				{
                    return Ok(new ResModel<bool> { Success = true, Message = "Note Deleted SuccessFully", Data =true });
                }
				else
				{
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Note not Deleted", Data = false });
                }

			}
			catch
			{
                return BadRequest(new ResModel<bool> { Success = false, Message = "Note not Deleted", Data = false });
            }
		}
		[Authorize]
		[HttpGet]
		[Route("GetAllNotes")]

		public ActionResult GetAllNotes()
		{
            var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            try
			{
				var response = noteManager.DisplayNotes(userId);
				if (response != null)
				{
                    return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Display All Notes SuccessFully", Data = response });
                }
				else
				{
                    return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "Display Notes UnSuccessFully", Data = response });
                }

			}
			catch
			{
                return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "Display Notes UnSuccessFully", Data = null });
            }
		}

		[Authorize]
		[HttpPut]
		[Route("UpdateNote")]

		public ActionResult UpdateNotes(int noteId, string newDescription, string newNoteTitle)
		{
			try
			{
				var response = noteManager.UpdateNote(noteId, newDescription, newNoteTitle);
				if (response != null)
				{
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Update SuccessFully", Data = response });
                }
				else
				{
					return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "Note Update UnSuccessfully", Data = response });

				}
            }
			
			catch
			{
				return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "Note Update UnSuccessfully", Data = null });


            }
        }

		[Authorize]
		[HttpPut]
		[Route("NoteArchive")]
		public ActionResult NoteArchive(int noteId)
		{
			try
			{
				var response = noteManager.IsArchive(noteId);
				if (response != null)
				{
                    return Ok(new ResModel<bool> { Success = true, Message = "Note Archive SuccessFully", Data = response });
                }
				else
				{
                    return BadRequest(new ResModel<bool> { Success = false, Message ="Note is not Archived", Data = response });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false});
            }
        }

        [Authorize]
        [HttpPut]
        [Route("NotePin")]
        public ActionResult NotePin(int noteId)
        {
            try
            {
                var response = noteManager.IsPin(noteId);
                if (response != null)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Note has been Pinned", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Note is not Pinned", Data = response });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("NoteTrash")]
        public ActionResult NoteTrash(int noteId)
        {
            try
            {
                var response = noteManager.IsTrash(noteId);
                if (response != null)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Note has been  Trashed", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Note is not in Trash", Data = response });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

    }
}

