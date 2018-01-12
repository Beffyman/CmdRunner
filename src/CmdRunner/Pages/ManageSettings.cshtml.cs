using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdRunner.Dtos.Configuration;
using CmdRunner.Dtos.Path;
using CmdRunner.Extensions;
using CmdRunner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CmdRunner.Pages
{
	public class ManageSettingsModel : PageModel
	{
		public ISettingsManager Settings;

		public ManageSettingsModel(ISettingsManager settings)
		{
			Settings = settings;
		}

		public async Task OnGetAsync()
		{
			await Settings.LoadAsync();
			await Task.CompletedTask;
		}


		public async Task OnPostAsync()
		{
			await Settings.SaveAsync();
		}
	}
}