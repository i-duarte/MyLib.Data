using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLib.Web.Common
{
    public class WebPage : Page
    {
        protected CultureInfo CurrentCulture { get; set; }

        protected string AppPath
            => Server.MapPath("/");

        protected bool IsEmpty(TextBox txt)
            => string.IsNullOrEmpty(txt.Text);

        protected bool IsEmpty(
            HiddenField hf
        ) =>
            string.IsNullOrEmpty(hf.Value);

        public WebPage(CultureInfo ci)
        {
            CurrentCulture = ci;
        }

        protected string GetPhysicalDir(string nombre)
        {
            if (Request.PhysicalApplicationPath == null)
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
            IEnumerable<int> lista
        ) =>
            lista.Select(
                (tl) =>
                new DataItem()
                {
                    Id = tl, 
                    Description = ToString(tl)
                }
            );

        protected IEnumerable<DataItem> GetDataItemEnumerable(
            IEnumerable<(int, string)> lista
        ) =>
            lista.Select(
                (tupla) =>
                new DataItem()
                {
                    Id = tupla.Item1,
                    Description = tupla.Item2
                }
            );

        protected IEnumerable<DataItem> GetDataItemEnumerable(
            IEnumerable<string> lista
        ) =>
            lista.Select(
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
            lista.Select(
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
            lista.Select(
                (tl, i) =>
                new DataItem()
                {
                    Id = tl.Item1,
                    Description = tl.Item2
                }
            );

        protected void DescargarCsv(
            string fileName
            , string csvData
        )
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = 
                "applicatiion/octet-stream";

            Response.AppendHeader(
                "Content-Disposition"
                , $"attachment; filename={fileName};"
            );

            Response.Write(csvData);
            Response.Flush();
            Response.End();
        }

        protected void RegisterStartupScript(
            string key
            , string script
            , bool addScriptTags = true
        ) =>
            ScriptManager
                .RegisterStartupScript(
                    this
                    , GetType()
                    , key
                    , script
                    , addScriptTags
                );

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

        public void AddAjaxScript(
            string key
            , string script
            , Control control
            , bool addScriptTags = true
        ) =>
            ScriptManager
                .RegisterClientScriptBlock( 
                    control
                    , control.GetType()
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


        protected DateTime ToDateTime(
            string f
        ) =>
            Convert.ToDateTime(f, CurrentCulture);
        protected byte ToByte(
            string b
        ) =>
            Convert.ToByte(b, CurrentCulture);
        protected short ToInt16(
            string n
        ) =>
            Convert.ToInt16(n, CurrentCulture);
        protected int ToInt32(
            string n
        ) =>
            Convert.ToInt32(n, CurrentCulture);
        protected long ToInt64(
            string n
        ) =>
            Convert.ToInt64(n, CurrentCulture);

        protected string ToString(
            object s
        ) => 
            Convert.ToString(s, CurrentCulture);

        protected string ToString(
            int s
        ) =>
            Convert.ToString(s, CurrentCulture);
    }
}
