using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.ViewModels
{
	public class KanbanVm
	{
		public List<User> Users { get; set; }

		public List<Task> Tasks { get; set; }

		public List<TaskStage> TaskStages { get; set; }

		public List<TaskMember> TaskMembers { get; set; }
	}
}