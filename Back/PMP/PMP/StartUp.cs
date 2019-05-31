using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(PMP.Startup))]
namespace PMP
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}