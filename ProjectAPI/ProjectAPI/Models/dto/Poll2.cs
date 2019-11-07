using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models.dto
{
	public class Poll2
	{
		public int PollID { get; set; }
		public string Naam { get; set; }
		public List<Antwoord2> Antwoorden { get; set; }
		public List<PollGebruiker2> Users { get; set; }

	}
}
