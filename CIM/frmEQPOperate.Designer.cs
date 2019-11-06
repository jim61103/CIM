namespace CIM
{
    partial class frmEQPOperate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEQPOperate));
            this.txtPDLineNo = new System.Windows.Forms.TextBox();
            this.lblOPGroupNo = new System.Windows.Forms.Label();
            this.txtMono = new System.Windows.Forms.TextBox();
            this.lblPDLineNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductNo = new System.Windows.Forms.TextBox();
            this.txtOPGroupNo = new System.Windows.Forms.TextBox();
            this.lblTool = new System.Windows.Forms.Label();
            this.txtToolNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDioNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProgramNo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPwr = new System.Windows.Forms.Label();
            this.lblIGBT = new System.Windows.Forms.Label();
            this.txtPwrBDNo = new System.Windows.Forms.TextBox();
            this.txtIGBTNo = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnReturnHome = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.btnReturnPower = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPDLineNo
            // 
            this.txtPDLineNo.Location = new System.Drawing.Point(103, 57);
            this.txtPDLineNo.Name = "txtPDLineNo";
            this.txtPDLineNo.Size = new System.Drawing.Size(100, 21);
            this.txtPDLineNo.TabIndex = 13;
            // 
            // lblOPGroupNo
            // 
            this.lblOPGroupNo.AutoSize = true;
            this.lblOPGroupNo.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOPGroupNo.Location = new System.Drawing.Point(209, 57);
            this.lblOPGroupNo.Name = "lblOPGroupNo";
            this.lblOPGroupNo.Size = new System.Drawing.Size(74, 21);
            this.lblOPGroupNo.TabIndex = 12;
            this.lblOPGroupNo.Text = "作業站群";
            // 
            // txtMono
            // 
            this.txtMono.Location = new System.Drawing.Point(103, 17);
            this.txtMono.Name = "txtMono";
            this.txtMono.Size = new System.Drawing.Size(100, 21);
            this.txtMono.TabIndex = 11;
            // 
            // lblPDLineNo
            // 
            this.lblPDLineNo.AutoSize = true;
            this.lblPDLineNo.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPDLineNo.Location = new System.Drawing.Point(23, 58);
            this.lblPDLineNo.Name = "lblPDLineNo";
            this.lblPDLineNo.Size = new System.Drawing.Size(74, 21);
            this.lblPDLineNo.TabIndex = 10;
            this.lblPDLineNo.Text = "生產線別";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(23, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "工單編號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(23, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 15;
            this.label2.Text = "程式編號";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(209, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 19;
            this.label5.Text = "機種編號";
            // 
            // txtProductNo
            // 
            this.txtProductNo.Location = new System.Drawing.Point(289, 17);
            this.txtProductNo.Name = "txtProductNo";
            this.txtProductNo.Size = new System.Drawing.Size(152, 21);
            this.txtProductNo.TabIndex = 18;
            // 
            // txtOPGroupNo
            // 
            this.txtOPGroupNo.Location = new System.Drawing.Point(289, 57);
            this.txtOPGroupNo.Name = "txtOPGroupNo";
            this.txtOPGroupNo.Size = new System.Drawing.Size(152, 21);
            this.txtOPGroupNo.TabIndex = 22;
            // 
            // lblTool
            // 
            this.lblTool.AutoSize = true;
            this.lblTool.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTool.Location = new System.Drawing.Point(7, 129);
            this.lblTool.Name = "lblTool";
            this.lblTool.Size = new System.Drawing.Size(90, 21);
            this.lblTool.TabIndex = 23;
            this.lblTool.Text = "模治具編號";
            // 
            // txtToolNo
            // 
            this.txtToolNo.Location = new System.Drawing.Point(103, 129);
            this.txtToolNo.Name = "txtToolNo";
            this.txtToolNo.Size = new System.Drawing.Size(338, 21);
            this.txtToolNo.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtDioNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtProgramNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblPDLineNo);
            this.panel1.Controls.Add(this.txtMono);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtToolNo);
            this.panel1.Controls.Add(this.lblOPGroupNo);
            this.panel1.Controls.Add(this.lblTool);
            this.panel1.Controls.Add(this.txtPDLineNo);
            this.panel1.Controls.Add(this.txtOPGroupNo);
            this.panel1.Controls.Add(this.txtProductNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 195);
            this.panel1.TabIndex = 25;
            // 
            // txtDioNo
            // 
            this.txtDioNo.Location = new System.Drawing.Point(289, 94);
            this.txtDioNo.Name = "txtDioNo";
            this.txtDioNo.Size = new System.Drawing.Size(152, 21);
            this.txtDioNo.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(209, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 36;
            this.label3.Text = "整流子";
            // 
            // txtProgramNo
            // 
            this.txtProgramNo.Location = new System.Drawing.Point(103, 94);
            this.txtProgramNo.Name = "txtProgramNo";
            this.txtProgramNo.Size = new System.Drawing.Size(100, 21);
            this.txtProgramNo.TabIndex = 29;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(140, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 31);
            this.btnOK.TabIndex = 27;
            this.btnOK.Text = "開始";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(338, 337);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 31);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "關閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPwr
            // 
            this.lblPwr.AutoSize = true;
            this.lblPwr.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPwr.Location = new System.Drawing.Point(35, 213);
            this.lblPwr.Name = "lblPwr";
            this.lblPwr.Size = new System.Drawing.Size(74, 21);
            this.lblPwr.TabIndex = 29;
            this.lblPwr.Text = "PWR BD";
            // 
            // lblIGBT
            // 
            this.lblIGBT.AutoSize = true;
            this.lblIGBT.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIGBT.Location = new System.Drawing.Point(63, 252);
            this.lblIGBT.Name = "lblIGBT";
            this.lblIGBT.Size = new System.Drawing.Size(46, 21);
            this.lblIGBT.TabIndex = 30;
            this.lblIGBT.Text = "IGBT";
            // 
            // txtPwrBDNo
            // 
            this.txtPwrBDNo.Location = new System.Drawing.Point(115, 212);
            this.txtPwrBDNo.Name = "txtPwrBDNo";
            this.txtPwrBDNo.Size = new System.Drawing.Size(338, 21);
            this.txtPwrBDNo.TabIndex = 31;
            // 
            // txtIGBTNo
            // 
            this.txtIGBTNo.Location = new System.Drawing.Point(115, 252);
            this.txtIGBTNo.Name = "txtIGBTNo";
            this.txtIGBTNo.Size = new System.Drawing.Size(338, 21);
            this.txtIGBTNo.TabIndex = 32;
            // 
            // btnReturnHome
            // 
            this.btnReturnHome.Location = new System.Drawing.Point(453, 345);
            this.btnReturnHome.Name = "btnReturnHome";
            this.btnReturnHome.Size = new System.Drawing.Size(64, 22);
            this.btnReturnHome.TabIndex = 33;
            this.btnReturnHome.Text = "機台重置";
            this.btnReturnHome.UseVisualStyleBackColor = true;
            this.btnReturnHome.Click += new System.EventHandler(this.btnReturnHome_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Location = new System.Drawing.Point(12, 279);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(475, 55);
            this.lblMsg.TabIndex = 34;
            this.lblMsg.Text = "訊息:已完成";
            // 
            // lblMsg1
            // 
            this.lblMsg1.Location = new System.Drawing.Point(14, 378);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(401, 24);
            this.lblMsg1.TabIndex = 35;
            this.lblMsg1.Text = "註:";
            // 
            // btnReturnPower
            // 
            this.btnReturnPower.Font = new System.Drawing.Font("PMingLiU", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnReturnPower.Location = new System.Drawing.Point(453, 376);
            this.btnReturnPower.Name = "btnReturnPower";
            this.btnReturnPower.Size = new System.Drawing.Size(64, 22);
            this.btnReturnPower.TabIndex = 36;
            this.btnReturnPower.Text = "回復動力";
            this.btnReturnPower.UseVisualStyleBackColor = true;
            this.btnReturnPower.Click += new System.EventHandler(this.btnReturnPower_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(99, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 17);
            this.label4.TabIndex = 37;
            this.label4.Text = "(請依序刷完所有治具條碼後再刷入9999以完成治具刷入)";
            // 
            // frmEQPOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 409);
            this.Controls.Add(this.btnReturnPower);
            this.Controls.Add(this.lblMsg1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnReturnHome);
            this.Controls.Add(this.txtIGBTNo);
            this.Controls.Add(this.txtPwrBDNo);
            this.Controls.Add(this.lblIGBT);
            this.Controls.Add(this.lblPwr);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("PMingLiU", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEQPOperate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "噴印機操作";
            this.Load += new System.EventHandler(this.frmJanomeOperateEQP_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPDLineNo;
        private System.Windows.Forms.Label lblOPGroupNo;
        private System.Windows.Forms.TextBox txtMono;
        private System.Windows.Forms.Label lblPDLineNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductNo;
        private System.Windows.Forms.TextBox txtOPGroupNo;
        private System.Windows.Forms.Label lblTool;
        private System.Windows.Forms.TextBox txtToolNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtProgramNo;
        private System.Windows.Forms.Label lblPwr;
        private System.Windows.Forms.Label lblIGBT;
        private System.Windows.Forms.TextBox txtPwrBDNo;
        private System.Windows.Forms.TextBox txtIGBTNo;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnReturnHome;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.TextBox txtDioNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReturnPower;
        private System.Windows.Forms.Label label4;
    }
}