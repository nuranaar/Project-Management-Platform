using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;

namespace PMP.Controllers
{
    public class TaskController : BaseController
    {
        // GET: Task
        public ActionResult Index(string Slug)
        {

			TaskVm model = new TaskVm()
			{
				Users = db.Users.ToList(),
				Task = db.Tasks.FirstOrDefault(t => t.Slug == Slug),
				Activities = db.Activities.ToList()
			};
			model.Checklists = db.Checklists.Where(cl => cl.TaskId == model.Task.Id).ToList();
			model.Notes = db.Notes.Where(n => n.TaskId == model.Task.Id).ToList();
			model.Files = db.Files.Where(f => f.TaskId == model.Task.Id).ToList();
			model.TaskMembers = db.TaskMembers.Where(tm => tm.TaskId == model.Task.Id).ToList();
            return View(model);
        }
		public JsonResult GetStages()
		{
			var stages = db.TaskStages.Select(ts => new
			{
				ts.Id,
				ts.Name
			});

			return Json(stages, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult TaskCreate(Task task, TaskMember taskMember, string member)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			task.UserId = 1;

			db.Tasks.Add(task);
			db.SaveChanges();

			string[] emails = member.Split(' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				taskMember.TaskId = task.Id;
				var usr = db.Users.FirstOrDefault(u => u.Email == e);
				if (usr != null)
				{
					taskMember.UserId = usr.Id;
				}
				db.TaskMembers.Add(taskMember);
				db.SaveChanges();
			}
			task.TaskStage = db.TaskStages.Find(task.TaskStageId);
			return Json(new
			{
				task.Id,
				task.Name,
				task.Slug,
				Stage=task.TaskStage.Name
			}, JsonRequestBehavior.AllowGet);
		}
    }
}