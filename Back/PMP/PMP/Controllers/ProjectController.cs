using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;

namespace PMP.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Projects
        public ActionResult Index(string Slug)
        {
			ProjectVm model = new ProjectVm()
			{
				Users = db.Users.ToList(),
				Project = db.Projects.FirstOrDefault(p => p.Slug == Slug),
				Tasks = db.Tasks.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				TaskStages = db.TaskStages.ToList(),
				Activities = db.Activities.ToList(),
				Files=db.Files.ToList(),
			};
			model.ProjectMembers = db.ProjectMembers.Where(m => m.ProjectId == model.Project.Id).ToList();
			
			return View(model);
        }
		[HttpPost]
		public JsonResult ProjectCreate(Project project, ProjectMember projectMember, string member)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			project.UserId = 1;
			
			db.Projects.Add(project);
			db.SaveChanges();

			string[] emails = member.Split( ' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				projectMember.ProjectId = project.Id;
				var usr= db.Users.FirstOrDefault(u => u.Email ==e);
				if (usr != null)
				{
					projectMember.UserId = usr.Id;
				}
				db.ProjectMembers.Add(projectMember);
				db.SaveChanges();
			}
			
			return Json(new
			{
				project.Id,
				project.Name,
				project.Slug
			}, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult AddMember(ProjectMember projectMember, string member, int ProjectId)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			projectMember.ProjectId = ProjectId;

			string[] emails = member.Split(' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				var usr = db.Users.FirstOrDefault(u => u.Email == e);
				if (usr != null)
				{
					projectMember.UserId = usr.Id;
				}
				db.ProjectMembers.Add(projectMember);
				db.SaveChanges();
			}
			projectMember.Project = db.Projects.Find(projectMember.ProjectId);
			projectMember.User = db.Users.Find(projectMember.UserId);
			return Json(new
			{
				User = projectMember.User.Name + " " + projectMember.User.Surname,
				projectMember.User.Photo,
			}, JsonRequestBehavior.AllowGet);
		}
	}
}
