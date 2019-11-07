using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectAPI.Models;

namespace ProjectAPI.Models
{
	public class PollContext : DbContext
	{
		public PollContext(DbContextOptions<PollContext> options) : base(options)
		{
		}

		public DbSet<Poll> Polls { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Stem> Stemmen { get; set; }
		public DbSet<Antwoord> Antwoorden { get; set; }
		public DbSet<PollGebruiker> PollGebruikers { get; set; }
		public DbSet<Friend> Friends { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Poll>().ToTable("Poll");
			modelBuilder.Entity<User>().ToTable("User");
			modelBuilder.Entity<Stem>().ToTable("Stem");
			modelBuilder.Entity<Antwoord>().ToTable("Antwoord");
			modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
			modelBuilder.Entity<Friend>().ToTable("Friend");
		}

		public DbSet<ProjectAPI.Models.PollGebruiker> PollGebruiker { get; set; }

		public DbSet<ProjectAPI.Models.Stem> Stem { get; set; }

		public DbSet<ProjectAPI.Models.Antwoord> Antwoord { get; set; }
	}
}
