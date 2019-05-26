using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class TeamMember
	{
		public int Id { get; set; }

		[Required]
		public int TeamId { get; set; }

		[Required]
		public int UserId { get; set; }

		public Team Team { get; set; }

		public User User { get; set; }
	}
}