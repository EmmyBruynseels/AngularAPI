using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class PollGebruiker
	{
		public int PollGebruikerID { get; set; }
		public int PollID { get; set; }
		public int UserID { get; set; }

		public Poll Poll { get; set; }
		public User User { get; set; }
	}
}
