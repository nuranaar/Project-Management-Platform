using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.Filter;
using PMP.ViewModels;

namespace PMP.Controllers
{
		[Auth]
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			int userId = Convert.ToInt32(Session["UserId"]);

			UserVm model = new UserVm()
			{
				Admin = db.Users.Find(userId),
				Users = db.Users.ToList(),
				Projects = db.Projects.Where(p => p.UserId == userId).OrderByDescending(p => p.StartTime).ToList(),
				Teams = db.Teams.Where(t => t.UserId == userId).ToList(),
				Tasks = db.Tasks.Where(t => t.UserId == userId).ToList(),
				Checklists = db.Checklists.ToList(),
				TaskStages = db.TaskStages.ToList(),
				TeamMembers = db.TeamMembers.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				ProjectMembers = db.ProjectMembers.ToList()
			};
			return View(model);
		}
	}
}