using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;
using System.Data.Entity;
using PMP.Filter;

namespace PMP.Controllers
{
	[Auth]
    public class ProjectController : BaseController
    {
        // GET: Projects
        public ActionResult Index(string Slug, int AdminId)
        {
			int userId = Convert.ToInt32(Session["UserId"]);

			ProjectVm model = new ProjectVm()
			{
				Admin = db.Users.FirstOrDefault(u=>u.Id==userId),
				Users = db.Users.ToList(),
				Project = db.Projects.FirstOrDefault(p => p.Slug == Slug && p.UserId==AdminId),
				Tasks = db.Tasks.ToList(),
				TaskMembers = db.TaskMembers.ToList(),
				TaskStages = db.TaskStages.ToList(),
				Activities = db.Activities.OrderByDescending(a => a.Date).ToList(),
				Files = db.Files.ToList(),
				Checklists=db.Checklists.ToList()
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

			project.UserId = Convert.ToInt32(Session["UserId"]);
			
			db.Projects.Add(project);
			db.SaveChanges();

			ProjectMember projMem = new ProjectMember()
			{
				UserId = project.UserId,
				ProjectId = project.Id
			};
			db.ProjectMembers.Add(projMem);
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
			Activity act = new Activity()
			{
				UserId = project.UserId,
				Desc = "create project " + project.Name,
				Date = DateTime.Now
			};
			db.Activities.Add(act);
			db.SaveChanges();
			return Json(new
			{
				project.Id,
				project.Name,
				project.Slug,
				project.UserId
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

		[HttpPost]
		public JsonResult ProjectDelete(string Slug)
		{
			Project project = db.Projects.FirstOrDefault(p=>p.Slug==Slug);

			if (project == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}

			var mems= db.ProjectMembers.Where(pm => pm.ProjectId == project.Id).ToList();
			foreach (var mem in mems)
			{
				db.ProjectMembers.Remove(mem);

			}
			db.SaveChanges();
			db.Projects.Remove(project);
			db.SaveChanges();
			Activity act = new Activity()
			{
				UserId = project.UserId,
				Desc = "delete project " + project.Name,
				Date = DateTime.Now
			};
			db.Activities.Add(act);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult ProjectDetails(int ProjectId)
		{
			Project project = db.Projects.FirstOrDefault(t => t.Id == ProjectId);
			if (project == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}



			return Json(new
			{
				project.Id,
				project.Name,
				project.Slug,
				project.Desc,
				project.StartTime,
				project.EndTime
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult ProjectEdit(Project project)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			Project pr = db.Projects.Find(project.Id);
			pr.Name = project.Name;
			pr.Slug = project.Slug;
			pr.Desc = project.Desc;	
			db.Entry(pr).State = EntityState.Modified; 
			db.SaveChanges();
			Activity act = new Activity()
			{
				UserId = pr.UserId,
				Desc = "update project " + pr.Name,
				Date = DateTime.Now
			};
			db.Activities.Add(act);
			db.SaveChanges();
			return Json(new
			{
				pr.Id,
				pr.Name,
				pr.Slug,
				pr.Desc
			}, JsonRequestBehavior.AllowGet);
		}
	}
}
