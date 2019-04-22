using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class File
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int TaskId { get; set; }

		public string Name { get; set; }

		public string Weight { get; set; }

		public string Type { get; set; }

		public User User { get; set; }

		public Task Task { get; set; }
	}
}