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
    public class SalesController : ControllerBase
    {

        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();

        [HttpPost]
        [Route("getBranchAccess")]
        public async Task<ActionResult<Sales>> getBranchAccess(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select BO_BRANCH_MASTER.BRANCH_ID,BRANCH_DESC from BO_BRANCH_ACCESS inner join BO_BRANCH_MASTER on BO_BRANCH_ACCESS.BRANCH_ID=BO_BRANCH_MASTER.BRANCH_ID where USER_ID= '"+data.userid+"'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("salesdashboard_productwiseforcast")]
        public async Task<ActionResult<Sales>> salesdashboard_productwiseforcast(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SET DATEFORMAT DMY;SELECT FUNCTION_DESC AS Company,branch_desc AS Branch,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC AS 'Product',isnull(COUNT(CUST_LEAD_ID),0) AS 'Total Leads', isnull(CLOSEDCALLS.CLOSEDLEADS,0) AS 'Closed Leads', CAST(CAST(ISNULL(CLOSEDCALLS.CLOSEDLEADS,0) AS DECIMAL(10,2))/CAST(ISNULL(COUNT(CUST_LEAD_ID),0) AS DECIMAL(10,2))*100 AS DECIMAL(5,1)) AS '% Closed Leads' FROM LMS_CAMPAIGN_MASTER WITH(NOLOCK) INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = LMS_CAMPAIGN_MASTER.FUNCTION_ID INNER JOIN LMS_CUSTOMER_CAMPAIGN WITH(NOLOCK) ON LMS_CUSTOMER_CAMPAIGN.CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID inner join BO_BRANCH_MASTER on LMS_CUSTOMER_CAMPAIGN.BRANCH_ID=BO_BRANCH_MASTER.BRANCH_ID and dbo.BO_FUNCTION_MASTER.FUNCTION_ID=BO_BRANCH_MASTER.FUNCTION_ID LEFT OUTER JOIN (SELECT TCC_CAMP_ID,COUNT(TCC_CUST_LEAD_ID) AS CLOSEDLEADS,  LMS_CLOSED_CALLS.FUNCTION_ID, LMS_CLOSED_CALLS.BRANCH_ID FROM LMS_CLOSED_CALLS WITH(NOLOCK) INNER JOIN LMS_CAMPAIGN_MASTER WITH(NOLOCK) ON LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID = LMS_CLOSED_CALLS.TCC_CAMP_ID GROUP BY TCC_CAMP_ID, LMS_CLOSED_CALLS.FUNCTION_ID, LMS_CLOSED_CALLS.BRANCH_ID) CLOSEDCALLS ON CLOSEDCALLS.TCC_CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID and CLOSEDCALLS.BRANCH_ID =  LMS_CUSTOMER_CAMPAIGN.BRANCH_ID and CLOSEDCALLS.FUNCTION_ID = LMS_CAMPAIGN_MASTER.FUNCTION_ID WHERE LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_TYPE='C' AND dbo.lms_campaign_master.FUNCTION_ID='1' and LMS_CUSTOMER_CAMPAIGN.BRANCH_ID='"+ data.branchid + "' group by bo_function_master.function_desc,lms_campaign_master.tcm_campaign_shortdesc,closedcalls.closedleads,branch_desc";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }




        [HttpPost]
        [Route("update_current_location")]
        public async Task<ActionResult<Sales>> update_current_location(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "update BO_USER_MASTER set current_location ='" + data.Location + "',LOCATION_UPDATED_ON = GETDATE() where TUM_USER_CODE='" + data.usercode + "'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


       
        [HttpGet]
        [Route("getdashboard_sourcewise/{id}")]
        public List<Sales> getdashboard_sourcewise(int id)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "SELECT COUNT(CASE WHEN VAL ='1' THEN (TCC_LEAD_SOURCE) END) AS  'Personal',COUNT(CASE WHEN VAL ='2' THEN (TCC_LEAD_SOURCE) END) AS  'Web Lead',COUNT(CASE WHEN VAL ='3' THEN (TCC_LEAD_SOURCE) END) AS  'Branch',COUNT(CASE WHEN VAL ='4' THEN (TCC_LEAD_SOURCE) END) AS  'Corporate Website',COUNT(CASE WHEN VAL ='5' THEN (TCC_LEAD_SOURCE) END) AS  'Mobile' FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and TCC_CALLER_ID = " + id + "";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Personal = Convert.ToInt32(row[0]);
                    log.Web = Convert.ToInt32(row[1]);
                    log.Branch = Convert.ToInt32(row[2]);
                    log.Corporateweb = Convert.ToInt32(row[3]);
                    log.Mobile = Convert.ToInt32(row[4]);
                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("BranchLocation/{id}")]
        public List<Sales> BranchLocation(int id)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select a.LOCATION_ID,a.LOCATION_CODE,a.LOCATION_DESC from BO_BRANCH_LOCATION_MASTER a inner join BO_BRANCH_MASTER b on a.branch_id=b.branch_id where a.branch_id="+id+"";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.locationID = Convert.ToInt32(row[0]);
                    log.Locationcode = row[1].ToString().Trim();
                    log.Locationname = row[2].ToString().Trim();
                  
                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }



        [HttpGet]
        [Route("GetProduct")]
        public dynamic GetProduct()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select TCM_CAMPAIGN_SHORTDESC,TCM_CAMPAIGN_ID,PRODUCTTYPE from LMS_CAMPAIGN_MASTER";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];      
                    log.ProductName = row[0].ToString().Trim();
                    log.ProductID = Convert.ToInt32(row[1]);
                    log.ProductCatID = Convert.ToInt32(row[1]);

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("Nametitle")]
        public dynamic Nametitle()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE ='BO_TITLE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);
                  

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callpriority")]
        public dynamic callpriority()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLPRIORITY'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callrating")]
        public dynamic callrating()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLRATING'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callnature")]
        public dynamic callnature()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLNATURE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callstage")]
        public dynamic callstage()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLSTAGE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("Leadsource")]
        public dynamic Leadsource()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_LEADSOURCE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("customerresponse/{producttypeid}")]
        public string customerresponse(string producttypeid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MOB_CustResponse";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@producttypeid", producttypeid);


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
