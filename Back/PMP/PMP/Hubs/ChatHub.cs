using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using PMP.DAL;
using PMP.Models;

namespace SignalRChat
{
	public class ChatHub : Hub
	{
		protected readonly PMPcontext db = new PMPcontext();

		public void Send(int id, string message)
		{
			string userPhoto = db.Users.Find(id).Photo;
			Clients.All.broadcastMessage(userPhoto, message);
		}
	}
}