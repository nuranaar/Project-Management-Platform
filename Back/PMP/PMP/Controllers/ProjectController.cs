using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;

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
				TaskMembers = db.TaskMembers.ToList(),
				Activities = db.Activities.ToList(),
				Files=db.Files.ToList()
			};
			model.ProjectMembers = db.ProjectMembers.Where(m => m.ProjectId == model.Project.Id).ToList();
			model.Tasks = db.Tasks.Where(t => t.ProjectId == model.Project.Id).ToList();
			return View(model);
        }
    }
}