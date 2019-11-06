using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using Excel = Microsoft.Office.Interop.Excel;

namespace CIM
{
    public partial class test : Form
    {
        FileInfo fi;
        StringBuilder sb;
        FileSystemWatcher _watch = new FileSystemWatcher();
        private Thread m_thWork = null;
        private string rootpath = string.Empty;
        private string rootpathTmp = string.Empty;
        DirectoryInfo RptPath = null;

        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            try
            {
                //設定Log組態檔位置
                Utility.LogHelper.InitConfig(AppDomain.CurrentDomain.BaseDirectory + "logConfig.xml");

                rootpath = AppSetting.OutputData;
                rootpathTmp = rootpath + "tmp\\";
                RptPath = new DirectoryInfo(rootpath);

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
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        DataTable GetDataTable(string tableName, string leftTopCel, string rightbutCel)
        {
            bool hasTitle = false;
            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.Filter = "Excel(*.xlsx;*.xls)|*.xlsx;*.xls|所有文件(*.*)|*.*";
            openFile.Filter = "Excel(*.csv;*.xls)|*.xlsx;*.xls|所有文件(*.*)|*.*";
            string test1 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.InitialDirectory = "D:\\test123\\";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel) return null;
            var filePath = openFile.FileName;
            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            using (DataSet ds = new DataSet())
            {
                string strCon = string.Format("Provider=Microsoft.Jet.OLEDB.{0}.0;" +
                                "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                "data source={3};",
                                (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filePath);
                string strCom = " SELECT * FROM [" + tableName + "$" + leftTopCel + ":" + rightbutCel + "]  ";
                using (OleDbConnection myConn = new OleDbConnection(strCon))
                using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                {
                    myConn.Open();
                    myCommand.Fill(ds);
                }

                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds.Tables[0];


            }
        }

        private void MyFileSystemWatcher()
        {
            //設定所要監控的資料夾
            _watch.Path = rootpath;

            //設定所要監控的變更類型
            //_watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watch.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
            //設定所要監控的檔案
            _watch.Filter = "*.csv";

            //設定是否監控子資料夾
            // _watch.IncludeSubdirectories = true;

            //設定是否啟動元件，此部分必須要設定為 true，不然事件是不會被觸發的
            _watch.EnableRaisingEvents = true;

            //設定觸發事件
            _watch.Created += new FileSystemEventHandler(_watch_Created);
            _watch.Changed += new FileSystemEventHandler(_watch_Changed);
            _watch.Renamed += new RenamedEventHandler(_watch_Renamed);
            _watch.Deleted += new FileSystemEventHandler(_watch_Deleted);
        }

        /// <summary>
        /// 當所監控的資料夾有建立文字檔時觸發
        /// </summary>
        private void _watch_Created(object sender, FileSystemEventArgs e)
        {
            //sb = new StringBuilder();

            //dirInfo = new DirectoryInfo(e.FullPath.ToString());

            //sb.AppendLine("新建檔案於：" + dirInfo.FullName.Replace(dirInfo.Name, ""));
            //sb.AppendLine("新建檔案名稱：" + dirInfo.Name);
            //sb.AppendLine("建立時間：" + dirInfo.CreationTime.ToString());
            //sb.AppendLine("目錄下共有：" + dirInfo.Parent.GetFiles().Count() + " 檔案");
            //sb.AppendLine("目錄下共有：" + dirInfo.Parent.GetDirectories().Count() + " 資料夾");
        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案內容有異動時觸發
        /// </summary>
        private void _watch_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                LogHelper.Info("====== 資料儲存 ======");

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

                FileInfo[] filelist = RptPath.GetFiles("*.csv");  //擷取目錄下所有檔案內容，並存到 FileInfo array

                foreach (var file in filelist)
                {
                    // 檔案存在就刪除
                    if (System.IO.File.Exists(rootpathTmp + file))
                    {
                        try
                        {
                            System.IO.File.Delete(rootpathTmp + file);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }
                    }

                    File.Move(rootpath + file, rootpathTmp + file);

                    //System.IO.File.Delete(rootpath + file);

                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(rootpathTmp + file);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rowCount = xlRange.Rows.Count;//EXCEL 列數
                                                      //int colCount = xlRange.Columns.Count;//EXCEL 欄數

                    string strSQL = string.Empty;

                    string strResultSQL = string.Empty;

                    string IGBT = "IGBT";
                    string PWRNO = "PWRNO";
                    string DIONO = "DIONO";

                    string Range = "1";
                    string Result = "Pass";
                    string USERID = "548779";

                    for (int i = 8; i <= rowCount; i++)
                    {
                        strSQL = " INSERT INTO CUS_THERMALDATA VALUES ( '" + IGBT + i + "','" + PWRNO + i + "','" + DIONO + i + "',";

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
                            strTmpSQL = strTmpSQL + "'" + Range + "','" + Math.Abs(newDara - oldData) + "','" + Result + "','" + USERID + "',SYSDATE,'" + file + "') @ ";
                            strResultSQL = strResultSQL + strTmpSQL;
                        }
                    }
                    strResultSQL = strResultSQL.Substring(0, strResultSQL.Length - 2);

                    Global.MainForm = this;
                    Global.MESAdpt = new Common.MESAdapter();

                    int iResult = Global.MESAdpt.funAddThermalGreaseData(strResultSQL);

                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();


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


                    System.IO.File.Delete(rootpathTmp + file);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案重新命名時觸發
        /// </summary>
        private void _watch_Renamed(object sender, RenamedEventArgs e)
        {
            sb = new StringBuilder();

            fi = new FileInfo(e.FullPath.ToString());

            sb.AppendLine("檔名更新前：" + e.OldName.ToString());
            sb.AppendLine("檔名更新後：" + e.Name.ToString());
            sb.AppendLine("檔名更新前路徑：" + e.OldFullPath.ToString());
            sb.AppendLine("檔名更新後路徑：" + e.FullPath.ToString());
            sb.AppendLine("建立時間：" + fi.LastAccessTime.ToString());

            MessageBox.Show(sb.ToString());
        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案有被刪除時觸發
        /// </summary>
        private void _watch_Deleted(object sender, FileSystemEventArgs e)
        {
            sb = new StringBuilder();

            sb.AppendLine("被刪除的檔名為：" + e.Name);
            sb.AppendLine("檔案所在位址為：" + e.FullPath.Replace(e.Name, ""));
            sb.AppendLine("刪除時間：" + DateTime.Now.ToString());

            MessageBox.Show(sb.ToString());
        }

        private void btn_Before_Click(object sender, EventArgs e)
        {
            ReadTextFile(txtBefore);
        }

        private void btn_After_Click(object sender, EventArgs e)
        {
            ReadTextFile(txtAfter);
        }

        /// <summary>
        /// 將文字檔內容顯示於所指定的 TextBox
        /// </summary>
        /// <param name="p_TextBox">所指定的 TextBox</param>
        private void ReadTextFile(TextBox p_TextBox)
        {
            sb = new StringBuilder();

            string[] Txt_All_Lines = System.IO.File.ReadAllLines(@"D:\\OUTPUT\\aaa.csv", Encoding.Default);

            foreach (string Single_Line in Txt_All_Lines)
            {
                sb.AppendLine(Single_Line);
            }

            p_TextBox.Text = sb.ToString();
        }

        private void btn_Before_Click_1(object sender, EventArgs e)
        {
            try
            {
                string rootpath = AppSetting.OutputData;
                string rootpathTmp = rootpath + "tmp\\";

                DirectoryInfo RptPath = new DirectoryInfo(rootpath);

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

                FileInfo[] filelist = RptPath.GetFiles("*.csv");  //擷取目錄下所有檔案內容，並存到 FileInfo array

                foreach (var file in filelist)
                {
                    File.Move(rootpath + file, rootpathTmp + file);

                    //System.IO.File.Delete(rootpath + file);

                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(rootpathTmp + file);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rowCount = xlRange.Rows.Count;//EXCEL 列數
                                                      //int colCount = xlRange.Columns.Count;//EXCEL 欄數

                    string strSQL = string.Empty;

                    string strResultSQL = string.Empty;

                    string IGBT = "IGBT";
                    string PWRNO = "PWRNO";
                    string DIONO = "DIONO";

                    string Range = "1";
                    string Result = "Pass";
                    string USERID = "548779";

                    for (int i = 8; i <= 10; i++)
                    {
                        strSQL = " INSERT INTO CUS_THERMALDATA VALUES ( '" + IGBT + i + "','" + PWRNO + i + "','" + DIONO + i + "',";

                        string strTmpSQL = string.Empty;
                        strTmpSQL = strSQL;

                        float oldData = 0;
                        float newDara = 0;

                        for (int j = 1; j <= 5; j++)
                        {
                            string strItemValue = string.Empty;

                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                strItemValue = xlRange.Cells[i, j].Value2.ToString();
                            }
                            if (j == 4)
                            {
                                oldData = float.Parse(strItemValue);
                            }
                            if (j == 5)
                            {
                                newDara = float.Parse(strItemValue);
                            }
                            strTmpSQL = strTmpSQL + "'" + strItemValue + "',";
                        }
                        //strTmpSQL = strTmpSQL.Substring(0, strTmpSQL.Length - 1);
                        strTmpSQL = strTmpSQL + "'" + Range + "','" + Math.Abs(newDara - oldData) + "','" + Result + "','" + USERID + "',SYSDATE ) @ ";
                        strResultSQL = strResultSQL + strTmpSQL;
                    }
                    strResultSQL = strResultSQL.Substring(0, strResultSQL.Length - 2);

                    Global.MainForm = this;
                    Global.MESAdpt = new Common.MESAdapter();

                    int iResult = Global.MESAdpt.funAddThermalGreaseData(strResultSQL);

                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();


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


                    System.IO.File.Delete(rootpathTmp + file);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
