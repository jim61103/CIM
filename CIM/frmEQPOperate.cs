using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using Excel = Microsoft.Office.Interop.Excel;

namespace CIM
{
    public partial class frmEQPOperate : Form
    {
        private string strIP = string.Empty;
        private string strPort = string.Empty;
        private DataTable dtBomData = null;
        private string IGBTMaterialNo = string.Empty;

        //監控Excel後寫入資料
        FileSystemWatcher _watch = new FileSystemWatcher();
        private Thread m_thWork = null;
        private string rootpath = string.Empty;
        private string rootpathTmp = string.Empty;
        //DirectoryInfo RptPath = null;

        private string IGBT = string.Empty;
        private string PWRNO = string.Empty;
        private string DIONO = string.Empty;
        private float LowerLimit = 0;
        private float UpperLimit = 0;

        public frmEQPOperate()
        {
            InitializeComponent();
            txtToolNo.KeyPress += TxtToolNo_KeyPress;
            txtPwrBDNo.KeyPress += TxtPwrBDNo_KeyPress;
            txtIGBTNo.KeyPress += TxtIGBTNo_KeyPress;
            this.Shown += FrmJanomeOperateEQP_Shown;
            this.Closing += new System.ComponentModel.CancelEventHandler(frmEQPOperate_Closing);//觸發關閉視窗事件
        }

        private void FrmJanomeOperateEQP_Shown(object sender, EventArgs e)
        {
            txtToolNo.Focus();
        }

        private void frmJanomeOperateEQP_Load(object sender, EventArgs e)
        {
            try
            {
                SetInitialRow();

                QueryDiono();//取得整流值

                dtBomData = Global.MESAdpt.funLoadMOMaterialList(txtMono.Text.Trim());

                if (dtBomData == null || dtBomData.Rows.Count == 0)
                {
                    MessageBox.Show("查不到[工單BOM]資料，請重新確認");
                    this.Close();
                }

                #region 監控Excel後寫入資料
                //設定Log組態檔位置
                Utility.LogHelper.InitConfig(AppDomain.CurrentDomain.BaseDirectory + "logConfig.xml");

                rootpath = AppSetting.OutputData;
                rootpathTmp = rootpath + "tmp\\";
                DirectoryInfo RptPath = new DirectoryInfo(rootpath);

                FileInfo[] filelist = RptPath.GetFiles("*.csv");  //擷取目錄下所有檔案內容，並存到 FileInfo array

                //剛啟動，先清除所有資料夾內的資料
                foreach (var file in filelist)
                {
                    // 檔案存在就刪除
                    if (System.IO.File.Exists(rootpath + file))
                    {
                        try
                        {
                            System.IO.File.Delete(rootpath + file);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }
                    }
                }

                m_thWork = new Thread(MyFileSystemWatcher);
                m_thWork.Start();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        /// <summary>
        ///Form關閉時觸發事件
        /// </summary>
        private void frmEQPOperate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void MyFileSystemWatcher()
        {
            //設定所要監控的資料夾
            _watch.Path = rootpath;

            //設定所要監控的變更類型
            _watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            //_watch.NotifyFilter = NotifyFilters.FileName;
            //_watch.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
            //設定所要監控的檔案
            _watch.Filter = "*.csv";

            //設定是否監控子資料夾
            // _watch.IncludeSubdirectories = true;

            //設定是否啟動元件，此部分必須要設定為 true，不然事件是不會被觸發的
            _watch.EnableRaisingEvents = true;

            //設定觸發事件
            _watch.Created += new FileSystemEventHandler(_watch_Created);
            //_watch.Changed += new FileSystemEventHandler(_watch_Changed);
            //_watch.Renamed += new RenamedEventHandler(_watch_Renamed);
            //_watch.Deleted += new FileSystemEventHandler(_watch_Deleted);
        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案內容有異動時觸發
        /// </summary>
        private void _watch_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                LogHelper.Info("寫入watch_Changed");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void _watch_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (IGBT == string.Empty || PWRNO == string.Empty || DIONO == string.Empty)
                {
                    LogHelper.Info("IGBT=" + IGBT + ",PWRNO=" + PWRNO + ",DIONO=" + DIONO + " 此三項目不允許為空。時間:" + DateTime.Now.ToString());
                    if (IGBT == string.Empty) IGBT = "1";
                    if (PWRNO == string.Empty) PWRNO = "2";
                    if (DIONO == string.Empty) DIONO = "3";
                }

                double fLowerLimit = LowerLimit * 0.001;
                double fUpperLimit = UpperLimit * 0.001;
                string strResult = string.Empty;
                string strResult1 = string.Empty;
                string strResult2 = string.Empty;

                if (Directory.Exists(rootpath) == false)
                {
                    //新增資料夾
                    Directory.CreateDirectory(rootpath);
                }
                if (Directory.Exists(rootpathTmp) == false)
                {
                    //新增資料夾
                    Directory.CreateDirectory(rootpathTmp);
                }
                DirectoryInfo RptPath = new DirectoryInfo(rootpath);
                FileInfo[] filelist = RptPath.GetFiles("*.csv");  //擷取目錄下所有檔案內容，並存到 FileInfo array

                int iCount = 1;
                LogHelper.Info("=====開始處理檔案=====");
                foreach (var file in filelist)
                {
                    // 檔案存在就刪除
                    //if (System.IO.File.Exists(rootpathTmp + file))
                    //{
                    //    try
                    //    {
                    //        System.IO.File.Delete(rootpathTmp + file);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LogHelper.Error(ex);
                    //    }
                    //}
                    //File.Move(rootpath + file, rootpathTmp + file);

                    //System.IO.File.Delete(rootpath + file);

                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(rootpath + file);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    try
                    {
                        int rowCount = xlRange.Rows.Count;//EXCEL 列數
                                                          //int colCount = xlRange.Columns.Count;//EXCEL 欄數

                        string strSQL = string.Empty;

                        string strResultSQL = string.Empty;

                        if (rowCount >= 8)
                        {
                            LogHelper.Info("IGBT=" + IGBT + "，檔案名稱:" + file + "開始處理，總共檔案數量:" + filelist.Count() + "第" + iCount + "次");
                            iCount++;

                            for (int i = 8; i <= rowCount; i++)
                            {
                                strSQL = " INSERT INTO CUS_THERMALDATA VALUES ( '" + IGBT + "','" + PWRNO + "','" + DIONO + "',";

                                string strTmpSQL = string.Empty;
                                strTmpSQL = strSQL;

                                float oldData = 0;
                                float newDara = 0;
                                bool bolStatus = true;
                                for (int j = 1; j <= 5; j++)
                                {
                                    string strItemValue = string.Empty;

                                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                                    {
                                        strItemValue = xlRange.Cells[i, j].Value2.ToString();
                                    }
                                    if (strItemValue.Trim() == string.Empty)//如果有欄位沒資料，就跳過這列，防止空列
                                    {
                                        bolStatus = false;
                                        break;
                                    }
                                    if (j == 4)//根據廠商提供的固定格式寫死
                                    {
                                        oldData = float.Parse(strItemValue);
                                    }
                                    if (j == 5)//根據廠商提供的固定格式寫死
                                    {
                                        newDara = float.Parse(strItemValue);
                                    }
                                    strTmpSQL = strTmpSQL + "'" + strItemValue + "',";
                                }
                                if (bolStatus == true)
                                {
                                    strResult1 = fLowerLimit < Math.Abs(newDara - oldData) ? "PASS" : "NG";
                                    strResult2 = fUpperLimit > Math.Abs(newDara - oldData) ? "PASS" : "NG";
                                    strResult = strResult1 == "PASS" && strResult2 == "PASS" ? "PASS" : "NG";

                                    strTmpSQL = strTmpSQL + "'" + fLowerLimit + "','" + fUpperLimit + "','" + Math.Abs(newDara - oldData) + "','" + strResult + "','" + Global.UserNo + "',SYSDATE,'" + file + "') @ ";
                                    strResultSQL = strResultSQL + strTmpSQL;
                                }
                            }
                            strResultSQL = strResultSQL.Substring(0, strResultSQL.Length - 2);

                            int iResult = Global.MESAdpt.funAddThermalGreaseData(strResultSQL);
                        }
                        else
                        {
                            //LogHelper.Info("EXCEL無資料" + rootpathTmp + file);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        //release com objects to fully kill excel process from running in the background
                        Marshal.ReleaseComObject(xlRange);
                        Marshal.ReleaseComObject(xlWorksheet);

                        //close and release
                        xlWorkbook.Close();
                        Marshal.ReleaseComObject(xlWorkbook);

                        //quit and release
                        xlApp.Quit();
                        Marshal.ReleaseComObject(xlApp);
                        xlApp = null;
                        xlWorkbook = null;
                        xlWorksheet = null;
                        xlRange = null;
                    }
                    System.Threading.Thread.Sleep(10000);
                    System.IO.File.Delete(rootpath + file);
                    LogHelper.Info("=====IGBT=" + IGBT + "檔案名稱:" + file + "處理完成=====");
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TxtPwrBDNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(13))//按下ENTER鍵
                {
                    lblMsg.Text = string.Empty;

                    if (txtPwrBDNo.Text.ToString().Trim() == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        txtPwrBDNo.Text = txtPwrBDNo.Text.ToUpper().Trim();
                        txtIGBTNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void TxtIGBTNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(13))//按下ENTER鍵
                {
                    lblMsg.Text = string.Empty;

                    if (txtIGBTNo.Text.Trim() == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        txtIGBTNo.Text = txtIGBTNo.Text.ToUpper().Trim();
                        btnOK.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void TxtToolNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(13))//按下ENTER鍵
                {
                    //2019.08.27 Added by Andre Li. 為支援複合式治具，改為最後刷9999代表完成治具條碼刷入
                    //----------------------------------------------------------------------------------
                    txtToolNo.Text = txtToolNo.Text.Trim().ToUpper();

                    if (txtToolNo.Text.EndsWith("9999") == true)
                    {
                        txtToolNo.Text = txtToolNo.Text.Substring(0, txtToolNo.Text.Length - 4);
                        Application.DoEvents();
                    }
                    else
                    {
                        return;
                    }
                    //----------------------------------------------------------------------------------

                    lblMsg.Text = string.Empty;

                    if (txtToolNo.Text.Trim() == string.Empty)
                    {
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    //檢查治具是否存在
                    DataTable dtPrdAccessory = Global.MESAdpt.funLoadPRDAccessory(Global.ProductNo, Global.OPNo, txtToolNo.Text.ToUpper().Trim());

                    Cursor.Current = Cursors.Default;

                    if (dtPrdAccessory != null && dtPrdAccessory.Rows.Count > 0)
                    {
                        txtToolNo.Text = txtToolNo.Text.ToUpper().Trim();
                        txtPwrBDNo.ReadOnly = false;
                        txtIGBTNo.ReadOnly = false;
                        btnOK.Enabled = true;
                        txtPwrBDNo.Focus();
                        txtToolNo.ReadOnly = true;
                        lblTool.ForeColor = Color.Black;
                        lblPwr.ForeColor = Color.Blue;
                        lblIGBT.ForeColor = Color.Blue;
                    }
                    else
                    {
                        string[] arrMessage = new string[2];
                        arrMessage[0] = Global.ProductNo;
                        arrMessage[1] = txtToolNo.Text.Trim();

                        WriteMessage("[ERROR]查無此機種[{0}]對應的模治具編號[{1}]，請重新確認", arrMessage, Color.Red);

                        txtToolNo.Text = string.Empty;
                        txtToolNo.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void SetInitialRow()
        {
            dtBomData = null;

            txtMono.ReadOnly = true;
            txtOPGroupNo.ReadOnly = true;
            txtPDLineNo.ReadOnly = true;
            txtProductNo.ReadOnly = true;

            txtMono.Text = Global.Mono;
            txtPDLineNo.Text = Global.PDLineNo;
            txtOPGroupNo.Text = Global.OPGroupNo;
            txtProductNo.Text = Global.ProductNo;

            txtToolNo.ReadOnly = false;
            txtProgramNo.ReadOnly = true;
            txtPwrBDNo.ReadOnly = true;
            txtIGBTNo.ReadOnly = true;
            txtDioNo.ReadOnly = true;

            btnOK.Enabled = false;
            lblMsg.Text = string.Empty;
            txtProgramNo.Text = Global.ProgramNo;

            strIP = AppSetting.IP;
            strPort = AppSetting.Port;

            lblMsg1.Text = "註:若機台失去動力，可按機台重置鈕，再按回復動力鈕，若機台沒異常時按下回復動力按鈕，即會直接作業";
            lblMsg1.ForeColor = Color.Red;

            lblTool.ForeColor = Color.Blue;

        }
        private void cleanData()
        {
        }

        private void QueryDiono()
        {
            //檢查程式編號是否有維護
            //DataTable dtProFile = Global.MESAdpt.funLoadOPProfile(Global.ProductNo, "*");
            string strDioNo = AppSetting.DioNo;//整流值前三碼
                                               //取得整流值
            DataTable dtPRDASMS = Global.MESAdpt.funLoadPRDASMS(Global.ProductNo, Global.OPNo, strDioNo);

            if (dtPRDASMS != null && dtPRDASMS.Rows.Count > 0)
            {
                string strComponentNo = dtPRDASMS.Rows[0]["COMPONENTNO"].ToString().Trim(); //取得整流值

                txtDioNo.Text = strComponentNo;
            }
            else
            {
                txtDioNo.Text = "*";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                if (txtPwrBDNo.Text.Trim() == string.Empty)
                {
                    WriteMessage("[ERROR]請先輸入PWR BD值", Color.Red);
                    return;
                }

                if (txtIGBTNo.Text.Trim() == string.Empty)
                {
                    WriteMessage("[ERROR]請先輸入IGBT值", Color.Red);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                bool bolStatus = false;
                bolStatus = QueryPwrBDNoData(txtPwrBDNo.Text.ToUpper().Trim());

                //查詢PWR BD正確性
                if (bolStatus == false)
                {
                    txtPwrBDNo.Text = string.Empty;
                    txtPwrBDNo.Focus();
                    Cursor.Current = Cursors.Default;
                    return;
                }

                //查詢IGBT正確性
                bolStatus = QueryIGBTData(txtIGBTNo.Text.ToUpper().Trim());
                if (bolStatus == false)
                {
                    txtIGBTNo.Text = string.Empty;
                    txtIGBTNo.Focus();
                    Cursor.Current = Cursors.Default;
                    return;
                }

                //查詢設備需程式名稱
                if (string.IsNullOrEmpty(txtProgramNo.Text) == true)
                {
                    bolStatus = QueryProfile(txtProductNo.Text.Trim(), IGBTMaterialNo, txtDioNo.Text.Trim());
                    if (bolStatus == false)
                    {
                        txtIGBTNo.Text = string.Empty;
                        txtIGBTNo.Focus();
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }

                if (bolStatus == true)
                {
                    //儲存機台輸出ECXEL時的參數
                    IGBT = txtIGBTNo.Text.Trim();
                    PWRNO = txtPwrBDNo.Text.Trim();
                    DIONO = txtDioNo.Text.Trim();

                    //操作設備@@@
                    EquipmentStart(txtProgramNo.Text.Trim());

                    //System.Threading.Thread.Sleep(10000);

                    string[] arrMessage = new string[2];
                    arrMessage[0] = txtPwrBDNo.Text;
                    arrMessage[1] = txtIGBTNo.Text;

                    WriteMessage("[OK]PWR BD[{0}]、IGBT[{1}]-->機台開始作業…", arrMessage, Color.Blue);

                    txtPwrBDNo.Text = string.Empty;
                    txtIGBTNo.Text = string.Empty;
                    txtPwrBDNo.Focus();

                }

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EquipmentStart(string LotNo)
        {
            try
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));

                soc.Connect(remoteEP);

                Byte[] buffer = SendProgramNo("8", int.Parse(txtProgramNo.Text.Trim()));//傳送程式名稱

                soc.Send(buffer);
                
                soc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 機台重新回到原位
        /// </summary>
        private void EquipmentReturnHome()
        {
            try
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));

                soc.Connect(remoteEP);

                Byte[] buffer = null;

                buffer = SendStart("7");//傳送程式名稱

                soc.Send(buffer);

                buffer = SendStart("2");//傳送程式名稱

                soc.Send(buffer);

                soc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EquipmentReturnPower()
        {
            try
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));

                soc.Connect(remoteEP);

                Byte[] buffer = null;

                buffer = SendStart("3");//傳送程式名稱

                soc.Send(buffer);

                soc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        ///設備開始動作，含程式碼(12位元)
        ///bt[7]:2=選擇程式、8=選擇程式後啟動
        /// </summary>
        private byte[] SendProgramNo(string CommonCode, int ProgramNo)
        {
            string strData = string.Empty;

            string strProgranNo = Convert.ToString(ProgramNo, 16).ToUpper().Trim();

            if (ProgramNo < 16)
            {
                strData = "000";
            }
            else
            {
                strData = "00";
            }

            object bt2 = Encoding.ASCII.GetBytes(CommonCode + strData + strProgranNo);

            Byte[] buffer = bt2 as Byte[];

            Byte[] bt = new Byte[22];
            bt[0] = 0x00;//固定
            bt[1] = 0x00;//固定
            bt[2] = 0x00;//固定
            bt[3] = 0x0C;//固定
            bt[4] = 0x03;//固定
            bt[5] = 0x03;//固定
            bt[6] = 0x52;//固定Command Code
            bt[7] = buffer[0];//0x31;//動作指令
            bt[8] = buffer[1];//0x30;//程式編號
            bt[9] = buffer[2];//0x30;//程式編號
            bt[10] = buffer[3];//0x31;//程式編號
            bt[11] = buffer[4];//0x39;//程式編號

            return bt;
        }


        /// <summary>
        /// 功能描述:設備開始動作，不含程式碼(8位元)
        /// bt[7]:3=開始動作、2=回到工作區域、7=程式結束(回歸到一般待程式狀態)
        /// </summary>
        private byte[] SendStart(string CommonCode)
        {
            object bt1 = Encoding.ASCII.GetBytes(CommonCode);

            Byte[] buffer = bt1 as Byte[];

            Byte[] bt = new Byte[8];
            bt[0] = 0x00;//固定
            bt[1] = 0x00;//固定
            bt[2] = 0x00;//固定
            bt[3] = 0x08;//固定
            bt[4] = 0x03;//固定
            bt[5] = 0x03;//固定
            bt[6] = 0x52;//固定Command Code
            bt[7] = buffer[0];//固定Command Code

            return bt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool QueryPwrBDNoData(string LotNo)
        {
            DataTable dtProduct_Dist = null;
            DataTable dtMoAsmMapping = null;

            if (LotNo.Length < 5)
            {
                string[] arrMessage = new string[2];
                arrMessage[0] = LotNo;

                WriteMessage("[ERROR]PWR BD[{0}]不允許小於5位碼數", arrMessage, Color.Red);
                return false;
            }

            string strData = LotNo.Substring(2, 3);//固定3~5碼是編碼唯一值，透過編碼找出PWR BD料號

            //檢查PWD BD機邊編碼是否存在
            dtProduct_Dist = Global.MESAdpt.funLoadProduct_Dist(strData);

            if (dtProduct_Dist != null && dtProduct_Dist.Rows.Count > 0)
            {
                string strPWDProductNo = dtProduct_Dist.Rows[0]["PRODUCTNO"].ToString().Trim();

                dtMoAsmMapping = Global.MESAdpt.funLoadMOASMProcedure(Global.Mono, strPWDProductNo);

                //比較BOM資料是否存在BWR BD料號
                //DataRow[] foundRows = dtBomData.Select("MATERIALNO='" + strPWDProductNo + "'");
                if (dtMoAsmMapping.Rows.Count == 0)
                {
                    DataTable dtPRDASMS = Global.MESAdpt.funLoadPRDASMS(Global.ProductNo, strPWDProductNo);

                    if (dtPRDASMS.Rows.Count == 0)
                    {
                        string[] arrMessage = new string[2];
                        arrMessage[0] = strPWDProductNo;

                        WriteMessage("[ERROR]工單組裝關係不存在BWR BD料號[{0}]，請重新確認", arrMessage, Color.Red);
                        return false;
                    }
                }
            }
            else
            {
                string[] arrMessage = new string[1];
                arrMessage[0] = strData;
                WriteMessage("[ERROR]查不到機種簡碼資料，請重新確認(PWR BD[{0}])", arrMessage, Color.Red);

                return false;
            }
            return true;
        }

        private bool QueryIGBTData(string IGBTNo)
        {
            string strIGBTNo = IGBTNo;

            string strSQL = string.Empty;

            //取得IGBT料號
            DataTable dtIGBTData = Global.MESAdpt.funLoadBomByMonoAndBarCode(txtMono.Text.Trim());

            //檢查輸入的IGBT擷取碼對應的料號是否符合BOM表上的料號
            if (dtIGBTData != null && dtIGBTData.Rows.Count > 0)
            {
                string strIGBTNoMapping = string.Empty;

                foreach (DataRow row in dtIGBTData.Rows)
                {
                    int iStartIndex = int.Parse(row["BARCODESTAR"].ToString());
                    int iLengthrow = int.Parse(row["BARCODEEND"].ToString());

                    int iEndIndex = iLengthrow - iStartIndex + 1;

                    if (strIGBTNo.Length >= iLengthrow)
                    {
                        string strOriginalNo = strIGBTNo.Substring(iStartIndex - 1, iEndIndex);
                        string strMappingNo = row["BARCODESTRING"].ToString();

                        if (strOriginalNo == strMappingNo)
                        {
                            strIGBTNoMapping = row["MATERIALNO"].ToString().Trim();
                        }
                    }
                }
                if (strIGBTNoMapping == string.Empty)
                {
                    WriteMessage("[ERROR]查不到此[IGBT]資料存在BOM裡，請重新確認", Color.Red);
                    return false;
                }
                IGBTMaterialNo = strIGBTNoMapping;
                return true;
            }
            else
            {
                string[] arrMessage = new string[1];
                arrMessage[0] = txtMono.Text.Trim();
                WriteMessage("[ERROR]工單BOM料號查不到[物料模組-物料設定-物料條碼]設定資料，請重新確認(工單[{0}])", arrMessage, Color.Red);

                return false;
            }
        }

        private bool QueryProfile(string ProductNO, string IGBTNo, string DioNo)
        {
            DataTable dtProfile = Global.MESAdpt.funLoadCusPrdOpProFile(ProductNO, IGBTNo, DioNo);

            string strProfileNo = string.Empty;

            if (dtProfile != null && dtProfile.Rows.Count > 0)
            {
                strProfileNo = dtProfile.Rows[0]["PROFILENO"].ToString().Trim();

                bool bolResult = Utility.Functions.IsNumeric(strProfileNo);

                if (bolResult == false)
                {
                    string[] arrMessage = new string[2];
                    arrMessage[0] = strProfileNo;
                    WriteMessage("[ERROR]程式編號只允許設定正整數，請重新確認！(當前程式編號[{0}])", arrMessage, Color.Red);
                    return false;
                }
                LowerLimit = float.Parse(dtProfile.Rows[0]["LOWERLIMIT"].ToString());
                UpperLimit = float.Parse(dtProfile.Rows[0]["UPPERLIMIT"].ToString());
            }
            else
            {
                string[] arrMessage = new string[3];
                arrMessage[0] = ProductNO;
                arrMessage[1] = IGBTNo;
                arrMessage[2] = DioNo;
                WriteMessage("[ERROR]查不到機台程式編號，請重新確認(機種[{0}]、IGBT[{1}]、整流值[{2}])", arrMessage, Color.Red);

                return false;
            }
            txtProgramNo.Text = strProfileNo;

            return true;
        }

        /// <summary>
        /// lblMsg寫入訊息
        /// </summary>
        private void WriteMessage(string message, string[] arrMsg, Color ColorWord)
        {
            lblMsg.ForeColor = ColorWord;
            lblMsg.Text = string.Format(message, arrMsg);
        }

        /// <summary>
        /// lblMsg寫入訊息
        /// </summary>
        private void WriteMessage(string message, Color ColorWord)
        {
            lblMsg.ForeColor = ColorWord;
            lblMsg.Text = message;
        }

        private void btnReturnHome_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                EquipmentReturnHome();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnReturnPower_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                EquipmentReturnPower();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
