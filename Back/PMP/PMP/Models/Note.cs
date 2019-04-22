using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Note
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Desc { get; set; }

		public int TaskId { get; set; }

		public Task Task { get; set; }
	}
}