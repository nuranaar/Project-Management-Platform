using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.Models
{
	public class TaskStage
	{
		public int Id { get; set; }

		[Column("Task Stage")]
		public string Name { get; set; }

		public List<Task> Tasks { get; set; }
	}
}