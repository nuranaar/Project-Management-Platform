using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class User
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		public string Position { get; set; }

		public string Photo { get; set; }

		public string Slug { get; set; }

		[Column(TypeName = "ntext")]
		public string Biography { get; set; }



		public List<Team> Teams { get; set; }

		public List<TeamMember> TeamMembers { get; set; }

		public List<Project> Projects { get; set; }

		public List<ProjectMember> ProjectMembers { get; set; }

		public List<Task> Tasks { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

		public List<File> Files { get; set; }

		public List<Activity> Activities { get; set; }


	}
}