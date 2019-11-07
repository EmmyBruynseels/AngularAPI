using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class Friend
	{
		public int FriendID { get; set; }
		public int SenderID { get; set; }
		public int OntvangerID { get; set; }
		public bool Accepted { get; set; }

		//public User Sender { get; set; }
		//public User Ontvanger { get; set; }
	}
}
