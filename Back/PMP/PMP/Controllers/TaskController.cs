using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;
using System.IO;

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
			model.Checklists = db.Checklists.Where(cl => cl.TaskId == model.Task.Id).OrderByDescending(cl=>cl.Id).ToList();
			model.Notes = db.Notes.Where(n => n.TaskId == model.Task.Id).OrderByDescending(n => n.Id).ToList();
			model.Files = db.Files.Where(f => f.TaskId == model.Task.Id).OrderByDescending(n => n.Id).ToList();
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
		public JsonResult TaskCreate(Task task,
							   TaskMember taskMember,
							   Models.File startfile,
							   HttpPostedFileBase fileBase,
							   string member)
		{

			if (!ModelState.IsValid)
			{

				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			if (fileBase == null)
			{
				ModelState.AddModelError("file", "Please select file");

			}
			string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			string filename = date + fileBase.FileName;
			string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
			startfile.UserId = 1;
			fileBase.SaveAs(path);
			startfile.Name = filename;
			startfile.Weight = fileBase.ContentLength.ToString() + "-mb";
			startfile.Type = fileBase.ContentType;

			


			task.UserId = 1;
			db.Tasks.Add(task);
			db.SaveChanges();

			startfile.TaskId = task.Id;
			db.Files.Add(startfile);
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
				task.Slug,
				Stage = task.TaskStage.Name
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult ChecklistCreate(Checklist checklist, bool Check)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();
				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			checklist.Checked = Check;
			db.Checklists.Add(checklist);
			db.SaveChanges();

			return Json(new
			{
				checklist.Checked,
				checklist.Id,
				checklist.Text
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult NoteCreate(Note note)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();
				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			db.Notes.Add(note);
			db.SaveChanges();

			return Json(new
			{
				note.Title,
				note.Id,
				note.Desc
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult FileUpload(Models.File file,
							   HttpPostedFileBase fileBase, int TaskId)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;
				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();
				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			if (fileBase == null)
			{
				ModelState.AddModelError("file", "Please select file");

			}
			string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			string filename = date + fileBase.FileName;
			string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
			fileBase.SaveAs(path);
			file.TaskId = TaskId;
			file.UserId = 1;
			file.Name = filename;
			file.Weight = fileBase.ContentLength.ToString() + "-mb";
			file.Type = fileBase.ContentType;
			file.User = db.Users.Find(file.UserId);
			db.Files.Add(file);
			db.SaveChanges();
			return Json(new
			{
				file.User.Photo,
				User = file.User.Name + " " + file.User.Surname,
				file.Id,
				file.Name,
				file.Weight
			}, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult AddMember(TaskMember taskMember, string member, int TaskId)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}
			taskMember.TaskId = TaskId;

			string[] emails = member.Split(' ');
			foreach (var email in emails)
			{
				string e = email.Split(',', '\t', ';')[0];
				var usr = db.Users.FirstOrDefault(u => u.Email == e);
				if (usr != null)
				{
					taskMember.UserId = usr.Id;
				}
				db.TaskMembers.Add(taskMember);
				db.SaveChanges();
			}
			taskMember.Task = db.Tasks.Find(taskMember.TaskId);
			taskMember.User = db.Users.Find(taskMember.UserId);
			return Json(new
			{
				User = taskMember.User.Name + " " + taskMember.User.Surname,
				taskMember.User.Photo,
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult TaskDelete(string Slug)
		{
			Task task = db.Tasks.FirstOrDefault(t=> t.Slug == Slug);

			if (task == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}
			var files = db.Files.Where(n => n.TaskId == task.Id).ToList();
			foreach (var file in files)
			{
				db.Files.Remove(file);
			}
			var notes = db.Notes.Where(n => n.TaskId == task.Id).ToList();
			foreach (var note in notes)
			{
				db.Notes.Remove(note);
			}
			var checks = db.Checklists.Where(cl => cl.TaskId == task.Id).ToList();
			foreach (var check in checks)
			{
				db.Checklists.Remove(check);
			}
			var mems = db.TaskMembers.Where(tm => tm.TaskId == task.Id).ToList();
			foreach (var mem in mems)
			{
				db.TaskMembers.Remove(mem);
			}
			db.SaveChanges();
			db.Tasks.Remove(task);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult CheckitemDelete(int id)
		{
			Checklist checklist = db.Checklists.FirstOrDefault(cl=> cl.Id == id);

			if (checklist == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}
			
			db.Checklists.Remove(checklist);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult NoteDelete(int id)
		{
			Note note = db.Notes.FirstOrDefault(n => n.Id == id);

			if (note == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}

			db.Notes.Remove(note);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult FileDelete(int id)
		{
			var file = db.Files.FirstOrDefault(f => f.Id == id);

			if (file == null)
			{
				Response.StatusCode = 404;
				return Json(new
				{
					message = "Not Found!"
				}, JsonRequestBehavior.AllowGet);
			}

			db.Files.Remove(file);
			db.SaveChanges();
			return Json("", JsonRequestBehavior.AllowGet);
		}
	}

}