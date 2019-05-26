using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Required, StringLength(50)]
		public string Surname { get; set; }

		[Required, StringLength(100)]
		public string Password { get; set; }

		[Required, StringLength(100)]
		public string Email { get; set; }

		[StringLength(50)]
		public string Position { get; set; }

		[StringLength(200)]
		public string Photo { get; set; }

		[Column(TypeName = "ntext")]
		[StringLength(300)]
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