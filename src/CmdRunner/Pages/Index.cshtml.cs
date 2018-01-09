using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmdRunner.Dtos.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CmdRunner.Pages
{
	public class IndexModel : PageModel
	{
		public string ScriptRun { get; set; }

		public async Task OnGetAsync()
		{
			await Task.CompletedTask;
		}


	}
}