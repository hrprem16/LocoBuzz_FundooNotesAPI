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
					if(context.LabelTable.FirstOrDefault(a => a.LabelName == labelName) != null)
					{
						LabelEntity labelEntity = new LabelEntity();
                        context.LabelTable.Add(labelEntity);
                        context.SaveChanges();
                        return labelEntity;
                    }
					else
					{
						throw new Exception("Label is already present");
					}


					
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

		public HashSet<string> GetAllLabels(int userId)
		{
			try
			{
				var finduser = context.LabelTable.Where(a => a.UserId == userId).ToList();

				if (finduser != null)
				{
					HashSet<string> lablename = new HashSet<string>();
					foreach (var i in finduser)
					{
						lablename.Add(i.LabelName);
					}
					return lablename;
					//                      OR
					//var labels = context.LabelTable.Where(a => a.UserId == userId).Select(label => label.LabelName).GroupBy(label=>label.LabelName).ToList();

					//return labels;

				}
				else
				{
					throw new Exception("Label doesn't exist");
				}
			}
			catch
			{
				throw;
			}

		}

		public bool DeleteLabel(int userId, int labelId)
		{
			try
			{
                var filternote = context.LabelTable.FirstOrDefault(a => a.LabelId == labelId);
                if (filternote != null)
                {
                    context.LabelTable.Remove(filternote);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
			catch
			{
				throw new Exception("Label doesn't exist!");
			}
		}
	}
}

				   

					

