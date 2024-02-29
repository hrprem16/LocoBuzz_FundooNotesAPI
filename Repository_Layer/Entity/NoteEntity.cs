using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class NoteEntity
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NoteId { get; set; }

        public string NoteText { get; set; }
        public string NoteDescription { get; set;}
        public string Colour { get; set; }
        public string Image { get; set; }
        public string IsArchive { get; set; }
        public string IsPin { get; set; }
        public string IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("NotesUser")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity NotesUser { get; set; }






    }
}

