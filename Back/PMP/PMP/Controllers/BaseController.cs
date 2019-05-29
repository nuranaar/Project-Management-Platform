using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.DAL;
using PMP.Models;

namespace PMP.Controllers
{
	public class BaseController : Controller
	{
		protected readonly PMPcontext db = new PMPcontext();
		public BaseController()
		{
			ViewBag.Project = db.Projects.OrderByDescending(p => p.StartTime).ToList();
			ViewBag.ProjectMem = db.ProjectMembers.ToList();
			ViewBag.Task = db.Tasks.Include("Files");
			ViewBag.File = db.Files;
			ViewBag.User = db.Users.ToList();
		}

		

	}




}
