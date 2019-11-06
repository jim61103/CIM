using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class MESAdapter
    {
        private const string defString = "Null";
        private const int defInteger = -999;

        private StringReader tmpStringReader = null;
        private string InXml = "";
        private string OutXml = "";
        private string strIdentity = "";
        private string strParameter = "";
        private string XmlData = "";
        private string XmlSchema = "";

        private string m_strComputerName = "";
        private string m_strUserName = "";

        public MESAdapter()
        {
            m_strComputerName = System.Net.Dns.GetHostName();
            m_strUserName = "MESAdapter";
        }
        public string UserName
        {
            get
            {
                return m_strUserName;
            }

            set
            {
                m_strUserName = value;
            }
        }
        public int funLoadUserAccount(string strID, ref string UserNo)
        {
            //2009.10.19 Update by Ariel
            //預設初始值
            int nResult = -1;

            //組InXml的字串
            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());
            strParameter = CombineXMLParameter("useraccount", "UserAccount", "String", CInput(strID), "");
            InXml = CombineXMLRequest(strIdentity, strParameter);

            try
            {

                using (wsUSR.wsUSR wsUSR = new wsUSR.wsUSR())
                {
                    //wsUSR.wsUSR wsUSR = new wsUSR.wsUSR();
                    wsUSR.Url = LocalizeWebService(wsUSR.Url, false);
                    OutXml = wsUSR.ChkUserAccount(InXml);

                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        if (XmlDoc.GetElementsByTagName("userno")[0].SelectNodes("value").Count > 0)
                            UserNo = XmlDoc.DocumentElement.GetElementsByTagName("userno")[0].SelectNodes("value")[0].InnerText;

                        nResult = 0;
                    }
                    else
                        nResult = -1;

                }
                //wsUSR.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return nResult;
        }

        public int funLoadOPProperty(string OPNo, string PropertyNo)
        {
            int nReturn = -1;

            string tblName = "";

            try
            {
                DataSet dsTemp = new DataSet();

                //組InXml的字串
                strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());
                strParameter = CombineXMLParameter("opno", "OPNo", "String", CInput(OPNo), "");
                strParameter += CombineXMLParameter("propertyno", "PropertyNo", "String", CInput(PropertyNo), "");

                InXml = CombineXMLRequest(strIdentity, strParameter);

                using (wsOP.wsOP wsOP = new wsOP.wsOP())
                {
                    // wsOP.wsOP wsOP = new wsOP.wsOP();
                    wsOP.Url = LocalizeWebService(wsOP.Url, false);
                    OutXml = wsOP.LoadOPProperty(InXml);

                    XmlDocument XmlDoc = new XmlDocument();

                    XmlDoc.LoadXml(OutXml);

                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblName = XmlDoc.GetElementsByTagName("returnvalue")[0].SelectNodes("loadopproperty")[0].SelectNodes("name")[0].InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("loadopproperty")[0].SelectNodes("schema")[0].InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("loadopproperty")[0].SelectNodes("value")[0].InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader, XmlReadMode.InferSchema);
                            tmpStringReader.Close();
                        }

                        if (dsTemp.Tables[tblName].Rows.Count > 0)
                            nReturn = 0;

                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nReturn;
        }

        public int funAddOperator(string UserNo, string PDLineNo, string OPNo, string SubOPNo, int LogType)
        {

            string strShiftNo = defString;
            DateTime defWorkDay = new DateTime(1, 1, 1);
            DataView dvUserState = new DataView();

            int nReturn = -1;

            //呼叫班別相關資料
            GetShift(UserNo, ref strShiftNo, ref defWorkDay);

            //寫回資料庫
            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    //wsWIP.wsWIP wsWIP = new wsWIP.wsWIP();
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

                    if (LogType == 1)  //Login
                    {
                        strParameter = CombineXMLParameter("userno", "UserNo", "String", CInput(UserNo), "");
                        strParameter += CombineXMLParameter("hminame", "HMIName", "String", m_strComputerName, "");
                        strParameter += CombineXMLParameter("shiftno", "ShiftNo", "String", CInput(strShiftNo), "");
                        strParameter += CombineXMLParameter("workday", "WorkDay", "String", CInput(defWorkDay.ToString("yyyy/MM/dd")), "");
                        strParameter += CombineXMLParameter("pdlineno", "PDLineNo", "String", CInput(PDLineNo), "");
                        strParameter += CombineXMLParameter("opno", "OPNo", "String", CInput(OPNo), "");
                        strParameter += CombineXMLParameter("loginstate", "LogInState", "Integer", 0.ToString(), "");
                        strParameter += CombineXMLParameter("subop", "SubOP", "String", CInput(SubOPNo), "");
                        InXml = CombineXMLRequest(strIdentity, strParameter);
                        OutXml = wsWIP.UpdOperatorLogin(InXml);
                    }
                    else if (LogType == 2)  //Logout
                    {
                        funLoadUserBasisJoinLoginState(UserNo, ref dvUserState);
                        strParameter = CombineXMLParameter("userno", "UserNo", "String", CInput(UserNo), "");
                        strParameter += CombineXMLParameter("hminame", "HMIName", "String", m_strComputerName, "");

                        dvUserState.RowFilter = "UserNo='" + UserNo + "'";
                        if (dvUserState.Count > 0)
                            strParameter += CombineXMLParameter("loginserial", "LoginSerial", "String", System.Convert.ToString(dvUserState[0]["LoginSerial"]), "");

                        strParameter += CombineXMLParameter("loginstate", "LogInState", "Integer", 1.ToString(), "");
                        InXml = CombineXMLRequest(strIdentity, strParameter);
                        OutXml = wsWIP.UpdOperatorLogout(InXml);
                    }

                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                        nReturn = 0;

                    //wsWIP.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nReturn;
        }

        private void GetShift(string UserNo, ref string ShiftNo, ref DateTime defWorkDay)
        {
            DateTime dtFromTime = new DateTime(1, 1, 1);
            DateTime dtToTime = new DateTime(1, 1, 1);

            //組InXml的字串
            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());
            strParameter = CombineXMLParameter("userno", "userno", "String", CInput(UserNo), "");
            InXml = CombineXMLRequest(strIdentity, strParameter);

            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    //wsWIP.wsWIP wsWIP = new wsWIP.wsWIP();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.GetShift(InXml);
                    XmlDoc.LoadXml(OutXml);

                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        //取出ShiftNo
                        if (XmlDoc.GetElementsByTagName("shiftno")[0].SelectNodes("value").Count > 0)
                            ShiftNo = XmlDoc.DocumentElement.GetElementsByTagName("shiftno")[0].SelectNodes("value")[0].InnerText;

                        if (XmlDoc.GetElementsByTagName("fromtime")[0].SelectNodes("value").Count > 0)
                            dtFromTime = DateTime.Parse(XmlDoc.DocumentElement.GetElementsByTagName("fromtime")[0].SelectNodes("value")[0].InnerText);

                        if (XmlDoc.GetElementsByTagName("totime")[0].SelectNodes("value").Count > 0)
                            dtToTime = DateTime.Parse(XmlDoc.DocumentElement.GetElementsByTagName("totime")[0].SelectNodes("value")[0].InnerText);

                        DateTime datCurTime, datFrom, datTo;
                        datCurTime = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(DateTime.Now, DateFormat.ShortTime));
                        datFrom = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(dtFromTime, DateFormat.ShortTime));
                        datTo = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(dtToTime, DateFormat.ShortTime));

                        if (Microsoft.VisualBasic.DateAndTime.DateDiff(DateInterval.Second, datFrom, datTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) > 0)  //同一天
                        {
                            //WorkDay 預設為今天
                            defWorkDay = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate));
                        }
                        else    //有跨天,若目前時間落於區間內,WorkDay預設為昨天
                        {
                            if ((Microsoft.VisualBasic.DateAndTime.DateDiff(DateInterval.Second, datFrom, datCurTime, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) > 0) || (Microsoft.VisualBasic.DateAndTime.DateDiff(DateInterval.Second, datCurTime, datTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1) > 0))
                                defWorkDay = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(Microsoft.VisualBasic.DateAndTime.DateAdd(DateInterval.Day, -1, DateTime.Now), DateFormat.ShortDate));
                            else
                                defWorkDay = DateTime.Parse(Microsoft.VisualBasic.Strings.FormatDateTime(DateTime.Now, DateFormat.ShortDate));
                        }
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }
                //wsWIP.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void funLoadUserBasisJoinLoginState(string UserNo, ref DataView dvUserState)
        {
            DataSet dsTemp = new DataSet();
            string tblUserState = "";

            if (dsTemp.Tables[tblUserState] != null)
                dsTemp.Tables.Remove(tblUserState);

            //組InXml的字串
            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());
            strParameter = CombineXMLParameter("userno", "UserNo", "String", CInput(UserNo), "");
            InXml = CombineXMLRequest(strIdentity, strParameter);

            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    //wsWIP.wsWIP wsWIP = new wsWIP.wsWIP();
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.LoadUserBasisJoinLoginState(InXml);

                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblUserState = XmlDoc.GetElementsByTagName("returnvalue")[0].SelectNodes("loaduserbasisjoinloginstate")[0].SelectNodes("name")[0].InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("loaduserbasisjoinloginstate")[0].SelectNodes("schema")[0].InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("loaduserbasisjoinloginstate")[0].SelectNodes("value")[0].InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader, XmlReadMode.InferSchema);
                            tmpStringReader.Close();
                        }

                        dvUserState = dsTemp.Tables[tblUserState].DefaultView;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsWIP.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChkOPPrivilege(string UserNo, string OPNo, string SubOPNo)
        {
            XmlDocument XmlDoc = new XmlDocument();
            bool blnResult = false;

            //組InXml的字串
            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());
            strParameter = CombineXMLParameter("userno", "UserNo", "String", CInput(UserNo), "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", CInput(OPNo), "");
            if (SubOPNo != defString)
                strParameter += CombineXMLParameter("subopno", "SubOPNo", "String", CInput(SubOPNo), "");

            InXml = CombineXMLRequest(strIdentity, strParameter);

            try
            {
                using (wsUSR.wsUSR wsUSR = new wsUSR.wsUSR())
                {
                    //wsUSR.wsUSR wsUSR = new wsUSR.wsUSR();
                    wsUSR.Url = LocalizeWebService(wsUSR.Url, false);
                    OutXml = wsUSR.ChkUserOPPriv(InXml);
                    XmlDoc.LoadXml(OutXml);

                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        if (XmlDoc.GetElementsByTagName("result")[0].SelectNodes("value").Count > 0)
                        {
                            if (XmlDoc.DocumentElement.GetElementsByTagName("result")[0].SelectNodes("value")[0].InnerText.ToUpper() == "TRUE")
                                blnResult = true;
                        }
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                XmlDoc = null;
            }

            return blnResult;
        }

        public int ChkLogInstate(string UserNo, string PDLineNo, string OPNo, string SubOP)
        {
            DataView dvUserState = new DataView();
            int nReturn = 0;

            try
            {

                funLoadUserBasisJoinLoginState(UserNo, ref dvUserState);
                dvUserState.RowFilter = "UserNo='" + UserNo + "'";
                if (dvUserState.Count > 0)
                {
                    //判斷PDLineNo及OPNo值是否為空值
                    if (PDLineNo != "" && OPNo != "")
                    {
                        if (funLoadOPProperty(OPNo, "MultipleLogIn") != 0)
                            nReturn = -1;
                        else
                            nReturn = 0;
                    }
                    else
                    {
                        nReturn = 0;
                    }
                }
                else
                {
                    nReturn = 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nReturn;
        }

        #region Private Functions
        private string LocalizeWebService(string strUrl, bool blIsCustomized)
        {
            string[] tmpString = strUrl.Split('/');
            string str = string.Empty;
            if (blIsCustomized)
            {
                str = ConfigurationManager.AppSettings["CusWebServiceURL"];
            }
            else
            {
                str = ConfigurationManager.AppSettings["StdWebServiceURL"]; ;

            }
            strUrl = str + "/" + tmpString[4] + "/" + tmpString[5];

            return strUrl;
        }

        /// <summary>
        /// 功能:取出一般產線排程資料
        /// </summary>
        /// <param name="ProductNo"></param>
        /// <param name="ProductVersion"></param>
        /// <param name="OPNo"></param>
        /// <param name="AdditionalXml"></param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public DataTable funLoadOEMOLineDispatch(string OPGroupNo, string PDLineNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            //組InXml的字串
            DateTime NowDate = DateTime.Now.Date.AddHours(7).AddMinutes(50);

            if (NowDate > DateTime.Now)//前天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.AddDays(-1).ToString(), "");
            }
            else//當天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString("yyyy/MM/dd"), "");
            }
            //strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString(), "");
            strParameter += CombineXMLParameter("opgroupno", "OPGroupNo", "String", OPGroupNo, "");
            strParameter += CombineXMLParameter("pdlineno", "PDLineNo", "String", PDLineNo, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsOE.wsOE wsOE = new wsOE.wsOE())
                {
                    //wsOE.wsOE wsOE = new wsOE.wsOE();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsOE.Url = LocalizeWebService(wsOE.Url, false);
                    OutXml = wsOE.LoadOEMOLineDispatch(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadOEMOLineDispatch".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadOEMOLineDispatch".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadOEMOLineDispatch".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsOE.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        public DataTable funLoadPRDAccessory(string ProductNo, string OPNo, string AccessoryNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            //組InXml的字串
            //strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString(), "");
            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("accessoryno", "AccessoryNo", "String", AccessoryNo, "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadPRDAccessory(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadPRDAccessory".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDAccessory".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDAccessory".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        /// <summary>
        /// 功能:讀取機種ProFile，
        /// 資料來源:TBLPRDOPPROFILE
        /// </summary>
        /// <param name="ProductNo">機種</param>
        /// <param name="OPNo">作業站編號</param>
        /// <param name="ENGNo">製成別</param>
        public DataTable funLoadOPProfile(string ProductNo, string OPNo, string ENGNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            //組InXml的字串
            //strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString(), "");
            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");
            strParameter += CombineXMLParameter("engno", "ENGNo", "String", ENGNo, "");


            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadOPProfile(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadOPProfile".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadOPProfile".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadOPProfile".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }
        //TBLOEMOMATERIALLIST
        public DataTable funLoadMOMaterialList(string Mono)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            //組InXml的字串
            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");


            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsOE.wsOE wsOE = new wsOE.wsOE())
                {
                    //wsOE.wsOE wsOE = new wsOE.wsOE();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsOE.Url = LocalizeWebService(wsOE.Url, false);
                    OutXml = wsOE.LoadMOMaterialList(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadMOMaterialList".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadMOMaterialList".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadMOMaterialList".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsOE.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        public DataTable funLoadProduct_Dist(string data)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));


            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadProduct_Dist(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadProduct_Dist".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadProduct_Dist".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadProduct_Dist".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        public DataTable funLoadPRDASMS(string ProductNo, string OPNo, string ComponentNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");

            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");

            //strParameter += CombineXMLParameter("ComponentNo".ToLower(), "ComponentNo", "String", ComponentNo, "");

            //strParameter += CombineXMLParameter("materialtype", "MaterialType", "String", MaterialType, "");

            strParameter += CombineXMLAdditional(CombineAddXML_Condition("COMPONENTNO LIKE '" + ComponentNo + "%'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadPRDASMS(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadPRDASMS".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDASMS".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDASMS".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        public DataTable funLoadPRDASMS(string ProductNo, string ComponentNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");

            strParameter += CombineXMLParameter("ComponentNo".ToLower(), "ComponentNo", "String", ComponentNo, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadPRDASMS(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadPRDASMS".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDASMS".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadPRDASMS".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        public DataTable funLoadBomByMonoAndBarCode(string Mono)
        {
            DataTable dtBomData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");

            //strParameter += CombineXMLAdditional(CombineAddXML_Condition("MATERIALTYPE = '" + MaterialType + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadBomByMonoAndBarCode(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadBomByMonoAndBarCode".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadBomByMonoAndBarCode".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadBomByMonoAndBarCode".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtBomData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtBomData;
        }

        /// <summary>
        /// 功能:讀取噴印機程式編號
        /// 資料來源:CUS_PRDOPPROFILE
        /// </summary>
        public DataTable funLoadCusPrdOpProFile(string ProductNo, string IGBTMaterialNo, string DioNo)
        {
            DataTable dtMoData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("imbgtmaterialno", "IGBTMaterialNo", "String", IGBTMaterialNo, "");
            strParameter += CombineXMLParameter("diono", "DioNo", "String", DioNo, "");

            //strParameter += CombineXMLAdditional(CombineAddXML_Condition("PROFILENO = '" + "12" + "'"));


            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadCusPrdOpProFile(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadCusPrdOpProFile".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadCusPrdOpProFile".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadCusPrdOpProFile".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoData;
        }

        /// <summary>
        /// 功能:讀取工單產生的序號
        /// 資料來源:tblOEMOSerialCreateDetail
        /// </summary>
        /// <param name="IGBTMaterialNo">IABG料號</param>
        public DataTable funShowOEMOSerialCreateDetail(string Mono, string SerialType)
        {
            DataTable dtMoSerialData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");
            strParameter += CombineXMLParameter("serialtype", "SerialType", "String", SerialType, "");

            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    //wsPRD.wsPRD wsPRD = new wsPRD.wsPRD();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.ShowOEMOSerialCreateDetail(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("OEMOSerialCreateDetail".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("OEMOSerialCreateDetail".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("oemoserialcreateDetail".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoSerialData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsPRD.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoSerialData;
        }

        /// <summary>
        /// 功能:讀取工單
        /// 資料來源:TBLOEMOBASIS
        /// </summary>
        /// <param name="Mono">工單</param>
        public DataTable funLoadOEMOBasis(string Mono)
        {
            DataTable dtMoSerialData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");

            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsOE.wsOE wsOE = new wsOE.wsOE())
                {
                    //wsOE.wsOE wsOE = new wsOE.wsOE();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsOE.Url = LocalizeWebService(wsOE.Url, false);
                    OutXml = wsOE.LoadOEMOBasis(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadOEMOBasis".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadOEMOBasis".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("loadoemobasis".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMoSerialData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
                //wsOE.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMoSerialData;
        }

        /// <summary>
        /// 功能:取得雷雕機操作紀錄
        /// 資料來源:CUS_WIPMARKBUILDER
        /// </summary>
        /// <param name="Mono">工單</param>
        public DataTable funLoadWipMarkBuilder(string Mono, string ProductNo, string SerialNo, string OPNo, string ProFileNo)
        {
            DataTable dtMarkBuilder = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");
            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("serialno", "SerialNo", "String", SerialNo, "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");
            strParameter += CombineXMLParameter("profileno", "ProFileNo", "String", ProFileNo, "");

            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    //wsWIP.wsWIP wsWIP = new wsWIP.wsWIP();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.LoadWipMarkBuilder(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadWipMarkBuilder".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadWipMarkBuilder".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("loadwipmarkbuilder".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtMarkBuilder = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsWIP.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMarkBuilder;
        }

        /// <summary>
        /// 功能:寫入雷雕機操作紀錄
        /// 資料來源:CUS_WIPMARKBUILDER
        /// </summary>
        /// <param name="Mono">工單</param>
        public int funAddWipMarkBuilder(string Mono, string ProductNo, string SerialNo, string OPNo, string ProFileNo, string UserId)
        {
            int intReturn = -1;

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");
            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("serialno", "SerialNo", "String", SerialNo, "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");
            strParameter += CombineXMLParameter("profileno", "ProFileNo", "String", ProFileNo, "");
            strParameter += CombineXMLParameter("userid", "UserId", "String", UserId, "");
            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.AddWipMarkBuilder(InXml);


                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 0;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;
        }

        /// <summary>
        /// 功能:搬移資料至LOG後刪除原資料
        /// 資料來源:CUS_WIPMARKBUILDER,CUS_WIPMARKBUILDER_LOG
        ///</summary>
        /// <param name="Mono">工單</param>
        public int funDelWipMarkBuilder(string Mono, string ProductNo, string SerialNo, string OPNo, string ProFileNo)
        {
            int intReturn = -1;

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono", "Mono", "String", Mono, "");
            strParameter += CombineXMLParameter("productno", "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("serialno", "SerialNo", "String", SerialNo, "");
            strParameter += CombineXMLParameter("opno", "OPNo", "String", OPNo, "");
            strParameter += CombineXMLParameter("profileno", "ProFileNo", "String", ProFileNo, "");
            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.DelWipMarkBuilder(InXml);


                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 0;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;
        }

        /// <summary>
        /// 功能:寫入雷雕機操作紀錄
        /// 資料來源:CUS_WIPMARKBUILDER
        /// </summary>
        /// <param name="Mono">工單</param>
        public int funAddThermalGreaseData(string Data)
        {
            int intReturn = -1;

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("Data".ToLower(), "Data", "String", Data, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsEQP.wsEQP wsEQP = new wsEQP.wsEQP())
                {
                    wsEQP.Url = LocalizeWebService(wsEQP.Url, false);
                    OutXml = wsEQP.AddThermalGreaseData(InXml);


                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 0;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;
        }

        /// <summary>
        /// 功能:取得雷雕機操作紀錄
        /// 資料來源:CUS_WIPMARKBUILDER
        /// </summary>
        /// <param name="Mono">工單</param>
        public DataTable funLoadMOASMProcedure(string Mono, string ComponentNo)
        {
            DataTable dtData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("mono".ToLower(), "Mono", "String", Mono, "");
            //strParameter += CombineXMLParameter("ComponentNo".ToLower(), "ComponentNo", "String", ComponentNo, "");

            strParameter += CombineXMLAdditional(CombineAddXML_Condition("ComponentNo = '" + ComponentNo + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsWIP.wsWIP wsWIP = new wsWIP.wsWIP())
                {
                    //wsWIP.wsWIP wsWIP = new wsWIP.wsWIP();
                    XmlDocument XmlDoc = new XmlDocument();
                    wsWIP.Url = LocalizeWebService(wsWIP.Url, false);
                    OutXml = wsWIP.LoadMOASMProcedure(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadMOASMProcedure".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadMOASMProcedure".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadMOASMProcedure".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsWIP.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtData;
        }

        /// <summary>
        /// 功能:取得排程工單對應的噴印機程式資料
        /// 資料來源:CUS_PRDOPPROFILE
        /// </summary>
        /// <param name="Mono">工單</param>
        public DataTable funLoadCusMoToProFile(string Mono, string PDLineNo)
        {
            DataTable dtData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            DateTime NowDate = DateTime.Now.Date.AddHours(7).AddMinutes(50);

            if (NowDate > DateTime.Now)//前天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd"), "");
            }
            else//當天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString("yyyy/MM/dd"), "");
            }

            strParameter += CombineXMLParameter("mono".ToLower(), "Mono", "String", Mono, "");
            strParameter += CombineXMLParameter("PDLineNo".ToLower(), "PDLineNo", "String", PDLineNo, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.LoadCusMoToProFile(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadCusMoToProFile".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadCusMoToProFile".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadCusMoToProFile".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsWIP.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtData;
        }

        /// <summary>
        /// 功能:寫入噴印機程式資料
        /// 資料來源:CUS_PRDOPPROFILE
        /// </summary>
        /// <param name="Mono">工單</param>
        public int funAddCusMoToProFile(string ProductNo, string IGBTNo, string DioNo, string ProgramNo, string UserID, string lowerLimit, string UpperLimit)
        {
            int intReturn = -1;

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());


            //組InXml的字串
            DateTime NowDate = DateTime.Now.Date.AddHours(7).AddMinutes(50);

            if (NowDate > DateTime.Now)//前天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.AddDays(-1).ToString(), "");
            }
            else//當天班
            {
                strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString(), "");
            }
            //strParameter += CombineXMLParameter("dispdate", "DispDate", "DateTime", DateTime.Now.ToString(), "");

            strParameter += CombineXMLParameter("ProductNo".ToLower(), "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("IGBTNo".ToLower(), "IGBTNo", "String", IGBTNo, "");
            strParameter += CombineXMLParameter("DioNo".ToLower(), "DioNo", "String", DioNo, "");
            strParameter += CombineXMLParameter("ProgramNo".ToLower(), "ProgramNo", "String", ProgramNo, "");
            strParameter += CombineXMLParameter("UserID".ToLower(), "UserID", "String", UserID, "");
            strParameter += CombineXMLParameter("lowerLimit".ToLower(), "lowerLimit", "String", lowerLimit, "");
            strParameter += CombineXMLParameter("UpperLimit".ToLower(), "UpperLimit", "String", UpperLimit, "");
            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.AddCusMoToProFile(InXml);


                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 1;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;
        }

        /// <summary>
        /// 功能:修改噴印機程式資料
        /// 資料來源:CUS_PRDOPPROFILE
        /// </summary>
        /// <param name="Mono">工單</param>
        public int funUpdateCusMoToProFile(string ProductNo, string IGBTNo, string DioNo, string ProgramNo, string UserID, string lowerLimit, string UpperLimit)
        {
            int intReturn = -1;

            strParameter = string.Empty;
            string strAdditional = string.Empty;

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("ProductNo".ToLower(), "ProductNo", "String", ProductNo, "");
            strParameter += CombineXMLParameter("IGBTNo".ToLower(), "IGBTNo", "String", IGBTNo, "");
            strParameter += CombineXMLParameter("DioNo".ToLower(), "DioNo", "String", DioNo, "");
            strParameter += CombineXMLParameter("ProgramNo".ToLower(), "ProgramNo", "String", ProgramNo, "");
            strParameter += CombineXMLParameter("UserID".ToLower(), "UserID", "String", UserID, "");
            strParameter += CombineXMLParameter("lowerLimit".ToLower(), "lowerLimit", "String", lowerLimit, "");
            strParameter += CombineXMLParameter("UpperLimit".ToLower(), "UpperLimit", "String", UpperLimit, "");
            //strParameter = CombineXMLAdditional(CombineAddXML_Condition("PRODUCTCODE = '" + data + "'"));

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsPRD.wsPRD wsPRD = new wsPRD.wsPRD())
                {
                    wsPRD.Url = LocalizeWebService(wsPRD.Url, false);
                    OutXml = wsPRD.UpdateCusMoToProFile(InXml);


                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 1;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;
        }

        /// <summary>
        /// 功能:讀取BI 系統基本資料
        /// </summary>
        /// <param name="ChamberID">Chamber ID</param>
        public DataTable funLoadEQPBIData(string ChamberID)
        {
            strIdentity = string.Empty;

            strParameter = string.Empty;

            DataTable dtData = null;

            DataSet dsTemp = new DataSet();
            string tblTableName = "";

            string strAdditional = string.Empty;

            string SQLData = @"
            select* from (
            SELECT DISTINCT CCB_CARNO, CHBR_NO, TO_CHAR(CCB_COMBINETIME, 'MM/DD HH24:MI') CCB_COMBINETIME,
            CCB_DIRECTION, TO_CHAR(CCB_BURNOUTTIME, 'MM/DD HH24:MI') CCB_BURNOUTTIME, CCS_CARSTATUS,
            CCB_BURNOUTTIME BURNOUTTIME,
            CHBR_NO || CCB_DIRECTION || TO_CHAR(CCB_COMBINETIME, 'YYYYMMDDHH24MI') CONNECTINSID,
            CHBR_NO || CCB_DIRECTION || TO_CHAR(CCB_BURNOUTTIME, 'YYYYMMDDHH24MI') CONNECTOUTSID
            FROM    CHBR_CAR_BINDING CCB
            INNER JOIN
            CHBR_CAR_STATUS CCS
            ON CCB.CCB_CARNO = CCS.CCP_CARNO AND CCS.CCS_CARSTATUS IN('8', '9')
            WHERE(CCB_STATUS IS NULL OR CCB_STATUS = 2)
            AND CCB_COMBINETIME IS NOT NULL
            AND CHBR_NO = '{0}'
             AND CCB_BURNOUTTIME IS NOT NULL
            ) AA
            LEFT JOIN CHBR_CAR_WARNLOG BB
            on  AA.CHBR_NO = BB.CHBR_NO and
            AA.CCB_DIRECTION = BB.CCB_DIRECTION and
            AA.CONNECTINSID = BB.CCB_COMBINESID and
            AA.CONNECTOUTSID = BB.CCB_BURNOUTSID
            order by  CCB_BURNOUTTIME    ";

            SQLData = string.Format(SQLData, ChamberID);

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("SQLData".ToLower(), "SQLData", "String", SQLData, "");

            InXml = CombineXMLRequest(strIdentity, strParameter);
            try
            {
                using (wsEQP_Delta.wsEQP wsEQP = new wsEQP_Delta.wsEQP())
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    wsEQP.Url = LocalizeWebService(wsEQP.Url, true);
                    OutXml = wsEQP.QueryEQPSQLData(InXml);

                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        tblTableName = XmlDoc.GetElementsByTagName("returnvalue").Item(0).SelectNodes("LoadEQPData".ToLower()).Item(0).SelectNodes("name").Item(0).InnerXml;
                        //取出Schema,dataset讀取Schema可防止Null Field及DateTime的問題
                        XmlSchema = XmlDoc.DocumentElement.GetElementsByTagName("LoadEQPData".ToLower()).Item(0).SelectNodes("schema").Item(0).InnerXml;
                        if (XmlSchema != "")
                        {
                            //將XML讀入String Reader object中,因為Dataset讀入XML時必須透過String Reader物件
                            tmpStringReader = new System.IO.StringReader(XmlSchema);
                            dsTemp.ReadXmlSchema(tmpStringReader);
                            tmpStringReader.Close();
                        }
                        //取出Data
                        XmlData = XmlDoc.DocumentElement.GetElementsByTagName("LoadEQPData".ToLower()).Item(0).SelectNodes("value").Item(0).InnerXml;
                        if (XmlData != "")
                        {
                            tmpStringReader = new System.IO.StringReader(XmlData);
                            dsTemp.ReadXml(tmpStringReader);
                            tmpStringReader.Close();
                        }

                        dtData = dsTemp.Tables[tblTableName].Copy();
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }

                    //wsWIP.Dispose();
                    dsTemp.Dispose();
                    XmlDoc = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtData;
        }

        /// <summary>
        /// 功能:讀取BI 系統基本資料
        /// </summary>
        /// <param name="ChamberID">Chamber ID</param>
        public int funInsertEQPData(string strChbrNo, string strDirection, string LeftInTime, string LeftOutTime)
        {
            int intReturn = -1;

            strIdentity = string.Empty;

            strParameter = string.Empty;

            string strSQL = @"INSERT INTO CHBR_CAR_WARNLOG VALUES ('{0}', '{1}', '{2}', '{3}', 'Finished', SYSDATE)";

            string SQLData = string.Format(strSQL, strChbrNo, strDirection, LeftInTime, LeftOutTime);

            strIdentity = CombineXMLIdentity(m_strComputerName, m_strUserName, DateTime.Now.ToString());

            strParameter += CombineXMLParameter("SQLData".ToLower(), "SQLData", "String", SQLData, "");


            InXml = CombineXMLRequest(strIdentity, strParameter);

            try
            {
                using (wsEQP_Delta.wsEQP wsEQP = new wsEQP_Delta.wsEQP())
                {
                    wsEQP.Url = LocalizeWebService(wsEQP.Url, true);
                    OutXml = wsEQP.EQPExecuteSQL(InXml);

                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.LoadXml(OutXml);
                    if (ChkExecutionSuccess(XmlDoc))
                    {
                        intReturn = 1;
                    }
                    else
                    {
                        throw new Exception(GetExceptionSysMsg(XmlDoc) + "\n" + GetExceptionMesMsg(XmlDoc));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intReturn;

        }

        private string CombineXMLIdentity(string ComputerName, string CurUserNo, string SendTime)
        {
            return "<computername>" + ComputerName + "</computername>" +
                        "<curuserno>" + CurUserNo + "</curuserno>" +
                        "<sendtime>" + SendTime + "</sendtime>";
        }

        private string CombineXMLIdentity(string TransactionID, string ModuleID, string FunctionID, string ComputerName, string CurUserNo, string SendTime)
        {
            return "<transactionid>" + TransactionID + "</transactionid>" +
                        "<moduleid>" + ModuleID + "</moduleid>" +
                        "<functionid>" + FunctionID + "</functionid>" +
                        "<computername>" + ComputerName + "</computername>" +
                        "<curuserno>" + CurUserNo + "</curuserno>" +
                        "<sendtime>" + SendTime + "</sendtime>";
        }

        private string CombineXMLParameter(string value_name, string name, string type, string value, string desc)
        {
            return "<" + value_name.ToLower() + ">" +
                            "<name>" + name + "</name>" +
                            "<type>" + type + "</type>" +
                            "<value>" + value + "</value>" +
                            "<desc>" + desc + "</desc>" +
                        "</" + value_name + ">";
        }

        private string CombineXMLParameter(string name, string type, string value, string desc)
        {
            return "<" + name.ToLower() + ">" +
                            "<name>" + name + "</name>" +
                            "<type>" + type + "</type>" +
                            "<value>" + value + "</value>" +
                            "<desc>" + desc + "</desc>" +
                        "</" + name + ">";
        }

        private string CombineXMLRequest(string strIdentity, string strParameter)
        {
            string tmpString = "<request>" +
                            "<identity>" + strIdentity + "</identity>";

            if (strParameter != "")
                tmpString = tmpString + "<parameter>" + strParameter + "</parameter>";

            tmpString = tmpString + "</request>";

            return tmpString;
        }

        private string CombineXMLValue(string TagName, string Value)
        {
            return "<" + TagName + ">" + Value + "</" + TagName + ">";
        }

        private string CombineXMLValueTag(string Value)
        {
            return "<value>" + Value + "</value>";
        }

        private string CombineXMLRequest_Label(string Label_Name, int Print_Qty, string strParameter, string PrintName, string PrintPort)
        {
            string tmpString = "";

            if (PrintName == "" && PrintPort == "")
            {
                tmpString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                                         "<LABEL>" +
                                                         "<PRINT_VAR>" +
                                                         "<LABEL_TYPE>" + Label_Name + "</LABEL_TYPE>" +
                                                         "<LABEL_PATH/>" +
                                                         "<PRINTER_NAME/>" +
                                                         "<PRINTER_PORT/>" +
                                                         "<PRINT_CNT>" + Print_Qty + "</PRINT_CNT>" +
                                                         "</PRINT_VAR> ";
            }
            else
            {
                tmpString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                                         "<LABEL>" +
                                                         "<PRINT_VAR>" +
                                                         "<LABEL_TYPE>" + Label_Name + "</LABEL_TYPE>" +
                                                         "<LABEL_PATH/>" +
                                                         "<PRINTER_NAME>" + PrintName + "</PRINTER_NAME>" +
                                                         "<PRINTER_PORT>" + PrintPort + "</PRINTER_PORT>" +
                                                         "<PRINT_CNT>" + Print_Qty + "</PRINT_CNT>" +
                                                         "</PRINT_VAR> ";
            }

            if (strParameter != "")
                tmpString = tmpString + "<VAR>" + tmpString + "</VAR>";

            tmpString = tmpString + "</LABEL>";

            return tmpString;
        }

        private string CombineXMLParameter_Label(string value_name, string value)
        {
            if (value != defString)
                return "<" + value_name.ToUpper() + ">" + value + "</" + value_name.ToUpper() + ">";
            else
                return "<" + value_name.ToUpper() + ">  </" + value_name.ToUpper() + ">";
        }

        private string CombineXMLParameterMultiValue(string value_name, string name, string type, string value, string desc)
        {
            return "<" + value_name + ">" +
                                        "<name>" + name + "</name>" +
                                        "<type>" + type + "</type>" + value +
                                        "<desc>" + desc + "</desc>" +
                                        "</" + value_name + ">";
        }

        private string CombineXMLAdditional(string strAdditional)
        {
            return "<additional>" + strAdditional + "</additional>";
        }

        private string CombineAddXML_Add(string name, string type, string value)
        {
            return "<field>" +
                            "<name>" + name + "</name>" +
                            "<type>" + type + "</type>" +
                            "<value>" + value + "</value>" +
                            "</field>";
        }

        private string CombineAddXML_Edit(string name, string type, string value)
        {
            return "<field>" +
                            "<name>" + name + "</name>" +
                            "<type>" + type + "</type>" +
                            "<value>" + value + "</value>" +
                            "</field>";
        }

        private string CombineAddXML_Field(string name)
        {
            return "<field>" +
                            "<name>" + name + "</name>" +
                            "</field>";
        }

        private string CombineAddXML_Condition(string condition)
        {
            //2007/03/22,sammi.呼叫CombineAddXML_Condition時,不可加CInput.
            //因CInput會將單引號轉成二個單引號,若Condition為下述範例時,會Error.
            //strAdditional = CombineXMLAdditional(CombineAddXML_Condition("ModuleType in ('REWORK','SLR')"))
            //strParameter += strAdditional

            return "<condition>" + condition + "</condition>";

        }

        private string CInput(string strInput)
        {
            string tmpString = "";
            //2006/07/18,sammi.為一次解決前端呼叫WS未處理特殊字元的問題.
            //改由CombineXMLParameter、CombineXMLValue、CombineAddXML_Add、CombineAddXML_Edit時，加上ChgString

            tmpString = strInput;

            //將傳入值內的單引號轉換為可存入資料庫的格式
            //                 2. 將傳入值內的 &, >, < 三個特殊字元轉換為XmlDocument可解譯之代替符號                  
            //傳入值: strInput包含特殊字元的字串
            //傳回值: 將特殊字元變更為代替符號的字串

            //轉換 ' 為 '' (單引號轉為兩個單引號)
            tmpString = strInput.Replace("'", "''");

            //轉換 & 為 &amp;
            tmpString = tmpString.Replace("&", "&amp;");

            //CInput = Replace(CInput, """", "''")   'AddFlow的Xml字串不可將雙引號轉為兩個單引號,XMLToFlow會Error

            //轉換 > 為 &gt;
            tmpString = tmpString.Replace(">", "&gt;");

            //轉換 < 為 &lt;
            tmpString = tmpString.Replace("<", "&lt;");

            return tmpString;
        }

        private bool ChkExecutionSuccess(XmlDocument Xmldoc)
        {
            if (Xmldoc.DocumentElement["result"].InnerXml == "success")
                return true;
            else
                return false;
        }

        private string GetExceptionSysMsg(XmlDocument Xmldoc)
        {
            return CUnInput(Xmldoc.DocumentElement.GetElementsByTagName("sysmsg")[0].InnerXml);
        }

        private string GetExceptionMesMsg(XmlDocument Xmldoc)
        {
            return CUnInput(Xmldoc.DocumentElement.GetElementsByTagName("mesmsg")[0].InnerXml);
        }

        private string CUnInput(string strInput)
        {
            string tmpString = "";
            //將傳入值內的單引號轉換為可存入資料庫的格式
            //傳入值: strInput包含特殊字元的字串
            //傳回值: 將代替符號變更為特殊字元的字串

            //轉換 ' 為 '' (單引號轉為兩個單引號)
            //CUnInput = Replace(strInput, "'", "''")
            tmpString = strInput.Replace("\"\"", "'");

            //轉換 & 為 &amp;
            tmpString = tmpString.Replace("&amp;", "&");

            //轉換 > 為 &gt;
            tmpString = tmpString.Replace("&gt;", ">");

            //轉換 < 為 &lt;
            tmpString = tmpString.Replace("&lt;", "<");

            return tmpString;
        }

        private string GetExceptionCode(XmlDocument Xmldoc)
        {
            return CUnInput(Xmldoc.DocumentElement.GetElementsByTagName("code")[0].InnerXml);
        }

        private string GetExceptionStack(XmlDocument Xmldoc)
        {
            if (Xmldoc.DocumentElement.GetElementsByTagName("stack").Count > 0)
                return CUnInput(Xmldoc.DocumentElement.GetElementsByTagName("stack")[0].InnerXml);
            else
                return "";
        }
        #endregion
    }
}
