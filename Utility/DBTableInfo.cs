using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DBTableInfo
    {
        private static string strSQL = string.Empty;
        /// <summary>
        /// 功能描述:判斷是否是正整數
        /// </summary>
        public static DataTable getProgramData(string Mono)
        {
            try
            {
                DataTable dtProfileData = null;//工單資訊

                strSQL = @"select AA.*,BB.PROFILENO,BB.LOWERLIMIT, BB.UPPERLIMIT from (
                select e.*,NVL(F.COMPONENTNO,'*') DIONO,F.MATERIALTYPE from (
                Select DISTINCT c.MONO,d.PRODUCTNO,b.MATERIALNO IGBTMATERIALNO from  TBLMTLMATERIALBARCODE_PARTIAL  b
                INNER Join  TBLOEMOMATERIALLIST c On b.MATERIALNO= c.MATERIALNO 
                RIGHT JOIN TBLOEMOLINEDISPATCH d On d.MONO=c.MONO
                WHERE b.MATERIALNO LIKE '216%'  and  d.DISPDATE = TRUNC (SYSDATE)  and d.PDLINENO='ASSY-1' and d.OPGROUPNO='0030' 
                UNION 
                Select DISTINCT c.MONO,d.PRODUCTNO,b.MATERIALNO from  TBLMTLMATERIALBARCODE_PARTIAL  b 
                INNER Join  TBLOEMOMATERIALLIST c On b.MATERIALNO= c.MATERIALNO 
                RIGHT JOIN TBLOEMOLINEDISPATCH d On d.MONO=c.MONO
                WHERE b.MATERIALNO LIKE '38127%'  and  d.DISPDATE = TRUNC (SYSDATE)  and d.PDLINENO='ASSY-1' and d.OPGROUPNO='0030'
                ) e
                LEFT JOIN (
                select * from TBLPRDOPASMPROCEDURE WHERE COMPONENTNO like '200%'
                )  f
                on f.PRODUCTNO=e.PRODUCTNO
                ) AA LEFT JOIN CUS_PRDOPPROFILE BB
                on AA.PRODUCTNO=BB.PRODUCTNO and AA.IGBTMATERIALNO=BB.IGBTMATERIALNO and  AA.DIONO=BB.DIONO 
                WHERE 1=1 ";

                if (Mono != string.Empty)
                {
                    strSQL = strSQL + "And AA.MONO = '{0}'";
                }

                strSQL = string.Format(strSQL, Mono);

                dtProfileData = DB.ReadDT(strSQL);

                return dtProfileData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertProgramData(string ProductNo, string IGBTNo, string DioNo, string ProgramNo, string UserID, string lowerLimit, string UpperLimit)
        {
            try
            {
                int iResult = -1;

                strSQL = @"INSERT INTO CUS_PRDOPPROFILE (PRODUCTNO,IGBTMATERIALNO,DIONO,OPNO,PROFILENO,USERID,UPDATETIME,LOWERLIMIT,UPPERLIMIT) VALUES 
                ('{0}','{1}','{2}','ASSY','{3}','{4}',SYSDATE,'{5}','{6}')";

                strSQL = string.Format(strSQL, ProductNo, IGBTNo, DioNo, ProgramNo, UserID, lowerLimit, UpperLimit);

                iResult = DB.Execute(strSQL);

                strSQL = @"INSERT INTO CUS_PRDOPPROFILE_LOG
                            (PRODUCTNO,IGBTMATERIALNO,DIONO,OPNO,PROFILENO,USERID,UPDATETIME,LOWERLIMIT,UPPERLIMIT,ACTION) VALUES 
                            ('{0}','{1}','{2}','ASSY','{3}','{4}',SYSDATE,'{5}','{6}','onlineINSERT')";

                strSQL = string.Format(strSQL, ProductNo, IGBTNo, DioNo, ProgramNo, UserID, lowerLimit, UpperLimit);

                iResult = DB.Execute(strSQL);

                return iResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateProgramData(string ProductNo, string IGBTNo, string DioNo, string ProgramNo, string UserID, string lowerLimit, string UpperLimit)
        {
            try
            {
                int iResult = -1;

                strSQL = @"
                    UPDATE CUS_PRDOPPROFILE SET 
                    PROFILENO='{0}', USERID='{1}', UPDATETIME=SYSDATE, LOWERLIMIT='{2}', UPPERLIMIT='{3}' 
                    WHERE ProductNo='{4}' and IGBTMATERIALNO='{5}' and DioNo='{6}'";

                strSQL = string.Format(strSQL, ProgramNo, UserID, lowerLimit, UpperLimit, ProductNo, IGBTNo, DioNo);

                iResult = DB.Execute(strSQL);

                strSQL = @"INSERT INTO CUS_PRDOPPROFILE_LOG
                            (PRODUCTNO,IGBTMATERIALNO,DIONO,OPNO,PROFILENO,USERID,UPDATETIME,LOWERLIMIT,UPPERLIMIT,ACTION) VALUES 
                            ('{0}','{1}','{2}','ASSY','{3}','{4}',SYSDATE,'{5}','{6}','onlineUPDATE')";

                strSQL = string.Format(strSQL, ProductNo, IGBTNo, DioNo, ProgramNo, UserID, lowerLimit, UpperLimit);

                iResult = DB.Execute(strSQL);

                return iResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
