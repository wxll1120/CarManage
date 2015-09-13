using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Esoft.Framework.Utility.Form
{
    public class ControlUtil
    {
        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <typeparam name="T">数据源泛型</typeparam>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        public static void BindListControl<T>(ListControl control,
            ICollection<T> dataSource, string dataTextField, string dataValueField)
        {
            if (dataSource.Count.Equals(0))
                return;

            if (dataSource is Dictionary<string, string>)
            {
                Dictionary<string, string> dicDataSource =
                    dataSource as Dictionary<string, string>;

                if (dicDataSource != null)
                    BindListControl(control, dicDataSource, dataTextField, dataValueField);

                return;
            }

            List<ListControlItem> list = new List<ListControlItem>();

            Type type = typeof(T);
            PropertyInfo textProperty = type.GetProperty(dataTextField);
            PropertyInfo valueProperty = type.GetProperty(dataValueField);

            if (textProperty == null || valueProperty == null)
                return;

            foreach (T item in dataSource)
            {
                list.Add(new ListControlItem
                        {
                            Text = textProperty.GetValue(item, null).ToString(),
                            Value = valueProperty.GetValue(item, null).ToString()
                        });
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <typeparam name="T">数据源泛型</typeparam>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="selectedIndex">控件选中索引</param>
        public static void BindListControl<T>(ListControl control,
            ICollection<T> dataSource, string dataTextField,
            string dataValueField, int selectedIndex)
        {
            if (dataSource.Count.Equals(0))
                return;

            if (dataSource is Dictionary<string, string>)
            {
                Dictionary<string, string> dicDataSource =
                    dataSource as Dictionary<string, string>;

                if (dicDataSource != null)
                    BindListControl(control, dicDataSource,
                        dataTextField, dataValueField, selectedIndex);

                return;
            }

            List<ListControlItem> list = new List<ListControlItem>();

            Type type = typeof(T);
            PropertyInfo textProperty = type.GetProperty(dataTextField);
            PropertyInfo valueProperty = type.GetProperty(dataValueField);

            if (textProperty == null || valueProperty == null)
                return;

            foreach (T item in dataSource)
            {
                list.Add(new ListControlItem
                {
                    Text = textProperty.GetValue(item, null).ToString(),
                    Value = valueProperty.GetValue(item, null).ToString()
                });
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;

            if (selectedIndex >= 0)
                control.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <typeparam name="T">数据源泛型</typeparam>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="emptyItemText">空项显示文本</param>
        /// <param name="emptyItemValue">空项值</param>
        public static void BindListControl<T>(ListControl control,
            ICollection<T> dataSource, string dataTextField,
            string dataValueField, string emptyItemText, string emptyItemValue)
        {
            if (dataSource.Count.Equals(0))
                return;

            if (dataSource is Dictionary<string, string>)
            {
                Dictionary<string, string> dicDataSource =
                    dataSource as Dictionary<string, string>;

                if (dicDataSource != null)
                    BindListControl(control, dicDataSource,
                        dataTextField, dataValueField, emptyItemText, emptyItemValue);

                return;
            }

            List<ListControlItem> list = new List<ListControlItem>();

            Type type = typeof(T);
            PropertyInfo textProperty = type.GetProperty(dataTextField);
            PropertyInfo valueProperty = type.GetProperty(dataValueField);

            if (textProperty == null || valueProperty == null)
                return;

            foreach (T item in dataSource)
            {
                list.Add(new ListControlItem
                {
                    Text = textProperty.GetValue(item, null).ToString(),
                    Value = valueProperty.GetValue(item, null).ToString()
                });
            }

            if (!string.IsNullOrEmpty(emptyItemText))
            {
                list.Insert(0, new ListControlItem
                {
                    Text = emptyItemText,
                    Value = emptyItemValue
                });
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <typeparam name="T">数据源泛型</typeparam>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="emptyItemText">空项显示文本</param>
        /// <param name="emptyItemValue">空项值</param>
        public static void BindListControl<T>(ListControl control,
            ICollection<T> dataSource, string dataTextField,
            string dataValueField, string emptyItemText, string emptyItemValue,
            int selectedIndex)
        {
            if (dataSource.Count.Equals(0))
                return;

            if (dataSource is Dictionary<string, string>)
            {
                Dictionary<string, string> dicDataSource =
                    dataSource as Dictionary<string, string>;

                if (dicDataSource != null)
                    BindListControl(control, dicDataSource,
                        dataTextField, dataValueField, emptyItemText, emptyItemValue, selectedIndex);

                return;
            }

            List<ListControlItem> list = new List<ListControlItem>();

            Type type = typeof(T);
            PropertyInfo textProperty = type.GetProperty(dataTextField);
            PropertyInfo valueProperty = type.GetProperty(dataValueField);

            if (textProperty == null || valueProperty == null)
                return;

            foreach (T item in dataSource)
            {
                list.Add(new ListControlItem
                {
                    Text = textProperty.GetValue(item, null).ToString(),
                    Value = valueProperty.GetValue(item, null).ToString()
                });
            }

            if (!string.IsNullOrEmpty(emptyItemText))
            {
                list.Insert(0, new ListControlItem
                {
                    Text = emptyItemText,
                    Value = emptyItemValue
                });
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;

            if (selectedIndex >= 0)
                control.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <typeparam name="T">数据源泛型</typeparam>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="emptyItemText">空项显示文本</param>
        /// <param name="emptyItemValue">空项值</param>
        /// <param name="selectedIndex">控件选中索引</param>
        public static void BindListControl<T>(ListControl control, IList<T> dataSource,
            string dataTextField, string dataValueField, string emptyItemText,
            string emptyItemValue, int selectedIndex)
        {
            List<ListControlItem> list = new List<ListControlItem>();

            Type type = typeof(T);
            PropertyInfo textProperty = type.GetProperty(dataTextField);
            PropertyInfo valueProperty = type.GetProperty(dataValueField);

            if (textProperty == null || valueProperty == null)
                return;

            if (!string.IsNullOrEmpty(emptyItemText))
            {
                list.Add(new ListControlItem
                {
                    Text = emptyItemText,
                    Value = emptyItemValue
                });
            }

            foreach (T item in dataSource)
            {
                list.Add(new ListControlItem
                {
                    Text = textProperty.GetValue(item, null).ToString(),
                    Value = valueProperty.GetValue(item, null).ToString()
                });
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;

            if (selectedIndex >= 0)
                control.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型字典数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        public static void BindListControl(ListControl control,
            Dictionary<string, string> dataSource, string dataTextField,
            string dataValueField)
        {
            if (dataSource.Count.Equals(0))
                return;

            List<ListControlItem> list = new List<ListControlItem>();

            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                ListControlItem item = new ListControlItem();

                if (dataTextField.Equals("Key", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Key;
                    item.Value = pair.Value;
                }

                if (dataTextField.Equals("Value", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Value;
                    item.Value = pair.Key;
                }

                list.Add(item);
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型字典数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        public static void BindListControl(ListControl control,
            Dictionary<string, string> dataSource, string dataTextField,
            string dataValueField, int selectedIndex)
        {
            if (dataSource.Count.Equals(0))
                return;

            List<ListControlItem> list = new List<ListControlItem>();

            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                ListControlItem item = new ListControlItem();

                if (dataTextField.Equals("Key", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Key;
                    item.Value = pair.Value;
                }

                if (dataTextField.Equals("Value", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Value;
                    item.Value = pair.Key;
                }

                list.Add(item);
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;

            if (selectedIndex >= 0)
                control.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型字典数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="emptyItemText">空项显示文本</param>
        /// <param name="emptyItemValue">空项值</param>
        public static void BindListControl(ListControl control,
            Dictionary<string, string> dataSource, string dataTextField,
            string dataValueField, string emptyTextField, string emptyValueField)
        {
            if (dataSource.Count.Equals(0))
                return;

            List<ListControlItem> list = new List<ListControlItem>();

            list.Add(new ListControlItem { Text = emptyTextField, Value = emptyValueField });

            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                ListControlItem item = new ListControlItem();

                if (dataTextField.Equals("Key", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Key;
                    item.Value = pair.Value;
                }

                if (dataTextField.Equals("Value", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Value;
                    item.Value = pair.Key;
                }

                list.Add(item);
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;
        }

        /// <summary>
        /// 绑定ListControl控件
        /// </summary>
        /// <param name="control">ListControl控件</param>
        /// <param name="dataSource">泛型字典数据源</param>
        /// <param name="dataTextField">控件显示字段名称</param>
        /// <param name="dataValueField">控件值字段名称</param>
        /// <param name="emptyItemText">空项显示文本</param>
        /// <param name="emptyItemValue">空项值</param>
        public static void BindListControl(ListControl control,
            Dictionary<string, string> dataSource, string dataTextField,
            string dataValueField, string emptyTextField, string emptyValueField,
            int selectedIndex)
        {
            if (dataSource.Count.Equals(0))
                return;

            List<ListControlItem> list = new List<ListControlItem>();

            list.Add(new ListControlItem { Text = emptyTextField, Value = emptyValueField });

            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                ListControlItem item = new ListControlItem();

                if (dataTextField.Equals("Key", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Key;
                    item.Value = pair.Value;
                }

                if (dataTextField.Equals("Value", StringComparison.OrdinalIgnoreCase))
                {
                    item.Text = pair.Value;
                    item.Value = pair.Key;
                }

                list.Add(item);
            }

            control.DisplayMember = "Text";
            control.ValueMember = "Value";
            control.DataSource = list;

            if (selectedIndex >= 0)
                control.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// 根据列表项的显示文本设置ComboBox选中项
        /// </summary>
        /// <param name="control">ComboBox控件</param>
        /// <param name="text">列表项的显示文本</param>
        public static void SetListControlSelectedByText(ComboBox control, string text)
        {
            for (int i = 0; i < control.Items.Count; i++)
            {
                ListControlItem item = control.Items[i] as ListControlItem;
                if (item == null)
                    continue;

                if (item.Text.Equals(text))
                {
                    control.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 根据列表项的值设置ComboBox选中项
        /// </summary>
        /// <param name="control">ComboBox控件</param>
        /// <param name="value">列表项的值</param>
        public static void SetListControlSelectedByValue(ComboBox control, string value)
        {
            for (int i = 0; i < control.Items.Count; i++)
            {
                ListControlItem item = control.Items[i] as ListControlItem;
                if (item == null)
                    continue;

                if (item.Value.Equals(value))
                {
                    control.SelectedIndex = i;
                    break;
                }
            }
        }















        /// <summary>
        /// 设置DataGridView单元格的Value属性
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        public static void SetDataGridViewCellValue(DataGridView gridView, int rowIndex,
            int columnIndex, object value)
        {
            if (gridView.InvokeRequired)
            {
                gridView.BeginInvoke(new DeletgateSetDataGridViewValue(
                    SetDataGridViewCellValueSync), gridView, rowIndex,
                    columnIndex, value);
            }
            else
            {
                SetDataGridViewCellValueSync(gridView, rowIndex, columnIndex, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="gridView"></param>
        /// <param name="value"></param>
        private static void SetDataGridViewCellValueSync(DataGridView gridView,
            int rowIndex, int columnIndex, object value)
        {
            if (gridView != null && gridView.Rows.Count > rowIndex &&
                gridView.Columns.Count > columnIndex)
            {
                gridView.Rows[rowIndex].Cells[columnIndex].Value = value;
            }
        }

        /// <summary>
        /// 设置 ToolStripProgressBar 的 Value 属性
        /// 多线程安全
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private delegate void DeletgateSetToolProgressBarValue(
            ToolStripProgressBar progressBar, int value);

        private delegate void DeletgateSetProgressBarvalue(ProgressBar progressBar,
            int value);

        /// <summary>
        ///  设置 ToolStripProgressBar 的 Value 属性
        /// 线程安全
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="value"></param>
        private static void SetSetToolProgressBarValueSync(
            ToolStripProgressBar progressBar, int value)
        {
            progressBar.Value = value;
        }

        /// <summary>
        ///  设置 ToolStripProgressBar 的 Value 属性
        /// 线程安全
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="value"></param>
        private static void SetSetProgressBarValueSync(ProgressBar progressBar, int value)
        {
            progressBar.Value = value;
        }

        /// <summary>
        ///  设置 ToolStripProgressBar 的 Value 属性
        ///  多线程安全
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="value"></param>
        public static void SetToolProgressBarValue(ToolStripProgressBar progressBar,
            int value)
        {
            if (null == progressBar) return;
            value = value < 0 ? 0 : value;

            if (progressBar.Owner.InvokeRequired)
            {
                progressBar.Owner.BeginInvoke(
                    new DeletgateSetToolProgressBarValue(SetSetToolProgressBarValueSync),
                    progressBar, value);
            }
            else
            {
                SetSetToolProgressBarValueSync(progressBar, value);
            }
        }

        /// <summary>
        ///  设置 ToolStripProgressBar 的 Value 属性
        ///  多线程安全
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="value"></param>
        public static void SetToolProgressBarValue(ProgressBar progressBar, int value)
        {
            if (null == progressBar) return;
            value = value < 0 ? 0 : value;

            if (progressBar.InvokeRequired)
            {
                progressBar.BeginInvoke(new DeletgateSetProgressBarvalue(
                    SetSetProgressBarValueSync), progressBar, value);
            }
            else
            {
                SetSetProgressBarValueSync(progressBar, value);
            }
        }

        public static void DataGridViewIntegerSort(int sortColumnIndex,
            DataGridView dgv, DataGridViewSortCompareEventArgs e)
        {
            //检查排序列索引
            if (sortColumnIndex < 0 || sortColumnIndex >= dgv.Columns.Count)
                return;

            //引发事件列是否为排序列
            if (!e.Column.Index.Equals(sortColumnIndex))
                return;

            int sortResult = 0;

            e.SortResult = CompareValue(e.CellValue1, e.CellValue2, e.Column.ValueType);
            e.Handled = true;
        }

        private static int CompareValue(object o1, object o2, Type type)
        {
            //这里改成自己定义的比较 
            if (o1 == null)
                return o2 == null ? 0 : -1;
            else if (o2 == null)
                return 1;
            else if (type.IsPrimitive || type.IsEnum)
                return Convert.ToDouble(o1).CompareTo(Convert.ToDouble(o2));
            else if (type == typeof(DateTime))
                return Convert.ToDateTime(o1).CompareTo(o2);
            else
                return String.Compare(o1.ToString().Trim(), o2.ToString().Trim());
        }
    }

    public class ListControlItem
    {
        public ListControlItem()
        {
            Text = string.Empty;
            Value = string.Empty;
        }

        public string Text { get; set; }

        public string Value { get; set; }
    }

    /// <summary>
    /// 设置DataGridView的值
    /// </summary>
    /// <param name="columnIndex"></param>
    /// <param name="gridView"></param>
    /// <param name="value"></param>
    /// <param name="rowIndex"></param>
    public delegate void DeletgateSetDataGridViewValue(DataGridView gridView,
        int rowIndex, int columnIndex, object value);
}
