using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class File
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public int TaskId { get; set; }

		[Required, StringLength(150)]
		public string Name { get; set; }

		[Required]
		public string Weight { get; set; }

		[Required]
		public string Type { get; set; }

		public User User { get; set; }

		public Task Task { get; set; }
	}
}