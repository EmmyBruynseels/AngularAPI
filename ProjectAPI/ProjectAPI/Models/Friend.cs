using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
	public class Friend
	{
		public int FriendID { get; set; }
		public int? SenderID { get; set; }
		public int? OntvangerID { get; set; }
		public bool Accepted { get; set; }

		[ForeignKey("SenderID")]
		public User Sender { get; set; }

		[ForeignKey("OntvangerID")]
		public User Ontvanger { get; set; }
	}
}
