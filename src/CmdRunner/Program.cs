using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CmdRunner
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

			await WebHost.CreateDefaultBuilder(args)
				.UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
				.UseElectron(args)
				.UseStartup<Startup>()
				.Build()
				.RunAsync();
		}

	}
}
