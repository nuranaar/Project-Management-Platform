using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMP.Helpers
{
	public class Password
	{
		public int Id { get; set; }

		public string Current { get; set; }

		public string New { get; set; }

		public string Confirm { get; set; }

	}
}