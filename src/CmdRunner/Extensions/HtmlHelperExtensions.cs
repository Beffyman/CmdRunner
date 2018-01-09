using CmdRunner.Dtos.Configuration;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CmdRunner.Extensions
{
	public static class HtmlHelperExtensions
	{
		public static IHtmlContent DropDownForConfiguration<TModel>(
			this IHtmlHelper<TModel> html,
			Expression<Func<TModel, IList<ScriptLocationConfiguration>>> expression,
			Guid? selected = null,
			string @class = null)
		{
			var list = expression.Compile().Invoke(html.ViewData.Model);

			var str = Build_DropDownForConfiguration(list, expression.Body.ToString(), selected, @class);

			return new HtmlString(str.ToString());
		}

		public static IHtmlContent DropDownForConfiguration<TModel>(
			this IHtmlHelper<TModel> html,
			IList<ScriptLocationConfiguration> list,
			string @id,
			Guid? selected = null,
			string @class = null)
		{
			var str = Build_DropDownForConfiguration(list, id, selected, @class);

			return new HtmlString(str.ToString());
		}


		private static StringBuilder Build_DropDownForConfiguration(
			IList<ScriptLocationConfiguration> list,
			string @id,
			Guid? selected = null,
			string @class = null)
		{
			if (list == null)
			{
				throw new ArgumentNullException($"Provided list cannot be null");
			}

			if (@class == null)
			{
				@class = "form-control";
			}

			var str = new StringBuilder();
			//  < label for= "sel1" > Select list:</ label >
			str.AppendLine($@"<select id=""{@id}"" class=""{@class}"">");
			str.AppendLine($@"<option value="""" {(!selected.HasValue ? "selected" : string.Empty)} disabled>Select a configuration</option>");
			foreach (var item in list)
			{
				string isSelected = (selected == item.Id ? "selected" : string.Empty);
				str.AppendLine($@"<option {isSelected} value=""{item.Id}"">{item.DisplayName}</option>");
			}
			str.AppendLine($@"</select>");

			return str;
		}
	}
}
