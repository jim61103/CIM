using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace CIM
{
    public partial class frmJanomeJR3000 : Form
    {
        private DataTable m_dtMOData = null;//工單資訊
        private Boolean bolGvDataSelect = false;//判斷是否已選擇GridView

        public frmJanomeJR3000()
        {
            //Global.MainForm = this;
            //Global.MESAdpt = new MESAdapter();
            //Global.UserNo = string.Empty;
            //Global.OPNo = "ASSY";
            //Global.PDLineNo = "ASSY-1";
            //Global.OPGroupNo = "0030";
            //Global.UserNo = "TW81169";
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(frmJanomeJR3000_Closing);//觸發關閉視窗事件

            //GridView
            this.gvData.AutoGenerateColumns = false;//只呈現指定的欄位
            this.gvData.SelectionChanged += new EventHandler(gvData_SelectionChanged);
            this.gvData.RowsAdded += new DataGridViewRowsAddedEventHandler(gvData_RowsAdded);//新增INDEX
            this.gvData.AllowUserToAddRows = false;//移除多的空白欄位

            this.Shown += FrmJanomeJR3000_Shown;

            base.OnLoad(e);
        }
        /// <summary>
        ///開啟FORM執行，可使用Focus指定位置
        /// </summary>
        private void FrmJanomeJR3000_Shown(object sender, EventArgs e)
        {
            btnCheckEquipment.Focus();
        }
        /// <summary>
        ///開啟FORM執行，Load預設程式
        /// </summary>
        private void frmJanomeJR3000_Load(object sender, System.EventArgs e)
        {
            try
            {
                SetInitialRow();

                //首件檢查
                EQPcheck();

                //讀取工單資訊
                LoadMonoData();

                bolGvDataSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        ///初始化設定
        /// </summary>
        private void SetInitialRow()
        {
            gvData.ReadOnly = true;

            ckbEQPCheck1.Checked = false;
            ckbEQPCheck2.Checked = false;
            ckbEQPCheck3.Checked = false;
            ckbEQPCheck4.Checked = false;
            ckbEQPCheck1.Enabled = false;
            ckbEQPCheck2.Enabled = false;
            ckbEQPCheck3.Enabled = false;
            ckbEQPCheck4.Enabled = false;

            //TextBox
            txtPDLineNo.ReadOnly = true;
            txtOPGroupNo.ReadOnly = true;
            txtPDLineNo.Text = Global.PDLineNo;
            txtOPGroupNo.Text = Global.OPGroupNo;
            txtOPNo.Text = Global.OPNo;
            txtUserID.Text = Global.UserNo;
            txtOPNo.ReadOnly = true;
            txtUserID.ReadOnly = true;

            //Label
            lblEQPStatus.ForeColor = Color.Blue;
        }

        /// <summary>
        ///Form關閉時觸發事件
        /// </summary>
        private void frmJanomeJR3000_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();//關閉所有視窗
        }

        /// <summary>
        ///ShowDialog開啟的視窗，關閉時觸發事件
        /// </summary>
        void frmJanomeEQPCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            EQPcheck();
        }

        /// <summary>
        ///ShowDialog開啟的視窗，關閉時觸發事件
        /// </summary>
        void frmJanomeOperateEQP_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadMonoData();
            bolGvDataSelect = false;
        }

        /// <summary>
        ///加入GridView Row index
        /// </summary>
        protected void gvData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                this.gvData.Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
            }

        }

        protected void gvData_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvData.SelectedRows)
            {
                Global.Mono = row.Cells[1].Value.ToString();
                Global.PDLineNo = row.Cells[4].Value.ToString();
                Global.OPGroupNo = row.Cells[3].Value.ToString();
                Global.ProductNo = row.Cells[2].Value.ToString();

                bolGvDataSelect = true;
            }
        }

        /// <summary>
        ///功能說明:首件檢查
        /// </summary>
        private void EQPcheck()
        {
            #region 首件檢查
            //首件檢查
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);

            //判斷當前時間是日班,夜班
            string strWork = string.Empty;
            string strWorkShift = string.Empty;

            DateTime tNowtime = DateTime.Now;
            DateTime stime = DateTime.ParseExact("080000", "HHmmss", culture);//日班上班時間
            DateTime etime = DateTime.ParseExact("200000", "HHmmss", culture);//日班下班時間

            //比對當前時間是否同一個時間區間
            //不同做首件
            //相同看狀態是否要做
            if (DateTime.Compare(tNowtime, stime) > 0 && DateTime.Compare(tNowtime, etime) < 0)
            {
                strWork = "MorningShift";//日班
                strWorkShift = "日班";

            }
            else
            {
                strWork = "NightShift";//夜班
                strWorkShift = "夜班";
            }

            if (AppSetting.EQPShift.ToUpper().Trim() == strWork.ToUpper().Trim() && AppSetting.EQPStatus.ToUpper().Trim() == "Y")
            {
                ckbEQPCheck1.Checked = true;
                ckbEQPCheck2.Checked = true;
                ckbEQPCheck3.Checked = true;
                ckbEQPCheck4.Checked = true;

                lblEQPStatus.Text = "已完成";
                lblEQPStatus.ForeColor = Color.Blue;
                lblMsg.Text = "[提示]請點選工單後開始作業";
                lblMsg.ForeColor = Color.Blue;
                //不需做首件
                gvData.Enabled = true;
            }
            else if (AppSetting.EQPShift.ToUpper().Trim() != strWork.ToUpper().Trim())
            {
                //需做首件
                config.AppSettings.Settings["EQPStatus"].Value = "N";
                config.AppSettings.Settings["EQPShift"].Value = strWork;
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");
                lblEQPStatus.Text = "未完成";
                lblEQPStatus.ForeColor = Color.Red;
                lblMsg.Text = "[提示]請先完成機台首檢再開始作業";
                lblMsg.ForeColor = Color.Red;
                gvData.Enabled = false;
            }
            else
            {
                lblEQPStatus.Text = "未完成";
                lblEQPStatus.ForeColor = Color.Red;
                lblMsg.Text = "[提示]請先完成機台首檢再開始作業";
                lblMsg.ForeColor = Color.Red;
                gvData.Enabled = false;
            }
            lblWorkShift.Text = strWorkShift;
            lblWorkShift.ForeColor = Color.Blue;
            #endregion
        }

        /// <summary>
        ///功能說明:讀取工單資訊
        /// </summary>
        private void LoadMonoData()
        {
            m_dtMOData = null;
            gvData.DataSource = null;
            gvData.Refresh();

            //檢查使用者登入
            if (Global.UserNo == string.Empty)
            {
                MessageBox.Show("工號未登入", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            m_dtMOData = Global.MESAdpt.funLoadOEMOLineDispatch(Global.OPGroupNo, Global.PDLineNo);

            if (m_dtMOData != null && m_dtMOData.Rows.Count > 0)
            {
                gvData.DataSource = m_dtMOData;
                gvData.Rows[0].Selected = false;//不做預選
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        ///開啟機台操作介面
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Global.ProgramNo = string.Empty;

            if (bolGvDataSelect == false)
            {
                MessageBox.Show("請先選擇一筆資料");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            //string strProgramNo = QueryProfile();
            string strProgramNo = "12";//@@@@@@@@@@@@@@

            if (strProgramNo == "0") { return; }

            Global.ProgramNo = strProgramNo;
            frmJanomeOperateEQP objDlg = new frmJanomeOperateEQP();
            objDlg.FormClosed += new FormClosedEventHandler(frmJanomeOperateEQP_FormClosed);
            objDlg.ShowDialog();
            Cursor.Current = Cursors.Default;

        }


        private void btnCheckEquipment_Click(object sender, EventArgs e)
        {
            frmEQPCheckItem objDlg = new frmEQPCheckItem();
            objDlg.FormClosed += new FormClosedEventHandler(frmJanomeEQPCheck_FormClosed);
            objDlg.ShowDialog();
        }

        /// <summary>
        ///根據機種與PPNO查詢ProFile Number
        /// </summary>
        private string QueryProfile()
        {
            string strProgramNo = string.Empty;
            //檢查程式編號是否有維護
            DataTable dtProFile = Global.MESAdpt.funLoadOPProfile(Global.ProductNo, "*");

            if (dtProFile != null && dtProFile.Rows.Count > 0)
            {
                DataRow[] foundRows = dtProFile.Select("OPNO='" + Global.OPNo + "' and EQUIPMENTNO='*' ");

                if (foundRows.Count() == 1)
                {
                    strProgramNo = foundRows[0]["PROFILENO"].ToString().Trim();

                    Boolean bolResult = Utility.Functions.IsNumeric(strProgramNo);

                    if (bolResult == false)
                    {
                        MessageBox.Show("程式編號只允許設定正整數，請重新確認！(當前程式編號[" + strProgramNo + "]");
                        return "0";
                    }
                    else
                    {
                        return strProgramNo;
                    }
                }
                else if (foundRows.Count() > 1)
                {
                    MessageBox.Show("查詢到兩筆以上程式編號資料，無法開始作業，請重新確認(機種編號[" + Global.ProductNo + "])");
                    return "0";
                }
                else
                {
                    MessageBox.Show("查不到程式編號資料，無法開始作業，請重新確認(機種編號[" + Global.ProductNo + "])，作業站編號[" + Global.OPNo + "]");
                    return "0";
                }
            }
            else
            {
                MessageBox.Show("查不到程式編號資料，無法開始作業，請重新確認(機種編號[" + Global.ProductNo + "])");
                return "0";
            }


        }
    }
}
