using CmdRunner.Attributes.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Configuration
{
	public class ScriptApplicationConfiguration
	{
		[Tooltip("Name to display for this application")]
		public string DisplayName { get; set; }
		[Tooltip("Location to the application.  Relative to scripts and absolute locations are allowed.")]
		public string FileLocation { get; set; }
		[Tooltip("Arguments that will always be provided whenever a script is run with this application.")]
		public string[] ProvidedArgs { get; set; }
		[Tooltip("File extensions associated with this application")]//If it can't be found out from the OS.
		public string[] AssociatedExtensions { get; set; }
	}
}
