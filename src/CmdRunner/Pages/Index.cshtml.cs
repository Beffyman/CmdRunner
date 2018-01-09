using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CmdRunner.Dtos.Configuration;
using CmdRunner.Dtos.Path;
using CmdRunner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CmdRunner.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ISettings _settings;
		private readonly IFileManager _fileManager;

		public IndexModel(
			ISettings settings,
			IFileManager fileManager)
		{
			_settings = settings;
			_fileManager = fileManager;
		}

		public IEnumerable<IPathItem> ConfgurationFileItems { get; set; }

		public async Task OnGetAsync()
		{
			if (Directory.Exists(_settings.Location))
			{
				ConfgurationFileItems = _fileManager.GetItems(_settings.Location, _settings.CurrentConfiguration.Filters);
			}

			await Task.CompletedTask;
		}


	}
}