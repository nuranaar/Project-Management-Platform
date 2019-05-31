using PMP.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.ViewModels;
using PMP.Models;

namespace PMP.Controllers
{
	[Auth]
    public class ChatController : BaseController
    {
       
        public ActionResult Index(int id)
        {

			ChatVm model = new ChatVm()
			{
				Users = db.Users.ToList(),
				Team = db.Teams.FirstOrDefault(t=>t.Id==id),
				TeamMembers = db.TeamMembers.ToList()
				
			};
			model.Chat = db.Chats.FirstOrDefault(c => c.TeamId == model.Team.Id);
			model.Messages = db.Messages.Where(m=>m.ChatId==model.Chat.Id).OrderBy(m=>m.Date).ToList();

			return View(model);
        }

		[HttpPost]
		public JsonResult AddMessage(int UserId, int ChatId, string Message)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = 400;

				var errorList = ModelState.Values.SelectMany(m => m.Errors)
								 .Select(e => e.ErrorMessage)
								 .ToList();

				return Json(errorList, JsonRequestBehavior.AllowGet);
			}

			Message message = new Message()
			{
				UserId = UserId,
				ChatId = ChatId,
				Content = Message,
				Date = DateTime.Now
			};
			message.User = db.Users.FirstOrDefault(u => u.Id == message.UserId);
			return Json(new
			{
				message.Id,
				message.ChatId,
				message.Content,
				message.Date,
				Photo = message.User.Photo
			}, JsonRequestBehavior.AllowGet);
		}
	}
}	