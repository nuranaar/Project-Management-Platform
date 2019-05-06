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
			model.TeamMembers=db.TeamMembers.Where(m => m.TeamId == model.Team.Id).OrderByDescending(m=>m.Id).ToList();

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
				var usr = db.Users.FirstOrDefault(u => u.Email == e);
				if (usr != null)
				{
					teamMember.UserId = usr.Id;
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

		[HttpPost]
		public JsonResult AddMember(TeamMember teamMember, string member, int TeamId)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			teamMember.TeamId = TeamId;

			string[] emails = member.Split(' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				var usr = db.Users.FirstOrDefault(u => u.Email == e);
				if (usr != null)
				{
					teamMember.UserId = usr.Id;
				}
				db.TeamMembers.Add(teamMember);
				db.SaveChanges();
			}
			teamMember.Team = db.Teams.Find(teamMember.TeamId);
			teamMember.User = db.Users.Find(teamMember.UserId);
			return Json(new {
				User=teamMember.User.Name+" "+teamMember.User.Surname,
				teamMember.User.Photo,
				teamMember.User.Position
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult TeamDelete(string Slug)
		{
			Team team = db.Teams.FirstOrDefault(t=> t.Slug == Slug);

			if (team == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}

			var mems = db.TeamMembers.Where(tm => tm.TeamId == team.Id).ToList();
			foreach (var mem in mems)
			{
				db.TeamMembers.Remove(mem);

			}
			db.SaveChanges();
			db.Teams.Remove(team);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}
	}
}