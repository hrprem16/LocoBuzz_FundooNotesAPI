using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	
	public interface INoteRepository
	{
        public NoteEntity NoteCreation(int userId, AddNoteModel addNote);


        //public int NoteDeletion()

        //public List<NoteEntity> GetAllNotes(FundoContext context);


    }
}

