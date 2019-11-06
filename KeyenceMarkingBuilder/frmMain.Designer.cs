namespace KeyenceMarkingBuilder
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblMono = new System.Windows.Forms.Label();
            this.txtMono = new System.Windows.Forms.TextBox();
            this.lblProductNo = new System.Windows.Forms.Label();
            this.txtProductNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMonoQty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFinishedQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnFinishedQty = new System.Windows.Forms.TextBox();
            this.ltbUnFinishedSn = new System.Windows.Forms.ListBox();
            this.ltbFinishedSn = new System.Windows.Forms.ListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panMonoData = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbTextBarcode = new System.Windows.Forms.RadioButton();
            this.rdbText = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRealPrint = new System.Windows.Forms.RadioButton();
            this.rbSimulationPrint = new System.Windows.Forms.RadioButton();
            this.txtProgramNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFinishedSn = new System.Windows.Forms.Label();
            this.lblUnFinishedSn = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUserIDShow = new System.Windows.Forms.Label();
            this.btnClean = new System.Windows.Forms.Button();
            this.axMBActX1 = new AxMBActXLib.AxMBActX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbSNType3 = new System.Windows.Forms.RadioButton();
            this.rdbSNType6 = new System.Windows.Forms.RadioButton();
            this.panMonoData.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(483, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 15;
            this.btnQuery.Text = "查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(26, 121);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(465, 54);
            this.lblMsg.TabIndex = 25;
            this.lblMsg.Text = "操作訊息";
            // 
            // lblMono
            // 
            this.lblMono.AutoSize = true;
            this.lblMono.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMono.Location = new System.Drawing.Point(62, 30);
            this.lblMono.Name = "lblMono";
            this.lblMono.Size = new System.Drawing.Size(74, 21);
            this.lblMono.TabIndex = 26;
            this.lblMono.Text = "工單編號";
            // 
            // txtMono
            // 
            this.txtMono.Location = new System.Drawing.Point(142, 29);
            this.txtMono.Name = "txtMono";
            this.txtMono.Size = new System.Drawing.Size(100, 22);
            this.txtMono.TabIndex = 27;
            // 
            // lblProductNo
            // 
            this.lblProductNo.AutoSize = true;
            this.lblProductNo.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblProductNo.Location = new System.Drawing.Point(304, 10);
            this.lblProductNo.Name = "lblProductNo";
            this.lblProductNo.Size = new System.Drawing.Size(74, 21);
            this.lblProductNo.TabIndex = 28;
            this.lblProductNo.Text = "機種編號";
            // 
            // txtProductNo
            // 
            this.txtProductNo.Location = new System.Drawing.Point(384, 9);
            this.txtProductNo.Name = "txtProductNo";
            this.txtProductNo.Size = new System.Drawing.Size(159, 22);
            this.txtProductNo.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 30;
            this.label1.Text = "工單數量";
            // 
            // txtMonoQty
            // 
            this.txtMonoQty.Location = new System.Drawing.Point(140, 7);
            this.txtMonoQty.Name = "txtMonoQty";
            this.txtMonoQty.Size = new System.Drawing.Size(100, 22);
            this.txtMonoQty.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(256, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 21);
            this.label2.TabIndex = 32;
            this.label2.Text = "工單已完成數量";
            // 
            // txtFinishedQty
            // 
            this.txtFinishedQty.Location = new System.Drawing.Point(384, 40);
            this.txtFinishedQty.Name = "txtFinishedQty";
            this.txtFinishedQty.Size = new System.Drawing.Size(159, 22);
            this.txtFinishedQty.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(10, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 21);
            this.label3.TabIndex = 34;
            this.label3.Text = "工單未完成數量";
            // 
            // txtUnFinishedQty
            // 
            this.txtUnFinishedQty.Location = new System.Drawing.Point(138, 40);
            this.txtUnFinishedQty.Name = "txtUnFinishedQty";
            this.txtUnFinishedQty.Size = new System.Drawing.Size(100, 22);
            this.txtUnFinishedQty.TabIndex = 35;
            // 
            // ltbUnFinishedSn
            // 
            this.ltbUnFinishedSn.FormattingEnabled = true;
            this.ltbUnFinishedSn.ItemHeight = 12;
            this.ltbUnFinishedSn.Location = new System.Drawing.Point(50, 308);
            this.ltbUnFinishedSn.Name = "ltbUnFinishedSn";
            this.ltbUnFinishedSn.Size = new System.Drawing.Size(176, 196);
            this.ltbUnFinishedSn.TabIndex = 36;
            // 
            // ltbFinishedSn
            // 
            this.ltbFinishedSn.FormattingEnabled = true;
            this.ltbFinishedSn.ItemHeight = 12;
            this.ltbFinishedSn.Location = new System.Drawing.Point(445, 308);
            this.ltbFinishedSn.Name = "ltbFinishedSn";
            this.ltbFinishedSn.Size = new System.Drawing.Size(176, 196);
            this.ltbFinishedSn.TabIndex = 37;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(559, 165);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(79, 40);
            this.btnStart.TabIndex = 38;
            this.btnStart.Text = "開始作業";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panMonoData
            // 
            this.panMonoData.Controls.Add(this.groupBox2);
            this.panMonoData.Controls.Add(this.groupBox1);
            this.panMonoData.Controls.Add(this.txtProgramNo);
            this.panMonoData.Controls.Add(this.label7);
            this.panMonoData.Controls.Add(this.label1);
            this.panMonoData.Controls.Add(this.lblProductNo);
            this.panMonoData.Controls.Add(this.txtProductNo);
            this.panMonoData.Controls.Add(this.btnStart);
            this.panMonoData.Controls.Add(this.txtMonoQty);
            this.panMonoData.Controls.Add(this.lblMsg);
            this.panMonoData.Controls.Add(this.txtUnFinishedQty);
            this.panMonoData.Controls.Add(this.label2);
            this.panMonoData.Controls.Add(this.label3);
            this.panMonoData.Controls.Add(this.txtFinishedQty);
            this.panMonoData.Location = new System.Drawing.Point(2, 65);
            this.panMonoData.Name = "panMonoData";
            this.panMonoData.Size = new System.Drawing.Size(647, 209);
            this.panMonoData.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbTextBarcode);
            this.groupBox2.Controls.Add(this.rdbText);
            this.groupBox2.Location = new System.Drawing.Point(559, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(80, 71);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "序號型式";
            // 
            // rdbTextBarcode
            // 
            this.rdbTextBarcode.AutoSize = true;
            this.rdbTextBarcode.Location = new System.Drawing.Point(6, 43);
            this.rdbTextBarcode.Name = "rdbTextBarcode";
            this.rdbTextBarcode.Size = new System.Drawing.Size(71, 16);
            this.rdbTextBarcode.TabIndex = 1;
            this.rdbTextBarcode.Text = "文字條碼";
            this.rdbTextBarcode.UseVisualStyleBackColor = true;
            // 
            // rdbText
            // 
            this.rdbText.AutoSize = true;
            this.rdbText.Checked = true;
            this.rdbText.Location = new System.Drawing.Point(6, 21);
            this.rdbText.Name = "rdbText";
            this.rdbText.Size = new System.Drawing.Size(47, 16);
            this.rdbText.TabIndex = 0;
            this.rdbText.TabStop = true;
            this.rdbText.Text = "文字";
            this.rdbText.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRealPrint);
            this.groupBox1.Controls.Add(this.rbSimulationPrint);
            this.groupBox1.Location = new System.Drawing.Point(559, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(80, 74);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "雷雕模式";
            // 
            // rbRealPrint
            // 
            this.rbRealPrint.AutoSize = true;
            this.rbRealPrint.Location = new System.Drawing.Point(4, 21);
            this.rbRealPrint.Name = "rbRealPrint";
            this.rbRealPrint.Size = new System.Drawing.Size(71, 16);
            this.rbRealPrint.TabIndex = 45;
            this.rbRealPrint.TabStop = true;
            this.rbRealPrint.Text = "實際刻印";
            this.rbRealPrint.UseVisualStyleBackColor = true;
            // 
            // rbSimulationPrint
            // 
            this.rbSimulationPrint.AutoSize = true;
            this.rbSimulationPrint.Location = new System.Drawing.Point(4, 43);
            this.rbSimulationPrint.Name = "rbSimulationPrint";
            this.rbSimulationPrint.Size = new System.Drawing.Size(71, 16);
            this.rbSimulationPrint.TabIndex = 46;
            this.rbSimulationPrint.TabStop = true;
            this.rbSimulationPrint.Text = "模擬刻印";
            this.rbSimulationPrint.UseVisualStyleBackColor = true;
            // 
            // txtProgramNo
            // 
            this.txtProgramNo.Location = new System.Drawing.Point(138, 74);
            this.txtProgramNo.Name = "txtProgramNo";
            this.txtProgramNo.Size = new System.Drawing.Size(405, 22);
            this.txtProgramNo.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(26, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 21);
            this.label7.TabIndex = 42;
            this.label7.Text = "程式檔案名稱";
            // 
            // lblFinishedSn
            // 
            this.lblFinishedSn.AutoSize = true;
            this.lblFinishedSn.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblFinishedSn.Location = new System.Drawing.Point(471, 284);
            this.lblFinishedSn.Name = "lblFinishedSn";
            this.lblFinishedSn.Size = new System.Drawing.Size(122, 21);
            this.lblFinishedSn.TabIndex = 41;
            this.lblFinishedSn.Text = "工單已完成序號";
            // 
            // lblUnFinishedSn
            // 
            this.lblUnFinishedSn.AutoSize = true;
            this.lblUnFinishedSn.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblUnFinishedSn.Location = new System.Drawing.Point(79, 284);
            this.lblUnFinishedSn.Name = "lblUnFinishedSn";
            this.lblUnFinishedSn.Size = new System.Drawing.Size(122, 21);
            this.lblUnFinishedSn.TabIndex = 40;
            this.lblUnFinishedSn.Text = "工單未完成序號";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(455, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 23;
            this.label5.Text = "使用者工號";
            // 
            // lblUserIDShow
            // 
            this.lblUserIDShow.AutoSize = true;
            this.lblUserIDShow.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold);
            this.lblUserIDShow.Location = new System.Drawing.Point(551, 9);
            this.lblUserIDShow.Name = "lblUserIDShow";
            this.lblUserIDShow.Size = new System.Drawing.Size(42, 21);
            this.lblUserIDShow.TabIndex = 42;
            this.lblUserIDShow.Text = "工號";
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(574, 29);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 44;
            this.btnClean.Text = "清除";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // axMBActX1
            // 
            this.axMBActX1.Enabled = true;
            this.axMBActX1.Location = new System.Drawing.Point(670, 12);
            this.axMBActX1.Name = "axMBActX1";
            this.axMBActX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMBActX1.OcxState")));
            this.axMBActX1.Size = new System.Drawing.Size(591, 492);
            this.axMBActX1.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbSNType6);
            this.panel1.Controls.Add(this.rdbSNType3);
            this.panel1.Location = new System.Drawing.Point(248, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 30);
            this.panel1.TabIndex = 45;
            // 
            // rdbSNType3
            // 
            this.rdbSNType3.AutoSize = true;
            this.rdbSNType3.Checked = true;
            this.rdbSNType3.Location = new System.Drawing.Point(3, 6);
            this.rdbSNType3.Name = "rdbSNType3";
            this.rdbSNType3.Size = new System.Drawing.Size(71, 16);
            this.rdbSNType3.TabIndex = 0;
            this.rdbSNType3.Text = "單體序號";
            this.rdbSNType3.UseVisualStyleBackColor = true;
            // 
            // rdbSNType6
            // 
            this.rdbSNType6.AutoSize = true;
            this.rdbSNType6.Location = new System.Drawing.Point(95, 6);
            this.rdbSNType6.Name = "rdbSNType6";
            this.rdbSNType6.Size = new System.Drawing.Size(71, 16);
            this.rdbSNType6.TabIndex = 1;
            this.rdbSNType6.Text = "客戶序號";
            this.rdbSNType6.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 520);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.axMBActX1);
            this.Controls.Add(this.lblUserIDShow);
            this.Controls.Add(this.panMonoData);
            this.Controls.Add(this.txtMono);
            this.Controls.Add(this.lblMono);
            this.Controls.Add(this.lblFinishedSn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ltbFinishedSn);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.ltbUnFinishedSn);
            this.Controls.Add(this.lblUnFinishedSn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "雷雕機操作";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panMonoData.ResumeLayout(false);
            this.panMonoData.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblMono;
        private System.Windows.Forms.TextBox txtMono;
        private System.Windows.Forms.Label lblProductNo;
        private System.Windows.Forms.TextBox txtProductNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMonoQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFinishedQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUnFinishedQty;
        private System.Windows.Forms.ListBox ltbUnFinishedSn;
        private System.Windows.Forms.ListBox ltbFinishedSn;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panMonoData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUnFinishedSn;
        private System.Windows.Forms.Label lblFinishedSn;
        private System.Windows.Forms.Label lblUserIDShow;
        private AxMBActXLib.AxMBActX axMBActX1;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.TextBox txtProgramNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbRealPrint;
        private System.Windows.Forms.RadioButton rbSimulationPrint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbTextBarcode;
        private System.Windows.Forms.RadioButton rdbText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbSNType6;
        private System.Windows.Forms.RadioButton rdbSNType3;
    }
}

