using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility
{
    public partial class Functions
    {
        /// <summary>
        /// 功能描述:判斷是否是正整數
        /// </summary>
        public static bool IsNumeric(string TextBoxValue)
        {
            try
            {
                double number;

                if (Double.TryParse(TextBoxValue, out number))
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// lblMsg寫入訊息
        /// </summary>
        public static void WriteErrorMessage(string message, string[] arrMsg)
        {
            string strErrorMessage = string.Format(message, arrMsg);
            MessageBox.Show(strErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void WriteInfoMessage(string message, string[] arrMsg)
        {
            string strErrorMessage = string.Format(message, arrMsg);
            MessageBox.Show(strErrorMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void WriteErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void WriteInfoMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
