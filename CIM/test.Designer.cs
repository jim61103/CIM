namespace CIM
{
    partial class test
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Before = new System.Windows.Forms.Button();
            this.btn_After = new System.Windows.Forms.Button();
            this.txtBefore = new System.Windows.Forms.TextBox();
            this.txtAfter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Before
            // 
            this.btn_Before.Location = new System.Drawing.Point(24, 145);
            this.btn_Before.Name = "btn_Before";
            this.btn_Before.Size = new System.Drawing.Size(75, 23);
            this.btn_Before.TabIndex = 1;
            this.btn_Before.Text = "button2";
            this.btn_Before.UseVisualStyleBackColor = true;
            this.btn_Before.Click += new System.EventHandler(this.btn_Before_Click_1);
            // 
            // btn_After
            // 
            this.btn_After.Location = new System.Drawing.Point(147, 145);
            this.btn_After.Name = "btn_After";
            this.btn_After.Size = new System.Drawing.Size(75, 23);
            this.btn_After.TabIndex = 2;
            this.btn_After.Text = "button3";
            this.btn_After.UseVisualStyleBackColor = true;
            // 
            // txtBefore
            // 
            this.txtBefore.Location = new System.Drawing.Point(24, 196);
            this.txtBefore.Name = "txtBefore";
            this.txtBefore.Size = new System.Drawing.Size(100, 22);
            this.txtBefore.TabIndex = 3;
            // 
            // txtAfter
            // 
            this.txtAfter.Location = new System.Drawing.Point(147, 196);
            this.txtAfter.Name = "txtAfter";
            this.txtAfter.Size = new System.Drawing.Size(100, 22);
            this.txtAfter.TabIndex = 4;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtAfter);
            this.Controls.Add(this.txtBefore);
            this.Controls.Add(this.btn_After);
            this.Controls.Add(this.btn_Before);
            this.Controls.Add(this.button1);
            this.Name = "test";
            this.Text = "test";
            this.Load += new System.EventHandler(this.test_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Before;
        private System.Windows.Forms.Button btn_After;
        private System.Windows.Forms.TextBox txtBefore;
        private System.Windows.Forms.TextBox txtAfter;
    }
}