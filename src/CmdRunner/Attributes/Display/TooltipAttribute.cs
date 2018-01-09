using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdRunner.Attributes.Display
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class TooltipAttribute : Attribute
	{
		public string Text { get; }
		public TooltipAttribute(string text)
		{
			Text = text;
		}
	}
}
