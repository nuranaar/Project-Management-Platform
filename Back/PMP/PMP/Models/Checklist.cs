using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Checklist
	{
		public int Id { get; set; }

		[Column(TypeName ="ntext")]
		public string Text { get; set; }

		public bool Checked { get; set; }

		public int TaskId { get; set; }

		public Task Task { get; set; }
	}
}