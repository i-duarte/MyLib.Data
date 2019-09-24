using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLib.Web.Controls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
	public class DropDownList : WebControl
	{
		private const string OptionTemplate =
			"<option value=\"{value}\" selected>{text}</ option >";
		private const string Template =
			@"<select>
			{options}
			</ select >
			";

		public object DataSource { get; set; }

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				var s = (string)ViewState["Text"];
				return string.IsNullOrEmpty(s) ? string.Empty : s;
			}

			set => ViewState["Text"] = value;
		}

		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(Text);
		}
	}
}
