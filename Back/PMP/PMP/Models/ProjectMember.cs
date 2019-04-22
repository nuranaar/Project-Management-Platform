using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class ProjectMember
	{
		public int Id { get; set; }

		public int ProjectId { get; set; }

		public int UserId { get; set; }

		public Project Project { get; set; }

		public User User { get; set; }
	}
}