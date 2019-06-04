using PMP.Filter;
using PMP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.Helpers;
using System.Web.Helpers;

namespace PMP.Controllers
{
	[Auth]
	public class SettingController : BaseController
	{
		// GET: Setting
		public ActionResult Index(int Id)
		{
			User model = db.Users.Find(Id);
			
			return View(model);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Admin admin, HttpPostedFileBase Photo)
		{
			User user = db.Users.FirstOrDefault(u => u.Id == admin.Id);

			if (Photo == null)
			{
				db.Entry(user).Property(u => u.Photo).IsModified = false;
			}
			else
			{
				string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Photo.FileName;
				string path = Path.Combine(Server.MapPath("~/Uploads"), filename);

				Photo.SaveAs(path);

				user.Photo = filename;
			}
			user.Name = admin.Name;
			user.Position = admin.Position;
			user.Biography = admin.Biography;
			user.Email = admin.Email;
			user.Surname = admin.Surname;
			db.Entry(user).State = EntityState.Modified;
			db.SaveChanges();

			return RedirectToAction("index");
		}


		[HttpPost]
		public JsonResult EditPassword(string Current, string New, string Confirm, int Id)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			User user = db.Users.FirstOrDefault(u => u.Id == Id);
			bool resp = true;
			bool result = Crypto.VerifyHashedPassword(user.Password, Current);
			
			if (result)
			{
				if (New == Confirm)
				{
					user.Password = Crypto.HashPassword(New);
					db.Entry(user).State = EntityState.Modified;
					db.SaveChanges();
					
				}
			}
			else
			{
				resp = false;
			}
			return Json(resp, JsonRequestBehavior.AllowGet);
		}

	}
}