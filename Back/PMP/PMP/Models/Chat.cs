using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Chat
	{
		public int Id { get; set; }

		public string Photo { get; set; }

		[Required]
		[Display(Name = "Team")]
		public int TeamId { get; set; }

		public Team Team { get; set; }

		public List<Message> Messages { get; set; }
	}
}