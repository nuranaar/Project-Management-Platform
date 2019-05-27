using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.ViewModels
{
	public class TaskVm
	{
		public User Admin { get; set; }

		public List<User> Users { get; set; }

		public Task Task { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

		public List<TaskStage> TaskStages { get; set; }

		public List<Checklist> Checklists { get; set; }

		public List<Note> Notes { get; set; }

		public List<File> Files { get; set; }

		public List<Activity> Activities { get; set; }

	}
}