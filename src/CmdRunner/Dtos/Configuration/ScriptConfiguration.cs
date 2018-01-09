using CmdRunner.Attributes.Display;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Configuration
{
	public class ScriptConfiguration
	{

		[JsonIgnore]
		public ScriptLocationConfiguration ParentConfiguration { get; set; }

		[Tooltip("Name to display inside the script panel")]
		public string DisplayName { get; set; }
		[Tooltip("Arguments provided whenever this script is ran")]
		public string[] Arguments { get; set; }
		[Tooltip("The script will run in a window with the associated script application, isolating it from the CmdRunner")]
		public bool OpenInWindow { get; set; }
	}
}
