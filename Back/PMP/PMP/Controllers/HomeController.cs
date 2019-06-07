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
				Admin = db.Users.FirstOrDefault(u=>u.Id==userId),
				Users = db.Users.ToList(),
				Projects = db.Projects.OrderByDescending(p => p.StartTime).ToList(),
				Teams = db.Teams.ToList(),
				Tasks = db.Tasks.ToList(),
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