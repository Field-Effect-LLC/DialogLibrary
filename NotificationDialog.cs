using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace DialogLibrary
{
    public partial class NotificationDialog : Form
    {
        public bool DontShowAgain { get; set; }

        [Browsable(true), DefaultValue(false)]
        public bool EscapeCancels { get; set; }

        public static NotificationDialogResult Show(
            string Message,
            string Caption,
            MessageBoxButtons Buttons,
            string DontShowThisAgainText,
            MessageBoxIcon Icon,
            MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1)
        {
            using (NotificationDialog dlg = new NotificationDialog(Message, Caption, Buttons, DontShowThisAgainText, Icon, DefaultButton))
            {
                return dlg.ShowDialog();
            }
        }

        public new NotificationDialogResult ShowDialog()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            uint result = (uint)base.ShowDialog();
            if (this.DontShowAgain == true)
                result = (uint)result | (uint)NotificationDialogResult.DontShowAgain;
            return (NotificationDialogResult)result;
        }

        public NotificationDialog(
            string Message, 
            string Caption, 
            MessageBoxButtons Buttons, 
            string DontShowThisAgainText,
            MessageBoxIcon Icon, 
            MessageBoxDefaultButton DefaultButton)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.Message.Text = Message;
            this.Text = Caption;
            //this.MessageTable.RowStyles[0].Height = this.Message.Height;

            if (Icon != null && Icon != MessageBoxIcon.None)
            {
                PictureBox p = new PictureBox();
                p.Visible = true;
                //p.Dock = DockStyle.Fill;
                p.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                switch (Icon)
                {
                    case MessageBoxIcon.Asterisk:
                        p.Image = SystemIcons.Asterisk.ToBitmap();
                        break;
                    case MessageBoxIcon.Error:
                        p.Image = SystemIcons.Error.ToBitmap();
                        break;
                    case MessageBoxIcon.Exclamation:
                        p.Image = SystemIcons.Exclamation.ToBitmap();
                        break;
                    case MessageBoxIcon.Question:
                        p.Image = SystemIcons.Question.ToBitmap();
                        break;
                }
                p.Size = p.Image.Size;
                //MessageTable.ColumnStyles[0].Width = p.Width;
                p.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //MessageTable.Controls.Add(p, 0, 0);
                MainTable.Controls.Add(p, 0, 0);
            }

            Stack<Control> ButtonStack = new Stack<Control>();
            if (DontShowThisAgainText != null)
            {
                CheckBox DontShowAgain = new CheckBox();
                DontShowAgain.Text = DontShowThisAgainText;
                DontShowAgain.AutoSize = true;
                DontShowAgain.Checked = false;
                //ButtonPanel.Controls.Add(DontShowAgain);
                ButtonStack.Push(DontShowAgain);
                DontShowAgain.DataBindings.Add("Checked", this, "DontShowAgain");
            }
            else
            {
                DontShowAgain = false;
            }

            switch (Buttons)
            {

                case MessageBoxButtons.AbortRetryIgnore:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Abort));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Retry));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Ignore));
                    break;
                case MessageBoxButtons.OK:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.OK));
                    break;
                case MessageBoxButtons.OKCancel:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.OK));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Cancel));
                    break;
                case MessageBoxButtons.RetryCancel:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Retry));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Cancel));
                    break;
                case MessageBoxButtons.YesNo:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Yes));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.No));
                    break;
                case MessageBoxButtons.YesNoCancel:
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Yes));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.No));
                    ButtonStack.Push(ButtonFactory.CreateButton(System.Windows.Forms.DialogResult.Cancel));
                    break;
            }

            int NumButtons = (DontShowThisAgainText == null) ? ButtonStack.Count : ButtonStack.Count - 1;
            Control FocusButton = null;

            while (ButtonStack.Count > 0)
            {
                Control ToAdd = ButtonStack.Pop();

                ButtonPanel.Controls.Add(ToAdd);
                if (ToAdd.GetType() == typeof(Button))
                {
                    switch (DefaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            if (NumButtons == 1) FocusButton = ToAdd; break;
                        case MessageBoxDefaultButton.Button2:
                            if (NumButtons == 2) FocusButton = ToAdd; break;
                        case MessageBoxDefaultButton.Button3:
                            if (NumButtons == 3) FocusButton = ToAdd; break;
                    }
                    NumButtons--;
                }
            }

            if (FocusButton != null)
                FocusButton.Select();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (this.EscapeCancels)
                    {
                        e.Handled = true;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    break;
            }
            base.OnKeyUp(e);
        }
    }

    public enum NotificationDialogResult
    {
        // Summary:
        //     Nothing is returned from the dialog box. This means that the modal dialog
        //     continues running.
        None = 0,
        //
        // Summary:
        //     The dialog box return value is OK (usually sent from a button labeled OK).
        OK = 1,
        //
        // Summary:
        //     The dialog box return value is Cancel (usually sent from a button labeled
        //     Cancel).
        Cancel = 2,
        //
        // Summary:
        //     The dialog box return value is Abort (usually sent from a button labeled
        //     Abort).
        Abort = 3,
        //
        // Summary:
        //     The dialog box return value is Retry (usually sent from a button labeled
        //     Retry).
        Retry = 4,
        //
        // Summary:
        //     The dialog box return value is Ignore (usually sent from a button labeled
        //     Ignore).
        Ignore = 5,
        //
        // Summary:
        //     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes = 6,
        //
        // Summary:
        //     The dialog box return value is No (usually sent from a button labeled No).
        No = 7,
        //
        // Summary:
        //      Dialog result is ORed with this value if "Dont Show This Again" was checked:
        
        DontShowAgain=128,
    }

    internal static class ButtonFactory
    {
        public static Button CreateButton(System.Windows.Forms.DialogResult ButtonType)
        {
            if (ButtonType == DialogResult.None)
                return null;

            Button b = new Button();
            b.DialogResult = ButtonType;

            switch (ButtonType)
            {
                case DialogResult.Abort:
                    b.Text = "Abort"; break;
                case DialogResult.Cancel:
                    b.Text = "Cancel"; break;
                case DialogResult.Ignore:
                    b.Text = "Ignore"; break;
                case DialogResult.No:
                    b.Text = "No"; break;
                case DialogResult.OK:
                    b.Text = "OK"; break;
                case DialogResult.Retry:
                    b.Text = "Retry"; break;
                case DialogResult.Yes:
                    b.Text = "Yes"; break;
                default:
                    throw new ArgumentException();
            }

            return b;
        }
    }

}
