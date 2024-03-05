using System;
using Common_Layer.RequestModels;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class LabelManager:ILabelManager
	{
        public readonly ILabelRepository labelRepository;

        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public LabelEntity AddLabel(string addLabel, int userId, int noteId)
        {
            return labelRepository.AddLabel(addLabel, userId, noteId);
        }
        public LabelEntity LabelUpdate(string newLabelName, int noteId, int labelId)
        {
            return labelRepository.LabelUpdate(newLabelName, noteId, labelId);
        }

        public HashSet<string> GetAllLabels(int userId)
        {
            return labelRepository.GetAllLabels(userId);
        }
        public bool DeleteLabel(int userId, int labelId)
        {
            return labelRepository.DeleteLabel(userId,labelId);
        }
       
    }
}

