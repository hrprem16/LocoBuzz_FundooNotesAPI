using System;
using Common_Layer.RequestModels;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class CollabManager: ICollabManager
    {
        public readonly ICollabRepository collabRepository;

        public CollabManager(ICollabRepository collabRepository)
        {
            this.collabRepository = collabRepository;
        }

        public CollaboratorEntity AddCollab(string collabEmail, int userId, int noteId)
        {
            return collabRepository.AddCollab(collabEmail, userId, noteId);
        }
        public NoteEntity UpdateCollab(UpdateCollabModel model, string collabEmail, int noteId)
        {
            return collabRepository.UpdateCollab(model, collabEmail, noteId);
        }
        public string RemoveCollab(int userId, int collabId)
        {
            return collabRepository.RemoveCollab(userId, collabId);
        }
    }
}

