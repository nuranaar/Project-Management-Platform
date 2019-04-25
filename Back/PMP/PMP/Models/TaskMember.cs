using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class TaskMember
	{
		public int Id { get; set; }

		public int TaskId { get; set; }

		public int UserId { get; set; }

		public Task Task { get; set; }

		public User User { get; set; }
	}
}