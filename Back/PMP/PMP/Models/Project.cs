using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMP.Models
{
	public class Project
	{
		public int Id { get; set; }

		[Display(Name="Project Admin")]
		public int UserId { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Column(TypeName = "ntext")]
		[StringLength(250)]
		public string Desc { get; set; }

		[Required]
		public DateTime StartTime { get; set; }

		[Required]                           
		public DateTime EndTime { get; set; }

        public int ?TeamId { get; set; }

        public string Slug { get; set; }

		public User User { get; set; }

		public Team Team { get; set; }

		public List<ProjectMember> ProjectMembers { get; set; }

        public List<Task> Tasks { get; set; }

    }
}