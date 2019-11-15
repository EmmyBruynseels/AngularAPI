using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models.dto
{
	public class Poll_dto
	{
		public int PollID { get; set; }
		public string Naam { get; set; }
		public List<Antwoord_dto> Antwoorden { get; set; }
		public List<PollGebruiker_dto> Users { get; set; }

	}
}
