using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Message
	{

		public int Id { get; set; }

		public string Content { get; set; }
		
		[Required]
		public int ChatId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		[Display(Name = "from User")]
		public int UserId { get; set; }

		public User User { get; set; }

		public Chat Chat { get; set; }

	}
}