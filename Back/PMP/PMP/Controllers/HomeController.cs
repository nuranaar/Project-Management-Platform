using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;

namespace PMP.Controllers
{
    public class HomeController : BaseController
    {
		int userId = 1; //session
        // GET: Home
        public ActionResult Index()
        {
			UserVm model = new UserVm()
			{
				Admin = db.Users.Find(userId),
				Users = db.Users.ToList(),
				Projects = db.Projects.Where(p => p.UserId == userId).OrderByDescending(p => p.StartTime).ToList(),
				Teams = db.Teams.Where(t=>t.UserId==userId).ToList(),
				Tasks = db.Tasks.Where(t => t.UserId == userId).ToList(),
				TaskStages = db.TaskStages.ToList(),
				TeamMembers = db.TeamMembers.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				ProjectMembers = db.ProjectMembers.ToList()
			};
			return View(model);
        }
    }
}