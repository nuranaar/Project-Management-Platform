using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Checklist
	{
		public int Id { get; set; }

		[Column(TypeName ="ntext")]
		[Required, StringLength(100)]
		public string Text { get; set; }

		[Required]
		public bool Checked { get; set; }

		[Required]
		public int TaskId { get; set; }

		public Task Task { get; set; }
	}
}