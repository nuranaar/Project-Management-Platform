using System;
using System.Collections.Generic;
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
		static Dictionary<string, int> ChatMember = new Dictionary<string, int>();

		public void Send(int userId, int chatId, string message)
		{
			Message mess = new Message()
			{
				UserId = userId,
				ChatId = chatId,
				Content = message,
				Date = DateTime.Now
			};
			db.Messages.Add(mess);
			db.SaveChanges();
			User user = db.Users.SingleOrDefault(u => u.Id == userId);
			Chat chat = db.Chats.FirstOrDefault(c => c.Id == chatId);
			foreach (var connection in ChatMember.Where(c => c.Value == chatId).ToList())
			{
				Clients.Client(connection.Key).addMessage(user.Photo, mess.Date, mess.Content, user.Id);
			}
		}
		public void SendFile(int userId, int chatId, HttpPostedFileBase fileBase)
		{
			Message mess = new Message()
			{
				UserId = userId,
				ChatId = chatId,
				Date = DateTime.Now
			};

			if (fileBase != null)
			{
				string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				string filename = date + fileBase.FileName;
				string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
				mess.File = filename;
			}

			db.Messages.Add(mess);
			db.SaveChanges();
			User user = db.Users.SingleOrDefault(u => u.Id == userId);
			Chat chat = db.Chats.FirstOrDefault(c => c.Id == chatId);
			foreach (var connection in ChatMember.Where(c => c.Value == chatId).ToList())
			{
				Clients.Client(connection.Key).addFile(user.Photo, mess.Date, user.Id, mess.File);
			}
		}

		public void AddMember(int chatId)
		{
			ChatMember.Add(Context.ConnectionId, chatId);
		}
	}
}