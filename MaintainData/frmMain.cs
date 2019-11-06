using Common;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace MaintainData
{
    public partial class frmMain : Form
    {
        private int iColumnLength = 0;//判斷是否已選擇GridView
        private string strColumn = string.Empty;
        public frmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {

            this.gvData.AllowUserToAddRows = false;//移除多的空白欄位

            base.OnLoad(e);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                SetInitialRow();

                StringBuilder SQL = new StringBuilder();
                List<OracleParameter> parmList = new List<OracleParameter>();
                string strSQL = string.Empty;
                strSQL = @"select * from CUS_PRDOPPROFILE order by PRODUCTNO ASC ";
                //SQL.AppendLine(" SELECT USERNO             ");
                //SQL.AppendLine("   FROM TBLUSRUSERBASIS    ");
                //SQL.AppendLine("  WHERE USERNO = :USERNO   ");
                //parmList.Add(new OracleParameter(":USERNO", txtUserNo.Text.Trim()));
                //OracleDataReader dr = DB.Read(SQL.ToString(), parmList.ToArray());
                DataTable dtData = DB.ReadDT(strSQL);
                if (dtData.Rows.Count > 0)
                {
                    gvData.DataSource = dtData;
                }

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

            gvData.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right |
                AnchorStyles.Top |
                AnchorStyles.Left;

            gvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//填滿所有



            this.gvData.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12);
            this.gvData.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            this.gvData.MultiSelect = false;

            txtPath.ReadOnly = true;
            lblMsg.Text = string.Empty;

            strColumn = "PRODUCTNO,IGBTMATERIALNO,DIONO,ACCESSORYNO,ACCESSORYTYPE,OPNO,EQUIPMENTNO,PROFILENO,PROFILEDESCR,LOWERLIMIT,UPPERLIMIT";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //目前只要維護一個Table，先寫死，未來若有多個table做維護，再寫成Config方式供USER選擇要寫入到哪個TABLE

                //欄位名稱

                string[] arrColumn = strColumn.Split(',');

                iColumnLength = arrColumn.Count();

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Title = "請選擇文件";
                fileDialog.Filter = "Excel Files|*.xls;*.xlsx";

                string rootpath = string.Empty;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    rootpath = fileDialog.FileName;//返回文件的完整路径                

                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                    rootpath +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                    OleDbConnection con = new OleDbConnection(constr);

                    con.Open();

                    DataTable dtSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string strSheetName = string.Empty;

                    foreach (DataRow row in dtSheetName.Rows)
                    {
                        strSheetName = row["TABLE_NAME"].ToString();
                        break;
                    }

                    OleDbCommand oconn = new OleDbCommand("Select * From [" + strSheetName + "] WHERE PRODUCTNO is not null", con);
                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    var excelColumn = data.Columns;
                    if (excelColumn.Count != arrColumn.Count())
                    {
                        string[] arrMessage = new string[2];
                        arrMessage[0] = Convert.ToString(arrColumn.Count());
                        arrMessage[1] = strColumn;
                        Utility.Functions.WriteErrorMessage("[ERROR]Excel格式錯誤，請確認欄位需為[{0}]欄，順序為:[{1}]", arrMessage);
                        con.Close();
                        return;
                    }
                    //判斷EXCEK欄位與系統欄位是否吻合
                    for (int i = 0; i < arrColumn.Count(); i++)
                    {
                        string strColumn1 = arrColumn[i].Trim().ToUpper();
                        string strColumn2 = excelColumn[i].ToString().ToUpper().Trim();
                        if (strColumn1 != strColumn2)
                        {
                            string[] arrMessage = new string[2];
                            arrMessage[0] = Convert.ToString(i + 1);
                            arrMessage[1] = strColumn1;

                            Utility.Functions.WriteErrorMessage("[ERROR]Excel格式錯誤，第[{0}]欄位需定義為[{1}]，請重新確認", arrMessage);
                            con.Close();
                            return;
                        }
                    }
                    gvData.DataSource = data;
                    gvData.Rows[0].Selected = false;//不做預選

                    txtPath.Text = rootpath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string strTmpSQL = string.Empty;
                string strSQLResult = string.Empty;

                if (txtUserID.Text.Trim() == string.Empty)
                {
                    Utility.Functions.WriteErrorMessage("[ERROR]請先輸入工號");
                    return;
                }

                if (gvData.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in gvData.Rows)
                    {
                        strTmpSQL = " INSERT INTO CUS_PRDOPPROFILE (" + strColumn + ",USERID,UPDATETIME) VALUES (";
                        string strSQLColumn = string.Empty;
                        for (int i = 0; i < iColumnLength; i++)
                        {
                            strSQLColumn = strSQLColumn + "'" + row.Cells[i].Value.ToString().Trim() + "' , ";
                        }
                        strSQLResult = strSQLResult + strTmpSQL + strSQLColumn + "'" + txtUserID.Text.Trim() + "' ,SYSDATE) @ ";
                    }
                }
                else
                {
                    Utility.Functions.WriteErrorMessage("[ERROR]資料列沒有資料，請先選擇檔案");
                    return;
                }
                strSQLResult = strSQLResult.Substring(0, strSQLResult.Length - 2);

                int iResult = Global.MESAdpt.funAddThermalGreaseData(strSQLResult);

                lblMsg.Text = "訊息:資料已完成匯入";
                lblMsg.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                lblMsg.Text = "訊息:匯入失敗，請重新確認";
                lblMsg.ForeColor = Color.Red;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
