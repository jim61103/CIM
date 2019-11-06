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

namespace CIM
{
    public partial class frmProfileMaintain : Form
    {
        private bool Status = false;

        public frmProfileMaintain()
        {
            InitializeComponent();
            this.Shown += frmProfileMaintain_Shown;
            //this.Closing += new System.ComponentModel.CancelEventHandler(frmEQPOperate_Closing);//觸發關閉視窗事件
        }

        private void frmProfileMaintain_Shown(object sender, EventArgs e)
        {
            txtProgramNo.Focus();
        }

        private void frmProfileMaintain_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProfileData = Global.MESAdpt.funLoadCusMoToProFile(Global.Mono, Global.PDLineNo);

                //DataTable dtProfileData = DBTableInfo.getProgramData(Global.Mono);

                SetInitialRow();

                if (dtProfileData.Rows.Count > 0)
                {
                    txtMono.Text = dtProfileData.Rows[0]["MONO"].ToString();
                    txtProductNo.Text = dtProfileData.Rows[0]["PRODUCTNO"].ToString();
                    txtIGBT.Text = dtProfileData.Rows[0]["IGBTMATERIALNO"].ToString();
                    txtDioNo.Text = dtProfileData.Rows[0]["DIONO"].ToString();
                    txtProgramNo.Text = dtProfileData.Rows[0]["PROFILENO"].ToString();
                    txtLowerLimit.Text = dtProfileData.Rows[0]["LOWERLIMIT"].ToString();
                    txtUpperLimit.Text = dtProfileData.Rows[0]["UPPERLIMIT"].ToString();

                    if (txtProgramNo.Text == string.Empty)
                    {
                        Status = true;
                    }

                }
                else
                {
                    MessageBox.Show("此工單BOM找不到IGBT(216或38127開頭料號)或IGBT物料條碼未設定命名規則，請重新確認");
                    this.Close();
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
            Status = false;

            txtMono.ReadOnly = true;
            txtProductNo.ReadOnly = true;
            txtIGBT.ReadOnly = true;
            txtDioNo.ReadOnly = true;
            txtProgramNo.ReadOnly = false;
            txtLowerLimit.ReadOnly = false;
            txtUpperLimit.ReadOnly = false;

            txtProgramNo.Text = string.Empty;
            txtProductNo.Text = string.Empty;
            txtIGBT.Text = string.Empty;
            txtDioNo.Text = string.Empty;
            txtProgramNo.Text = string.Empty;
            txtLowerLimit.Text = string.Empty;
            txtUpperLimit.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProgramNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("程式編號不允許為空");
                    return;
                }
                if (txtLowerLimit.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("厚度下限不允許為空");
                    return;
                }
                if (txtUpperLimit.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("厚度上限不允許為空");
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;

                int iResult = -1;

                if (Status == true)
                {
                    //iResult = Utility.DBTableInfo.InsertProgramData(txtProductNo.Text.Trim(), txtIGBT.Text.Trim(),
                    //txtDioNo.Text.Trim(), txtProgramNo.Text.Trim(), Global.UserNo, txtLowerLimit.Text.Trim(), txtUpperLimit.Text.Trim());
                    iResult = Global.MESAdpt.funAddCusMoToProFile(txtProductNo.Text.Trim(), txtIGBT.Text.Trim(),
                    txtDioNo.Text.Trim(), txtProgramNo.Text.Trim(), Global.UserNo, txtLowerLimit.Text.Trim(), txtUpperLimit.Text.Trim());
                }
                else
                {
                    //iResult = Utility.DBTableInfo.UpdateProgramData(txtProductNo.Text.Trim(), txtIGBT.Text.Trim(),
                    //txtDioNo.Text.Trim(), txtProgramNo.Text.Trim(), Global.UserNo, txtLowerLimit.Text.Trim(), txtUpperLimit.Text.Trim());
                    iResult = Global.MESAdpt.funUpdateCusMoToProFile(txtProductNo.Text.Trim(), txtIGBT.Text.Trim(),
                    txtDioNo.Text.Trim(), txtProgramNo.Text.Trim(), Global.UserNo, txtLowerLimit.Text.Trim(), txtUpperLimit.Text.Trim());
                }
                if (iResult < 1)
                {
                    MessageBox.Show("更新失敗，請重新操作。");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("資料已更新。");
                }
                Cursor.Current = Cursors.Default;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
