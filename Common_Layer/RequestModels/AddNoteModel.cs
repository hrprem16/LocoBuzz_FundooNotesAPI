using System;
namespace Common_Layer.RequestModels
{
	public class AddNoteModel
	{
        
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
    }
}

