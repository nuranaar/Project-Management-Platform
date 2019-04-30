using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;

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
    }
}