using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Nancy.Json;
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


        //deepak
       
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

        //shylaja

        [HttpPost]
        [Route("getdashboard_sourcewise")]
        public async Task<ActionResult<Sales>> getdashboard_sourcewise(Sales data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT COUNT(CASE WHEN VAL ='1' THEN (TCC_LEAD_SOURCE) END) AS  'Personal',COUNT(CASE WHEN VAL ='2' THEN (TCC_LEAD_SOURCE) END) AS  'Web Lead',COUNT(CASE WHEN VAL ='3' THEN (TCC_LEAD_SOURCE) END) AS  'Branch',COUNT(CASE WHEN VAL ='4' THEN (TCC_LEAD_SOURCE) END) AS  'Corporate Website',COUNT(CASE WHEN VAL ='5' THEN (TCC_LEAD_SOURCE) END) AS  'Mobile' FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and TCC_CALLER_ID = " + data.branchid + "";

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


        //shylaja dashboard stagewise

        [HttpPost]
        [Route("getdashboard_stagewise")]
        public async Task<ActionResult<Sales>> getdashboard_stagewise(Sales data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select COUNT(CASE WHEN VAL ='2' THEN (TCC_CUST_LEAD_ID) END) AS  'Qualified',COUNT(CASE WHEN VAL ='5' THEN (TCC_CUST_LEAD_ID) END) AS    'Negotiation',COUNT(CASE WHEN VAL ='4' THEN (TCC_CUST_LEAD_ID) END) AS 'Proposal',COUNT(CASE WHEN VAL ='1' THEN (TCC_CUST_LEAD_ID) END) AS 'Enquiry',COUNT(CASE WHEN VAL ='6' THEN (TCC_CUST_LEAD_ID) END) AS 'Demo',COUNT(CASE WHEN VAL ='8' THEN (TCC_CUST_LEAD_ID) END) AS 'Lost',COUNT(CASE WHEN VAL ='7' THEN (TCC_CUST_LEAD_ID) END) AS 'Quotes Given',COUNT(CASE WHEN VAL ='3' THEN (TCC_CUST_LEAD_ID) END) AS 'Quality Testing done'FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and   BRANCH_ID = " + data.branchid + "";

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


        //deepak
        [HttpGet]
        [Route("BranchLocation/{id}")]
        public List<Sales> BranchLocation(int id)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select a.LOCATION_ID,a.LOCATION_CODE,a.LOCATION_DESC from BO_BRANCH_LOCATION_MASTER a inner join BO_BRANCH_MASTER b on a.branch_id=b.branch_id where a.branch_id=" + id + "";

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



        ////shylaja
        //[HttpGet]
        //[Route("BranchLocation/{id}")]
        //public string BranchLocation(int id)
        //{
        //    List<Sales> Logdata = new List<Sales>();
        //    string Logdata1 = string.Empty;
        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {
        //        string query = "";
        //        query = "select a.LOCATION_ID,a.LOCATION_CODE,a.LOCATION_DESC from BO_BRANCH_LOCATION_MASTER a inner join BO_BRANCH_MASTER b on a.branch_id=b.branch_id where a.branch_id=" + id + "";

        //        dbConn.Open();
        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);
        //        for (int i = 0; i < results.Rows.Count; i++)
        //        {
        //            DataRow row = results.Rows[i];
        //            Logdata1 = DataTableToJSONWithStringBuilder(results);
        //        }
        //        dbConn.Close();
        //        return Logdata1;
        //    }
        //}


        //deepak
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


        ////shylaja
        //[HttpGet]
        //[Route("GetProduct")]
        //public string GetProduct()
        //{
        //    List<Sales> Logdata = new List<Sales>();

        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {
        //        string Logdata1 = string.Empty;
        //        string query = "";
        //        query = "select TCM_CAMPAIGN_SHORTDESC,TCM_CAMPAIGN_ID,PRODUCTTYPE from LMS_CAMPAIGN_MASTER";

        //        dbConn.Open();
        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);
        //        for (int i = 0; i < results.Rows.Count; i++)
        //        {
        //            DataRow row = results.Rows[i];
        //            Logdata1 = DataTableToJSONWithStringBuilder(results);


        //        }
        //         dbConn.Close();
        //        return Logdata1;
        //    }
        //}




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
                //string sql = "MOB_CustResponse";
                string sql = "MBL_Mob_AddContact";
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

        //insert lead shylaja
        [HttpGet]
        [Route("insertlead")]
        public string insertlead(string functionid, string BRANCH_ID, string productcategoryid, string productid, string campaignid, string customerfname, string customerlname, string mobile, string OfficePhone, string ResidencePhone, string callpriorityid, string callratingid, string callnatureid, string leadsourceid, string callstageid, string customerresponse, string NextCallDate, string time, string remarks, string ExpectedClose, string ExpectedAmount, string Leadby, string UserID, string userType, string LocationId, string EmailId, string Currency)
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    if (functionid.ToString() == "0" || functionid.ToString() == "" || functionid.ToString() == string.Empty || functionid.ToString() == "null")
                    {
                        functionid =null;
                    }


                    if (BRANCH_ID.ToString() == "0" || BRANCH_ID.ToString() == "" || BRANCH_ID.ToString() == string.Empty || BRANCH_ID.ToString() == "null")
                    {
                        BRANCH_ID = null;
                    }
                    if (productcategoryid.ToString() == "0" || productcategoryid.ToString() == "" || productcategoryid.ToString() == string.Empty || productcategoryid.ToString() == "null")
                    {
                        productcategoryid = null;
                    }

                    if (productid.ToString() == "0" || productid.ToString() == "" || productid.ToString() == string.Empty || productid.ToString() == "null")
                    {
                        productid = null;
                    }
                    if (campaignid.ToString() == "0" || campaignid.ToString() == "" || campaignid.ToString() == string.Empty || campaignid.ToString() == "null")
                    {
                        campaignid = null;
                    }

                    if (customerfname.ToString() == "0" || customerfname.ToString() == "" || customerfname.ToString() == string.Empty || customerfname.ToString() == "null")
                    {
                        customerfname = null;
                    }

                    if (customerlname.ToString() == "0" || customerlname.ToString() == "" || customerlname.ToString() == string.Empty || customerlname.ToString() == "null")
                    {
                        customerlname = null;
                    }

                    if (mobile.ToString() == "0" || mobile.ToString() == "" || mobile.ToString() == string.Empty || mobile.ToString() == "null")
                    {
                        mobile = null;
                    }

                    if (OfficePhone.ToString() == "0" || OfficePhone.ToString() == "" || OfficePhone.ToString() == string.Empty || OfficePhone.ToString() == "null")
                    {
                        OfficePhone = null;
                    }

                    if (ResidencePhone.ToString() == "0" || ResidencePhone.ToString() == "" || ResidencePhone.ToString() == string.Empty || ResidencePhone.ToString() == "null")
                    {
                        ResidencePhone = null;
                    }

                    if (callpriorityid.ToString() == "0" || callpriorityid.ToString() == "" || callpriorityid.ToString() == string.Empty || callpriorityid.ToString() == "null")
                    {
                        callpriorityid = null;
                    }

                    if (callratingid.ToString() == "0" || callratingid.ToString() == "" || callratingid.ToString() == string.Empty || callratingid.ToString() == "null")
                    {
                        callratingid = null;
                    }

                    if (callnatureid.ToString() == "0" || callnatureid.ToString() == "" || callnatureid.ToString() == string.Empty || callnatureid.ToString() == "null")
                    {
                        callnatureid= null;
                    }

                    if (leadsourceid.ToString() == "0" || leadsourceid.ToString() == "" || leadsourceid.ToString() == string.Empty || leadsourceid.ToString() == "null")
                    {
                        leadsourceid = null;
                    }

                    if (callstageid.ToString() == "0" || callstageid.ToString() == "" || callstageid.ToString() == string.Empty || callstageid.ToString() == "null")
                    {
                        callstageid=null;
                    }

                    if (customerresponse.ToString() == "0" || customerresponse.ToString() == "" || customerresponse.ToString() == string.Empty || customerresponse.ToString() == "null")
                    {
                        customerresponse= null;
                    }

                    if (NextCallDate.ToString() == "0" || NextCallDate.ToString() == "" || NextCallDate.ToString() == string.Empty || NextCallDate.ToString() == "null")
                    {
                        NextCallDate = null;
                    }

                    if (time.ToString() == "0" || time.ToString() == "" || time.ToString() == string.Empty || time.ToString() == "null")
                    {
                        time = null;
                    }

                    if (remarks.ToString() == "0" || remarks.ToString() == "" || remarks.ToString() == string.Empty || remarks.ToString() == "null")
                    {
                        remarks = null;
                    }
                    if (ExpectedClose.ToString() == "0" || ExpectedClose.ToString() == "" || ExpectedClose.ToString() == string.Empty || ExpectedClose.ToString() == "null")
                    {
                        ExpectedClose = null;
                    }

                    if (ExpectedAmount.ToString() == "0" || ExpectedAmount.ToString() == "" || ExpectedAmount.ToString() == string.Empty || ExpectedAmount.ToString() == "null")
                    {
                        ExpectedAmount = "0";
                    }

                    if (Leadby.ToString() == "0" || Leadby.ToString() == "" || Leadby.ToString() == string.Empty || Leadby.ToString() == "null")
                    {
                        Leadby ="0";
                    }
                    if (UserID.ToString() == "0" || UserID.ToString() == "" || UserID.ToString() == string.Empty || UserID.ToString() == "null")
                    {
                        UserID = "0";
                    }
                    if (userType.ToString() == "0" || userType.ToString() == "" || userType.ToString() == string.Empty || userType.ToString() == "null")
                    {
                        userType="0";
                    }
                    if (LocationId.ToString() == "0" || LocationId.ToString() == "" || LocationId.ToString() == string.Empty || LocationId.ToString() == "null")
                    {
                        LocationId ="0";
                    }
                    if (EmailId.ToString() == "0" || EmailId.ToString() == "" || EmailId.ToString() == string.Empty || EmailId.ToString() == "null")
                    {
                        EmailId ="0";
                    }
                    if (Currency.ToString() == "0" || Currency.ToString() == "" || Currency.ToString() == string.Empty || Currency.ToString() == "null")
                    {
                        Currency="0";
                    }
                    string Logdata1 = string.Empty;









                    dbConn.Open();
                    string sql = "MBL_MOB_NewLeadInsert";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FUNCTION_ID", functionid);
                    cmd.Parameters.AddWithValue("@BRANCH_ID", BRANCH_ID);
                    cmd.Parameters.AddWithValue("@productcategoryid", productcategoryid);
                    cmd.Parameters.AddWithValue("@productid", productid);
                    cmd.Parameters.AddWithValue("@campaignid", campaignid);
                    cmd.Parameters.AddWithValue("@customerfname", customerfname);
                    cmd.Parameters.AddWithValue("@customerlname", customerlname);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@OfficePhone", OfficePhone);
                    cmd.Parameters.AddWithValue("@ResidencePhone", ResidencePhone);
                    cmd.Parameters.AddWithValue("@callpriorityid", callpriorityid);
                    cmd.Parameters.AddWithValue("@callratingid", callratingid);
                    cmd.Parameters.AddWithValue("@callnatureid", callnatureid);
                    cmd.Parameters.AddWithValue("@leadsourceid", leadsourceid);
                    cmd.Parameters.AddWithValue("@callstageid", callstageid);
                    cmd.Parameters.AddWithValue("@customerresponse", customerresponse);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@NextCallDate", NextCallDate);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@ExpectedClose", ExpectedClose);
                    cmd.Parameters.AddWithValue("@ExpectedAmount", ExpectedAmount);
                    cmd.Parameters.AddWithValue("@Leadby", Leadby);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@userType", userType);
                    cmd.Parameters.AddWithValue("@LocationId", LocationId);
                    cmd.Parameters.AddWithValue("@Currency", Currency);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);

                    cmd.ExecuteNonQuery();

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
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return json;
            }

        }

        //shylaja
        [HttpGet]
        [Route("Getcustomer/{EmpCode}")]
        public string Getcustomer(string EmpCode)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select CUST_LNAME as Code,CUST_ID as ID from LMS_CUSTOMER_MASTER where CUST_LNAME ='" + EmpCode + "'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                //var result = (new { recordsets = Logdata1 });
                return Logdata1;
            }
        }

//end


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
