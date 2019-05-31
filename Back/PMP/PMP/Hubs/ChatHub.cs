using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using Microsoft.AspNet.SignalR;
using PMP.DAL;
using PMP.Models;

namespace SignalRChat
{
	public class ChatHub : Hub
	{
		protected readonly PMPcontext db = new PMPcontext();

		public void Send(int userId, int chatId,string message)
		{
			Message mess = new Message()
			{
				UserId=userId,
				ChatId=chatId,
				Content=message,
				Date=DateTime.Now
			};
			//if (fileBase != null)
			//{
			//	string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			//	string filename = date + fileBase.FileName;
			//	string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
			//	mess.File = filename;
			//}
			db.Messages.Add(mess);
			db.SaveChanges();
			User user = db.Users.SingleOrDefault(u=>u.Id==userId);
			//Clients.OthersInGroup(teamMember).broadcastMessage(user.Photo, mes.Date, mes.Content, mes.File);
			Clients.Others.addMessage(user.Photo, mess.Date, mess.Content, chatId);
		}
	}
}