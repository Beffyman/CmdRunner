using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Path
{
	public class FileItem : IPathItem
	{
		public string Name => System.IO.Path.GetFileNameWithoutExtension(FullPath);

		public string FullPath { get; set; }

		public FileItem(string fullPath)
		{
			FullPath = fullPath;
		}
	}
}
