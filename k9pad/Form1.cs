﻿/*

Notepad clone with 9 tabs for text.
K.Andreev - 2016, Simplified BSD license

*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace k9pad
{
    public partial class Form1 : Form
    {
        //Array of TabPage controls that are containers for the text boxes
        private TabPage[] arrTabPage = new TabPage[9];
        //Array of textbox controls, one for each tab
        public static TextBox[] arrTextBox = new TextBox[9];
        //Array of strings for each tab that holds the filename for each text file
        private string[] strFileName = new string[9];
        //Array of boolean for each tab if the content has changed in any text
        private bool[] bChanged = new bool[9];
        //Temp varaible 
        public static int intCounter = 0;
        //For printing
        private PrintDocument printDoc = new PrintDocument();
        private PageSettings pgSettings = new PageSettings();
        private PrinterSettings prtSettings = new PrinterSettings();
        private string strToPrint;

        public Form1()
        {
            InitializeComponent();            
            //Initialize status line info
            for (int i = 0; i > strFileName.GetLength(0); i++)
            {
                strFileName[i] = null;
                bChanged[i] = false;
            }
            //Create each tab page and change the properties
            for (int i = 0; i < arrTabPage.GetLength(0); i++)
            {                
                intCounter = i + 1;
                arrTabPage[i] = new TabPage();
                arrTabPage[i].Name = "tabPage" + intCounter.ToString();
                arrTabPage[i].Text = intCounter.ToString();
                tabControl1.TabPages.Add(arrTabPage[i]);
                arrTabPage[i].Parent = tabControl1;
                arrTabPage[i].Dock = DockStyle.Fill;
            }
            //Create each text box and change the properties
            for (int i = 0; i < arrTextBox.GetLength(0); i++)
            {
                intCounter = i + 1;
                arrTextBox[i] = new TextBox();
                arrTextBox[i].Text = arrTextBox[i].Name;
                arrTabPage[i].Controls.Add(arrTextBox[i]);
                arrTextBox[i].Multiline = true;
                arrTextBox[i].MaxLength = 0;
                arrTextBox[i].AcceptsTab = true;
                arrTextBox[i].AllowDrop = true;
                arrTextBox[i].WordWrap = true;
                arrTextBox[i].ScrollBars = ScrollBars.Both;
                arrTextBox[i].Dock = DockStyle.Fill;
                arrTextBox[i].Font = new Font("Courier New", 10, FontStyle.Regular);
                arrTextBox[i].TextChanged += new EventHandler(Text_Changed);
                arrTextBox[i].KeyUp += new KeyEventHandler(Key_Up);
                arrTextBox[i].DragEnter += new DragEventHandler(TextBox_DragEnter);
                arrTextBox[i].DragDrop += new DragEventHandler(TextBox_DragDrop);                
            }
            arrTextBox[0].Select();
            arrTextBox[0].Focus();
            //Update status labels at the bottom
            toolStripStatusLabelFontName.Text = "FTN: " + fontDialog1.Font.Name;
            toolStripStatusLabelFontSize.Text = "FTS: " + fontDialog1.Font.Size;
            toolStripStatusLabelFileChanged.Text = "FC: F";
            //Attach the print handler
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            //Get command line args - used when you double click a text file            
            string[] args = Environment.GetCommandLineArgs();
            //Return if no input argument
            if (args.Length != 2) return;
            try
            {
                if (args[1] != null && args[1].Length > 0)
                {
                    if (File.Exists(args[1]))
                    {
                        intCounter = 0;
                        myOpenFile(args[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "File Open");
            }            
        }

        //Allow drag & drop to open a file
        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                myOpenFile(files[0]);
            }
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        //Update the status line when the text changes
        protected void Text_Changed(object sender, EventArgs e)
        {
            //Get the current tab
            intCounter = tabControl1.SelectedIndex;
            bChanged[intCounter] = true;
            toolStripStatusLabelFileChanged.Text = "FC: T";
            //Get the current carret position (text cursor)
            int intLine = arrTextBox[intCounter].GetLineFromCharIndex(arrTextBox[intCounter].SelectionStart);
            int intColumn = arrTextBox[intCounter].SelectionStart - 
                arrTextBox[intCounter].GetFirstCharIndexFromLine(intLine);
            intLine++;
            intColumn++;
            toolStripStatusLabelLine.Text = "LN: " + intLine.ToString();
            toolStripStatusLabelColumn.Text = "CO: " + intColumn.ToString();
        }

        //Update status when a key is released
        protected void Key_Up(object sender, KeyEventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            int intLine = arrTextBox[intCounter].GetLineFromCharIndex(arrTextBox[intCounter].SelectionStart);
            int intColumn = arrTextBox[intCounter].SelectionStart - 
                arrTextBox[intCounter].GetFirstCharIndexFromLine(intLine);
            intLine++;
            intColumn++;
            toolStripStatusLabelLine.Text = "LN: " + intLine.ToString();
            toolStripStatusLabelColumn.Text = "CO: " + intColumn.ToString();
            //Switch tab when CTRL + 1, 2, 3 etc is pressed
            if (e.Control && e.KeyCode == Keys.D1)
            {
                tabControl1.SelectedTab = arrTabPage[0];
                arrTextBox[0].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D2)
            {
                tabControl1.SelectedTab = arrTabPage[1];
                arrTextBox[1].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D3)
            {
                tabControl1.SelectedTab = arrTabPage[2];
                arrTextBox[2].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D4)
            {
                tabControl1.SelectedTab = arrTabPage[3];
                arrTextBox[3].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D5)
            {
                tabControl1.SelectedTab = arrTabPage[4];
                arrTextBox[4].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D6)
            {
                tabControl1.SelectedTab = arrTabPage[5];
                arrTextBox[5].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D7)
            {
                tabControl1.SelectedTab = arrTabPage[6];
                arrTextBox[6].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D8)
            {
                tabControl1.SelectedTab = arrTabPage[7];
                arrTextBox[7].Focus();
            }
            if (e.Control && e.KeyCode == Keys.D9)
            {
                tabControl1.SelectedTab = arrTabPage[8];
                arrTextBox[8].Focus();
            }
        }

        //Update font name and font size in the status line
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                arrTextBox[intCounter].Font = fontDialog1.Font;
                arrTextBox[intCounter].ForeColor = fontDialog1.Color;
            }
            toolStripStatusLabelFontName.Text = "FTN: " + fontDialog1.Font.Name;
            toolStripStatusLabelFontSize.Text = "FTS: " + fontDialog1.Font.Size;
        }

        //Change the background color of a text box
        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                arrTextBox[intCounter].BackColor = colorDialog1.Color;
            }
        }

        //Word Wrap
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strWordWrap;
            intCounter = tabControl1.SelectedIndex;
            if (arrTextBox[intCounter].WordWrap)
            {
                strWordWrap = "WW: F";
                arrTextBox[intCounter].WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
            }
            else
            {
                strWordWrap = "WW: T";
                arrTextBox[intCounter].WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
            }
            toolStripStatusLabelWordWrap.Text = strWordWrap;
        }

        //Update status line when tab changes
        private void tabControl1_Click(object sender, EventArgs e)
        {
            string strWordWrap, strFileChanged;
            intCounter = tabControl1.SelectedIndex;
            if (arrTextBox[intCounter].WordWrap)
            {
                strWordWrap = "WW: T";
            }
            else
            {
                strWordWrap = "WW: F";
            }
            if (bChanged[intCounter])
            {
                strFileChanged = "FC: T";
            }
            else
            {
                strFileChanged = "FC: F";
            }

            toolStripStatusLabelWordWrap.Text = strWordWrap;
            toolStripStatusLabelFontName.Text = "FTN: " + arrTextBox[intCounter].Font.Name;
            toolStripStatusLabelFontSize.Text = "FTS: " + arrTextBox[intCounter].Font.Size;
            toolStripStatusLabelFileName.Text = "FN: " + Path.GetFileName(strFileName[intCounter]);
            toolStripStatusLabelFileChanged.Text = strFileChanged;
        }

        //New file
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            if (bChanged[intCounter]) {
                DialogResult dialogResult = 
                    MessageBox.Show("All changes will be lost. Are you sure?", "New File", 
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            arrTextBox[intCounter].Clear();
            bChanged[intCounter] = false;
            toolStripStatusLabelFileChanged.Text = "FC: F";
            strFileName[intCounter] = "";
            toolStripStatusLabelFileName.Text = "FN: " + strFileName[intCounter];

        }

        //Open file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myOpenFile(openFileDialog1.FileName);
            }
        }

        private void myOpenFile(string strFileOpenName)
        {
                try
                {
                    System.IO.StreamReader OpenFile = new System.IO.StreamReader(strFileOpenName);
                    arrTextBox[intCounter].Text = OpenFile.ReadToEnd();
                    OpenFile.Close();
                    bChanged[intCounter] = false;
                    toolStripStatusLabelFileChanged.Text = "FC: F";
                    strFileName[intCounter] = strFileOpenName;
                    toolStripStatusLabelFileName.Text = "FN: " + 
                    Path.GetFileName(strFileName[intCounter]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " 
                        + ex.Message);
                }
        }

        //Save file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            saveFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            if (strFileName[intCounter] != null)
            {
                try
                {
                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(strFileName[intCounter]);
                    SaveFile.WriteLine(arrTextBox[intCounter].Text);
                    SaveFile.Close();
                    bChanged[intCounter] = false;
                    toolStripStatusLabelFileChanged.Text = "FC: F";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + 
                        ex.Message);
                }
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveFileDialog1.FileName);
                        SaveFile.WriteLine(arrTextBox[intCounter].Text);
                        SaveFile.Close();
                        strFileName[intCounter] = saveFileDialog1.FileName;
                        toolStripStatusLabelFileName.Text = "FN: " + 
                            Path.GetFileName(strFileName[intCounter]);
                        bChanged[intCounter] = false;
                        toolStripStatusLabelFileChanged.Text = "FC: F";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not write file to disk. Original error: " + 
                            ex.Message);
                    }
                }
            }
        }

        //Save As
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            saveFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    SaveFile.WriteLine(arrTextBox[intCounter].Text);
                    SaveFile.Close();
                    strFileName[intCounter] = saveFileDialog1.FileName;
                    toolStripStatusLabelFileName.Text = "FN: " + Path.GetFileName(strFileName[intCounter]);
                    bChanged[intCounter] = false;
                    toolStripStatusLabelFileChanged.Text = "FC: F";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + 
                        ex.Message);
                }
            }
        }

        //Status bar on | off
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
            statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            arrTextBox[intCounter].Undo();
        }

        //Select All
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            arrTextBox[intCounter].SelectAll();
        }

        //Cut
        private void cutrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            arrTextBox[intCounter].Cut();
        }

        //Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            arrTextBox[intCounter].Copy();
        }

        //paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intCounter = tabControl1.SelectedIndex;
            arrTextBox[intCounter].Paste();
        }

        //About...
        private void aboutK9padToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.Show();
        }

        //Help...
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No help for you!", "HEEEELP!!!!!");
        }

        //Go To line
        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 gotoBox = new Form2();            
            gotoBox.Show();

            int intGoToLine;
            if (Int32.TryParse(Form2.strGoToLine, out intGoToLine))
            {
                intCounter = tabControl1.SelectedIndex;
                arrTextBox[intCounter].HideSelection = false;
                try {
                    arrTextBox[intCounter].SelectionStart = 
                        arrTextBox[intCounter].GetFirstCharIndexFromLine(intGoToLine - 1);
                    arrTextBox[intCounter].SelectionLength = 
                        arrTextBox[intCounter].Lines[intGoToLine - 1].Length;
                    arrTextBox[intCounter].ScrollToCaret();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Out of range! Error: " + ex.Message, "Go To");
                }
            }
            else {
                return;
            }
        }

        //Find 
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 findBox = new Form3();
            findBox.ShowDialog();
        }

        //Find next
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selStart;
            if (String.IsNullOrEmpty(Form3.strFind))
            {
                return;
            }
            if (Form3.bFindMatchCase)
            {
                try
                {
                    selStart = arrTextBox[intCounter].Text.IndexOf(Form3.strFind, 
                        arrTextBox[intCounter].SelectionStart + 1, StringComparison.Ordinal);
                }
                catch (Exception)
                {
                    selStart = -1;
                }
            }
            else
            {
                try
                {
                    selStart = arrTextBox[intCounter].Text.IndexOf(Form3.strFind, 
                        arrTextBox[intCounter].SelectionStart + 1, StringComparison.OrdinalIgnoreCase);
                }
                catch (Exception)
                {
                    selStart = -1;
                }
            }
            if (selStart == -1)
            {
                MessageBox.Show(Form3.strFind + " was not found", "Find");
                return;
            }
            arrTextBox[Form1.intCounter].Select(selStart, Form3.strFind.Length);
            arrTextBox[Form1.intCounter].ScrollToCaret();           
        }

        //Replace
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 replaceBox = new Form4();           
            replaceBox.Show();

        }

        //Exit program
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Assume none of the 9 texts changed
            bool bSomethingChanged = false;

            for (int i = 0; i < arrTextBox.GetLength(0); i++)
            {
                if (bChanged[i]) bSomethingChanged = true;
            }
            //If some text changed ask for confirmation, otherwise just exit
            if (bSomethingChanged)
            {
                if (MessageBox.Show("Are you sure you want to close?", "Nothing will be saved...",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        
        //Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //page setup
        private void PageSetupStripMenuItem3_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.PageSettings = pgSettings;
            pageSetupDialog.PrinterSettings = prtSettings;
            pageSetupDialog.AllowOrientation = true;
            pageSetupDialog.AllowMargins = true;
            pageSetupDialog.ShowDialog();
        }

        //Print menu
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            printDoc.DefaultPageSettings = pgSettings;
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDoc;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strToPrint = arrTextBox[intCounter].Text;
                printDoc.Print();
            }
        }

        //Print Preview
        private void PrintPreviewStripMenuItem3_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();            
            dlg.Document = printDoc;
            strToPrint = arrTextBox[intCounter].Text;
            dlg.ShowDialog();
        }

        //Print Document
        private void printDoc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;
              
            e.Graphics.MeasureString(strToPrint, arrTextBox[intCounter].Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);
            e.Graphics.DrawString(strToPrint, arrTextBox[intCounter].Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);
            strToPrint = strToPrint.Substring(charactersOnPage);
            e.HasMorePages = (strToPrint.Length > 0);
        }

    }
}