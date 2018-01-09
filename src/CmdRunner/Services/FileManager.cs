using CmdRunner.Dtos.Path;
using CmdRunner.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Services
{
	public interface IFileManager : ISingletonInjectable
	{
		IEnumerable<IPathItem> GetItems(string location, string[] filters = null);

	}
	public class FileManager : IFileManager
	{
		public FileManager()
		{

		}

		public IEnumerable<IPathItem> GetItems(string location, string[] filters = null)
		{
			var fastFilters = new HashSet<string>(filters);
			foreach (var item in Directory.EnumerateFileSystemEntries(location, "*"))
			{
				if (Directory.Exists(item))//It a directory
				{
					var dir = new DirectoryItem(item);

					dir.Children = GetItems(item, filters);
					yield return dir;
				}
				else if (File.Exists(item))//Is a file
				{
					//Check if file exists in filters
					if (!fastFilters.Any()
						|| fastFilters.Contains(Path.GetExtension(item), StringComparer.CurrentCultureIgnoreCase))
					{
						yield return new FileItem(item);
					}
				}
			}
		}


	}
}
