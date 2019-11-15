using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models.dto
{
	public class Antwoord_dto
	{
		public int PollID { get; set; }
		public string Naam { get; set; }
		public int AntwoordID { get; set; }

		public List<Stem_dto> Stemmen { get; set; }
	}
}
