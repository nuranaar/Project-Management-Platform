 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Task
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		[Display(Name="Description")]
		[ StringLength(250)]
		public string Desc { get; set; }

		[Required]
		public DateTime StartTime { get; set; }

		[Required]
		public DateTime EndTime { get; set; }

		public string Slug { get; set; }

		public int ?ProjectId { get; set; }

		public List<File> Files { get; set; } 

		[Required]
		[ForeignKey("TaskStage")]
		public int TaskStageId { get; set; }
		
		public TaskStage TaskStage { get; set; }

		public User User { get; set; }

		public Project Project { get; set; }

		public List<TaskMember> TaskMembers { get; set; }

		public List<Checklist> Checklists { get; set; }

		public List<Note> Notes { get; set; }

	}
}