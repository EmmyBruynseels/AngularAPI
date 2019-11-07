using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class Stem
	{
		public int StemID { get; set; }
		public int AntwoordID { get; set; }
		public int UserID { get; set; }

		public Antwoord Antwoord { get; set; }
		public User User { get; set; }
	}
}
