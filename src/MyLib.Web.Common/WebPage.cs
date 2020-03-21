using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLib.Web.Common
{
	public class WebPage : Page
	{
		protected string AppPath 
			=> Server.MapPath("/");

		protected bool IsEmpty(TextBox txt)
			=> string.IsNullOrEmpty(txt.Text);

		protected string GetPhysicalDir(string nombre)
		{
			if(Request.PhysicalApplicationPath == null)
			{
				throw new MyLibWebRequestException(
					"No se encontrol el path de la aplicacion en el servidor"
				);
			}

			var dir =
				Path.Combine(
					Request.PhysicalApplicationPath
					, nombre
				);
			ForceExistsDir(dir);
			return dir;
		}

		private void ForceExistsDir(string dir)
		{
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
		}

		protected IEnumerable<DataItem> GetDataItemEnumerable(
			string[] lista
		) =>
			lista
				.Select(
					(tl, i) =>
						new DataItem()
						{
							Id = i + 1,
							Description = tl
						}
				);
		
		protected IEnumerable<DataItem> GetDataItemEnumerable(
			(bool, string)[] lista
		) =>
			lista
				.Select(
					(tl, i) =>
						new DataItem()
						{
							Id = tl.Item1,
							Description = tl.Item2
						}
				);

		protected IEnumerable<DataItem> GetDataItemEnumerable(
			(string, string)[] lista
		) => 
			lista
			.Select(
				(tl, i) =>
				new DataItem() {
					Id = tl.Item1
					, Description = tl.Item2
				}
			);

		protected void DescargarCsv(
			string fileName
			, string csvData
		)
		{
			Response.Clear();
			Response.ContentType = "text/csv";
			Response.AppendHeader(
				"Content-Disposition"
				, $"attachment; filename={fileName}"
			);
			Response.Write(csvData);
			Response.Flush();
			Response.End();
		}

		protected void RegisterPostBack(
			Control control
		) 
			=> 
			ScriptManager
			.GetCurrent(Page)
			.RegisterPostBackControl(control);

		public void AddAjaxScript(
			string key
			, string script
			, bool addScriptTags = true
		) => 
			ScriptManager
				.RegisterClientScriptBlock(
					this
					, GetType()
					, key
					, script
					, addScriptTags
				);

		public void SetCookie(
			string name,
			string value
		)
		{
			var cookie = Page.Response.Cookies[name];
			if (cookie != null)
			{
				cookie.Value = value;
			}
		}

		public string GetCookie(
			string name
		) => 
			Page
			.Request
			.Cookies[name]
			?.Value 
			?? ""
			;
	}
}
