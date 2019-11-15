using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models.dto
{
	public class PollGebruiker_dto
	{
		public int PollID { get; set; }
		public int UserID { get; set; }
		public bool isAdmin { get; set; }
	}
}
