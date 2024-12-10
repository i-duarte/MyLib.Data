using MyLib.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyLib.Web.Extensions
{
    public enum AlertType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    public static class UserInterface
    {
        private static string GetAlert(AlertType alertType)
        {
            switch (alertType)
            {
                case AlertType.Danger:
                    return "danger";
                case AlertType.Dark:
                    return "dark";
                case AlertType.Info:
                    return "info";
                case AlertType.Light:
                    return "light";
                case AlertType.Primary:
                    return "primary";
                case AlertType.Secondary:
                    return "secondary";
                case AlertType.Success:
                    return "success";
                case AlertType.Warning:
                    return "warning";
                default:
                    return "";
            }
        }

        public static void ShowWarning(
            this HtmlGenericControl control
            , string msg
        ) => control.ShowAlert(msg, AlertType.Warning);

        public static void ShowDanger(
            this HtmlGenericControl control
            , string msg
        ) => control.ShowAlert(msg, AlertType.Danger);

        public static void ShowSucces(
            this HtmlGenericControl control
            , string msg
        ) => control.ShowAlert(msg, AlertType.Success);

        public static void ShowSecondary(
            this HtmlGenericControl control
            , string msg
        ) => control.ShowAlert(msg, AlertType.Secondary);

        public static void ShowAlert(
            this HtmlGenericControl control
            , string msg
            , AlertType alertType
        )
        {
            control.Visible = true;
            control.Attributes.Add("class", $"alert alert-{GetAlert(alertType)}");
            control.InnerHtml = msg;
        }

        private static void SeleccionaElPrimero(
            this ListControl cmb
        )
        {
            if (cmb.Items.Count > 0)
            {
                cmb.Items[0].Selected = true;
            }
        }

        private static void AddTodos(
            ListControl cmb
            , string todos
        )
        {
            if (!string.IsNullOrEmpty(todos))
            {
                cmb.Items.Insert(0, new ListItem(todos, "0"));
            }
        }

        public static DropDownList Fill(
            this DropDownList cmb
            , object dataSource
            , string todos = ""
        ) =>
            Fill(cmb, dataSource, "", "", todos);

        public static DropDownList Fill(
            this DropDownList cmb
            , IEnumerable<DataItem> dataSource
            , string todos = ""
        )
            => Fill(cmb, dataSource, "Description", "Id", todos);

        public static DropDownList Fill(
            this DropDownList cmb
            , object dataSource
            , string dataTextField
            , string dataValueField
            , string todos = ""
        )
        {
            cmb.Items.Clear();
            cmb.SelectedIndex = -1;
            cmb.SelectedValue = null;
            cmb.ClearSelection();
            cmb.DataSource = dataSource;
            cmb.DataTextField = dataTextField;
            cmb.DataValueField = dataValueField;
            cmb.DataBind();
            AddTodos(cmb, todos);
            cmb.SeleccionaElPrimero();
            return cmb;
        }

        public static void SelectByText(this DropDownList source, string text)
        {
            var item = source.Items.FindByText(text);
            if(item != null)
            {
                item.Selected = true;
            }
        }

        public static string SelectedText(
            this DropDownList cmb
            , string defaultValue = ""
        )
            => cmb.SelectedItem?.Text
               ?? defaultValue;

        public static DropDownList Select(
            this DropDownList source
            , object obj
        )
        {
            try
            {
                source.SelectedValue = Convert.ToString(obj);
            }
            catch { }

            return source;
        }

        public static void ConfiguraPaginador(
            GridView gv
            , int itemsXPagina
        )
        {
            if (itemsXPagina == 0)
            {
                gv.AllowPaging = false;
            }
            else
            {
                gv.AllowPaging = true;
                gv.PageIndex = 0;
                gv.PageSize = itemsXPagina;
            }
        }

        public static T SelectedValue<T>(
            this DropDownList cmb
            , T defaultValue
        )
            => cmb.SelectedItem == null
                ? defaultValue
                : (T)Convert
                    .ChangeType(
                        cmb
                            .SelectedItem
                            .Value
                        , typeof(T)
                    );
    }
}
