
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM
{
    public class Global
    {
        static private System.Windows.Forms.Form s_objMainForm = null;
        static private MESAdapter s_objMESAdapter = null;
        static private string s_strUserNo = string.Empty;
        static private string s_strOPNo = string.Empty;
        static private string s_strPDLineNo = string.Empty;
        static private string s_strOPGroupNo = string.Empty;
        static private string s_strMono = string.Empty;
        static private string s_strProductNo = string.Empty;
        static private string s_strProgramNo = string.Empty;
        static private string s_strIGBTNo = string.Empty;


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
        static public string OPNo
        {
            get
            {
                return s_strOPNo;
            }

            set
            {
                s_strOPNo = value;
            }
        }
        static public string PDLineNo
        {
            get
            {
                return s_strPDLineNo;
            }

            set
            {
                s_strPDLineNo = value;
            }
        }
        static public string OPGroupNo
        {
            get
            {
                return s_strOPGroupNo;
            }

            set
            {
                s_strOPGroupNo = value;
            }
        }
        static public string Mono
        {
            get
            {
                return s_strMono;
            }

            set
            {
                s_strMono = value;
            }
        }
        static public string ProductNo
        {
            get
            {
                return s_strProductNo;
            }

            set
            {
                s_strProductNo = value;
            }
        }

        static public string ProgramNo
        {
            get
            {
                return s_strProgramNo;
            }

            set
            {
                s_strProgramNo = value;
            }
        }
        static public string IGBTNo
        {
            get
            {
                return s_strIGBTNo;
            }

            set
            {
                s_strIGBTNo = value;
            }
        }
        #endregion
    }
}