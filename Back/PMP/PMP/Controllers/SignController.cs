using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PMP.Models;

namespace PMP.Controllers
{
	public class SignController : BaseController
	{
		// GET: Sign
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Index(string Email, string Password)
		{
			if (ModelState.IsValid)
			{

				User lgn = db.Users.FirstOrDefault(u => u.Email == Email);

				if (lgn != null)
				{
					if (Crypto.VerifyHashedPassword(lgn.Password, Password))
					{
						Session["signin"] = true;
						Session["UserId"] = lgn.Id;
						return RedirectToAction("index", "home");
					}
				}
				if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
				{
					ModelState.AddModelError("summary", "Enter email and password.");
				}
				else
				{
					ModelState.AddModelError("summary", "Email or password incorrect.");
				}
				return View();
			}

			return View();
		}

		
		public ActionResult Signup()
		{

			return View();
		}
		[HttpPost]
		public ActionResult Signup(User user)
		{

			return View();
		}
		public ActionResult Logout()
		{
			Session.Remove("signin");
			Session.Remove("UserId");

			return RedirectToAction("index");
		}
	}
}