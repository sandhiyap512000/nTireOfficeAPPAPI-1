using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CAMSController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();


        [HttpGet]
        [Route("camsbranchcount")]
        public string camsbranchcount()
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();
            // SQLCONNECTION dbcon = new SQLCONNECTION();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "CAMS_BRANCHWISE_ASSETCOUNT";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);


                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }
                return Logdata1;
            }
        }

        [HttpGet]
        [Route("camscategorycount/{struserid}")]
        public string camscategorycount(string struserid)
        {
            string Logdata1 = string.Empty;
            DataSet dscategorycount = new DataSet();
            DataTable dtcategorycount = new DataTable();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "CAMS_ASSETCATEGORY_COUNT";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", struserid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("camsmaintenancecurrentmonthcount/{struserid}")]
        public string camsmaintenancecurrentmonthcount(string struserid)
        {
            string Logdata1 = string.Empty;

            DataTable dtmaintenancecurrentmonthcount = new DataTable();
            DataSet dsmaintenancecurrentmonthcount = new DataSet();
          
                DataSet DS = new DataSet();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "CAMS_CURRENT_MONTH_MAINTENANCE_CHART";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", struserid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsmaintenancecurrentmonthcount);

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }
                return Logdata1;
            }
        }


        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }



    }
}
