using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Utility
{
    public partial class AppSetting
    {
        public static string IP
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["IP"];

                return strData.ToUpper().Trim();
            }
        }
        public static string Port
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["Port"];

                return strData.ToUpper().Trim();
            }
        }
        public static string EQPStatus
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["EQPStatus"];

                return strData.ToUpper().Trim();
            }
        }
        public static string EQPShift
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["EQPShift"];

                return strData.ToUpper().Trim();
            }
        }
        public static string StdWebServiceURL
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["StdWebServiceURL"];

                return strData.ToUpper().Trim();
            }
        }
        public static string CusWebServiceURL
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["CusWebServiceURL"];

                return strData.ToUpper().Trim();
            }
        }
        public static string OPNo
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["OPNo"];

                return strData.ToUpper().Trim();
            }
        }
        public static string PDLineNo
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["PDLineNo"];

                return strData.ToUpper().Trim();
            }
        }
        public static string OPGroupNo
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["OPGroupNo"];

                return strData.ToUpper().Trim();
            }
        }
        public static string DioNo
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["DioNo"];

                return strData.ToUpper().Trim();
            }
        }
        public static string MainProgram
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["MainProgram"];

                return strData.ToUpper().Trim();
            }
        }
        public static string EquipmentFilePath
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["EquipmentFilePath"];

                return strData.ToUpper().Trim();
            }
        }
        public static string OutputData
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["OutputData"];

                return strData.ToUpper().Trim();
            }
        }
        /// <summary>
        ///BI Chamber ID
        /// </summary>
        public static string ChamberID
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["ChamberID"];

                return strData.ToUpper().Trim();
            }
        }
        /// <summary>
        ///提醒前時間
        /// </summary>
        public static string WarnTime
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["WarnTime"];

                if (strData == null) { strData = "0"; }

                return strData.Trim();
            }
        }
        /// <summary>
        ///設備ON & OFF 閃爍間隔(ms)
        /// </summary>
        public static string EQPDelayTime
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["EQPDelayTime"];

                if (strData == null) { strData = "0"; }

                return strData.Trim();
            }
        }
        /// <summary>
        ///設備ON & OFF 閃爍間隔(ms)
        /// </summary>
        public static string EQPQueryTime
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["EQPQueryTime"];

                if (strData == null) { strData = "30000"; }

                return strData.Trim();
            }
        }
        /// <summary>
        ///機台亮燈訊號
        /// </summary>
        public static int Buffer
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["Buffer"];

                if (strData == null) { strData = "0"; }

                int Buffer = int.Parse(strData.Trim());

                return Buffer;
            }
        }
        /// <summary>
        ///機台亮燈訊號
        /// </summary>
        public static int SendCount
        {
            get
            {
                string strData = ConfigurationManager.AppSettings["SendCount"];

                if (strData == null) { strData = "0"; }

                int Count = int.Parse(strData.Trim());

                return Count;
            }
        }
    }
}
