﻿using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner
{
	public static class Electronize
	{
		public static string Electron_App = Environment.GetEnvironmentVariable(nameof(Electron_App));
		public static bool IsElectron
		{
			get
			{
				bool.TryParse(Electron_App, out bool isElectron);
				return isElectron;
			}
		}

		public static IWebHostBuilder TryElectronize(this IWebHostBuilder builder, string[] args)
		{
			if (IsElectron)
			{
				return builder.UseElectron(args);
			}
			else
			{
				return builder;
			}
		}


		public static async Task TryElectronOnStart()
		{
			if (IsElectron)
			{
				await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
				{
					DarkTheme = true,
					AutoHideMenuBar = true,
					Title = $"{typeof(Electronize).Namespace} - V{typeof(Electronize).Assembly.GetName().Version}"
				});
			}
		}

	}
}
