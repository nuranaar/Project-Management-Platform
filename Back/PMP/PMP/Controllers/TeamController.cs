using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;

namespace PMP.Controllers
{
    public class TeamController : BaseController
    {
        // GET: Team
        public ActionResult Index(string Slug)
        {

			TeamVm model = new TeamVm()
			{
				Users=db.Users.ToList(),
				Team=db.Teams.FirstOrDefault(t=>t.Slug==Slug),
				Tasks=db.Tasks.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				Projects= db.Projects.OrderByDescending(p => p.StartTime).ToList(),
				ProjectMembers = db.ProjectMembers.ToList()
			};
			model.TeamMembers=db.TeamMembers.Where(m => m.TeamId == model.Team.Id).ToList();

			return View(model);
        }
    }
}