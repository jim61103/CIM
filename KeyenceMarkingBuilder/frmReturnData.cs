using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyenceMarkingBuilder
{
    public partial class frmReturnData : Form
    {
        private DataTable dtUseData = null;
        public frmReturnData()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(frmReturnData_Closing);//觸發關閉視窗事件
            this.Shown += frmReturnData_Shown;

            txtMono.KeyPress += TxtMono_KeyPress;

            base.OnLoad(e);
        }
        private void frmReturnData_Load(object sender, EventArgs e)
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

        private void SetInitialRow()
        {
            dtUseData = null;

            txtMono.ReadOnly = false;
            txtMono.Text = string.Empty;

            lblMsg.Text = string.Empty;
            lblUserIDShow.Text = Global.UserNo;
            lblUnFinishedSn.ForeColor = Color.Blue;
            lblFinishedSn.ForeColor = Color.Blue;
            lblUnQTY.Text = string.Empty;
            lblQTY.Text = string.Empty;
            lblUnQTY.ForeColor = Color.Blue;
            lblQTY.ForeColor = Color.Blue;

            txtSn.Text = string.Empty;
            txtSn.ReadOnly = true;

            btnConfirm.Enabled = false;
            btnQuery.Enabled = true;

            ltbUnFinishedSn.Items.Clear();
            ltbFinishedSn.Items.Clear();

        }
        private void CleanData()
        {
            dtUseData = null;

            txtMono.ReadOnly = false;
            txtMono.Text = string.Empty;

            lblMsg.Text = string.Empty;
            lblUserIDShow.Text = Global.UserNo;
            lblUnFinishedSn.ForeColor = Color.Blue;
            lblFinishedSn.ForeColor = Color.Blue;
            lblUnQTY.Text = string.Empty;
            lblQTY.Text = string.Empty;
            lblUnQTY.ForeColor = Color.Blue;
            lblQTY.ForeColor = Color.Blue;

            txtSn.Text = string.Empty;
            txtSn.ReadOnly = true;

            btnConfirm.Enabled = false;
            btnQuery.Enabled = true;

            ltbUnFinishedSn.Items.Clear();
            ltbFinishedSn.Items.Clear();
        }

        private void frmReturnData_Shown(object sender, EventArgs e)
        {
            txtMono.Focus();
        }
        private void frmReturnData_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();//關閉所有視窗
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dtUseData = null;

                if (txtMono.Text.Trim() == string.Empty)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                #region 讀取工單資訊

                string strMono = txtMono.Text.Trim();

                DataTable dtMono = Global.MESAdpt.funLoadOEMOBasis(strMono);

                if (dtMono != null && dtMono.Rows.Count > 0)
                {
                    DataTable dtMoSerialData = Global.MESAdpt.funShowOEMOSerialCreateDetail(strMono, "6");

                    if (dtMoSerialData != null && dtMoSerialData.Rows.Count > 0)
                    {
                        dtMoSerialData.DefaultView.Sort = "SERIALNO";

                        foreach (DataRow row in dtMoSerialData.Rows)
                        {
                            ltbUnFinishedSn.Items.Add(row["SERIALNO"].ToString());
                        }
                        //取得已完成雷雕紀錄
                        dtUseData = Global.MESAdpt.funLoadWipMarkBuilder(strMono, string.Empty, string.Empty, string.Empty, string.Empty);

                        //移除已完成紀錄
                        foreach (DataRow row in dtUseData.Rows)
                        {
                            string strItem = row["SERIALNO"].ToString();
                            ltbUnFinishedSn.Items.Remove(strItem);
                            ltbFinishedSn.Items.Add(strItem);
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

                lblUnQTY.Text = "(" + Convert.ToString(ltbUnFinishedSn.Items.Count) + ")";
                lblQTY.Text = "(" + Convert.ToString(ltbFinishedSn.Items.Count) + ")";

                lblMsg.Text = "訊息:請輸入重工序號";
                lblMsg.ForeColor = Color.Blue;

                txtMono.ReadOnly = true;

                btnQuery.Enabled = false;
                btnConfirm.Enabled = true;
                txtSn.ReadOnly = false;
                txtSn.Focus();
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSn.Text.Trim() == string.Empty)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;

                string strSn = txtSn.Text.Trim();

                DataRow[] foundRows = dtUseData.Select("SERIALNO='" + strSn + "'");

                if (foundRows.Count() == 0)
                {
                    string[] arrMessage = new string[1];
                    arrMessage[0] = strSn;

                    Utility.Functions.WriteErrorMessage("查無此重工序號[{0}]，請重新確認", arrMessage);
                    return;
                }

                int iResult = Global.MESAdpt.funDelWipMarkBuilder(txtMono.Text.Trim(), string.Empty, strSn, string.Empty, string.Empty);

                if (iResult != 0)
                {
                    Utility.Functions.WriteErrorMessage("[ERROR]資料寫入失敗，請重新操作");

                    return;
                }
                CleanData();
                lblMsg.Text= lblMsg.Text = "訊息:重工序號["+ strSn + "]已完成";
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
            try
            {
                CleanData();
                txtMono.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
