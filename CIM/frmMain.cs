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
    public partial class frmMain : Form
    {
        private DataTable m_dtMOData = null;//工單資訊

        private DataTable dtProfileData = null;//工單資訊

        private Boolean bolGvDataSelect = false;//判斷是否已選擇GridView

        public frmMain()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(frmMain_Closing);//觸發關閉視窗事件

            //GridView
            this.gvData.AutoGenerateColumns = false;//只呈現指定的欄位
            this.gvData.SelectionChanged += new EventHandler(gvData_SelectionChanged);
            this.gvData.RowsAdded += new DataGridViewRowsAddedEventHandler(gvData_RowsAdded);//新增INDEX
            this.gvData.AllowUserToAddRows = false;//移除多的空白欄位
            this.Shown += frmMain_Shown;

            base.OnLoad(e);
        }

   
        /// <summary>
        ///開啟FORM執行，可使用Focus指定位置
        /// </summary>
        private void frmMain_Shown(object sender, EventArgs e)
        {
            btnCheckEquipment.Focus();
        }
        /// <summary>
        ///開啟FORM執行，Load預設程式
        /// </summary>
        private void frmMain_Load(object sender, System.EventArgs e)
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

            gvData.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right |
                AnchorStyles.Top |
                AnchorStyles.Left;

            gvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//填滿所有

            this.gvData.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12);
            this.gvData.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            this.gvData.MultiSelect = false;

            //this.gvData.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;//選擇顏色

        }

        /// <summary>
        ///Form關閉時觸發事件
        /// </summary>
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();//關閉所有視窗
        }

        /// <summary>
        ///ShowDialog開啟的視窗，關閉時觸發事件
        /// </summary>
        void frmEQPCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            EQPcheck();
        }

        /// <summary>
        ///ShowDialog開啟的視窗，關閉時觸發事件
        /// </summary>
        void frmProfileMaintain_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadMonoData();
            bolGvDataSelect = false;
        }


        /// <summary>
        ///ShowDialog開啟的視窗，關閉時觸發事件
        /// </summary>
        void frmEQPOperate_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadMonoData();
            bolGvDataSelect = false;
        }

        /// <summary>
        ///加入GridView Row index,RowDataBound
        /// </summary>
        protected void gvData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            gvData.DefaultCellStyle.BackColor = Color.White;

            for (int i = 0; i < e.RowCount; i++)
            {
                gvData.Rows[e.RowIndex + i].Cells[0].Value = (e.RowIndex + i + 1).ToString();//設定編號

                string[] arrMOQty = gvData.Rows[e.RowIndex + i].Cells[5].Value.ToString().Split('.');

                string strMOQty = arrMOQty[0].ToString();

                gvData.Rows[e.RowIndex + i].Cells[5].Value = strMOQty;//去小數點

                string strMono = gvData.Rows[e.RowIndex + i].Cells[3].Value.ToString().Trim();

                DataRow[] foundRows = dtProfileData.Select("MONO='" + strMono + "'");

                if (foundRows.Count() != 0)
                {
                    string strProfile = foundRows[0]["PROFILENO"].ToString().Trim();
                    gvData.Rows[e.RowIndex + i].Cells[10].Value = strProfile;//設定程式編號
                    if (strProfile == string.Empty)
                    {
                        //gvData.Rows[e.RowIndex + i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }

        protected void gvData_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvData.SelectedRows)
            {
                Global.Mono = string.Empty;
                Global.PDLineNo = string.Empty;
                Global.OPGroupNo = string.Empty;
                Global.ProductNo = string.Empty;

                Global.Mono = row.Cells[3].Value.ToString();
                Global.PDLineNo = row.Cells[7].Value.ToString();
                Global.OPGroupNo = row.Cells[6].Value.ToString();
                Global.ProductNo = row.Cells[4].Value.ToString();

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
            DateTime stime = DateTime.ParseExact("075000", "HHmmss", culture);//日班上班時間
            DateTime etime = DateTime.ParseExact("200000", "HHmmss", culture);//日班下班時間

            //比對當前時間是否同一個時間區間
            //不同做首件
            //相同看狀態是否要做
            if (DateTime.Compare(tNowtime, stime) > 0 && DateTime.Compare(tNowtime, etime) < 0)
            {
                strWork = DateTime.Now.ToString("yyyyMMdd") + "MorningShift";//日班
                strWorkShift = "日班";

            }
            else
            {
                if (DateTime.Compare(tNowtime, stime) < 0)
                {
                    strWork = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                }
                else
                {
                    strWork = DateTime.Now.ToString("yyyyMMdd");
                }
                strWork = strWork + "NightShift";//夜班
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
            dtProfileData = null;
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
                m_dtMOData.Columns.Add("profileNo");

                m_dtMOData.DefaultView.Sort = "PLANSTARTTIME";

                //dtProfileData = DBTableInfo.getProgramData(string.Empty);

                dtProfileData = Global.MESAdpt.funLoadCusMoToProFile("", Global.PDLineNo);

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

            frmEQPOperate objDlg = new frmEQPOperate();
            objDlg.FormClosed += new FormClosedEventHandler(frmEQPOperate_FormClosed);
            objDlg.ShowDialog();

            Cursor.Current = Cursors.Default;

        }
        private void btnCheckEquipment_Click(object sender, EventArgs e)
        {
            frmEQPCheckItem objDlg = new frmEQPCheckItem();
            objDlg.FormClosed += new FormClosedEventHandler(frmEQPCheck_FormClosed);
            objDlg.ShowDialog();
        }

        private void btnProfileNo_Click(object sender, EventArgs e)
        {


            if (bolGvDataSelect == false)
            {
                MessageBox.Show("請先選擇一筆資料");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            frmProfileMaintain objDlg = new frmProfileMaintain();
            objDlg.FormClosed += new FormClosedEventHandler(frmProfileMaintain_FormClosed);
            objDlg.ShowDialog();

            Cursor.Current = Cursors.Default;
        }
    }
}
