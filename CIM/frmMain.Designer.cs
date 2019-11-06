namespace CIM
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gvData = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISPDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MONODESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MONO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MOREMAINQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OPGroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDLINENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIFTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANENDTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPDLineNo = new System.Windows.Forms.Label();
            this.txtPDLineNo = new System.Windows.Forms.TextBox();
            this.lblOPGroupNo = new System.Windows.Forms.Label();
            this.txtOPGroupNo = new System.Windows.Forms.TextBox();
            this.btnCheckEquipment = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEQPStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWorkShift = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.ckbEQPCheck1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbEQPCheck2 = new System.Windows.Forms.CheckBox();
            this.ckbEQPCheck3 = new System.Windows.Forms.CheckBox();
            this.ckbEQPCheck4 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOPNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnProfileNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gvData
            // 
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.DISPDATE,
            this.MONODESC,
            this.MONO,
            this.PRODUCTNO,
            this.MOREMAINQTY,
            this.OPGroupNo,
            this.PDLINENO,
            this.SHIFTNO,
            this.PLANENDTIME,
            this.profileNo});
            this.gvData.Location = new System.Drawing.Point(61, 176);
            this.gvData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gvData.Name = "gvData";
            this.gvData.RowHeadersWidth = 51;
            this.gvData.RowTemplate.Height = 24;
            this.gvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvData.Size = new System.Drawing.Size(1467, 368);
            this.gvData.TabIndex = 5;
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            this.Number.HeaderText = "編號";
            this.Number.MinimumWidth = 6;
            this.Number.Name = "Number";
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Number.Width = 50;
            // 
            // DISPDATE
            // 
            this.DISPDATE.DataPropertyName = "DISPDATE";
            this.DISPDATE.HeaderText = "上線日期";
            this.DISPDATE.MinimumWidth = 6;
            this.DISPDATE.Name = "DISPDATE";
            this.DISPDATE.Width = 107;
            // 
            // MONODESC
            // 
            this.MONODESC.DataPropertyName = "MONODESC";
            this.MONODESC.HeaderText = "項次";
            this.MONODESC.MinimumWidth = 6;
            this.MONODESC.Name = "MONODESC";
            this.MONODESC.Width = 107;
            // 
            // MONO
            // 
            this.MONO.DataPropertyName = "MONO";
            this.MONO.HeaderText = "工單編號";
            this.MONO.MinimumWidth = 6;
            this.MONO.Name = "MONO";
            this.MONO.Width = 150;
            // 
            // PRODUCTNO
            // 
            this.PRODUCTNO.DataPropertyName = "PRODUCTNO";
            this.PRODUCTNO.HeaderText = "機種編號";
            this.PRODUCTNO.MinimumWidth = 6;
            this.PRODUCTNO.Name = "PRODUCTNO";
            this.PRODUCTNO.Width = 180;
            // 
            // MOREMAINQTY
            // 
            this.MOREMAINQTY.DataPropertyName = "MOREMAINQTY";
            this.MOREMAINQTY.HeaderText = "工單剩餘數量";
            this.MOREMAINQTY.MinimumWidth = 6;
            this.MOREMAINQTY.Name = "MOREMAINQTY";
            this.MOREMAINQTY.Width = 130;
            // 
            // OPGroupNo
            // 
            this.OPGroupNo.DataPropertyName = "OPGROUPNO";
            this.OPGroupNo.HeaderText = "作業站群";
            this.OPGroupNo.MinimumWidth = 6;
            this.OPGroupNo.Name = "OPGroupNo";
            this.OPGroupNo.Width = 107;
            // 
            // PDLINENO
            // 
            this.PDLINENO.DataPropertyName = "PDLINENO";
            this.PDLINENO.HeaderText = "生產線別編號";
            this.PDLINENO.MinimumWidth = 6;
            this.PDLINENO.Name = "PDLINENO";
            this.PDLINENO.Width = 130;
            // 
            // SHIFTNO
            // 
            this.SHIFTNO.DataPropertyName = "SHIFTNO";
            this.SHIFTNO.HeaderText = "班別編號";
            this.SHIFTNO.MinimumWidth = 6;
            this.SHIFTNO.Name = "SHIFTNO";
            this.SHIFTNO.Width = 107;
            // 
            // PLANENDTIME
            // 
            this.PLANENDTIME.DataPropertyName = "PLANENDTIME";
            this.PLANENDTIME.HeaderText = "預計開始時間";
            this.PLANENDTIME.MinimumWidth = 6;
            this.PLANENDTIME.Name = "PLANENDTIME";
            this.PLANENDTIME.Width = 130;
            // 
            // profileNo
            // 
            this.profileNo.DataPropertyName = "profileNo";
            this.profileNo.HeaderText = "噴印程式編號";
            this.profileNo.MinimumWidth = 6;
            this.profileNo.Name = "profileNo";
            this.profileNo.Width = 120;
            // 
            // lblPDLineNo
            // 
            this.lblPDLineNo.AutoSize = true;
            this.lblPDLineNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPDLineNo.Location = new System.Drawing.Point(56, 36);
            this.lblPDLineNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPDLineNo.Name = "lblPDLineNo";
            this.lblPDLineNo.Size = new System.Drawing.Size(132, 25);
            this.lblPDLineNo.TabIndex = 6;
            this.lblPDLineNo.Text = "生產線別編號";
            // 
            // txtPDLineNo
            // 
            this.txtPDLineNo.Location = new System.Drawing.Point(195, 35);
            this.txtPDLineNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPDLineNo.Name = "txtPDLineNo";
            this.txtPDLineNo.Size = new System.Drawing.Size(132, 25);
            this.txtPDLineNo.TabIndex = 7;
            // 
            // lblOPGroupNo
            // 
            this.lblOPGroupNo.AutoSize = true;
            this.lblOPGroupNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOPGroupNo.Location = new System.Drawing.Point(337, 36);
            this.lblOPGroupNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOPGroupNo.Name = "lblOPGroupNo";
            this.lblOPGroupNo.Size = new System.Drawing.Size(152, 25);
            this.lblOPGroupNo.TabIndex = 8;
            this.lblOPGroupNo.Text = "作業站群組編號";
            // 
            // txtOPGroupNo
            // 
            this.txtOPGroupNo.Location = new System.Drawing.Point(495, 36);
            this.txtOPGroupNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOPGroupNo.Name = "txtOPGroupNo";
            this.txtOPGroupNo.Size = new System.Drawing.Size(132, 25);
            this.txtOPGroupNo.TabIndex = 9;
            // 
            // btnCheckEquipment
            // 
            this.btnCheckEquipment.Location = new System.Drawing.Point(1428, 38);
            this.btnCheckEquipment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCheckEquipment.Name = "btnCheckEquipment";
            this.btnCheckEquipment.Size = new System.Drawing.Size(100, 50);
            this.btnCheckEquipment.TabIndex = 10;
            this.btnCheckEquipment.Text = "機台首檢";
            this.btnCheckEquipment.UseVisualStyleBackColor = true;
            this.btnCheckEquipment.Click += new System.EventHandler(this.btnCheckEquipment_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(383, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "首件狀態";
            // 
            // lblEQPStatus
            // 
            this.lblEQPStatus.AutoSize = true;
            this.lblEQPStatus.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.lblEQPStatus.Location = new System.Drawing.Point(481, 84);
            this.lblEQPStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEQPStatus.Name = "lblEQPStatus";
            this.lblEQPStatus.Size = new System.Drawing.Size(72, 25);
            this.lblEQPStatus.TabIndex = 12;
            this.lblEQPStatus.Text = "未完成";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(56, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "當前班別";
            // 
            // lblWorkShift
            // 
            this.lblWorkShift.AutoSize = true;
            this.lblWorkShift.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.lblWorkShift.Location = new System.Drawing.Point(163, 84);
            this.lblWorkShift.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkShift.Name = "lblWorkShift";
            this.lblWorkShift.Size = new System.Drawing.Size(52, 25);
            this.lblWorkShift.TabIndex = 14;
            this.lblWorkShift.Text = "日班";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(1308, 106);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 50);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "開始作業";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // ckbEQPCheck1
            // 
            this.ckbEQPCheck1.AutoSize = true;
            this.ckbEQPCheck1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.ckbEQPCheck1.Location = new System.Drawing.Point(168, 125);
            this.ckbEQPCheck1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbEQPCheck1.Name = "ckbEQPCheck1";
            this.ckbEQPCheck1.Size = new System.Drawing.Size(154, 29);
            this.ckbEQPCheck1.TabIndex = 16;
            this.ckbEQPCheck1.Text = "離子風扇確認";
            this.ckbEQPCheck1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(61, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "首件項目";
            // 
            // ckbEQPCheck2
            // 
            this.ckbEQPCheck2.AutoSize = true;
            this.ckbEQPCheck2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.ckbEQPCheck2.Location = new System.Drawing.Point(343, 125);
            this.ckbEQPCheck2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbEQPCheck2.Name = "ckbEQPCheck2";
            this.ckbEQPCheck2.Size = new System.Drawing.Size(134, 29);
            this.ckbEQPCheck2.TabIndex = 18;
            this.ckbEQPCheck2.Text = "氣壓值確認";
            this.ckbEQPCheck2.UseVisualStyleBackColor = true;
            // 
            // ckbEQPCheck3
            // 
            this.ckbEQPCheck3.AutoSize = true;
            this.ckbEQPCheck3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.ckbEQPCheck3.Location = new System.Drawing.Point(495, 124);
            this.ckbEQPCheck3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbEQPCheck3.Name = "ckbEQPCheck3";
            this.ckbEQPCheck3.Size = new System.Drawing.Size(193, 29);
            this.ckbEQPCheck3.TabIndex = 19;
            this.ckbEQPCheck3.Text = "XY table做動確認";
            this.ckbEQPCheck3.UseVisualStyleBackColor = true;
            // 
            // ckbEQPCheck4
            // 
            this.ckbEQPCheck4.AutoSize = true;
            this.ckbEQPCheck4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.ckbEQPCheck4.Location = new System.Drawing.Point(711, 122);
            this.ckbEQPCheck4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbEQPCheck4.Name = "ckbEQPCheck4";
            this.ckbEQPCheck4.Size = new System.Drawing.Size(154, 29);
            this.ckbEQPCheck4.TabIndex = 20;
            this.ckbEQPCheck4.Text = "機台原點確認";
            this.ckbEQPCheck4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(651, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "作業站編號";
            // 
            // txtOPNo
            // 
            this.txtOPNo.Location = new System.Drawing.Point(779, 35);
            this.txtOPNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOPNo.Name = "txtOPNo";
            this.txtOPNo.Size = new System.Drawing.Size(132, 25);
            this.txtOPNo.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(933, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "使用者工號";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(1061, 35);
            this.txtUserID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(132, 25);
            this.txtUserID.TabIndex = 24;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(651, 81);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(124, 27);
            this.lblMsg.TabIndex = 25;
            this.lblMsg.Text = "操作訊息";
            // 
            // btnProfileNo
            // 
            this.btnProfileNo.Location = new System.Drawing.Point(1428, 106);
            this.btnProfileNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProfileNo.Name = "btnProfileNo";
            this.btnProfileNo.Size = new System.Drawing.Size(100, 50);
            this.btnProfileNo.TabIndex = 26;
            this.btnProfileNo.Text = "噴印機程式維護";
            this.btnProfileNo.UseVisualStyleBackColor = true;
            this.btnProfileNo.Click += new System.EventHandler(this.btnProfileNo_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1561, 564);
            this.Controls.Add(this.btnProfileNo);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOPNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckbEQPCheck4);
            this.Controls.Add(this.ckbEQPCheck3);
            this.Controls.Add(this.ckbEQPCheck2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckbEQPCheck1);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.lblWorkShift);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblEQPStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheckEquipment);
            this.Controls.Add(this.txtOPGroupNo);
            this.Controls.Add(this.lblOPGroupNo);
            this.Controls.Add(this.txtPDLineNo);
            this.Controls.Add(this.lblPDLineNo);
            this.Controls.Add(this.gvData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工單開線";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.Label lblPDLineNo;
        private System.Windows.Forms.TextBox txtPDLineNo;
        private System.Windows.Forms.Label lblOPGroupNo;
        private System.Windows.Forms.TextBox txtOPGroupNo;
        private System.Windows.Forms.Button btnCheckEquipment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEQPStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWorkShift;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox ckbEQPCheck1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbEQPCheck2;
        private System.Windows.Forms.CheckBox ckbEQPCheck3;
        private System.Windows.Forms.CheckBox ckbEQPCheck4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOPNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnProfileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn DISPDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MONODESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MONO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOREMAINQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPGroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDLINENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIFTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLANENDTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn profileNo;
    }
}