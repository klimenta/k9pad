using System;
using System.Text;
using System.Windows.Forms;

namespace k9pad
{
    public partial class Form4 : Form
    {
        private bool bReplaceMatchCase;
        private StringComparison scMatchCase;

        public Form4()
        {
            InitializeComponent();
            bReplaceMatchCase = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (Form1.arrTextBox[Form1.intCounter].SelectedText != "")
            {
                Form1.arrTextBox[Form1.intCounter].SelectedText = textWith.Text;
            } else
            {
                MessageBox.Show("Click Find Next first", "Find");
            }
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            int selStart;
            if (String.IsNullOrEmpty(textWhat.Text))
            {
                return;
            }
            if (bReplaceMatchCase)
            {
                selStart = Form1.arrTextBox[Form1.intCounter].Text.IndexOf(textWhat.Text, Form1.arrTextBox[Form1.intCounter].SelectionStart + 1, StringComparison.Ordinal);
            }
            else
            {
                selStart = Form1.arrTextBox[Form1.intCounter].Text.IndexOf(textWhat.Text, Form1.arrTextBox[Form1.intCounter].SelectionStart + 1, StringComparison.OrdinalIgnoreCase);

            }
            if (selStart == -1)
            {
                MessageBox.Show(textWhat.Text + " was not found", "Replace");
                return;
            }
            Form1.arrTextBox[Form1.intCounter].Select(selStart, textWhat.Text.Length);
            Form1.arrTextBox[Form1.intCounter].HideSelection = false;
            Form1.arrTextBox[Form1.intCounter].ScrollToCaret();            
        }

        private string ReplaceString(string strInputString, string strOldValue, string strNewValue, StringComparison scComparison)
        {
            StringBuilder sbTempString = new StringBuilder();

            int previousIndex = 0;
            int index = strInputString.IndexOf(strOldValue, scComparison);
            while (index != -1)
            {
                sbTempString.Append(strInputString.Substring(previousIndex, index - previousIndex));
                sbTempString.Append(strNewValue);
                index += strOldValue.Length;

                previousIndex = index;
                index = strInputString.IndexOf(strOldValue, index, scComparison);
            }
            sbTempString.Append(strInputString.Substring(previousIndex));

            return sbTempString.ToString();
        }

        private void chkMatchCase_CheckStateChanged(object sender, EventArgs e)
        {
            bReplaceMatchCase = !bReplaceMatchCase;
            if (bReplaceMatchCase)
            {
                scMatchCase = StringComparison.Ordinal;
            }
            else
            {
                scMatchCase = StringComparison.OrdinalIgnoreCase;
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            Form1.arrTextBox[Form1.intCounter].Text = ReplaceString(Form1.arrTextBox[Form1.intCounter].Text, textWhat.Text.Trim(), textWith.Text.Trim(), scMatchCase);
        }

        private void textWhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFindNext.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
