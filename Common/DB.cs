using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Configuration;
using System.Collections;

public class DB
{
    public static string ConnectionString()
    {
        return ConfigurationManager.AppSettings["ConnStr"];
        //return "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.144.111)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=tymesdb01)));User Id=messeries;Password=sa;";
    }

    public static OracleDataReader Read(string SQL, OracleParameter[] values = null)
    {
        OracleConnection con = new OracleConnection(ConnectionString());

        using (OracleCommand cmd = new OracleCommand(SQL, con))
        {
            try
            {
                con.Open();

                if (values != null)
                {
                    cmd.Parameters.AddRange(values);
                }

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (OracleException E)
            {
                con.Close();
                throw new Exception("Read(): " + E.Message + ": " + SQL);
            }
        }
    }

    public static DataTable ReadDT(string SQL, OracleParameter[] values = null)
    {
        using (OracleConnection con = new OracleConnection(ConnectionString()))
        {
            using (OracleCommand cmd = new OracleCommand(SQL, con))
            {
                try
                {
                    con.Open();

                    if (values != null)
                    {
                        cmd.Parameters.AddRange(values);
                    }

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
                catch (OracleException E)
                {
                    con.Close();
                    throw new Exception("ReadDT(): " + E.Message + ": " + SQL);
                }
                finally
                {
                    //cmd.Dispose();
                    con.Close();
                }
            }
        }
    }

    public static DataSet ReadDS(string SQL, string strTableName, OracleParameter[] values = null)
    {
        using (OracleConnection con = new OracleConnection(ConnectionString()))
        {
            using (OracleCommand cmd = new OracleCommand(SQL, con))
            {
                try
                {
                    con.Open();

                    if (values != null)
                    {
                        cmd.Parameters.AddRange(values);
                    }

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    da.Fill(ds, strTableName);

                    return ds;
                }
                catch (OracleException E)
                {
                    con.Close();
                    throw new Exception("ReadDS(): " + E.Message + ": " + SQL);
                }
            }
        }
    }

    public static int Execute(string SQL, OracleParameter[] values = null)
    {
        using (OracleConnection con = new OracleConnection(ConnectionString()))
        {
            using (OracleCommand cmd = new OracleCommand(SQL, con))
            {
                try
                {
                    con.Open();

                    if (values != null)
                    {
                        cmd.Parameters.AddRange(values);
                    }

                    return cmd.ExecuteNonQuery();
                }
                catch (OracleException E)
                {
                    con.Close();
                    throw new Exception("Execute(): " + E.Message + ": " + SQL);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    public static int ExecuteTrans(ArrayList SQLStringList)
    {
        using (OracleConnection con = new OracleConnection(ConnectionString()))
        {
            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            OracleTransaction trans = con.BeginTransaction();
            cmd.Transaction = trans;

            string strSQL = "";
            int intAffect = 0;
            try
            {
                foreach (string strSQLA in SQLStringList)
                {
                    strSQL = strSQLA;
                    if (strSQL.Trim().Length > 1)
                    {
                        cmd.CommandText = strSQL;
                        cmd.ExecuteNonQuery();

                        intAffect += 1;
                    }
                }

                trans.Commit();
                return intAffect;
            }
            catch (OracleException E)
            {
                trans.Rollback();
                con.Close();
                return 0;
                throw new Exception("ExecuteTrans(): " + E.Message + ": " + strSQL);
            }
            finally
            {
                con.Close();
            }
        }
    }

}