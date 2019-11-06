using Common;
using Modbus.Device;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Utility;

namespace PLC_RTUEN01
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        private bool bolLeftLight = false; //
        private bool bolRightLight = false; //
        Thread myThread1 = null;
        //Thread myThread2 = null;
        Socket soc1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Byte[] bufferLeftOpen = null;
        Byte[] bufferLeftClose = null;

        Byte[] bufferRightOpen = null;
        Byte[] bufferRightClose = null;

        int EQPDelayTime = int.Parse(AppSetting.EQPDelayTime.ToString());

        DataTable dtData = null;
        Logger logger = null;

        string LeftInTime = string.Empty;
        string LeftOutTime = string.Empty;
        string RightInTime = string.Empty;
        string RightOutTime = string.Empty;

        public frmMain()
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(frmMain_Closing);//觸發關閉視窗事件
            InitializeComponent();
            InitializeTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetInitialRow();

                try
                {
                    System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                    System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));
                    soc1.Connect(remoteEP);

                    int iLeftClose = soc1.Send(bufferLeftClose);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("設備連線異常，系統即將關閉。");
                    this.Close();
                }
                LoadMainData();

                ThreadStart myRun1 = new ThreadStart(RunThread1);
                myThread1 = new Thread(myRun1);
                myThread1.IsBackground = true;
                myThread1.Start();

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                LoadMainData();
            }
            catch (Exception ex)
            {
                logger.Error("Error:" + ex.ToString());
            }
        }

        private void InitializeTimer()
        {
            timer1.Interval = int.Parse(AppSetting.EQPQueryTime);
            //timer1.Tick += new EventHandler(timer1_Tick);
        }

        /// <summary>
        ///初始化設定
        /// </summary>
        private void SetInitialRow()
        {
            Global.MESAdpt = new MESAdapter();
            logger = LogManager.GetCurrentClassLogger();

            lblChamberLeft.Text = AppSetting.ChamberID + " Left";
            lblChamberRight.Text = AppSetting.ChamberID + " Right";
            lblStatusLeft.Text = "Wait";
            lblStatusRight.Text = "Wait";
            lblBurnInTimeLeft.Text = "N/A";
            lblBurnOutTimeLeft.Text = "N/A";
            lblReminderTimeLeft.Text = "N/A";

            lblBurnInTimeRight.Text = "N/A";
            lblBurnOutTimeRight.Text = "N/A";
            lblReminderTimeRight.Text = "N/A";

            pictureBox1.Image = Image.FromFile(@"Images\empty.png");
            pictureBox2.Image = Image.FromFile(@"Images\empty.png");

            //Byte CommLeftBt = 0x04;

            byte[] Buffer = new Byte[1];

            int intValue = AppSetting.Buffer;
            Buffer = BitConverter.GetBytes(intValue);

            byte[] oriBytes = { 0xB0, 0x2D };

            // byte[] bt = {0x2D };

            //Byte CommLeftBt = bBuffer[0];  //0x2D;//訊號55亮燈

            bufferLeftOpen = new Byte[12];
            bufferLeftOpen[0] = 0x00;//Identifier
            bufferLeftOpen[1] = 0x00;//Identifier
            bufferLeftOpen[2] = 0x00;//Identifier
            bufferLeftOpen[3] = 0x00;//Identifier
            bufferLeftOpen[4] = 0x00;//Modbus指令的長度
            bufferLeftOpen[5] = 0x06;//Modbus指令的長度
            bufferLeftOpen[6] = 0x01;//站號
            bufferLeftOpen[7] = 0x05;//功能碼
            bufferLeftOpen[8] = 0x05;//Modbus位址
            bufferLeftOpen[9] = Buffer[0];//Modbus位址(第幾個訊號要On)
            bufferLeftOpen[10] = 0xFF;//寫入值，FF 00 代表將此Coil輸出設定為ON
            bufferLeftOpen[11] = 0x00;//寫入值，00 00 代表將此Coil輸出設定為OFF



            bufferLeftClose = new Byte[12];
            bufferLeftClose[0] = 0x00;//Identifier
            bufferLeftClose[1] = 0x00;//Identifier
            bufferLeftClose[2] = 0x00;//Identifier
            bufferLeftClose[3] = 0x00;//Identifier
            bufferLeftClose[4] = 0x00;//Modbus指令的長度
            bufferLeftClose[5] = 0x06;//Modbus指令的長度
            bufferLeftClose[6] = 0x01;//站號
            bufferLeftClose[7] = 0x05;//功能碼
            bufferLeftClose[8] = 0x05;//Modbus位址
            bufferLeftClose[9] = Buffer[0];//Modbus位址(第幾個訊號要On)
            bufferLeftClose[10] = 0x00;//寫入值，FF 00 代表將此Coil輸出設定為ON
            bufferLeftClose[11] = 0x00;//寫入值，00 00 代表將此Coil輸出設定為OFF

            Byte CommRightBt = 0x04;

            bufferRightOpen = new Byte[12];
            bufferRightOpen[0] = 0x00;//Identifier
            bufferRightOpen[1] = 0x00;//Identifier
            bufferRightOpen[2] = 0x00;//Identifier
            bufferRightOpen[3] = 0x00;//Identifier
            bufferRightOpen[4] = 0x00;//Modbus指令的長度
            bufferRightOpen[5] = 0x06;//Modbus指令的長度
            bufferRightOpen[6] = 0x01;//站號
            bufferRightOpen[7] = 0x05;//功能碼
            bufferRightOpen[8] = 0x05;//Modbus位址
            bufferRightOpen[9] = CommRightBt;//Modbus位址(第幾個訊號要On)
            bufferRightOpen[10] = 0xFF;//寫入值，FF 00 代表將此Coil輸出設定為ON
            bufferRightOpen[11] = 0x00;//寫入值，00 00 代表將此Coil輸出設定為OFF

            bufferRightClose = new Byte[12];
            bufferRightClose[0] = 0x00;//Identifier
            bufferRightClose[1] = 0x00;//Identifier
            bufferRightClose[2] = 0x00;//Identifier
            bufferRightClose[3] = 0x00;//Identifier
            bufferRightClose[4] = 0x00;//Modbus指令的長度
            bufferRightClose[5] = 0x06;//Modbus指令的長度
            bufferRightClose[6] = 0x01;//站號
            bufferRightClose[7] = 0x05;//功能碼
            bufferRightClose[8] = 0x05;//Modbus位址
            bufferRightClose[9] = CommRightBt;//Modbus位址(第幾個訊號要On)
            bufferRightClose[10] = 0x00;//寫入值，FF 00 代表將此Coil輸出設定為ON
            bufferRightClose[11] = 0x00;//寫入值，00 00 代表將此Coil輸出設定為OFF

        }

        /// <summary>
        ///讀取基本資訊
        /// </summary>
        private void LoadMainData()
        {
            //logger.Info("=========== 開始處理 ===========");

            string strSQL = string.Empty;

            dtData = null;

            dtData = Global.MESAdpt.funLoadEQPBIData(AppSetting.ChamberID);

            if (dtData.Rows.Count > 0)
            {

                foreach (DataRow item in dtData.Rows)
                {
                    string strSTATUS = item["STATUS"].ToString();

                    if (strSTATUS.ToUpper() == "FINISHED")
                    {
                        continue;
                    }

                    string strChamberNo = item["CHBR_NO"].ToString();
                    string strChamberLocation = item["CCB_DIRECTION"].ToString();
                    string strBurnInTime = item["CCB_COMBINETIME"].ToString();
                    string strBurnOutTime = item["CCB_BURNOUTTIME"].ToString();
                    string strReminderTime = string.Empty;

                    string strTmpReminderTime = item["BURNOUTTIME"].ToString();
                    DateTime dtTime = Convert.ToDateTime(strTmpReminderTime);
                    dtTime = dtTime.AddMinutes(-(int.Parse(AppSetting.WarnTime)));
                    strReminderTime = dtTime.ToString("MM/dd HH:mm");

                    switch (strChamberLocation)
                    {
                        case "L":
                            LeftInTime = item["CONNECTINSID"].ToString();
                            LeftOutTime = item["CONNECTOUTSID"].ToString();

                            if (DateTime.Now > dtTime)
                            {
                                //傳遞訊號通知
                                bolLeftLight = true;

                                lblStatusLeft.Text = "Warn";
                                lblStatusLeft.ForeColor = Color.Red;
                                pictureBox1.Image = Image.FromFile(@"Images\redBlinking.gif");
                            }
                            else
                            {
                                bolLeftLight = false;
                                lblStatusLeft.Text = "Run";
                                lblStatusLeft.ForeColor = Color.Green;
                                pictureBox1.Image = Image.FromFile(@"Images\green.png");
                            }

                            lblBurnInTimeLeft.Text = strBurnInTime;
                            lblBurnOutTimeLeft.Text = strBurnOutTime;
                            lblReminderTimeLeft.Text = strReminderTime;
                            break;
                        case "R":
                            RightInTime = item["CONNECTINSID"].ToString();
                            RightOutTime = item["CONNECTOUTSID"].ToString();

                            if (DateTime.Now > dtTime)
                            {
                                //傳遞訊號通知
                                bolRightLight = true;

                                lblStatusRight.Text = "Warn";
                                lblStatusRight.ForeColor = System.Drawing.Color.Red;
                                pictureBox2.Image = Image.FromFile(@"Images\redBlinking.gif");
                            }
                            else
                            {
                                bolRightLight = false;
                                lblStatusRight.Text = "Run";
                                lblStatusRight.ForeColor = Color.Green;
                                pictureBox2.Image = Image.FromFile(@"Images\green.png");
                            }
                            lblBurnInTimeRight.Text = strBurnInTime;
                            lblBurnOutTimeRight.Text = strBurnOutTime;
                            lblReminderTimeRight.Text = strReminderTime;
                            break;
                        default:
                            break;
                    }
                }

            }
            //logger.Info("=========== 處理完成 ===========");
        }

        private void RunThread1()
        {
            try
            {
                int iLoopCount = Utility.AppSetting.SendCount;

                int iexcuteCount = iLoopCount;

                int iLoop = 10;

                while (iLoop == 10)
                {
                    try
                    {
                        if (soc1.Connected == false)
                        {
                            soc1 = null;
                            soc1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));
                            soc1.Connect(remoteEP);
                        }


                        if (bolLeftLight == false && bolRightLight == false)
                        {
                            iexcuteCount = iLoopCount;
                            int iClose = soc1.Send(bufferLeftClose);
                            Thread.Sleep(500);
                        }
                        else
                        {

                            iexcuteCount--;

                            if (iexcuteCount >= 0)
                            {
                                int iOpen = soc1.Send(bufferLeftOpen);
                                Thread.Sleep(EQPDelayTime);
                                int iClose = soc1.Send(bufferLeftClose);
                                Thread.Sleep(EQPDelayTime);
                            }
                            else
                            {
                                int iOpen = soc1.Send(bufferLeftOpen);
                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(60000);
                        logger.Info("Error:設備連線失敗，" + ex.ToString() + "\r\n");
                        soc1 = null;
                        soc1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(AppSetting.IP);
                        System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse(AppSetting.Port));
                        soc1.Connect(remoteEP);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("Error:" + ex.ToString() + "\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int CommonCode = 0;

                Byte CommBt = 0;
                switch (CommonCode)
                {
                    case 0: 
                        CommBt = 0x00;
                        break;
                    case 1: 
                        CommBt = 0x01;
                        break;
                    default:
                        CommBt = 0x00;
                        break;
                }
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.1.3");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 502);

                soc.Connect(remoteEP);

                Byte[] buffer = new Byte[12];
                buffer[0] = 0x00;//Identifier
                buffer[1] = 0x00;//Identifier
                buffer[2] = 0x00;//Identifier
                buffer[3] = 0x00;//Identifier
                buffer[4] = 0x00;//Modbus指令的長度
                buffer[5] = 0x06;//Modbus指令的長度
                buffer[6] = 0x01;//站號
                buffer[7] = 0x05;//功能碼
                buffer[8] = 0x05;//Modbus位址
                buffer[9] = CommBt;//Modbus位址(第幾個訊號要On)
                buffer[10] = 0xFF;//寫入值，FF 00 代表將此Coil輸出設定為ON
                buffer[11] = 0x00;//寫入值，00 00 代表將此Coil輸出設定為OFF
                int aa = soc.Send(buffer);

                soc.Close();

                //Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                logger.Info("Error:" + ex.ToString() + "\r\n");
            }
        }

        private void btnLeftClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (bolLeftLight == true)
                {
                    string strChbrNo = AppSetting.ChamberID;
                    string strDirection = "L";

                    int iResult = Global.MESAdpt.funInsertEQPData(strChbrNo, strDirection, LeftInTime, LeftOutTime);

                    if (iResult < 1)
                    {

                    }

                    bolLeftLight = false;
                    pictureBox1.Image = Image.FromFile(@"Images\empty.png");
                }
            }
            catch (Exception ex)
            {
                logger.Info("Error:" + ex.ToString() + "\r\n");
            }
        }

        private void btnRightClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (bolRightLight == true)
                {
                    string strChbrNo = AppSetting.ChamberID;
                    string strDirection = "R";

                    int iResult = Global.MESAdpt.funInsertEQPData(strChbrNo, strDirection, RightInTime, RightOutTime);

                    bolRightLight = false;
                    pictureBox2.Image = Image.FromFile(@"Images\empty.png");
                }
            }
            catch (Exception ex)
            {
                logger.Info("Error:" + ex.ToString());
            }
        }

        /// <summary>
        ///Form關閉時觸發事件
        /// </summary>
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                myThread1.Abort();
                int iLeftClose = soc1.Send(bufferLeftClose);
            }
            catch (Exception ex)
            {
                logger.Info("Error:" + ex.ToString() + "\r\n");
            }
            finally
            {
                soc1.Close();
                //soc2.Close();
            }
            Application.Exit();//關閉所有視窗
        }

    }

}
