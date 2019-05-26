using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Activity
	{
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }

		[Column(TypeName = "ntext")]
		[Required]
		public string Desc { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public User User { get; set; }
	}
}