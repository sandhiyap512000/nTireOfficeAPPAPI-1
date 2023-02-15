using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using MobileAppAPI.Models;

namespace MobileAppAPI.Models
{
    

    public class SQLAccesLayer
    {
       // DataAccessLayer.DataAccessLayer objData = new DataAccessLayer.DataAccessLayer();

        Helper objData = new Helper();
        // string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AngularJSDatabase"].ToString();
        // private string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlCon"].ToString();
        public string getString(string strSql)
        {            
            string strString = "";
            SqlCommand sqlCmd = null;
            using (SqlConnection cn = new SqlConnection(objData.Connectionstring()))
            {
                try
                {
                    cn.Open();
                    sqlCmd = new SqlCommand(strSql, cn);
                    strString = Convert.ToString(sqlCmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    sqlCmd.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
            return strString;
        }

        public string ExecuteNonQuery(string sql)
        {
            string status = string.Empty;

            string auditQuery = sql;
            SqlConnection sqlCon = null;
            if (sqlCon == null || sqlCon.State == System.Data.ConnectionState.Closed)
            {
                sqlCon = new SqlConnection(objData.Connectionstring());
                sqlCon.Open();
            }
            SqlTransaction trans = sqlCon.BeginTransaction();
            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon, trans);
            try
            {
                int row = sqlCmd.ExecuteNonQuery();
                trans.Commit();
                if (row > 0)
                    status = "Y";
                else
                    status = "N";
            }
            catch (Exception e)
            {
                trans.Rollback();
                sqlCon.Close();
                sqlCon.Dispose();
                status = "N";
                throw new Exception(e.ToString());

            }

            return status;
        }

        public DataSet getDataSet(string strSQL)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(objData.Connectionstring()))
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    if (strSQL != "")
                    {
                        sqlCmd.Connection = cn;
                        cn.Open();
                        SqlDataAdapter objSda = new SqlDataAdapter(strSQL, cn);
                        objSda.SelectCommand.CommandTimeout = 100000;
                        objSda.Fill(ds);
                    }
                    try
                    {
                        //AuditTrailTrack(strSQL, "");
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlCmd.Dispose();
                    cn.Close();
                }
            }
            return ds;
        }

        public bool RowExists(string tableName, string condition)
        {
            bool _isExists = false;
            try
            {
                string query = "SET DATEFORMAT DMY; select 1 from " + tableName + " where " + condition;
                using (SqlConnection cn = new SqlConnection(objData.Connectionstring()))
                {                   
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    _isExists = Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                // ex.ErrorLog();
            }
            return _isExists;
        }
    }
}