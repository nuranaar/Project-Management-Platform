using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMP.Filter
{
	public class Auth:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (HttpContext.Current.Session["signin"] == null)
			{
				filterContext.Result = new RedirectResult("~/sign");
				return;
			}
			base.OnActionExecuting(filterContext);
		}
	}
}