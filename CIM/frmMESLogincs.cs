using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIM
{
    public partial class frmMESLogincs : Form
    {


        public frmMESLogincs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //txtUserId.SelectAll();
            Application.DoEvents();

            string strUserID = txtUserID.Text.Trim();

            if (string.IsNullOrEmpty(strUserID) == false)
            {
                string strTempUser = "";
                int intLogInState = 0;

                if (Global.MESAdpt.funLoadUserAccount(tbxUserNo.Text.Trim().ToUpper(), ref strTempUser) == -1)
                {
                    this.tbxMESMsg.Text = "帳號無法辨識";
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
                    if (Global.MESAdpt.ChkOPPrivilege(strTempUser, Global.OPNo, "Null") == true)
                    {
                        //檢查工號是否登入
                        intLogInState = Global.MESAdpt.ChkLogInstate(strTempUser, Global.PDLineNo, Global.OPNo, "Null");

                        if (intLogInState == -1)
                        {
                            this.tbxMESMsg.Text = "重覆登入";
                        }
                        else if (intLogInState == 1)
                        {
                            this.tbxMESMsg.Text = "已登入它站";
                        }
                        else
                        {
                            //新增登入資訊
                            if (Global.MESAdpt.funAddOperator(strTempUser, Global.PDLineNo, Global.OPNo, "Null", 1) == -1)
                            {
                                this.tbxMESMsg.Text = "重覆登入";
                            }
                            else
                            {
                                this.tbxMESMsg.Text = "登入成功";
                                Global.UserNo = strTempUser;
                                Global.MESAdpt.UserName = strTempUser;
                            }
                        }
                    }
                    else
                    {
                        this.tbxMESMsg.Text = "無此作業站資格";
                    }
                }
            }


        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            txtUserID.SelectAll();
        }
    }
}
