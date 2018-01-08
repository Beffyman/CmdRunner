using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmdRunner.Pages
{
	public class IndexModel : PageModel
	{

		public IList<string> Configurations { get; set; }
		public string ScriptRun { get; set; }

		public void OnGet()
		{

		}
	}
}