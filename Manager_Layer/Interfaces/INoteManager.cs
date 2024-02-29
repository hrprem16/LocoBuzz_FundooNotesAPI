﻿using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface INoteManager
	{
        public NoteEntity NoteCreation(int userId, AddNoteModel addNote);

        public bool DeleteNote(int NoteId);

        public List<NoteEntity> DisplayNotes(int userId);
        public NoteEntity UpdateNote(int noteId, string newNoteDescription, string newNoteText);

    }
}

