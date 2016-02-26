namespace k9pad
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnGoTo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBoxGoTo = new System.Windows.Forms.TextBox();
            this.labelGoTo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(16, 55);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(75, 23);
            this.btnGoTo.TabIndex = 0;
            this.btnGoTo.Text = "Go To";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(97, 55);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textBoxGoTo
            // 
            this.textBoxGoTo.Location = new System.Drawing.Point(16, 29);
            this.textBoxGoTo.Name = "textBoxGoTo";
            this.textBoxGoTo.Size = new System.Drawing.Size(156, 20);
            this.textBoxGoTo.TabIndex = 2;
            this.textBoxGoTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxGoTo_KeyDown);
            this.textBoxGoTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxGoTo_KeyPress);
            // 
            // labelGoTo
            // 
            this.labelGoTo.AutoSize = true;
            this.labelGoTo.Location = new System.Drawing.Point(13, 13);
            this.labelGoTo.Name = "labelGoTo";
            this.labelGoTo.Size = new System.Drawing.Size(98, 13);
            this.labelGoTo.TabIndex = 3;
            this.labelGoTo.Text = "Enter Line Number:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 88);
            this.Controls.Add(this.labelGoTo);
            this.Controls.Add(this.textBoxGoTo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGoTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go To Line";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBoxGoTo;
        private System.Windows.Forms.Label labelGoTo;
    }
}