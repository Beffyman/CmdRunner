using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Path
{
	public class DirectoryItem : IPathItem
	{
		public DirectoryItem(string fullPath)
		{
			FullPath = fullPath;
		}

		public string Name => System.IO.Path.GetFileNameWithoutExtension(FullPath);

		public string FullPath { get; set; }

		public IEnumerable<IPathItem> Children { get; set; }
	}
}
