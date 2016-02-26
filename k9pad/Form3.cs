using System;
using System.Windows.Forms;

namespace k9pad
{
    public partial class Form3 : Form
    {       
        public static string strFind;
        public static bool bFindMatchCase;

        public Form3()
        {
            InitializeComponent();
            bFindMatchCase = false;
        }

        //Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Find
        private void btnFind_Click(object sender, EventArgs e)
        {
            int intSelStart;
            strFind = textFind.Text;
            if (bFindMatchCase)
            {
                try
                {
                    intSelStart = Form1.arrTextBox[Form1.intCounter].Text.IndexOf(strFind, StringComparison.Ordinal);
                }
                catch (Exception)
                {
                    intSelStart = -1;
                }
            }
            else
            {
                try
                {
                    intSelStart = Form1.arrTextBox[Form1.intCounter].Text.IndexOf(strFind, StringComparison.OrdinalIgnoreCase);
                }
                catch (Exception)
                {
                    intSelStart = -1;
                }
            }
            if (intSelStart == -1)
            {
                MessageBox.Show(strFind + " was not found", "Find");
                return;
            }
            Form1.arrTextBox[Form1.intCounter].Select(intSelStart, strFind.Length);
            Form1.arrTextBox[Form1.intCounter].ScrollToCaret();
            this.Close();
        }

        //Match case
        private void chkMatchCase_CheckStateChanged(object sender, EventArgs e)
        {
            bFindMatchCase = !bFindMatchCase;
        }

        private void textFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind.PerformClick();             
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
