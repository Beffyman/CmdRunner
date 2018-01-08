using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CmdRunner.Configuration;

namespace CmdRunner
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			string contentDirectory = Directory.GetCurrentDirectory();
#if DEBUG
			contentDirectory = Directory.GetCurrentDirectory();
#else
			contentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
#endif


			Directory.SetCurrentDirectory(contentDirectory);

			await WebHost.CreateDefaultBuilder(args)
				.UseContentRoot(contentDirectory)
				.TryElectronize(args)
				.UseStartup<Startup>()
				.Build()
				.RunAsync();
		}

	}
}
