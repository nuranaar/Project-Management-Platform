using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMP.Models;

namespace PMP.ViewModels
{
	public class ChatVm
	{

		public List<User> Users { get; set; }

		public Team Team { get; set; }

		public List<TeamMember> TeamMembers { get; set; }

		public Chat Chat { get; set; }

		public List<Message> Messages { get; set; }

		public List<File> Files { get; set; }
	}
}