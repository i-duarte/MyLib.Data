using System;
using System.Web.SessionState;
using System.Web.UI;

namespace MyLib.Web.Common
{
    public class WebSession<T>
    {
        private string Nombre { get; set; }
        private HttpSessionState Session { get; set; }

        public WebSession(
            string nombre,
            HttpSessionState session
        ) : this(nombre, session, default)
        {

        }

        public WebSession(
            string nombre
            , HttpSessionState session
            , T defaultValue
        )
        {
            Nombre = nombre;
            Session = session;
            Data = 
                string.IsNullOrEmpty(Convert.ToString(Session[Nombre]))
                ? defaultValue
                : (T)Session[Nombre];
        }


        public WebSession(
            string nombre,
            Page page
        ) : this(nombre, page.Session)
        {            
        }

        public WebSession(
            string nombre
            , Page page
            , T defaultValue
        ) : this(nombre, page.Session, defaultValue)
        {
        }

        public T Data
        {
            get => (T)Session[Nombre];
            set => Session[Nombre] = value;
        }
    }
}
