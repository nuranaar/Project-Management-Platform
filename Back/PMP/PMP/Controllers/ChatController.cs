using PMP.Filter;
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
	[Auth]
    public class ChatController : BaseController
    {
       
        public ActionResult Index(int id)
        {

			ChatVm model = new ChatVm()
			{
				Users = db.Users.ToList(),
				Team = db.Teams.FirstOrDefault(t => t.Id == id),
				TeamMembers = db.TeamMembers.ToList(),
				Files=db.Files.ToList()
			};
			model.Chat = db.Chats.FirstOrDefault(c => c.TeamId == model.Team.Id);
			model.Messages = db.Messages.Where(m=>m.ChatId==model.Chat.Id).OrderBy(m=>m.Date).ToList();

			return View(model);
        }

	}
}	