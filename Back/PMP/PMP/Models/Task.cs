using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Task
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int ProjectId { get; set; }

		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		public string Desc { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }



		public User User { get; set; }

		public Project Project { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

		public List<File> Files { get; set; }

		public List<Checklist> Checklists { get; set; }

		public List<Note> Notes { get; set; }


	}
}