using Common;
using MBActXLib;
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

namespace KeyenceMarkingBuilder
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(frmMain_Closing);//觸發關閉視窗事件
            this.Shown += frmMain_Shown;

            txtMono.KeyPress += TxtMono_KeyPress;

            base.OnLoad(e);
        }

        private void TxtMono_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(13))//按下ENTER鍵
                {
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        ///開啟FORM執行，可使用Focus指定位置
        /// </summary>
        private void frmMain_Shown(object sender, EventArgs e)
        {
            txtMono.Focus();
        }
        /// <summary>
        ///開啟FORM執行，Load預設程式
        /// </summary>
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            try
            {
                SetInitialRow();
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
            Global.MainForm = this;
            Global.MESAdpt = new MESAdapter();
            Global.OPNo = AppSetting.OPNo;//作業站編號
            Global.PDLineNo = AppSetting.PDLineNo;
            Global.OPGroupNo = AppSetting.OPGroupNo;
            //TextBox
            txtMono.ReadOnly = false;
            txtMonoQty.ReadOnly = true;
            txtFinishedQty.ReadOnly = true;
            txtUnFinishedQty.ReadOnly = true;
            txtProductNo.ReadOnly = true;
            txtProgramNo.ReadOnly = true;
            txtMono.Text = string.Empty;
            txtMonoQty.Text = string.Empty;
            txtFinishedQty.Text = string.Empty;
            txtUnFinishedQty.Text = string.Empty;
            txtProductNo.Text = string.Empty;
            txtProgramNo.Text = string.Empty;

            lblMsg.Text = string.Empty;
            lblUserIDShow.Text = Global.UserNo;
            ltbUnFinishedSn.Visible = false;
            ltbFinishedSn.Visible = false;
            lblUnFinishedSn.ForeColor = Color.Blue;
            lblFinishedSn.ForeColor = Color.Blue;

            btnQuery.Enabled = true;
            btnStart.Enabled = false;

            panMonoData.Visible = false;

            rbRealPrint.Checked = true;



            axMBActX1.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right |
                AnchorStyles.Top |
                AnchorStyles.Left;

            ltbUnFinishedSn.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Top |
                AnchorStyles.Left;

            ltbFinishedSn.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Top |
                AnchorStyles.Left;

        }

        /// <summary>
        ///Form關閉時觸發事件
        /// </summary>
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();//關閉所有視窗
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                //LoadEquipmentData();

                //return;

                if (txtMono.Text.Trim() == string.Empty)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                #region 讀取工單資訊

                string strMono = txtMono.Text.Trim();

                DataTable dtMono = null;

                dtMono = Global.MESAdpt.funLoadOEMOBasis(strMono);

                string strProgramNO = string.Empty;

                if (dtMono != null && dtMono.Rows.Count > 0)
                {
                    // 檢查機種檔案
                    strProgramNO = QueryProfile(dtMono.Rows[0]["PRODUCTNO"].ToString()).Trim();

                    if (strProgramNO == "N/A")
                    {
                        return;
                    }
                    // 機台連線@@@@
                    LoadEquipmentData(strProgramNO);

                    //取得工單客戶序號
                    string SerialType = "3";
                    if (rdbSNType3.Checked == true)
                    {
                        SerialType = "3";
                    }
                    else if (rdbSNType6.Checked == true)
                    {
                        SerialType = "6";
                    }
                    DataTable dtMoSerialData = Global.MESAdpt.funShowOEMOSerialCreateDetail(strMono, SerialType);

                    if (dtMoSerialData != null && dtMoSerialData.Rows.Count > 0)
                    {
                        //dtMoSerialData.DefaultView.Sort = "SERIALNO";

                        dtMoSerialData.DefaultView.Sort = "SERIALNO asc";
                        dtMoSerialData = dtMoSerialData.DefaultView.ToTable();

                        foreach (DataRow row in dtMoSerialData.Rows)
                        {
                            ltbUnFinishedSn.Items.Add(row["SERIALNO"].ToString());
                        }
                        //取得已完成雷雕紀錄
                        DataTable dtUseData = Global.MESAdpt.funLoadWipMarkBuilder(strMono, string.Empty, string.Empty, string.Empty, string.Empty);

                        //移除已完成紀錄
                        foreach (DataRow row in dtUseData.Rows)
                        {
                            string strItem = row["SERIALNO"].ToString();
                            ltbUnFinishedSn.Items.Remove(strItem);
                            ltbFinishedSn.Items.Add(strItem);
                        }
                        if (ltbUnFinishedSn.Items.Count == 0)
                        {
                            string[] arrMessage = new string[1];
                            arrMessage[0] = txtMono.Text.Trim();
                            Utility.Functions.WriteInfoMessage("此工單[{0}]雕刻已完成，請重新確認", arrMessage);

                            ltbUnFinishedSn.Items.Clear();
                            ltbFinishedSn.Items.Clear();

                            return;
                        }

                    }
                    else
                    {
                        string[] arrMessage = new string[1];
                        arrMessage[0] = txtMono.Text.Trim();
                        Utility.Functions.WriteErrorMessage("查不到此工單[{0}]的生產序號，請重新確認", arrMessage);
                        return;
                    }
                }
                else
                {
                    string[] arrMessage = new string[1];
                    arrMessage[0] = txtMono.Text.Trim();
                    Utility.Functions.WriteErrorMessage("查不到此工單[{0}]資訊，請重新確認", arrMessage);

                    return;
                }
                #endregion

                Global.ProductNo = dtMono.Rows[0]["PRODUCTNO"].ToString();
                Global.ProgramNo = strProgramNO;

                txtUnFinishedQty.Text = Convert.ToString(ltbUnFinishedSn.Items.Count);
                txtFinishedQty.Text = Convert.ToString(ltbFinishedSn.Items.Count); ;
                txtMonoQty.Text = dtMono.Rows[0]["MOQTY"].ToString();
                txtProductNo.Text = dtMono.Rows[0]["PRODUCTNO"].ToString();
                txtProgramNo.Text = strProgramNO;

                ltbUnFinishedSn.Visible = true;
                ltbFinishedSn.Visible = true;
                ltbFinishedSn.TopIndex = ltbFinishedSn.Items.Count - 1;
                ltbUnFinishedSn.SelectedIndex = 0;
                lblMsg.Text = "訊息:下一次雕刻序號[" + ltbUnFinishedSn.Items[0].ToString() + "]";
                lblMsg.ForeColor = Color.Blue;

                panMonoData.Visible = true;

                txtMono.ReadOnly = true;

                btnQuery.Enabled = false;
                btnStart.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        ///根據機種與PPNO查詢ProFile Number
        /// </summary>
        private string QueryProfile(string ProductNo)
        {
            string strProgramNo = string.Empty;
            //檢查程式編號是否有維護
            DataTable dtProFile = Global.MESAdpt.funLoadOPProfile(ProductNo, Global.OPNo, "*");

            if (dtProFile != null && dtProFile.Rows.Count == 1)
            {
                string strProfileNo = dtProFile.Rows[0]["PROFILENO"].ToString().Trim();

                return strProfileNo;
            }
            else if (dtProFile.Rows.Count > 1)
            {
                string[] arrMessage = new string[2];
                arrMessage[0] = ProductNo;
                arrMessage[1] = Global.OPNo;
                Utility.Functions.WriteErrorMessage("查詢到兩筆以上程式編號資料，無法開始作業，請重新確認(機種編號[{0})，作業站編號[{1}]", arrMessage);
                return "N/A";
            }
            else
            {
                string[] arrMessage = new string[2];
                arrMessage[0] = ProductNo;
                arrMessage[1] = Global.OPNo;
                Utility.Functions.WriteErrorMessage("查不到程式編號資料，無法開始作業，請重新確認(機種編號[{0}])，作業站編號[{1}]", arrMessage);
                return "N/A";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // 顯示MessageBox 
                DialogResult Result = MessageBox.Show("請確認是否已放置待雕刻物品，確認完成後按下[確定鈕]後開始作業", "提醒", MessageBoxButtons.OKCancel);
                if (Result == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (rbRealPrint.Checked == true)
                    {
                        if (ltbUnFinishedSn.Items.Count == 0)
                        {
                            Utility.Functions.WriteInfoMessage("已完成");
                            return;
                        }

                        //操作過的序號處理
                        string strItemSn = ltbUnFinishedSn.Items[0].ToString();

                        //機器動作
                        bool EQPStatus = EquipmentStart(strItemSn);

                        //bool EQPStatus = true;

                        if (EQPStatus == false)
                        {
                            string[] arrMessage = new string[1];
                            arrMessage[0] = strItemSn;
                            Utility.Functions.WriteErrorMessage("機台序號[{0}]操作失敗，請重新操作", arrMessage);
                            return;
                        }


                        int iResult = Global.MESAdpt.funAddWipMarkBuilder(txtMono.Text, txtProductNo.Text, strItemSn, Global.OPNo, Global.ProgramNo, Global.UserNo);

                        if (iResult != 0)
                        {
                            string[] arrMessage = new string[1];
                            arrMessage[0] = strItemSn;
                            Utility.Functions.WriteErrorMessage("資料[{0}]寫入失敗，請重新操作", arrMessage);
                            return;
                        }

                        ltbUnFinishedSn.Items.Remove(strItemSn);
                        ltbFinishedSn.Items.Add(strItemSn);
                        txtUnFinishedQty.Text = Convert.ToString(ltbUnFinishedSn.Items.Count);
                        txtFinishedQty.Text = Convert.ToString(ltbFinishedSn.Items.Count);
                        ltbFinishedSn.TopIndex = ltbFinishedSn.Items.Count - 1;
                        ltbFinishedSn.TopIndex = ltbFinishedSn.Items.Count - 1;
                        if (ltbUnFinishedSn.Items.Count == 0)
                        {
                            Utility.Functions.WriteInfoMessage("已完成");
                            CleanData();
                            return;
                        }

                        ltbUnFinishedSn.SelectedIndex = 0;
                        lblMsg.Text = "訊息:下一次雕刻序號[" + ltbUnFinishedSn.Items[0].ToString() + "]";
                    }
                    else
                    {
                        if (ltbUnFinishedSn.Items.Count == 0)
                        {
                            Utility.Functions.WriteInfoMessage("已完成");
                            return;
                        }

                        //操作過的序號處理
                        string strItemSn = ltbUnFinishedSn.Items[0].ToString();

                        //機器動作
                        bool EQPStatus = EquipmentSimulation(strItemSn);

                        if (EQPStatus == false)
                        {
                            string[] arrMessage = new string[1];
                            arrMessage[0] = strItemSn;
                            Utility.Functions.WriteErrorMessage("機台序號[{0}]操作失敗，請重新操作", arrMessage);
                            return;
                        }
                    }
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanData();
        }
        private void CleanData()
        {
            txtMono.ReadOnly = false;
            txtMonoQty.ReadOnly = true;
            txtFinishedQty.ReadOnly = true;
            txtUnFinishedQty.ReadOnly = true;
            txtProductNo.ReadOnly = true;
            txtProgramNo.ReadOnly = true;
            txtMono.Text = string.Empty;
            txtMonoQty.Text = string.Empty;
            txtFinishedQty.Text = string.Empty;
            txtUnFinishedQty.Text = string.Empty;
            txtProductNo.Text = string.Empty;
            txtProgramNo.Text = string.Empty;

            lblMsg.Text = string.Empty;
            ltbUnFinishedSn.Visible = false;
            ltbFinishedSn.Visible = false;

            btnQuery.Enabled = true;
            panMonoData.Visible = false;

            ltbUnFinishedSn.Items.Clear();
            ltbFinishedSn.Items.Clear();
            btnStart.Enabled = false;

            txtMono.Focus();

        }

        /// <summary>
        ///機台連線且讀取使用檔案
        /// </summary>
        private void LoadEquipmentData(string ProgramNo)
        {
            if (axMBActX1.Comm.IsOnline)
            {
                axMBActX1.Comm.Offline();
            }

            //获取控制器序号
            axMBActX1.Comm.ConnectionType = ConnectionTypes.CONNECTION_USB;
            axMBActX1.InitMBActX(MarkingUnitTypes.MARKINGUNIT_MDV9920);

            axMBActX1.IsAutoRedraw = true; //是否自动更新预览

            int controllerSerialNo = 0;
            axMBActX1.Comm.UsbControllerSerialNo = controllerSerialNo;//SB 连接时，获取/设置连接目标控制器的序列号

            axMBActX1.Comm.Online();

            axMBActX1.Context = ContextTypes.CONTEXT_EDITING;

            //axMBActX1.SendControllerProgram(1991, @"C:\Users\ALVIN.HW.TSAI\Documents\MarkingFiles\Keyence MD-V9920\0\function\test1.MFP");//MFP把檔案寫道1991程序

            //axMBActX1.SendControllerProgram(1991, @"C:\Users\ALVIN.HW.TSAI\Documents\MarkingFiles\Keyence MD-V9920\0\function\" + ProgramNo + ".MFP");//MFP把檔案寫到1991程序
            axMBActX1.SendControllerProgram(1991, AppSetting.EquipmentFilePath + ProgramNo + ".MFP");//MFP把檔案寫到1991程序

            axMBActX1.Context = ContextTypes.CONTEXT_CONTROLLER;

            string cmd = string.Empty;

            //axMBActX1.Height = 500;
            //axMBActX1.Width = 800;

            axMBActX1.ZoomAdjust();
        }

        /// <summary>
        ///機台開始雕刻
        /// </summary>
        private bool EquipmentStart(string Sn)
        {
            try
            {
                string cmd = string.Empty;

                string response = "";

                cmd = "C2,1991,0," + Sn;//C2=修改內容,1991=程序編號,0=字串序號,Sn=變更內容

                response = axMBActX1.Comm.SendAndReceive(cmd);

                //2019.08.22 Added by Andre Li.
                if (rdbTextBarcode.Checked == true)
                {
                    cmd = "C2,1991,1," + Sn;        //Barcode對應到1號
                    response = axMBActX1.Comm.SendAndReceive(cmd);
                }

                //axMBActX1.Operation.StartGuideLaserMarking(GuideLaserTypes.GUIDELASER_ONETIME);//模擬刻印
                axMBActX1.Operation.StartMarking();//實際刻印
                axMBActX1.ZoomAdjust();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///機台模擬雕刻
        /// </summary>
        private bool EquipmentSimulation(string Sn)
        {
            try
            {
                string cmd = string.Empty;

                string response = "";

                cmd = "C2,1991,0," + Sn;//C2=修改內容,1991=程序編號,0=字串序號,Sn=變更內容

                response = axMBActX1.Comm.SendAndReceive(cmd);

                //2019.08.22 Added by Andre Li.
                if (rdbTextBarcode.Checked == true)
                {
                    cmd = "C2,1991,1," + Sn;        //Barcode對應到1號
                    response = axMBActX1.Comm.SendAndReceive(cmd);
                }

                axMBActX1.Operation.StartGuideLaserMarking(GuideLaserTypes.GUIDELASER_ONETIME);//模擬刻印
                axMBActX1.ZoomAdjust();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
