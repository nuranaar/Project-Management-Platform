using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.ViewModels
{
	public class UserVm
	{
		public List<User> Users { get; set; }

		public List<Team> Teams { get; set; }

		public List<Project> Projects { get; set; }

		public List<Task> Tasks { get; set; }

		public List<TeamMember> TeamMembers { get; set; }

		public List<ProjectMember> ProjectMembers { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

	}
}