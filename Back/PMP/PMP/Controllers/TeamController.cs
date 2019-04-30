using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;

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
				TaskStages=db.TaskStages.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				Projects= db.Projects.OrderByDescending(p => p.StartTime).ToList(),
				ProjectMembers = db.ProjectMembers.ToList()
			};
			model.TeamMembers=db.TeamMembers.Where(m => m.TeamId == model.Team.Id).ToList();

			return View(model);
        }
		[HttpPost]
		public JsonResult TeamCreate(Team team, TeamMember teamMember, string member)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			team.UserId = 2;

			db.Teams.Add(team);
			db.SaveChanges();

			string[] emails = member.Split(' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				teamMember.TeamId = team.Id;
				var usrId = db.Users.FirstOrDefault(u => u.Email == e).Id;
				if (usrId != null)
				{
					teamMember.UserId = usrId;
				}
				db.TeamMembers.Add(teamMember);
				db.SaveChanges();
			}
			
			return Json(new
			{
				team.Id,
				team.Name,
				team.Slug
			}, JsonRequestBehavior.AllowGet);
		}
	}
}