﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class CollaboratorEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CollabId { get; set; }

		public string CollabEmailId { get; set; }

		[ForeignKey("CollabBy")]
		public int UserId { get; set; }

		[ForeignKey("CollabTo")]
		public int NoteId { get; set; }

		[JsonIgnore]
		public virtual UserEntity CollabBy { get; set; }

		[JsonIgnore]
		public virtual NoteEntity CollabTo { get; set; }

    }
}

        

