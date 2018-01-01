using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			await WebHost.CreateDefaultBuilder(args)
				//.UseElectron(args)
				.UseStartup<Startup>()
				.Build()
				.RunAsync();
		}

	}
}
