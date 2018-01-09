using CmdRunner.Attributes.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Configuration
{
	public class ScriptLocationConfiguration
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		[Tooltip("Display name for this configuration")]
		public string DisplayName { get; set; }
		[Tooltip("Folder that contains the scripts")]
		public string FolderLocation { get; set; }
		[Tooltip("File extension filters for what scripts to show")]
		public string[] Filters { get; set; }
		[Tooltip("Include subfolders in the list")]
		public bool IncludeSubFolders { get; set; }
	}
}
