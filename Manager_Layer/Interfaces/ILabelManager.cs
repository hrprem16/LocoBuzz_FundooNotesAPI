using System;
using Common_Layer.RequestModels;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ILabelManager
	{
        public LabelEntity AddLabel(string addLabel, int userId, int noteId);
        public LabelEntity LabelUpdate(string newLabelName, int noteId, int labelId);
        public HashSet<string> GetAllLabels(int userId);
        public bool DeleteLabel(int userId, int labelId);

    }
}

