﻿using System;
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

	}
}

