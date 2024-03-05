﻿using System;
using Common_Layer.RequestModels;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ICollabManager
	{
        public CollaboratorEntity AddCollab(string collabEmail, int userId, int noteId);
        public NoteEntity UpdateCollab(UpdateCollabModel model, string collabEmail, int noteId);
        public string RemoveCollab(int userId, int collabId);

    }
}

