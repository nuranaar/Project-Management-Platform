using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Note
	{
		public int Id { get; set; }

		[ StringLength(50)]
		public string Title { get; set; }

		[ StringLength(150)]
		public string Desc { get; set; }

		[Required]
		public int TaskId { get; set; }

		public Task Task { get; set; }
	}
}