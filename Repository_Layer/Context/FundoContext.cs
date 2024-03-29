﻿using System;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;

namespace Repository_Layer.Context
{
	public class FundoContext:DbContext
	{
		public FundoContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<UserEntity> UserTable { get; set; }

		public DbSet<NoteEntity> NoteSTable { get; set; }

		public DbSet<LabelEntity> LabelTable { get; set; }

		public DbSet<CollaboratorEntity> CollabTable { get; set; }

		
	}
}


