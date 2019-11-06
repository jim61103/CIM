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

namespace CIM
{
    public partial class frmEQPCheckItem : Form
    {
        public frmEQPCheckItem()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (ckbEQPCheck1.Checked == true && ckbEQPCheck2.Checked == true && ckbEQPCheck3.Checked == true && ckbEQPCheck4.Checked == true)
                {
                    //首件檢查
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                    IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);

                    //判斷當前時間是日班,夜班
                    string strWork = string.Empty;

                    DateTime tNowtime = DateTime.Now;
                    DateTime stime = DateTime.ParseExact("080000", "HHmmss", culture);//當前時間
                    DateTime etime = DateTime.ParseExact("200000", "HHmmss", culture);//當前時間

                    //比對當前時間是否同一個時間區間
                    //不同做首件
                    //相同看狀態是否要
                    if (DateTime.Compare(tNowtime, stime) > 0 && DateTime.Compare(tNowtime, etime) < 0)
                    {
                        strWork = DateTime.Now.ToString("yyyyMMdd") + "MorningShift";//日班
                    }
                    else
                    {
                        if (DateTime.Compare(tNowtime, stime) < 0)
                        {
                            strWork = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                        }
                        else
                        {
                            strWork = DateTime.Now.ToString("yyyyMMdd");
                        }
                        strWork = strWork + "NightShift";//夜班
                    }
                    config.AppSettings.Settings["EQPStatus"].Value = "Y";
                    config.AppSettings.Settings["EQPShift"].Value = strWork;
                    config.Save(ConfigurationSaveMode.Minimal);
                    ConfigurationManager.RefreshSection("appSettings");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("請完成所有首件項目再按確認");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
