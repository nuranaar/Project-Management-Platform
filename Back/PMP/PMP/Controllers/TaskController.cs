using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;
using System.IO;
using System.Data.Entity;
using PMP.Filter;

namespace PMP.Controllers
{
    [Auth]
    public class TaskController : BaseController
    {
        // GET: Task

        public ActionResult Index(string Slug, int AdminId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (db.Tasks.FirstOrDefault(t => t.Slug == Slug && t.UserId == AdminId) == null || Slug == null || AdminId == 0)
            {
                return HttpNotFound();
            }

            TaskVm model = new TaskVm()
            {
                Admin = db.Users.FirstOrDefault(u => u.Id == userId),
                Users = db.Users.ToList(),
                Task = db.Tasks.FirstOrDefault(t => t.Slug == Slug && t.UserId == AdminId),
                Activities = db.Activities.OrderByDescending(a => a.Date).ToList(),
                Checklists = db.Checklists.OrderByDescending(c => c.Id).ToList(),
                Notes = db.Notes.OrderByDescending(n => n.Id).ToList(),
                Files = db.Files.OrderByDescending(f => f.Id).ToList(),
                TaskMembers = db.TaskMembers.ToList()
            };

            return View(model);
        }

        public ActionResult Kanban()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            KanbanVm model = new KanbanVm()
            {
                Users = db.Users.ToList(),
                Tasks = db.Tasks.Where(t => t.UserId == userId).ToList(),
                TaskStages = db.TaskStages.ToList(),
                TaskMembers = db.TaskMembers.ToList()
            };
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
        public JsonResult GetProjects()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var stages = db.Projects.Select(p => p.UserId == userId ? new
            {
                p.Id,
                p.Name
            } : null
            );

            return Json(stages, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeams()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var stages = db.Teams.Select(t => t.UserId == userId ? new
            {
                t.Id,
                t.Name
            } : null);

            return Json(stages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TaskCreate(Task task,
                               TaskMember taskMember,
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
            Models.File startfile = new Models.File();
            startfile.UserId = Convert.ToInt32(Session["UserId"]);
            fileBase.SaveAs(path);
            startfile.Name = filename;
            startfile.Weight = fileBase.ContentLength.ToString() + "-mb";
            startfile.Type = fileBase.ContentType;

            task.UserId = Convert.ToInt32(Session["UserId"]);
            if (db.Tasks.FirstOrDefault(t => t.Slug == task.Slug) != null)
            {
                task.Slug = task.Slug + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
            db.Tasks.Add(task);
            db.SaveChanges();

            startfile.TaskId = task.Id;
            db.Files.Add(startfile);
            db.SaveChanges();

            TaskMember taskMem = new TaskMember()
            {
                UserId = task.UserId,
                TaskId = task.Id
            };
            db.TaskMembers.Add(taskMem);
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
            Activity act = new Activity()
            {
                UserId = task.UserId,
                Desc = "create task " + task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
            db.SaveChanges();
            return Json(new
            {
                task.Id,
                task.Slug,
                task.UserId
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
            checklist.Task = db.Tasks.Find(checklist.TaskId);

            Activity act = new Activity()
            {
                UserId = checklist.Task.UserId,
                Desc = "add checkitem to task " + checklist.Task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
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
            note.Task = db.Tasks.Find(note.TaskId);
            Activity act = new Activity()
            {
                UserId = note.Task.UserId,
                Desc = "add note to task " + note.Task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
            db.SaveChanges();
            return Json(new
            {
                note.Title,
                note.Id,
                note.Desc
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FileUpload(HttpPostedFileBase fileBase, int TaskId)
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
            Models.File file = new Models.File();
            file.TaskId = TaskId;
            file.UserId = Convert.ToInt32(Session["UserId"]);
            file.Name = filename;
            file.Weight = fileBase.ContentLength.ToString() + "-mb";
            file.Type = fileBase.ContentType;
            file.User = db.Users.Find(file.UserId);
            db.Files.Add(file);
            db.SaveChanges();
            file.Task = db.Tasks.FirstOrDefault(t => t.Id == file.TaskId);
            Activity act = new Activity()
            {
                UserId = file.Task.UserId,
                Desc = "add new file to task " + file.Task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
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
            Task task = db.Tasks.FirstOrDefault(t => t.Slug == Slug);

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
            Activity act = new Activity()
            {
                UserId = task.UserId,
                Desc = "detete task " + task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckitemDelete(int id)
        {
            Checklist checklist = db.Checklists.FirstOrDefault(cl => cl.Id == id);

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

        [HttpPost]
        public JsonResult NoteDetails(int Id)
        {
            Note note = db.Notes.Find(Id);
            if (note == null)
            {
                Response.StatusCode = 404;
                return Json(new
                {
                    message = "Not Found!"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                note.Title,
                note.Id,
                note.Desc
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NoteEdit(Note notes)
        {
            Note note = db.Notes.FirstOrDefault(n => n.Id == notes.Id);
            note.Desc = notes.Desc;
            note.Title = notes.Title;
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return Json(errorList, JsonRequestBehavior.AllowGet);
            }

            db.Entry(note).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new
            {
                note.Title,
                note.Id,
                note.Desc
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckDetails(int Id)
        {
            Checklist check = db.Checklists.Find(Id);
            if (check == null)
            {
                Response.StatusCode = 404;
                return Json(new
                {
                    message = "Not Found!"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                check.Checked,
                check.Id,
                check.Text
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckEdit(int id, string text, bool check)
        {
            Checklist checklist = db.Checklists.Find(id);
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return Json(errorList, JsonRequestBehavior.AllowGet);
            }

            checklist.Checked = check;
            checklist.Text = text;
            checklist.Task = db.Tasks.Find(checklist.TaskId);

            db.Entry(checklist).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                checklist.Checked,
                checklist.Id,
                checklist.Text
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TaskDetails(int TaskId)
        {
            Task task = db.Tasks.Find(TaskId);
            task.TaskStage = db.TaskStages.Find(task.TaskStageId);
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return Json(errorList, JsonRequestBehavior.AllowGet);
            }


            return Json(new
            {
                task.Id,
                task.Name,
                task.Slug,
                task.Desc,
                Stage = new
                {
                    task.TaskStage.Id,
                    task.TaskStage.Name
                }
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TaskEdit(int Id, string Name, string Desc, int Stage, string Slug, int Project)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                return Json(errorList, JsonRequestBehavior.AllowGet);
            }
            Task task = db.Tasks.Find(Id);
            task.UserId = Convert.ToInt32(Session["UserId"]);
            task.Name = Name;
            task.Desc = Desc;
            task.ProjectId = Project;
            if (db.Tasks.FirstOrDefault(t => t.Slug == Slug && t.UserId == task.UserId) != null)
            {
                task.Slug = Slug + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
            else
            {
                task.Slug = Slug;
            }
            task.TaskStageId = Stage;
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
            task.TaskStage = db.TaskStages.Find(task.TaskStageId);
            Activity act = new Activity()
            {
                UserId = task.UserId,
                Desc = "update task " + task.Name,
                Date = DateTime.Now
            };
            db.Activities.Add(act);
            db.SaveChanges();
            return Json(new
            {
                task.Id,
                task.Name,
                task.Slug,
                task.Desc,
                task.UserId,
                TaskStage = new
                {
                    task.TaskStage.Id,
                    task.TaskStage.Name
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DelActivities(int Id)
        {
            List<TaskMember> taskMembers = db.TaskMembers.Where(pm => pm.TaskId == Id).ToList();

            foreach (TaskMember member in taskMembers)
            {
                List<Activity> activities = db.Activities.Where(a => a.UserId == member.UserId).ToList();
                foreach (Activity act in activities)
                {
                    db.Activities.Remove(act);
                    db.SaveChanges();
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }

}