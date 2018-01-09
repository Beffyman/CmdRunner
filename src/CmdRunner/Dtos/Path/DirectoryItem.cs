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

		public IList<IPathItem> Children { get; set; }

		public void PopulateChildren()
		{
			Children?.Clear();
			if (Children == null)
			{
				Children = new List<IPathItem>();
			}

			foreach (var item in Directory.EnumerateFiles(FullPath))
			{
				var fileAttributes = File.GetAttributes(item);
				if ((fileAttributes & FileAttributes.Directory) != 0)
				{
					Children.Add(new DirectoryItem(item));
				}
				else
				{
					Children.Add(new FileItem(item));
				}
			}
		}
	}
}
