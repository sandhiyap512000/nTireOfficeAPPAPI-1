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
    public class PropertyController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();

        #region Dashboard
        [HttpGet]
        [Route("customerpayments")]
        public string customerpayments(string strfunction, string branch, string userid)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (strfunction.ToString() == "0" || strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                    {
                        strfunction = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }                   
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }

                    string Logdata1 = string.Empty;
                    string sql = "MOB_PROPERTY_DASHBOARD"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@type", "CP");

                    cmd.ExecuteNonQuery();

                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
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


        [HttpGet]
        [Route("emloyeemaintenance")]
        public string emloyeemaintenance(string strfunction, string branch, string userid)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (strfunction.ToString() == "0" || strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                    {
                        strfunction = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }

                    string Logdata1 = string.Empty;
                    string sql = "MOB_PROPERTY_DASHBOARD"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@type", "EI");

                    cmd.ExecuteNonQuery();

                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
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

        [HttpGet]
        [Route("issuestatus")]
        public string issuestatus(string strfunction, string branch, string userid)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (strfunction.ToString() == "0" || strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                    {
                        strfunction = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }

                    string Logdata1 = string.Empty;
                    string sql = "MOB_PROPERTY_DASHBOARD"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@type", "IS");

                    cmd.ExecuteNonQuery();

                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
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

        [HttpGet]
        [Route("propertystatus")]
        public string propertystatus(string strfunction, string branch, string userid)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (strfunction.ToString() == "0" || strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                    {
                        strfunction = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }

                    string Logdata1 = string.Empty;
                    string sql = "MOB_PROPERTY_DASHBOARD"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@type", "PS");

                    cmd.ExecuteNonQuery();

                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
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

        [HttpGet]
        [Route("tobevaccant")]
        public string tobevaccant(string strfunction, string branch, string userid)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (strfunction.ToString() == "0" || strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                    {
                        strfunction = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }

                    string Logdata1 = string.Empty;
                    string sql = "MOB_PROPERTY_DASHBOARD"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@type", "TV");

                    cmd.ExecuteNonQuery();

                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
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


        [HttpGet]
        [Route("fm_rental_summary/{strFunctionId}/{strBranchId}/{strLocationId}/{strPropertyId}/{strPropertyDesc}/{rentelCode}/{strStatus}/{pageIndex}/{pageSize}/{sortExpression}/{alphaname}/{Split_ID}/{strusertype}/{userid}")]
        public string fm_rental_summary(string strFunctionId=null, string strBranchId = null, string strLocationId = null, string strPropertyId = null, string strPropertyDesc = null, string rentelCode = null, string strStatus=null, string pageIndex =null , string pageSize=null, string sortExpression = null, string alphaname = null, String Split_ID = null, string strusertype = null, string userid = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                // string sql = "dbo.HRMS_COMMON_DROPDOWN";
                string sql = "MOB_FM_RENTAL_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (strFunctionId.ToString() != "" || strFunctionId.ToString() != string.Empty || strFunctionId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@FunctionId", strFunctionId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FunctionId", "0");
                }

                if (strBranchId.ToString() != "" || strBranchId.ToString() != string.Empty || strBranchId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@BranchId", strBranchId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchId", "0");
                }

                if (strLocationId.ToString() != "" || strLocationId.ToString() != string.Empty || strLocationId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@LocationId", strLocationId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LocationId", "0");
                }
                if (strPropertyId.ToString() != "" || strPropertyId.ToString() != string.Empty || strPropertyId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@PropertyId", strPropertyId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PropertyId", "0");
                }
                if (strPropertyDesc.ToString() != "" || strPropertyDesc.ToString() != string.Empty || strPropertyDesc.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@PropertyDesc", strPropertyDesc);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PropertyDesc", "0");
                }

                if (rentelCode.ToString() != "" || rentelCode.ToString() != string.Empty || rentelCode.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@rentelCode", rentelCode);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rentelCode", "0");
                }
                if (strStatus.ToString() != "" || strStatus.ToString() != string.Empty || strStatus.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@Status", strStatus);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status", "A");
                }
                if (pageIndex.ToString() != "" || pageIndex.ToString() != string.Empty || pageIndex.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pageIndex", "0");
                }
                if (pageSize.ToString() != "" || pageSize.ToString() != string.Empty || pageSize.ToString() != null || pageSize.ToString() != "0")
                {
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pageSize", "20");
                }
                if (strusertype.ToString() != "" || strusertype.ToString() != string.Empty || strusertype.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strusertype", strusertype);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strusertype", "0");
                }
                if (userid.ToString() != "" || userid.ToString() != string.Empty || userid.ToString() != null )
                {

                    cmd.Parameters.AddWithValue("@userid", userid);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@userid", "0");
                }
                if (alphaname.ToString() != "" || alphaname.ToString() != string.Empty || alphaname.ToString() != null)
                {

                    cmd.Parameters.AddWithValue("@alphaname", alphaname);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@alphaname", "0");
                }



                cmd.Parameters.AddWithValue("@sortExpression", "property_desc");
                cmd.Parameters.AddWithValue("@Split_ID", "");
               
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
        [Route("getpaymentdetails/{strFunctionId}/{strBranchId}/{strLocationId}/{strPropertyId}/{strPropertyDesc}/{rentelCode}/{strStatus}/{pageIndex}/{pageSize}/{sortExpression}/{alphaname}/{Split_ID}/{strusertype}/{userid}")]
        public string getpaymentdetails(string strFunctionId = null, string strBranchId = null, string strLocationId = null, string strPropertyId = null, string strPropertyDesc = null, string rentelCode = null, string strStatus = null, string pageIndex = null, string pageSize = null, string sortExpression = null, string alphaname = null, String Split_ID = null, string strusertype = null, string userid = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                // string sql = "dbo.HRMS_COMMON_DROPDOWN";
                string sql = "MBL_FM_ReceiptSummary_GetRentalSummaryNew";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (strFunctionId.ToString() != "" || strFunctionId.ToString() != string.Empty || strFunctionId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strFunctionId", strFunctionId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strFunctionId", "0");
                }

                if (strBranchId.ToString() != "" || strBranchId.ToString() != string.Empty || strBranchId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strBranchId", strBranchId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strBranchId", "0");
                }

                if (strLocationId.ToString() != "" || strLocationId.ToString() != string.Empty || strLocationId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strLocationId", strLocationId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strLocationId", "0");
                }
                if (strPropertyId.ToString() != "" || strPropertyId.ToString() != string.Empty || strPropertyId.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strPropertyId", strPropertyId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strPropertyId", "0");
                }
                if (strPropertyDesc.ToString() != "" || strPropertyDesc.ToString() != string.Empty || strPropertyDesc.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strPropertyDesc", strPropertyDesc);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strPropertyDesc", "0");
                }

                if (rentelCode.ToString() != "" || rentelCode.ToString() != string.Empty || rentelCode.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@rentelCode", rentelCode);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rentelCode", "0");
                }
                if (strStatus.ToString() != "" || strStatus.ToString() != string.Empty || strStatus.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strStatus", strStatus);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strStatus", "A");
                }
                if (pageIndex.ToString() != "" || pageIndex.ToString() != string.Empty || pageIndex.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pageIndex", "0");
                }
                if (pageSize.ToString() != "" || pageSize.ToString() != string.Empty || pageSize.ToString() != null || pageSize.ToString() != "0")
                {
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pageSize", "20");
                }
                if (strusertype.ToString() != "" || strusertype.ToString() != string.Empty || strusertype.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@strusertype", strusertype);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@strusertype", "0");
                }
                if (userid.ToString() != "" || userid.ToString() != string.Empty || userid.ToString() != null)
                {

                    cmd.Parameters.AddWithValue("@userid", userid);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@userid", "0");
                }
                if (alphaname.ToString() != "" || alphaname.ToString() != string.Empty || alphaname.ToString() != null)
                {

                    cmd.Parameters.AddWithValue("@alphaname", alphaname);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@alphaname", "0");
                }



                cmd.Parameters.AddWithValue("@sortExpression", "property_desc");
                cmd.Parameters.AddWithValue("@Split_ID", "");

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
        [Route("bindproperty/{function_id}/{branch_id}/{propertycode}")]
        public string bindproperty(string function_id,string branch_id,string propertycode)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = " select *,BO_PARAMETER.TEXT as Department from CAMS_ASSET_MASTER inner join BO_PARAMETER on BO_PARAMETER.val=CAMS_ASSET_MASTER.ASSET_DEPARTMENT and BO_PARAMETER.type='bo_team'   where CAMS_ASSET_MASTER.function_id='"+ function_id + "' and CAMS_ASSET_MASTER.Branch_id='"+ branch_id + "' and CAMS_ASSET_MASTER.asset_code like'%"+ propertycode + "%'";

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


        [HttpGet]
        [Route("bindcategory/{cat}/{subcat}")]
        public string bindcategory(string cat, string subcat)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select ISSUEDESCRIPTION text , CATEGORY_ID VAL FROM  CAMS_USERREQUEST_REASON where category_id='"+ cat + "' and sub_category_id='"+ subcat + "' and status='A' order by text asc ";

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



        [HttpPost]
        [Route("get_training_details")]
        public async Task<ActionResult<Property>> get_training_details(Property data)
        {
            // string struser = data.user_lower;

            List<Property> Logdata = new List<Property>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";

            int REF_ID;
            int S_NO;
            string wkno = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "select cast(isnull(max(isnull(ASSET_REF_NO,0)),0)+1 as decimal) from CAMS_LAST_MAINTENANCE with (nolock) where 1=1  and FUNCTION_ID='" + data.functionid + "'  and BRANCH_ID='" + data.branchid + "'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Property log = new Property();
                    DataRow row = results.Rows[i];

                    REF_ID = Convert.ToInt32(row[0]);
                    DataSet ds = new DataSet();

                    ds.Tables.Add("pm_request");
                    ds.Tables[0].Columns.Add("company_cd");
                    ds.Tables[0].Columns.Add("lang_cd");
                    ds.Tables[0].Columns.Add("division_cd");
                    ds.Tables[0].Columns.Add("pmr_asset_reference");
                    ds.Tables[0].Columns.Add("Approval_Required");
                    ds.Tables[0].Columns.Add("problemdescr");
                    ds.Tables[0].Columns.Add("Priority");
                    ds.Tables[0].Columns.Add("Problem_Type");
                    ds.Tables[0].Columns.Add("Problem_Service_Type");
                    ds.Tables[0].Columns.Add("asset_activity_id");
                    ds.Tables[0].Columns.Add("pmr_reference");
                    ds.Tables[0].Columns.Add("pmr_asset_cagtegory");
                    ds.Tables[0].Columns.Add("pmr_pm_type");
                    ds.Tables[0].Columns.Add("pmr_details");
                    ds.Tables[0].Columns.Add("pm_due_date");
                    ds.Tables[0].Columns.Add("pm_changed_due_date");
                    ds.Tables[0].Columns.Add("pmr_counter_reading");
                    ds.Tables[0].Columns.Add("pmr_requested_by");
                    ds.Tables[0].Columns.Add("pmr_requesteddatetime");
                    ds.Tables[0].Columns.Add("pm_approved_date");
                    ds.Tables[0].Columns.Add("pm_completion_date");
                    ds.Tables[0].Columns.Add("pm_done_by");
                    ds.Tables[0].Columns.Add("pmr_planned_hrs");
                    ds.Tables[0].Columns.Add("pmr_actual_hrs");
                    ds.Tables[0].Columns.Add("pmr_deviation_reason");
                    ds.Tables[0].Columns.Add("pmr_breakdown_desc");
                    ds.Tables[0].Columns.Add("pmr_breakdown_reason");
                    ds.Tables[0].Columns.Add("pmr_breakdown_remedy");
                    ds.Tables[0].Columns.Add("pmr_tasks_carriedout");
                    ds.Tables[0].Columns.Add("pmr_status");
                    ds.Tables[0].Columns.Add("workflowstatus");
                    ds.Tables[0].Columns.Add("rel_status");
                    ds.Tables[0].Columns.Add("act_desc");
                    ds.Tables[0].Columns.Add("pmm_asset_reference");
                    ds.Tables[0].Columns.Add("amd_activity_id");
                    ds.Tables[0].Columns.Add("pmm_asset_desc");
                    ds.Tables[0].Columns.Add("pmm_asset_department");
                    ds.Tables[0].Columns.Add("departText");
                    ds.Tables[0].Columns.Add("equipmenttext");
                    ds.Tables[0].Columns.Add("pmm_equipment_type");
                    ds.Tables[0].Columns.Add("pmm_asset_sl_no");
                    ds.Tables[0].Columns.Add("pmm_asset_location");
                    ds.Tables[0].Columns.Add("pmr_shutdown_request");
                    ds.Tables[0].Columns.Add("refno");
                    ds.Tables[0].Columns.Add("category");
                    ds.Tables[0].Columns.Add("ASSET_DURATION");
                    ds.Tables[0].Columns.Add("ASSET_OWNER");
                    ds.Tables[0].Columns.Add("DYNFIELD1");
                    ds.Tables[0].Columns.Add("DYNFIELD2");
                    ds.Tables[0].Columns.Add("DYNFIELD3");
                    ds.Tables[0].Columns.Add("DYNFIELD4");
                    ds.Tables[0].Columns.Add("DYNFIELD5");
                    ds.Tables[0].Columns.Add("DYNFIELD6");
                    ds.Tables[0].Columns.Add("DYNFIELD7");
                    ds.Tables[0].Columns.Add("DYNFIELD8");
                    ds.Tables[0].Columns.Add("DYNFIELD9");
                    ds.Tables[0].Columns.Add("DYNFIELD10");
                    ds.Tables[0].Columns.Add("controltext");
                    ds.Tables[0].Columns.Add("Created_by");
                    ds.Tables[0].Columns.Add("ipaddress");


                    DataRow newrow = ds.Tables[0].NewRow();
                    newrow.BeginEdit();
                    int count = 1;
                    string controltext = "";

                    newrow["Approval_Required"] = "Y";
                    newrow["pmr_asset_reference"] = data.assetid;

                    newrow["pmr_reference"] = REF_ID;

                    newrow["Priority"] = data.Priority;

                    newrow["asset_activity_id"] = "0";
                    newrow["pm_due_date"] = data.pm_due_date;
                    newrow["pmr_requested_by"] = data.userid;
                    newrow["pmr_pm_type"] = data.drpPMType;
                    newrow["pmr_counter_reading"] = "0";
                    newrow["pmr_details"] = data.txtDetails;
                    newrow["division_cd"] = data.branchid;
                    newrow["category"] = "0";
                    newrow["act_desc"] = data.txtDetails;
                    newrow["amd_activity_id"] = "0";
                    newrow["ASSET_DURATION"] = "00:00";
                    newrow["ASSET_OWNER"] = data.assetownerid;
                    newrow["Created_by"] = data.userid;

                    string strIpAddress = "::1";
                    newrow["ipaddress"] = strIpAddress;

                    newrow["pmr_shutdown_request"] = "N";

                    newrow.EndEdit();
                    ds.Tables[0].Rows.Add(newrow);


                    string query1 = "select serial_no+1 from BO_SLNO_PARAMETER where type='URWorkOrderNumber'";

                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);

                    for (int j = 0; j < results1.Rows.Count; j++)
                    {
                        Property log1 = new Property();
                        DataRow row1 = results1.Rows[i];

                        S_NO = Convert.ToInt32(row1[0]);

                        string query2 = "select serial_no+1 from BO_SLNO_PARAMETER where type='URWorkOrderNumber'";

                        SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                        var reader2 = cmd2.ExecuteReader();
                        System.Data.DataTable results2 = new System.Data.DataTable();
                        results2.Load(reader2);

                        for (int k = 0; k < results2.Rows.Count; k++)
                        {
                            Property log2 = new Property();
                            DataRow row2 = results2.Rows[i];

                            S_NO = Convert.ToInt32(row2[0]);

                            string sql11 = "select prefix + '' + serial_no  + '' + isnull(suffix,'') from BO_slno_parameter where type='URWorkOrderNumber' and slno_domain='" + data.functionid + "'";

                            SqlCommand cmd3 = new SqlCommand(sql11, dbConn);
                            var reader3 = cmd3.ExecuteReader();
                            System.Data.DataTable results3 = new System.Data.DataTable();
                            results3.Load(reader3);


                            for (int a = 0; a < results3.Rows.Count; a++)
                            {
                                Property log3 = new Property();
                                DataRow row3 = results3.Rows[i];

                                wkno = Convert.ToString(row3[0]);

                                DataRow row4 = ds.Tables[0].Rows[0];

                                string strQry = "Exec CAMS_UIR_Save " + data.branchid + "," + data.functionid + "," + row4["pmr_asset_reference"] + "," + row4["pmr_reference"] + "," + row4["amd_activity_id"] + ",'" + row4["pm_due_date"] + "','" + row4["pmr_pm_type"].ToString() + "','" + row4["pmr_details"].ToString() + "','" + row4["problemdescr"] + "','" + row4["Priority"] + "','" + row4["Problem_Type"] + "','" + row4["Problem_Service_Type"] + "','" + row4["Created_by"] + "','" + row4["ipaddress"] + "','" + wkno + "','" + row4["ASSET_DURATION"] + "','" + row4["ASSET_OWNER"] + "','" + row4["pmr_requested_by"] + "','','','','A','Insert'";

                                SqlCommand cmd4 = new SqlCommand(strQry, dbConn);
                                var reader4 = cmd4.ExecuteReader();
                                System.Data.DataTable results4 = new System.Data.DataTable();
                                results4.Load(reader4);

                                string strsql = "exec CAMS_Mail_Save '" + data.functionid + "','" + data.branchid + "','" + row4["pmr_asset_reference"] + "','" + row4["pmr_reference"] + "','" + row4["amd_activity_id"] + "','" + row4["Created_by"] + "',''";

                                SqlCommand cmd5 = new SqlCommand(strsql, dbConn);
                                var reader5 = cmd5.ExecuteReader();
                                System.Data.DataTable results5 = new System.Data.DataTable();
                                results5.Load(reader5);


                                string strQry1 = "set dateformat dmy;INSERT INTO CAMS_EMAIL_NOTIFICATION (ASSET_ID,ASSET_CODE,STATUS,UPDATED_DATE,MAIL_DATE,FLAG) VALUES (" + data.assetid + ",'" + data.assetcode + "','P',getdate(),getdate(),'UR')";

                                SqlCommand cmd6 = new SqlCommand(strQry1, dbConn);
                                var reader6 = cmd6.ExecuteReader();
                                System.Data.DataTable results6 = new System.Data.DataTable();
                                results6.Load(reader6);




                            }


                        }




                    }

                }
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok("Issue raised successfully. Issue ref number :"+ result);



            }
        }


        [HttpGet]
        [Route("bindbranch/{function_id}/{userid}")]
        public string bindbranch(string function_id,string userid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from bo_branch_master inner join BO_BRANCH_ACCESS on BO_BRANCH_ACCESS.BRANCH_ID=bo_branch_master.BRANCH_ID and bo_branch_master.FUNCTION_ID=BO_BRANCH_ACCESS.FUNCTION_ID where BO_BRANCH_ACCESS.USER_ID='"+ userid + "' and BO_BRANCH_ACCESS.FUNCTION_ID='"+ function_id + "'";


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



        [HttpGet]
        [Route("getissuestatus/{strfunction}/{strbranch}/{propertyid}")]
        public string getissuestatus(string strfunction, string strbranch, string propertyid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT distinct CAMS_LAST_MAINTENANCE.branch_id,ASSET_REF_NO as refno,CAMS_PARAMETER_BRANCH.VAL, CAMS_PARAMETER_BRANCH.text as store_name,CAMS_ASSET_MASTER.ASSET_CODE as pmm_asset_code, CAMS_ASSET_MASTER.ASSET_DESCRIPTION as Asset_name, (case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC else  CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_DESC   end ) as amd_activity_desc, CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID as pm_asset_reference,CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY as pm_frequency,   case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then  CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)    when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)   else CONVERT(VARCHAR(5), cams_asset_request.ASSET_DURATION,108) end  as amd_maintenance_duration, CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID as amd_activity_id,convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_LAST_MAINTENANCE, 103) as CAMS_last_maintenance, convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_NEXT_MAINTENANCE, 103) as pm_next_maintenance,(case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'T' then 'Time Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'C' then 'Counter Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'B' then 'BreakDown' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'I' then 'Improvement' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'S' then 'Shutdown'  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'G' then 'General' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'D' then 'Conditional' end) as pm_frequency_mode,CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ASSIGNEDTO,Default_User_Type,isnull(IS_DEFAULT_USER, 'Y') as 'IS_DEFAULT_USER',tum_user_name,CAMS_TASKS_ASSIGNED.CAMS_USER_ID,case CAMS_ASSET_REQUEST.asset_start_status when 'Y' then 'Started' when 'P' then 'Pending' else 'New' end as Realease_status,case when CAMS_MAINTENANCE_DETAILS.Criticality is not null then BPCR.text else BPCR_P.TEXT end as 'Criticality',ASSET_NEXT_MAINTENANCE,CMD_CREATED_ON FROM CAMS_LAST_MAINTENANCE WITH(NOLOCK) left outer JOIN CAMS_MAINTENANCE_DETAILS  WITH(NOLOCK) ON CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ID = CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID and CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_STATUS != 'S' and CAMS_MAINTENANCE_DETAILS.FUNCTION_ID = CAMS_MAINTENANCE_DETAILS.FUNCTION_ID AND CAMS_MAINTENANCE_DETAILS.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID AND CAMS_MAINTENANCE_DETAILS.CMD_ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID INNER JOIN  CAMS_ASSET_MASTER WITH(NOLOCK) ON CAMS_LAST_MAINTENANCE.FUNCTION_ID = CAMS_ASSET_MASTER.FUNCTION_ID AND CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID = CAMS_ASSET_MASTER.ASSET_ID AND CAMS_LAST_MAINTENANCE.BRANCH_ID = CAMS_ASSET_MASTER.BRANCH_ID left outer join CAMS_ASSET_REQUEST with(nolock) on CAMS_ASSET_REQUEST.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and CAMS_ASSET_REQUEST.ASSET_PMR_REFERENCE = CAMS_LAST_MAINTENANCE.ASSET_REF_NO AND CAMS_ASSET_REQUEST.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID AND CAMS_ASSET_REQUEST.ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID   inner JOIN  CAMS_PARAMETER_BRANCH WITH(NOLOCK) ON CAMS_ASSET_MASTER.FUNCTION_ID = CAMS_PARAMETER_BRANCH.FUNCTION_ID   AND CAMS_ASSET_MASTER.ASSET_DEPARTMENT = CAMS_PARAMETER_BRANCH.val and CAMS_PARAMETER_BRANCH.type = 'bo_Team' left outer join CAMS_TASKS_ASSIGNED on CAMS_TASKS_ASSIGNED.CAMS_REF_NO = CAMS_LAST_MAINTENANCE.ASSET_REF_NO  and CAMS_TASKS_ASSIGNED.FUNCTION_ID = CAMS_ASSET_REQUEST.FUNCTION_ID left outer join BO_USER_MASTER  on TUM_USER_ID = CAMS_USER_ID and BO_USER_MASTER.FUNCTION_ID = CAMS_TASKS_ASSIGNED.FUNCTION_ID left outer join BO_PARAMETER  BPCR with(nolock) on BPCR.VAL = CAMS_MAINTENANCE_DETAILS.Criticality and BPCR.type = 'Criticality' and BPCR.status = 'A' left join BO_PARAMETER BPCR_P with(nolock) on convert(varchar, BPCR_P.VAL)= CAMS_LAST_MAINTENANCE.priority  and BPCR_P.type = 'Criticality' and BPCR_P.status = 'A' INNER JOIN BO_BRANCH_MASTER ON BO_BRANCH_MASTER.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID AND BO_BRANCH_MASTER.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID INNER JOIN BO_FUNCTION_ACCESS WITH(NOLOCK) ON BO_FUNCTION_ACCESS.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and BO_FUNCTION_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = BO_FUNCTION_ACCESS.FUNCTION_ID INNER JOIN BO_BRANCH_ACCESS WITH(NOLOCK) ON BO_BRANCH_ACCESS.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID AND BO_BRANCH_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY and BO_BRANCH_ACCESS.BRANCH_ID = BO_BRANCH_MASTER.BRANCH_ID INNER JOIN fm_property_master on substring([asset_sbs_no], PatIndex('%[0-9]%', [asset_sbs_no]), len([asset_sbs_no]))= fm_property_master.property_id and cams_asset_master.asset_sbs_no is not null and asset_sbs_no not like '%Batc%' and asset_sbs_no != '' and asset_sbs_no not like '%Seria%' and asset_sbs_no not like '%Lenov%' and asset_sbs_no not like '%Non%' where CAMS_LAST_MAINTENANCE.FUNCTION_ID = '"+ strfunction + "' and CAMS_LAST_MAINTENANCE.BRANCH_ID = '"+ strbranch + "' and fm_property_master.property_id='"+propertyid+"'";


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


        #endregion



        #region sowmi


        [HttpGet]
        [Route("getbranchid")]
        public string getbranchid()


        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = " select BRANCH_ID,BRANCH_CODE,BRANCH_DESC,FUNCTION_ID,ZONE_ID,STATUS from Bo_branch_master where STATUS = 'A' and FUNCTION_ID=1 ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }


        [HttpGet]
        [Route("getlocation/{functionid}/{branchid}")]
        public string getlocation(string functionid, string branchid)


        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = " select FUNCTION_ID,BRANCH_ID,LOCATION_ID,LOCATION_CODE,LOCATION_DESC,STATUS from BO_BRANCH_LOCATION_MASTER where STATUS='A' and FUNCTION_ID='"+ functionid + "' and branch_id='"+ branchid + "'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }

        [HttpGet]
        [Route("getProperty1")]
        public string getProperty1()


        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = " select function_id,Branch_id,location_id,property_id,property_code,property_desc from fm_property_master";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }
        #endregion



        [HttpGet]
        [Route("getProperty")]
        public string getProperty()


        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select function_id,Branch_id,location_id,property_id,property_code,property_desc from fm_property_master";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }



        [HttpGet]
        [Route("getPropertycode/{code}/{functionid}/{branchid}/{locationid}")]
        public string getPropertycode(string code,string functionid,string branchid,string locationid)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select * from fm_property_master where property_code like '%"+ code + "%' and function_id='"+ functionid + "' and Branch_id='"+ branchid + "' and location_id='"+ locationid + "' ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
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
