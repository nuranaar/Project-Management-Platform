using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class TaskMember
	{
		public int Id { get; set; }

		[Required]
		public int TaskId { get; set; }

		[Required]
		public int UserId { get; set; }

		public Task Task { get; set; }

		public User User { get; set; }
	}
}