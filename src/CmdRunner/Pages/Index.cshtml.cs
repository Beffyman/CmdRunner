﻿using System;
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
	public class IndexModel : PageModel
	{
		private readonly ISettingsManager _settings;
		private readonly IFileManager _fileManager;
		public IEnumerable<IPathItem> ConfgurationFileItems { get; set; }

		public IndexModel(
			ISettingsManager settings,
			IFileManager fileManager)
		{
			_settings = settings;
			_fileManager = fileManager;
		}

		public async Task OnGetAsync()
		{
			LoadFileItems();

			await Task.CompletedTask;
		}

		private void LoadFileItems()
		{
			if (Directory.Exists(_settings.CurrentConfiguration?.FolderLocation))
			{
				ConfgurationFileItems =
					_fileManager.GetItems(
					_settings.CurrentConfiguration.FolderLocation,
					_settings.CurrentConfiguration?.Filters);
			}
			else
			{
				ConfgurationFileItems = new IPathItem[] { };
			}
		}

		[HttpGet]
		public IActionResult OnGetList()
		{
			LoadFileItems();

			var sb = new StringBuilder();
			foreach (var item in ConfgurationFileItems ?? new IPathItem[] { })
			{
				sb.Build_ListForPathItem(item);
			}

			return Content(sb.ToString(), "text/html");
		}

		[HttpPost]
		public IActionResult OpenSettings()
		{
			return RedirectToPage("ManageSettings");
		}

	}
}