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
                note.NoteDescription = addNote.NoteDescription;
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


        public bool DeleteNote(int NoteId)
        {
            try
            {
                var note = context.NoteSTable.FirstOrDefault(a => a.NoteId == NoteId);

                if (note != null)
                {
                    context.NoteSTable.Remove(note);
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
                throw new Exception("Note is not deleted");
            }
           
        }

        public List<NoteEntity> DisplayNotes(int userId)
        {
            try
            {
                var userNotes = context.NoteSTable.Where(a => a.UserId == userId).ToList();

                if (userNotes != null)
                {
                    //List<NoteEntity> noteEntities = new List<NoteEntity>();

                   
                    return userNotes;
                }
                else
                {
                    throw new Exception("User Not Found");
                }

            }
            catch
            {
                throw new Exception("User Not Found");
            }
        }

        public NoteEntity UpdateNote(int noteId, string newNoteDescription, string newNoteText)
        {
            try
            {
                // Find the note to update
                var updateNote = context.NoteSTable.FirstOrDefault(a => a.NoteId == noteId);

                if (updateNote != null)
                {
                    // Update the properties
                    updateNote.NoteDescription = newNoteDescription;
                    updateNote.NoteText = newNoteText;

                    // Update the entity in the database
                    context.NoteSTable.Update(updateNote);
                    context.SaveChanges(); // Commit changes to the database

                    return updateNote; // Return the updated entity if needed
                }
            }
            catch 
            {
                // Handle exceptions (log, throw, etc.)
                throw new Exception("Note is not updated");
            }

            return null; // Return null if the note with the specified ID is not found
        }

    }
}

