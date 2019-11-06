namespace KeyenceMarkingBuilder
{
    partial class frmReturnData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReturnData));
            this.btnClean = new System.Windows.Forms.Button();
            this.lblUserIDShow = new System.Windows.Forms.Label();
            this.txtMono = new System.Windows.Forms.TextBox();
            this.lblMono = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.ltbFinishedSn = new System.Windows.Forms.ListBox();
            this.ltbUnFinishedSn = new System.Windows.Forms.ListBox();
            this.lblUnFinishedSn = new System.Windows.Forms.Label();
            this.lblFinishedSn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblUnQTY = new System.Windows.Forms.Label();
            this.lblQTY = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(357, 31);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 50;
            this.btnClean.Text = "清除";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // lblUserIDShow
            // 
            this.lblUserIDShow.AutoSize = true;
            this.lblUserIDShow.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserIDShow.Location = new System.Drawing.Point(498, 2);
            this.lblUserIDShow.Name = "lblUserIDShow";
            this.lblUserIDShow.Size = new System.Drawing.Size(42, 21);
            this.lblUserIDShow.TabIndex = 49;
            this.lblUserIDShow.Text = "工號";
            // 
            // txtMono
            // 
            this.txtMono.Location = new System.Drawing.Point(108, 32);
            this.txtMono.Name = "txtMono";
            this.txtMono.Size = new System.Drawing.Size(162, 22);
            this.txtMono.TabIndex = 48;
            // 
            // lblMono
            // 
            this.lblMono.AutoSize = true;
            this.lblMono.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMono.Location = new System.Drawing.Point(28, 33);
            this.lblMono.Name = "lblMono";
            this.lblMono.Size = new System.Drawing.Size(74, 21);
            this.lblMono.TabIndex = 47;
            this.lblMono.Text = "工單編號";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(402, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 46;
            this.label5.Text = "使用者工號";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(276, 31);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 45;
            this.btnQuery.Text = "查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ltbFinishedSn
            // 
            this.ltbFinishedSn.FormattingEnabled = true;
            this.ltbFinishedSn.ItemHeight = 12;
            this.ltbFinishedSn.Location = new System.Drawing.Point(392, 195);
            this.ltbFinishedSn.Name = "ltbFinishedSn";
            this.ltbFinishedSn.Size = new System.Drawing.Size(176, 196);
            this.ltbFinishedSn.TabIndex = 52;
            // 
            // ltbUnFinishedSn
            // 
            this.ltbUnFinishedSn.FormattingEnabled = true;
            this.ltbUnFinishedSn.ItemHeight = 12;
            this.ltbUnFinishedSn.Location = new System.Drawing.Point(32, 195);
            this.ltbUnFinishedSn.Name = "ltbUnFinishedSn";
            this.ltbUnFinishedSn.Size = new System.Drawing.Size(176, 196);
            this.ltbUnFinishedSn.TabIndex = 51;
            // 
            // lblUnFinishedSn
            // 
            this.lblUnFinishedSn.AutoSize = true;
            this.lblUnFinishedSn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.lblUnFinishedSn.Location = new System.Drawing.Point(47, 171);
            this.lblUnFinishedSn.Name = "lblUnFinishedSn";
            this.lblUnFinishedSn.Size = new System.Drawing.Size(122, 21);
            this.lblUnFinishedSn.TabIndex = 53;
            this.lblUnFinishedSn.Text = "工單未完成序號";
            // 
            // lblFinishedSn
            // 
            this.lblFinishedSn.AutoSize = true;
            this.lblFinishedSn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.lblFinishedSn.Location = new System.Drawing.Point(404, 171);
            this.lblFinishedSn.Name = "lblFinishedSn";
            this.lblFinishedSn.Size = new System.Drawing.Size(122, 21);
            this.lblFinishedSn.TabIndex = 54;
            this.lblFinishedSn.Text = "工單已完成序號";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(28, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 55;
            this.label1.Text = "重工序號";
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(108, 76);
            this.txtSn.Name = "txtSn";
            this.txtSn.Size = new System.Drawing.Size(162, 22);
            this.txtSn.TabIndex = 56;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(276, 75);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 57;
            this.btnConfirm.Text = "確定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(27, 117);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(465, 54);
            this.lblMsg.TabIndex = 58;
            this.lblMsg.Text = "操作訊息";
            // 
            // lblUnQTY
            // 
            this.lblUnQTY.AutoSize = true;
            this.lblUnQTY.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblUnQTY.Location = new System.Drawing.Point(166, 171);
            this.lblUnQTY.Name = "lblUnQTY";
            this.lblUnQTY.Size = new System.Drawing.Size(42, 21);
            this.lblUnQTY.TabIndex = 59;
            this.lblUnQTY.Text = "(20)";
            // 
            // lblQTY
            // 
            this.lblQTY.AutoSize = true;
            this.lblQTY.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblQTY.Location = new System.Drawing.Point(526, 171);
            this.lblQTY.Name = "lblQTY";
            this.lblQTY.Size = new System.Drawing.Size(42, 21);
            this.lblQTY.TabIndex = 60;
            this.lblQTY.Text = "(20)";
            // 
            // frmReturnData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 403);
            this.Controls.Add(this.lblQTY);
            this.Controls.Add(this.lblUnQTY);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtSn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFinishedSn);
            this.Controls.Add(this.lblUnFinishedSn);
            this.Controls.Add(this.ltbFinishedSn);
            this.Controls.Add(this.ltbUnFinishedSn);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.lblUserIDShow);
            this.Controls.Add(this.txtMono);
            this.Controls.Add(this.lblMono);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReturnData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重工雕刻序號";
            this.Load += new System.EventHandler(this.frmReturnData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Label lblUserIDShow;
        private System.Windows.Forms.TextBox txtMono;
        private System.Windows.Forms.Label lblMono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ListBox ltbFinishedSn;
        private System.Windows.Forms.ListBox ltbUnFinishedSn;
        private System.Windows.Forms.Label lblUnFinishedSn;
        private System.Windows.Forms.Label lblFinishedSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblUnQTY;
        private System.Windows.Forms.Label lblQTY;
    }
}