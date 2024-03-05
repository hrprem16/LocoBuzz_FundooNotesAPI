using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class CollabRepository:ICollabRepository
    {
		private readonly FundoContext context;

		public CollabRepository(FundoContext context)
		{
			this.context = context;
		}

		public CollaboratorEntity AddCollab(string collabEmail,int userId, int noteId)
		{
			var findnote = context.NoteSTable.FirstOrDefault(a => a.NoteId == noteId);

			if (findnote != null)
			{
				var findcollab = context.CollabTable.FirstOrDefault(e => e.CollabEmailId == collabEmail && e.NoteId==noteId);
				if (findcollab == null)
				{
					CollaboratorEntity collaboratorEntity = new CollaboratorEntity();

					collaboratorEntity.CollabEmailId = collabEmail;
					collaboratorEntity.UserId = userId;
					collaboratorEntity.NoteId = noteId;

					context.CollabTable.Add(collaboratorEntity);
					context.SaveChanges();
					return collaboratorEntity;

				}
				else
				{
					throw new Exception("Collaborator unable to add!");
				}
			}
			else
			{
				throw new Exception("Note doesn't Exist");
			}
		}

       
        public NoteEntity UpdateCollab( UpdateCollabModel model, string collabEmail,int noteId)
        {
			var findnote = context.CollabTable.FirstOrDefault(a => a.NoteId==noteId);
			if (findnote != null)
			{
				var findcollab = findnote.CollabEmailId == collabEmail;
				if (findcollab)
				{
					var updateNoteDetails = context.NoteSTable.FirstOrDefault(e => e.NoteId ==noteId);
					if (updateNoteDetails != null)
					{
						updateNoteDetails.NoteTitle = model.newTitle;
						updateNoteDetails.NoteDescription = model.newDescription;

						context.NoteSTable.Update(updateNoteDetails);
						context.SaveChanges();
						return updateNoteDetails;
					}
					else
					{
						throw new Exception("Note doesn't found!");
					}
				}
				else
				{
					throw new Exception("Collab not found!");
				}
			}
			else
			{
				throw new Exception("Note not found!");
			}
        }

		public string RemoveCollab(int userId,int collabId)
		{
			var findUser = context.CollabTable.FirstOrDefault(a => a.UserId == userId);

			if (findUser != null)
			{
				if (findUser.CollabId == collabId)
				{
					context.CollabTable.Remove(findUser);
					context.SaveChanges();
					return findUser.CollabEmailId;
				}
				else
				{
					return "Collab User not found";
				}

			}
			else
			{
				throw new Exception("Collaborator not found!");
			}

		}
    }
}

