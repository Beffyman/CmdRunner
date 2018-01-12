using CmdRunner.Dtos.Configuration;
using CmdRunner.Dtos.Path;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CmdRunner.Extensions
{
	public static class HtmlHelperExtensions
	{

		#region Location Configuration Dropdown

		public static IHtmlContent DropDownForConfiguration<TModel>(
			this IHtmlHelper<TModel> html,
			Expression<Func<TModel, IList<ScriptLocationConfiguration>>> expression,
			Guid? selected = null,
			string @class = null,
			bool includeNone = true)
		{
			var list = expression.Compile().Invoke(html.ViewData.Model);

			var str = Build_DropDownForConfiguration(list, expression.Body.ToString(), selected, @class, includeNone);

			return new HtmlString(str.ToString());
		}

		public static IHtmlContent DropDownForConfiguration<TModel>(
			this IHtmlHelper<TModel> html,
			IList<ScriptLocationConfiguration> list,
			string @id,
			Guid? selected = null,
			string @class = null,
			bool includeNone = true)
		{
			var str = Build_DropDownForConfiguration(list, id, selected, @class, includeNone);

			return new HtmlString(str.ToString());
		}


		internal static StringBuilder Build_DropDownForConfiguration(
			IList<ScriptLocationConfiguration> list,
			string @id,
			Guid? selected = null,
			string @class = null,
			bool includeNone = true)
		{
			if (list == null)
			{
				throw new ArgumentNullException($"Provided list cannot be null");
			}

			@class = $"{@class ?? string.Empty} form-control dropdown";

			var str = new StringBuilder();
			//  < label for= "sel1" > Select list:</ label >
			str.AppendLine($@"<select id=""{@id}"" class=""{@class}"">");
			if (includeNone)
			{
				str.AppendLine($@"<option value="""" {(!selected.HasValue ? "selected" : string.Empty)} disabled>Select a configuration</option>");
			}
			foreach (var item in list)
			{
				string isSelected = (selected == item.Id ? "selected" : string.Empty);
				str.AppendLine($@"<option {isSelected} value=""{item.Id}"">{item.DisplayName}</option>");
			}
			str.AppendLine($@"</select>");

			return str;
		}
		#endregion Location Configuration Dropdown


		#region Script Configuration List

		public static IHtmlContent ListForPathItem<TModel>(
			this IHtmlHelper<TModel> html,
			Expression<Func<TModel, IEnumerable<IPathItem>>> expression)
		{
			var list = expression.Compile().Invoke(html.ViewData.Model);
			if (list == null)
			{
				throw new ArgumentNullException($"Provided list cannot be null");
			}

			var str = new StringBuilder();
			foreach (var item in list)
			{
				str.Build_ListForPathItem(item);
			}

			return new HtmlString(str.ToString());
		}

		public static IHtmlContent ListForPathItem<TModel>(
			this IHtmlHelper<TModel> html,
			IEnumerable<IPathItem> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException($"Provided list cannot be null");
			}

			var str = new StringBuilder();
			foreach (var item in list)
			{
				str.Build_ListForPathItem(item);
			}

			return new HtmlString(str.ToString());
		}

		internal static void Build_ListForPathItem(this StringBuilder builder, IPathItem item)
		{
			if (item is DirectoryItem dir)
			{
				builder.AppendLine($@"<li>");
				builder.AppendLine($@"<a href=""#{item.Name}"" data-toggle=""collapse"" aria-expanded=""false"">{item.Name}</a>");
				builder.AppendLine($@"<ul class=""collapselist-unstyled"" id=""{item.Name}"">");
				foreach (var child in dir.Children)
				{
					builder.Build_ListForPathItem(child);
				}
				builder.AppendLine($@"</ul>");
				builder.AppendLine($@"</li>");
			}
			else if (item is FileItem)
			{
				builder.AppendLine($@"<li><a href=""#"">{item.Name}</a></li>");
			}
		}

		#endregion Script Configuration List


		public static bool IsDebug(this IHtmlHelper html)
		{
#if DEBUG
			return true;
#else
			return false;
#endif
		}

		public static string AssemblyVersion(this IHtmlHelper html)
		{
			return typeof(HtmlHelperExtensions).Assembly.GetName().Version.ToString();
		}

		public static string GetCurrentDirectory(this IHtmlHelper html)
		{
			return Directory.GetCurrentDirectory();
		}
	}
}
