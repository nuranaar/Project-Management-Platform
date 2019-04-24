using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.DAL;

namespace PMP.Controllers
{
	public class BaseController : Controller
	{
		protected readonly PMPcontext db = new PMPcontext();
		public BaseController()
		{
			ViewBag.Project = db.Projects.OrderByDescending(p => p.StartTime).ToList();

		}
	}
}