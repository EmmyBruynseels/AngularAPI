using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class DBInitializer
	{
		public static void Initialize(PollContext context)
		{
			context.Database.EnsureCreated();

			if (context.Polls.Any())
			{
				return;
			}

			context.Polls.AddRange(
				new Poll { Naam = "Movies" },
				new Poll { Naam = "Series" }
			);
			context.SaveChanges();

			context.Users.AddRange(
				new User { Email = "emmy.bruynseels@hotmail.com", Username = "Emmy", Password = "Emmy" },
				new User { Email = "emmy.bruynseels@outlook.com", Username = "EmmyBruynseels", Password = "EmmyBruynseels" },
				new User { Email = "gytha.bruynseels@hotmail.com", Username = "GythaBruynseels", Password = "GythaBruynseels" }
			);
			context.SaveChanges();


			context.Friends.AddRange(
				//new Friend { Sender = context.Users.Single(u => u.UserID == 1), Ontvanger = context.Users.Single(u => u.UserID == 2), Accepted = true },
				new Friend { SenderID =1 , OntvangerID = 2, Accepted = true },
				//new Friend { Sender = context.Users.Single(u => u.UserID == 3), Ontvanger = context.Users.Single(u => u.UserID == 1), Accepted = false }
				new Friend { SenderID = 3, OntvangerID =1, Accepted = false }
				);
			context.SaveChanges();

			context.PollGebruikers.AddRange(
				new PollGebruiker { Poll = context.Polls.Single(p => p.PollID == 1), User = context.Users.Single(u => u.UserID == 1), isAdmin= true },
				new PollGebruiker { Poll = context.Polls.Single(p => p.PollID == 1), User = context.Users.Single(u => u.UserID == 2), isAdmin = false },
				new PollGebruiker { Poll = context.Polls.Single(p => p.PollID == 2), User = context.Users.Single(u => u.UserID == 1), isAdmin = true }
			);
			context.SaveChanges();

			context.Antwoorden.AddRange(
				new Antwoord { Naam = "Avengers: Endgame", Poll = context.Polls.Single(p => p.PollID == 1) },
				new Antwoord { Naam = "Iron Man 3", Poll = context.Polls.Single(p => p.PollID == 1) },
				new Antwoord { Naam = "Captain America: Civil War", Poll = context.Polls.Single(p => p.PollID == 1) },
				new Antwoord { Naam = "Arrow", Poll = context.Polls.Single(p => p.PollID == 2) },
				new Antwoord { Naam = "The Flash", Poll = context.Polls.Single(p => p.PollID == 2) },
				new Antwoord { Naam = "Legends of Tomorrow", Poll = context.Polls.Single(p => p.PollID == 2) },
				new Antwoord { Naam = "Supergirl", Poll = context.Polls.Single(p => p.PollID == 2) }
			);
			context.SaveChanges();

			context.Stemmen.AddRange(
				new Stem { Antwoord = context.Antwoorden.Single(a => a.AntwoordID == 1), User = context.Users.Single(u => u.UserID == 1) },
				new Stem { Antwoord = context.Antwoorden.Single(a => a.AntwoordID == 4), User = context.Users.Single(u => u.UserID == 1) }
			);
			context.SaveChanges();
		}
	}
}
