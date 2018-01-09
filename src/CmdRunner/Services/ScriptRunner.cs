using CmdRunner.Dtos.Configuration;
using CmdRunner.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Services
{

	public interface IScriptRunner : IScopedInjectable
	{

	}

	public class ScriptRunner : IScriptRunner
	{
		public ScriptRunner()
		{

		}

		public void Run(ScriptConfiguration config)
		{

		}


	}
}
