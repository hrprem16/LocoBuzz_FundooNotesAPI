using System;
using Common_Layer.RequestModels;
using Manager_Layer.Interfaces;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class NoteManager:INoteManager
	{
        public readonly INoteRepository noteRepository;

        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }
        public NoteEntity NoteCreation(int userId ,AddNoteModel addNotes)
        {
            return noteRepository.NoteCreation(userId,addNotes);
        }

        public bool DeleteNote(int NoteId)
        {
            return this.noteRepository.DeleteNote(NoteId);
        }

        public List<NoteEntity> DisplayNotes(int userId)
        {
            return this.noteRepository.DisplayNotes(userId);
        }
        public NoteEntity UpdateNote(int noteId, string newNoteDescription, string newNoteText)
        {
            return this.noteRepository.UpdateNote(noteId, newNoteDescription, newNoteText);
        }

        public bool IsArchive(int noteId)
        {
            return this.noteRepository.IsArchive(noteId);
        }
        public bool IsPin(int noteId)
        {
            return this.noteRepository.IsPin(noteId);
        }
        public bool IsTrash(int noteId)
        {
            return this.noteRepository.IsTrash(noteId);
        }


    }
}

