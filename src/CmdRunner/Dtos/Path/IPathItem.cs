using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Dtos.Path
{
	public interface IPathItem
	{
		string Name { get; }
		string FullPath { get; set; }
	}
}
