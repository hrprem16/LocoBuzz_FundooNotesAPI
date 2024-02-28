using System;
using Common_Layer.RequestModels;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class NoteRepository:INoteRepository
	{
        public readonly FundoContext context;
        public NoteRepository(FundoContext context)
        {
            this.context = context;
        }
        public NoteEntity NoteCreation(int userId,AddNoteModel addNote)
        {
            var user = context.UserTable.FirstOrDefault(a => a.UserId == userId);
            if (user != null)
            {
                NoteEntity note = new NoteEntity();

                note.UserId = user.UserId;
                note.NoteText = addNote.NoteText;
                note.Colour = addNote.Colour;
                note.IsArchive = addNote.IsArchive;
                note.IsPin = addNote.IsPin;
                note.IsTrash = addNote.IsTrash;
                note.Image = addNote.Image;
                note.UpdatedAt = DateTime.Now;
                note.CreatedAt = DateTime.Now;

                context.NoteSTable.Add(note);
                context.SaveChanges();

                return note;
            }
            else
            {
                throw new Exception("User is not yet reigistered");
            }

        }


        //public List<NoteEntity> GetAllNotes(FundoContext context)
        //{

        //}
    }
}

