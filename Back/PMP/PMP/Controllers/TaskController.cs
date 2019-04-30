using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMP.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index(string Slug)
        {
            return Content(Slug);
        }
    }
}