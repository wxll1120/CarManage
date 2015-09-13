using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Esoft.Framework.Utility.Form
{
    public class FormUtil
    {
        public const int WM_NCHITTEST = 0x0084;
        public const int HT_LEFT = 10;
        public const int HT_RIGHT = 11;
        public const int HT_TOP = 12;
        public const int HT_TOPLEFT = 13;
        public const int HT_TOPRIGHT = 14;
        public const int HT_BOTTOM = 15;
        public const int HT_BOTTOMLEFT = 16;
        public const int HT_BOTTOMRIGHT = 17;
        public const int HT_CAPTION = 2;
        
        public static DialogResult ShowDialogForm(System.Windows.Forms.Form form,
            Size formSize, Size maxSize, Size minSize, string formName, 
            System.Windows.Forms.Form parentForm,
            bool minimizeBox, bool maximizeBox, bool controlBox)
        {
            //ContainerForm contextForm = new ContainerForm();

            form.Dock = DockStyle.Fill;
            form.MaximumSize = maxSize;
            form.MinimumSize = minSize;


            //oyxdelete20090217contextForm.Text = "外贸快登手" + EFTMIS.Logic.AssmblyInfo.GetVersonName() + "--" + formName;
            //contextForm.Text = AssmblyInfo.GetVersonName() + "--" + formName;//oyxAdd20090219
            //contextForm.Controls.Add(control);
            //contextForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = maximizeBox;
            form.MinimizeBox = minimizeBox;
            form.ControlBox = controlBox;
            //if (AssmblyInfo.GetAssmblySoft() == AssmblyInfo.DH) //zhangzhiming add20090224
            //{
            //    contextForm.Icon = Properties.Resources.DH;
            //}
            //else
            //{
            //    contextForm.Icon = Properties.Resources.ESoft;
            //}
            form.StartPosition = FormStartPosition.CenterParent;
            if (formSize != Size.Empty)
            {

                form.Size = formSize;
            }

            //释放资源事件
            //contextForm.Disposed += new EventHandler(Form_Disposed);
            DialogResult result = form.ShowDialog(parentForm);
            //DialogResult result = new DialogResult();
            form.Dispose();
            //contextForm.Disposed -= new EventHandler(Form_Disposed);

            return result;
        }

        public static DialogResult ShowDialogControl(Control control, Size formSize,
            String formName, System.Windows.Forms.Form parentForm, Boolean minimizeBox,
            bool maximizeBox, bool controlBox)
        {
            System.Windows.Forms.Form contextForm = new System.Windows.Forms.Form();

            if (formSize != Size.Empty)
                contextForm.Size = formSize;

            contextForm.MaximizeBox = maximizeBox;
            contextForm.MinimizeBox = minimizeBox;
            contextForm.ControlBox = controlBox;
            contextForm.StartPosition = FormStartPosition.CenterParent;

            contextForm.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            //释放资源事件
            DialogResult result = contextForm.ShowDialog(parentForm);
            contextForm.Dispose();

            return result;
        }

        public static DialogResult ShowDialogControl(Control control, Size formSize,
            Size maxSize, Size minSize, String formName, 
            System.Windows.Forms.Form parentForm, Boolean minimizeBox, 
            Boolean maximizeBox, Boolean allowClose, bool controlBox)
        {
            System.Windows.Forms.Form contextForm = new System.Windows.Forms.Form();

            if (formSize != Size.Empty)
                contextForm.Size = formSize;
            
            contextForm.MaximumSize = maxSize;
            contextForm.MinimumSize = minSize;
            contextForm.MaximizeBox = maximizeBox;
            contextForm.MinimizeBox = minimizeBox;
            contextForm.ControlBox = allowClose;
            contextForm.StartPosition = FormStartPosition.CenterParent;

            contextForm.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            //释放资源事件
            DialogResult result = contextForm.ShowDialog(parentForm);
            contextForm.Dispose();

            return result;
        }

        public static DialogResult ShowDialogControl(Control control, Size maxSize, 
            Size minSize, String formName, System.Windows.Forms.Form parentForm, 
            Boolean minimizeBox, Boolean maximizeBox, Boolean allowClose, bool controlBox)
        {
            System.Windows.Forms.Form contextForm = new System.Windows.Forms.Form();

            contextForm.AutoSize = true;
            contextForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            
            contextForm.MaximumSize = maxSize;
            contextForm.MinimumSize = minSize;
            contextForm.Text = formName;
            contextForm.Controls.Add(control);control.Dock = DockStyle.Fill;
            contextForm.MaximizeBox = maximizeBox;
            contextForm.MinimizeBox = minimizeBox;
            contextForm.ControlBox = allowClose;
            contextForm.StartPosition = FormStartPosition.CenterParent;

            

            //释放资源事件
            DialogResult result = contextForm.ShowDialog(parentForm);
            contextForm.Dispose();

            return result;
        }

        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="message">信息文本</param>
        public static DialogResult ShowTipMessageBox(string message)
        {
            return MessageBox.Show(message, "提示", MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

       

        /// <summary>
        /// 弹出选择对话框
        /// </summary>
        /// <param name="strMessage">信息文本</param>
        public static DialogResult ShowChooseMessageBox(string strMessage)
        {
            return MessageBox.Show(strMessage, "选择", MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question);
        }

        /// <summary>
        /// 填出包含是和否的对话框
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns>返回对话框返回值</returns>
        public static DialogResult ShowConfirmMessageBox(string message)
        {
            return MessageBox.Show(message, "提示", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        }
    }
}
