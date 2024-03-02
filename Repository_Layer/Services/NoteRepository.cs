using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
                note.NoteTitle = addNote.NoteTitle;
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
                    updateNote.NoteTitle = newNoteText;

                    updateNote.UpdatedAt = DateTime.UtcNow;

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
        public bool IsArchive(int userId,int noteId)
        {
            var filterUser = context.NoteSTable.Where(a => a.UserId == userId);
            
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);

                if (findNotes.IsArchive == false)
                {
                    findNotes.IsArchive = true;
                    context.SaveChanges();
                    return findNotes.IsArchive;
                }
                else
                {
                    findNotes.IsArchive = false;
                    context.SaveChanges();
                    return findNotes.IsArchive;

                }
            }
            else
            {
                throw new Exception("Note Note Found");
            }
        }
        public bool IsPin(int userId,int noteId)
        {
            var filterUser = context.NoteSTable.Where(a => a.UserId == userId);

           
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);
                if (findNotes.IsPin == false)
                {
                    findNotes.IsPin = true;
                    context.SaveChanges();
                    return findNotes.IsPin;
                }
                else
                {
                    findNotes.IsPin = false;
                    context.SaveChanges();
                    return findNotes.IsPin;

                }
            }
            else
            {
                throw new Exception("Note not pinned yet");
            }
        }
        public bool IsTrash(int userId,int noteId)
        {
            var filterUser = context.NoteSTable.Where(a => a.UserId == userId);
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);
                if (findNotes.IsTrash == false)
                {
                    findNotes.IsTrash = true;
                    context.SaveChanges();
                    return findNotes.IsTrash;
                }
                else
                {
                    findNotes.IsTrash = false;
                    context.SaveChanges();
                    return findNotes.IsTrash;

                }
            }
            else
            {
                throw new Exception("Note not found in Trash");
            }
        }

        public string Colour(int userId,int noteId,string colour)
        {
            var filterUser = context.NoteSTable.Where(a => a.UserId == userId);
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);
                if (findNotes != null)
                {
                    findNotes.Colour = colour;
                    context.SaveChanges();
                    return findNotes.Colour;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string UploadImage(string filePath, int userId,int noteId)
        {
            try
            {
                var filterUser = context.NoteSTable.Where(a => a.UserId == userId);

                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NoteId == noteId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dz2emvokk", "734452883777881", "RRlJONvtfnLJZgviiyDH3Lf-ufQ");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = findNotes.NoteTitle
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        context.SaveChanges();

                        return findNotes.Image;

                    }
                    return null;

                }
                else
                {
                    //if user not found
                    return null;
                }
            }
            catch
            {
                // throw exception if user not found
                throw;
            }

        }

       
    }

    
}

