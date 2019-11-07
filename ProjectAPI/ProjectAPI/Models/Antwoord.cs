using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class Antwoord
	{
		public int AntwoordID { get; set; }
		public string Naam { get; set; }

		public Poll Poll { get; set; }
		public int PollID { get; set; }
		public ICollection<Stem> Stemmen { get; set; }
	}
}
