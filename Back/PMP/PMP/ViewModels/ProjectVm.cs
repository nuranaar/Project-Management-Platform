using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.ViewModels
{
	public class ProjectVm
	{
		public List<User> Users { get; set; }

		public List<Task> Tasks { get; set; }

		public Project Project { get; set; }

		public List<TaskStage> TaskStages { get; set; }

		public List<ProjectMember> ProjectMembers { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

		public List<File> Files { get; set; }

		public List<Activity> Activities { get; set; }
	}
}