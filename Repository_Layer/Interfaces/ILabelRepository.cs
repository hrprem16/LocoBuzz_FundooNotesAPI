using System;
using Common_Layer.RequestModels;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface ILabelRepository
	{
        public LabelEntity AddLabel(string labelName, int userId, int noteId);
        public LabelEntity LabelUpdate(string newLabelName, int noteId, int labelId);
        public HashSet<string> GetAllLabels(int userId);
        public bool DeleteLabel(int userId, int labelId);
        
    }
}

