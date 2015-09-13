using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;

namespace Esoft.Framework.Utility.Common
{
    public class WebUtil
    {
        public static void BindListControl<T>(ListControl control, 
            ICollection<T> dataSource, string dataTextField, string dataValueField)
        {
            control.DataSource = dataSource;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();
        }

        public static void BindListControl<T>(ListControl control, 
            ICollection<T> dataSource, string dataTextField,
            string dataValueField, int selectedIndex)
        {
            control.DataSource = dataSource;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();

            if (selectedIndex < control.Items.Count)
                control.SelectedIndex = selectedIndex;
        }


        public static void BindListControl<T>(ListControl control, 
            IList<T> dataSource, string dataTextField, string dataValueField,
            string emptyItemText, string emptyItemValue)
        {
            control.DataSource = dataSource;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();

            ListItem item = new ListItem(emptyItemText, emptyItemValue);
            control.Items.Insert(0, item);
        }

        public static void BindListControl<T>(ListControl control, IList<T> dataSource, 
            string dataTextField, string dataValueField,
            string emptyItemText, string emptyItemValue, int selectedIndex)
        {
            control.DataSource = dataSource;
            control.DataTextField = dataTextField;
            control.DataValueField = dataValueField;
            control.DataBind();

            ListItem item = new ListItem(emptyItemText, emptyItemValue);
            control.Items.Insert(0, item);

            if (selectedIndex < control.Items.Count)
                control.SelectedIndex = selectedIndex;
        }


        public static void SetListControlSelectedByValue(ListControl control, 
            string value)
        {
            for (int i = 0; i < control.Items.Count; i++)
            {
                if (control.Items[i].Value.Equals(value))
                {
                    control.SelectedIndex = i;
                    break;
                }
            }
        }

        public static void SetListControlSelectedByText(ListControl control, 
            string text)
        {
            for (int i = 0; i < control.Items.Count; i++)
            {
                if (control.Items[i].Text.Equals(text))
                {
                    control.SelectedIndex = i;
                    break;
                }
            }
        }

        public static void OpenWindow(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            System.Web.UI.Page page = HttpContext.Current.Handler as System.Web.UI.Page;
            if (page == null)
                return;

            System.Web.UI.ClientScriptManager cs = page.ClientScript;

            if (!cs.IsClientScriptBlockRegistered(page.GetType(), "OpenWindow"))
            {
                cs.RegisterStartupScript(page.GetType(), "OpenWindow",
                   "window.open('" + url + "');", true);
            }
        }

        /// <summary>
        /// 数据绑定过滤
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string BindingFilter(object data)
        {
            if (data == null)
                return string.Empty;

            return data.ToString();
        }
        /// <summary>
        /// 数据绑定过滤
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string BindingFilter(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            return data;
        }

        /// <summary>
        /// 将服务端Url转换为在请求客户端可用的 Url
        /// </summary>
        /// <param name="url">服务端Url</param>
        /// <returns></returns>
        public static string ResolveUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            return System.Web.VirtualPathUtility.ToAbsolute(url);
        }

        /// <summary>
        /// 将服务端Url转换为在请求客户端可用的 Url
        /// </summary>
        /// <param name="url">服务端Url</param>
        /// <returns></returns>
        public static string ResolveUrl(object url)
        {
            if (url == null)
                return string.Empty;

            return System.Web.VirtualPathUtility.ToAbsolute(url.ToString());
        }
    }
}
