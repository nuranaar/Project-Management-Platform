using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PMP.Models;

namespace PMP.DAL
{
	public class PMPcontext : DbContext
	{
		public PMPcontext() : base("PMPcontext")
		{

		}

		public DbSet<User> Users { get; set; }

		public DbSet<Team> Teams { get; set; }

		public DbSet<Project> Projects { get; set; }

		public DbSet<Task> Tasks { get; set; }

		public DbSet<File> Files { get; set; }

		public DbSet<Activity> Activities { get; set; }

		public DbSet<Checklist> Checklists { get; set; }

		public DbSet<Note> Notes { get; set; }

		public DbSet<ProjectMember> ProjectMembers { get; set; }

		public DbSet<TaskMember> TaskMembers { get; set; }

		public DbSet<TeamMember> TeamMembers { get; set; }

		
	}
}