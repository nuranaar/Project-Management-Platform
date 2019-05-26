using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Team
	{
		public int Id { get; set; }

		[Column("Team Admin")]
		[Required]
		public int UserId { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		[Required, StringLength(50)]
		public string Desc { get; set; }

		public string Slug { get; set; }

		public User User { get; set; }

		public List<TeamMember> TeamMembers { get; set; }
	}
}