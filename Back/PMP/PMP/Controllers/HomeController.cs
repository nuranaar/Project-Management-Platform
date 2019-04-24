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
        // GET: Home
        public ActionResult Index()
        {
			UserVm model = new UserVm()
			{
				Users = db.Users.ToList(),
				Projects = db.Projects.OrderByDescending(p => p.StartTime).ToList(),
				Teams = db.Teams.ToList(),
				Tasks = db.Tasks.ToList(),
				TeamMembers = db.TeamMembers.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				ProjectMembers = db.ProjectMembers.ToList()
			};
			return View(model);
        }
    }
}