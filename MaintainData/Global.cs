using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintainData
{
    public class Global
    {
        static private System.Windows.Forms.Form s_objMainForm = null;
        static private MESAdapter s_objMESAdapter = null;
        static private string s_strUserNo = string.Empty;


        #region Public Properties
        static public System.Windows.Forms.Form MainForm
        {
            get
            {
                return s_objMainForm;
            }

            set
            {
                s_objMainForm = value;
            }
        }

        static public MESAdapter MESAdpt
        {
            get
            {
                return s_objMESAdapter;
            }

            set
            {
                s_objMESAdapter = value;
            }
        }

        static public string UserNo
        {
            get
            {
                return s_strUserNo;
            }

            set
            {
                s_strUserNo = value;
            }
        }
        #endregion
    }
}
