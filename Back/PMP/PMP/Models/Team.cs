using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Team
	{
		public int Id { get; set; }

		[Column("Team Admin")]
		public int UserId { get; set; }

		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		public string Desc { get; set; }

		public string Slug { get; set; }



		public List<TeamMember> TeamMembers { get; set; }
	}
}