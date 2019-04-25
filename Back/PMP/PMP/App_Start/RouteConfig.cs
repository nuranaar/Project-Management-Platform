using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMP
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				name: "ProjectUrl",
				url: "{controller}/{*slug}",
				defaults: new { controller = "Project", action = "Index", slug = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "TeamUrl",
				url: "{controller}/{*slug}",
				defaults: new { controller = "Team", action = "Index", slug = UrlParameter.Optional }
			);
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
