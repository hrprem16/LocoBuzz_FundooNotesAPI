using System;
namespace Common_Layer.RequestModels
{
	public class AddNoteModel
	{
        
        public string NoteText { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public string IsArchive { get; set; }
        public string IsPin { get; set; }
        public string IsTrash { get; set; }
    }
}

