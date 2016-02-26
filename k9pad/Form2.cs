using System;
using System.Windows.Forms;

namespace k9pad
{
    public partial class Form2 : Form
    {
        public static string strGoToLine;
        public Form2()
        {
            InitializeComponent();           
            textBoxGoTo.Select();
            textBoxGoTo.Focus();
        }

        //Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Go To when pressed enter or esc
        private void textBoxGoTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //Execute Go To
        private void btnGoTo_Click(object sender, EventArgs e)
        {
            strGoToLine = textBoxGoTo.Text;
            this.Close();
        }

        private void textBoxGoTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGoTo.PerformClick();
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
