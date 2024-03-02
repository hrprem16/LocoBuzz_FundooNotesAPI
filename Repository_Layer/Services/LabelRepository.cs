using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class LabelRepository : ILabelRepository
	{
		private readonly FundoContext context;

		public LabelRepository(FundoContext context)
		{
			this.context = context;
		}

		public LabelEntity AddLabel(string labelName, int userId, int noteId)
		{
			try
			{
				var findNote = context.NoteSTable.FirstOrDefault(a => a.NoteId == noteId);

				if (findNote != null)
				{
					LabelEntity labelEntity = new LabelEntity();
					labelEntity.LabelName = labelName;
					labelEntity.NoteId = noteId;
					labelEntity.UserId = userId;


					context.LabelTable.Add(labelEntity);
					context.SaveChanges();
					return labelEntity;
				}
				else
				{
					throw new Exception("Note doesn't exist!");
				}


			}
			catch
			{
				throw;
			}

		}

		public LabelEntity LabelUpdate(string newLabelName, int noteId, int labelId)
		{
			var findNote = context.LabelTable.FirstOrDefault(e => e.LabelId == labelId);
			if (findNote != null)
			{

				findNote.NoteId = noteId;
				findNote.LabelId = labelId;
				findNote.LabelName = newLabelName;

				context.SaveChanges();
				return findNote;


			}
			else
			{
				throw new Exception("Label is not Exist");
			}

		}
	}
}

				   

					

