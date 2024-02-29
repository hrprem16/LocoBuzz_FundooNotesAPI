using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	
	public interface INoteRepository
	{
        public NoteEntity NoteCreation(int userId, AddNoteModel addNote);


        public bool DeleteNote(int NoteId);
        public List<NoteEntity> DisplayNotes(int userId);
        public NoteEntity UpdateNote(int noteId, string newNoteDescription, string newNoteText);


    }
}

