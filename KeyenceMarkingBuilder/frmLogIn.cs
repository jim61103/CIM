using Common;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace KeyenceMarkingBuilder
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(frmLogIn_Closing);//觸發關閉視窗事件
            txtUserNo.KeyPress += TxtUserNo_KeyPress;

        }

        private void TxtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == Convert.ToChar(13))//按下ENTER鍵
                {
                    if (txtUserNo.Text.Trim() == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        btnConfirm.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmLogIn_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();//關閉所有視窗
        }
        private void frmLogIn_Load(object sender, EventArgs e)
        {
            Global.MainForm = this;
            Global.MESAdpt = new MESAdapter();
            Global.UserNo = string.Empty;
            Global.OPNo = AppSetting.OPNo;//作業站編號
            Global.PDLineNo = AppSetting.PDLineNo;
            Global.OPGroupNo = AppSetting.OPGroupNo;
            lblMsg.Text = string.Empty;
            txtUserNo.Focus();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                lblMsg.Text = string.Empty;

                txtUserNo.SelectAll();
                Application.DoEvents();

                if (txtUserNo.Text.Trim() != "")
                {
                    string strTempUser = "";
                    int intLogInState = 0;

                    try
                    {
                        StringBuilder SQL = new StringBuilder();
                        List<OracleParameter> parmList = new List<OracleParameter>();
                        SQL.AppendLine(" SELECT USERNO             ");
                        SQL.AppendLine("   FROM TBLUSRUSERBASIS    ");
                        SQL.AppendLine("  WHERE USERNO = :USERNO   ");
                        parmList.Add(new OracleParameter(":USERNO", txtUserNo.Text.Trim()));
                        OracleDataReader dr = DB.Read(SQL.ToString(), parmList.ToArray());
                        if (dr.Read())
                        {
                            this.lblMsg.Text = "登入成功";
                            Global.UserNo = txtUserNo.Text.Trim();
                            Global.MESAdpt.UserName = txtUserNo.Text.Trim();

                            frmMain objfrmMain = new frmMain();
                            this.Visible = false;//將Form1隱藏

                            objfrmMain.ShowDialog(this);
                            //this.Close();
                            this.Hide();
                        }
                        else
                        {
                            this.lblMsg.Text = "登入失敗";
                        }
                        dr.Close();
                    }
                    catch
                    {

                        if (Global.MESAdpt.funLoadUserAccount(txtUserNo.Text.Trim().ToUpper(), ref strTempUser) == -1)
                        {
                            this.lblMsg.Text = "帳號無法辨識";
                        }
                        else
                        {
                            //檢查是否有設定"允許重複登入"屬性
                            if (Global.MESAdpt.funLoadOPProperty(Global.OPNo, "MultipleLogIn") != 0)
                            {
                                //帳號先登出再重新登入(避免帳號重覆登入)
                                Global.MESAdpt.funAddOperator(strTempUser, "", "", "", 2);
                            }

                            //檢查是否有登入權限
                            if (Global.MESAdpt.ChkOPPrivilege(strTempUser, Global.OPNo, Global.PDLineNo) == true)
                            {
                                //檢查工號是否登入
                                intLogInState = Global.MESAdpt.ChkLogInstate(strTempUser, Global.PDLineNo, Global.OPNo, Global.PDLineNo);

                                if (intLogInState == -1)
                                {
                                    this.lblMsg.Text = "重覆登入";
                                }
                                else if (intLogInState == 1)
                                {
                                    this.lblMsg.Text = "已登入它站";
                                }
                                else
                                {
                                    //新增登入資訊
                                    if (Global.MESAdpt.funAddOperator(strTempUser, Global.PDLineNo, Global.OPNo, Global.PDLineNo, 1) == -1)
                                    {
                                        this.lblMsg.Text = "重覆登入";
                                    }
                                    else
                                    {
                                        this.lblMsg.Text = "登入成功";
                                        Global.UserNo = strTempUser;
                                        Global.MESAdpt.UserName = strTempUser;

                                        frmMain objfrmMain = new frmMain();
                                        this.Visible = false;//將Form1隱藏

                                        objfrmMain.ShowDialog(this);
                                        //this.Close();
                                        this.Hide();

                                    }
                                }
                            }
                            else
                            {
                                this.lblMsg.Text = "無此作業站資格";
                            }
                        }
                    }
                }
                //==
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void cleanData()
        {
            txtUserNo.Text = string.Empty;
            txtUserNo.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}


