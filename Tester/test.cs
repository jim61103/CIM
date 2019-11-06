using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            LabelManager2.Application OLE_Server = null;
            LabelManager2.Document OLE_DOC = null;
            string strResult = "";
            string strFile = "";
            OLE_Server = new LabelManager2.Application();

            //string ConnectionString = ConfigurationManager.AppSettings["ConnStr"];

            //using (var con = new OracleConnection(ConnectionString))//替換成自己的connection
            //{
            //    con.Open();


            //    string strSQL = @"INSERT INTO CHBR_CAR_WARNLOG VALUES ('{0}','{1}','{2}', '{3}','Finished',SYSDATE)";
            //    strSQL = string.Format(strSQL, Convert.ToString("CC"), "AA", "2019", "2018");



            //    OracleCommand cmd = con.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = strSQL;
            //    cmd.ExecuteScalar();

            //    DataTable dtData1 = DB.ReadDT("select * from CHBR_CAR_WARNLOG");
            //}


            //return;

            //using (OracleConnection con = new OracleConnection(ConnectionString))
            //{
            //    con.Open();

            //    OracleCommand cmd = new OracleCommand();
            //    cmd.Connection = con;
            //    OracleTransaction trans = con.BeginTransaction();
            //    cmd.Transaction = trans;

            //    string strSQL = "";
            //    int intAffect = 0;
            //    try
            //    {
            //        DataTable dtData1 = null;
            //        for (int i = 0; i < 9; i++)
            //        {

            //            dtData1 = null;
            //            dtData1 = DB.ReadDT("select * from CHBR_CAR_WARNLOG");

            //            strSQL = @"INSERT INTO CHBR_CAR_WARNLOG VALUES ('{0}','{1}','{2}', '{3}','Finished',SYSDATE)";
            //            strSQL = string.Format(strSQL, Convert.ToString(intAffect), "AA", "2019", "2018");
            //            cmd.CommandText = strSQL;
            //            cmd.ExecuteNonQueryAsync();

            //            intAffect = intAffect + 1;
            //        }
            //    }
            //    catch (OracleException E)
            //    {
            //        trans.Rollback();
            //        con.Close();
            //        throw new Exception("ExecuteTrans(): " + E.Message + ": " + strSQL);
            //    }
            //    finally
            //    {
            //        con.Close();
            //    }
            //    trans.Commit();
            //}


        }

    }
}
