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

        //[HttpGet]
        //[Route("getpropertyadditionalcharger/{functionid}/{branchid}/{locationid}/{propertycodeDescription}/{ChequeDate}")]
        //public string getpropertyadditionalcharger(string functionid, string branchid, string locationid, string propertycode, string Description, string ChequeDate)
        //{
        //    string Logdata1 = string.Empty;
        //    var logdata = "";
        //    DataSet dsbranchcount = new DataSet();

        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {


        //        dbConn.Open();
        //        string query = "";
        //        query = "Select FUNCTION_ID, BRANCH_ID, Branch, LOCATION_ID, Location, Customer, (cast(Propertycode as nvarchar(max)) + '~' + Description)  as CodeDesc,Currency,	Amount,	PAYMODE ,BANK   ,Reference,	ChequeDate from FMPAYMENTDETAILS where 1=1";
        //        //query = "SELECT * FROM FMPAYMENTDETAILS where 1=1";

        //        if (functionid != "" && functionid != "0")
        //        {
        //            query = query + " and FUNCTION_ID='" + functionid + "'";
        //        }
        //        if (branchid != "" && branchid != "0")
        //        {
        //            query = query + " and branch_ID='" + branchid + "'";
        //        }
        //        if (locationid != "" && locationid != "0")
        //        {
        //            query = query + " and LOCATION_id='" + locationid + "'";
        //        }
        //        if (propertycode != "" && propertycode != "0")
        //        {
        //            query = query + " and [Propertycode]='" + propertycode + "'";
        //        }

        //        if (Description != "" && Description != "0")
        //        {
        //            query = query + " and [CodeDesc]='" + Description + "'";
        //        }

        //        if (ChequeDate != "" && ChequeDate != "0")
        //        {
        //            query = query + " and [ChequeDate]='" + ChequeDate + "'";
        //        }


        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);
        //        Logdata1 = DataTableToJSONWithStringBuilder(results);
        //        dbConn.Close();

        //        //var result = (new { recordsets = Logdata1 });
        //        return Logdata1;
        //    }
        //}

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
        [Route("getpaymentdetails_totalamount/{strPropertyId}/{rentelID}")]

        public string getpaymentdetails_totalamount(string strPropertyId = null, string rentelID = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();
            string totalamount = string.Empty;
            int totalamount1=0;
            decimal tamount=0.00m;
            string output = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string strQry = "Exec SP_Recipt_summary_FM '" + strPropertyId + "','" + rentelID + "'";
                SqlCommand cmd1 = new SqlCommand(strQry, dbConn);
                var reader1 = cmd1.ExecuteReader();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results1.Load(reader1);

                for (int i = 0; i < results1.Rows.Count; i++)
                {
                    DataRow row = results1.Rows[i];
                    totalamount = results1.Rows[i]["BALANCE_PAY"].ToString();
                  //  totalamount1 += Convert.ToInt32(totalamount);
                    tamount += Convert.ToDecimal(results1.Rows[i]["BALANCE_PAY"].ToString());
                    //totalamount += results1.Rows[i]["BALANCE_PAY"].ToString();


                }
                output="Total Amount : " + tamount.ToString();



            }
            return output;
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
               

                query = " select  a.STATUS,	a.FUNCTION_ID,	a.BRANCH_ID,	a.ASSET_DEPARTMENT,	a.ASSET_ID	,a.ASSET_CODE	,a.ASSET_DESCRIPTION	,a.ASSET_CATEGORY	,a.ASSET_TYPE	,a.ASSET_VALUE,	a.ASSET_SBS_NO	,a.ASSET_SERIAL_NO,	a.ASSET_BRAND,	a.ASSET_MODE,	a.ASSET_PURCHASE_DATE,	a.ASSET_WARRANTY_TILL,	a.ASSET_OWNER_ID,	a.ASSET_PRODUCTION_LOSS	,a.ASSET_TOTAL_ITEMS,	a.ASSET_CAPACITY,	a.ASSET_CAPACITY_UOM,	a.ASSET_COUNTER_ENABLED	,a.ASSET_RUNNING_HOURSDAYS,a.ASSET_REMARKS	,a.ASSET_INSURANCE_ENABLED	,a.ASSET_INSURANCE_FROMDATE,	a.ASSET_INSURANCE_TODATE	,a.ASSET_INSURANCE_DETAILS	,a.TYPE,	a.ASSET_SUM_INSURED	,a.ASSET_PREMIUM_AMOUNT,	a.ASSET_TASK_DESCRIPTION,	a.ASSET_DEPRECIATION_TYPE	,a.ASSET_DEPRECIATION_PERCENTAGE,	a.ASSET_OBSOLETE_ENABLED,	a.ASSET_OBSOLETE_DATE,	a.ASSET_OBSOLETE_REMARKS,	a.ASSET_INSTALLATION_DATE	,a.ASSET_INSTALLED_BY	,a.ASSET_CERTIFICATE_ISSUED,	a.ASSET_WORKING_CONDITION	,a.ASSET_INSTALLATION_DETAILS,	a.ASSET_PRODUCTIONLOSS_UOM	,a.ASSET_OBSOLETE_AMOUNT,	a.ASSET_RESIDUAL_VALUE,	a.ASSET_BAR_CODE,	a.ASSET_APPREQ_USERINITIATED,	a.ASSET_LIFE_DATE	,a.CAMS_ASSET_MANUFACTURER,	a.CREATED_ON	,a.UPDATED_ON,	a.CREATED_BY,a.UPDATED_BY,	a.IPADDRESS,	a.ASSET_IMAGES,	a.RENT_LEASE,	a.ASSET_USER	,	a.REPLACE_ASSETCODE,	a.ASSET_REPLACEASSET,	a.ASSET_REPLACESCRAP_DATE,	a.ASSET_REPLACESCRAP_REMARKS,	a.Asset_Class,	a.OBSOLETE_USER,	a.Asset_Inspectiondays	,a.Asset_Inspection,	a.ASSET_LocationId	,a.asset_latitude,	a.asset_longitude,bo.FUNCTION_ID   ,bo.TYPE    ,bo.TEXT as Department,	bo.CODE ,bo.VAL ,bo.SEQUENCE    ,bo.ISLOCK  ,bo.rowid,	bo.CREATED_BY,	bo.UPDATED_BY,bo.LST_UPD_DATE   ,bo.IPADDRESS,	bo.STATUS,	bo.COLOR    ,bo.orderby,	bo.Test,	bo.IMAGE    ,bo.id  ,bo.Compliance_category,	bo.Product_category,	bo.Product_subcategory,FM_PROPERTY_MASTER.property_code,FM_PROPERTY_MASTER.property_id,FM_PROPERTY_MASTER.location_id from CAMS_ASSET_MASTER a inner join BO_PARAMETER bo on bo.val = a.ASSET_DEPARTMENT and bo.type = 'bo_team' inner join FM_PROPERTY_MASTER on FM_PROPERTY_MASTER.ASSET_ID=a.ASSET_ID  where a.function_id = '" + function_id + "' and a.Branch_id = '" + branch_id + "' and a.asset_code like'%" + propertycode + "%'";

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
        [Route("getmytask/{strfunction}/{branch}/{mode}/{fdate}/{tdate}/{Status}/{dept}/{tag}/{strUserId}/{UserType}/{pageIndex}/{pageSize}/{sortExpression}/{alphaname}/{drpcategory}/{drptype}/{TASKTYPE}/{PrCode}/{PrDesc}/{strCriticality}/{assetname}/{actmaintenence}/{wrkordno}")]
        public string getmytask(string strfunction, string branch, string mode, string fdate, string tdate, string Status, string dept, string tag, string strUserId, string UserType, string pageIndex, string pageSize, string sortExpression, string alphaname, string drpcategory, string drptype, string TASKTYPE, string PrCode, string PrDesc, string strCriticality, string assetname, string actmaintenence, string wrkordno)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";



                if (strfunction.ToString() == "" || strfunction.ToString() == string.Empty || strfunction.ToString() == null)
                {
                    strfunction="0";
                }
               

                if (branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                {
                    branch = "0";
                }
                

                if (mode.ToString() == "" || mode.ToString() == string.Empty || mode.ToString() == null)
                {
                    mode = "0";
                }
               
                if (fdate.ToString() == "" || fdate.ToString() == string.Empty || fdate.ToString() == null)
                {
                    fdate = "0";
                }
                
                if (tdate.ToString() == "" || tdate.ToString() == string.Empty || tdate.ToString() == null)
                {
                    tdate = "0";
                }
                
                if (Status.ToString() == "" || Status.ToString() == string.Empty || Status.ToString() == null)
                {
                    Status = "0";
                }
                
                if (dept.ToString() == "" || dept.ToString() == string.Empty || dept.ToString() == null)
                {
                    dept = "0";
                }
                
                if (tag.ToString() == "" || tag.ToString() == string.Empty || tag.ToString() == null)
                {
                    tag = "0";
                }

                if (strUserId.ToString() == "" || strUserId.ToString() == string.Empty || strUserId.ToString() == null)
                {
                    strUserId = "0";
                }
                if (UserType.ToString() == "" || UserType.ToString() == string.Empty || UserType.ToString() == null)
                {
                    UserType = "0";
                }
                if (pageIndex.ToString() == "" || pageIndex.ToString() == string.Empty || pageIndex.ToString() == null)
                {
                    pageIndex = "0";
                }
                if (pageSize.ToString() == "" || pageSize.ToString() == string.Empty || pageSize.ToString() == null || pageSize =="0")
                {
                    pageSize = "25";
                }
               
                if (drpcategory.ToString() == "" || drpcategory.ToString() == string.Empty || drpcategory.ToString() == null)
                {
                    drpcategory = "0";
                }
                if (drptype.ToString() == "" || drptype.ToString() == string.Empty || drptype.ToString() == null)
                {
                    drptype = "0";
                }
                if (TASKTYPE.ToString() == "" || TASKTYPE.ToString() == string.Empty || TASKTYPE.ToString() == null)
                {
                    TASKTYPE = "0";
                }
                if (PrCode.ToString() == "" || PrCode.ToString() == string.Empty || PrCode.ToString() == null)
                {
                    PrCode = "0";
                }
                if (PrDesc.ToString() == "" || PrDesc.ToString() == string.Empty || PrDesc.ToString() == null)
                {
                    PrDesc = "0";
                }
                if (strCriticality.ToString() == "" || strCriticality.ToString() == string.Empty || strCriticality.ToString() == null)
                {
                    strCriticality = "0";
                }
                if (assetname.ToString() == "" || assetname.ToString() == string.Empty || assetname.ToString() == null)
                {
                    assetname = "0";
                }
                if (actmaintenence.ToString() == "" || actmaintenence.ToString() == string.Empty || actmaintenence.ToString() == null)
                {
                    actmaintenence = "0";
                }
                if (wrkordno.ToString() == "" || wrkordno.ToString() == string.Empty || wrkordno.ToString() == null)
                {
                    wrkordno = "0";
                }

                sortExpression = "duedate_long desc";


                query = "EXEC MBL_CAMS_Pending_GETPENDINGDETAIL_DEPT '" + strfunction + "','" + branch + "','" + mode + "','" + fdate + "','" + tdate + "','" + Status + "','" + dept + "','" + tag + "','" + strUserId + "','" + UserType + "','" + pageIndex + "','" + pageSize + "','" + sortExpression + "','" + alphaname + "','" + drpcategory + "','" + drptype + "','" + TASKTYPE + "','" + PrCode + "','" + PrDesc + "','" + strCriticality + "','" + assetname + "','" + actmaintenence + "','" + wrkordno + "'";

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
        [Route("mytaskstatusupdate")]
        public async Task<ActionResult<Property>> mytaskstatusupdate(Property data)
        {


            List<Property> Logdata = new List<Property>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "update CAMS_ASSET_REQUEST set asset_start_status='"+data.status+"' where ASSET_PMR_REFERENCE='"+data.pmr_reference + "'  ";
              

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                
                dbConn.Close();

                
                return Ok("Status updated successfully");


            }
        }




        [HttpPost]
        [Route("insertadditionalcharges")]
        public async Task<ActionResult<Property>> insertadditionalcharges(Property data)
        {


            List<Property> Logdata = new List<Property>();
            string Logdata1 = string.Empty;
            string status = string.Empty;
            string Flag = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                if (data.status=="N" || data.status =="A")
                {
                    status = "P";
                }
                if (data.FLAG=="A"|| data.FLAG=="N")
                {
                    Flag = "P";
                }

                dbConn.Open();
                string query = "INSERT INTO FM_PROPERTY_DAMAGE_DETAILS (PROPERTY_ID,DAMAGE_DESCRIPTION,AMOUNT,STATUS,FLAG,DUE_DATE)VALUES (" +
                    "cast ('" + data.propertyid + "' as int),'" + data.DAMAGE_DESCRIPTION + "','" + data.AMOUNT + "','" + status + "','" + Flag + "'," +
                    "cast( '" + data.DUE_DATE + "' as datetime)) SELECT SCOPE_IDENTITY()";

                int REF_ID;

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Property log = new Property();
                    DataRow row = results.Rows[i];

                    REF_ID = Convert.ToInt32(row[0]);

                    string query1 = " DECLARE @Year varchar(4), @Month varchar(2), @Day varchar(4), @Result varchar(10) SET @Year = (SELECT " +
                        " substring('" + data.DUE_DATE + "', 9, 3)) SET @Month = (SELECT substring('" + data.DUE_DATE + "', 6, 7)) " +
                        "SET @Day = (SELECT " + "substring('" + data.DUE_DATE + "', 0, 5)) SET @Result = @Year + '-' + @Month + '-' + @Day  " +
                        "declare @RentalPay varchar(50);select @RentalPay=prefix + '' + serial_no+''+suffix   from BO_SLNO_PARAMETER where type='RentalPayment' and slno_domain='" + data.functionid + "'; set dateformat dmy;insert into FM_PROPERTY_RENT_PAYMENT_SCHEDULE ( function_id,branch_id,location_id, property_id,rent_id, payment_flag,payment_desc,pay_amount, pay_date,status,created_by, created_on,lst_upd_by,lst_upd_on,ipaddress,PROPERTY_SPLIT_ID,FLAG,Damage_ID) values( '" + data.function_id + "','" + data.branch_id + "','" + data.location + "', cast ('" + data.propertyid + "' as int),cast ('" + data.rentid + "' as int), '" + data.FLAG + "', '" + data.DAMAGE_DESCRIPTION + "' , '" + data.AMOUNT + "', @Result,'" + data.status + "','" + data.user_id + "',getdate(), '" + data.user_id + "',getdate(),'" + ":11" + "','" + data.PROPERTYSPLITID + "','D', " + REF_ID + ")";


                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);

                }



                dbConn.Close();


                return Ok("Saved successfully");


            }
        }


        //sowmi-31/10/22

        [HttpGet]
        [Route("quickrecipt/{function}/{location}/{property}/{SplitID}")]
        public string quickrecipt(string function, string location, string property, string SplitID)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select Row_Number()  over(order by ChequeNo) as SNO ,*from(select FM_PROPERTY_TRANSACTIONS_INFO.BANK_NAME as BankName, FM_PROPERTY_MASTER.property_code,FM_PROPERTY_MASTER.location_id,FM_PROPERTY_TRANSACTIONS_INFO.function_id,'BRANCH_CODE'=(select BRANCH_DESC from BO_BRANCH_MASTER bn where bn.BRANCH_ID=FM_PROPERTY_TRANSACTIONS_INFO.branch_id)," +
                    "'BRANCH_DESC' = (select BRANCH_DESC from BO_BRANCH_MASTER bn where bn.BRANCH_ID = FM_PROPERTY_TRANSACTIONS_INFO.branch_id)," +
                    "'LOCATION_CODE' = (select LOCATION_CODE from BO_BRANCH_LOCATION_MASTER bln where bln.LOCATION_ID = FM_PROPERTY_TRANSACTIONS_INFO.LOCATION_ID)," +
                    "'LOCATION_DESC' = (select LOCATION_DESC from BO_BRANCH_LOCATION_MASTER bln where bln.LOCATION_ID = FM_PROPERTY_TRANSACTIONS_INFO.LOCATION_ID)," +
                    " FM_PROPERTY_TRANSACTIONS_INFO.branch_id,FM_PROPERTY_TRANSACTIONS_INFO.CHEQUE_NO as ChequeNo, convert(varchar(10), CHEQUE_DATE, 103) as Chequedate, FM_PROPERTY_TRANSACTIONS_INFO.Amount, FM_PROPERTY_TRANSACTIONS_INFO.Remarks,case FM_PROPERTY_TRANSACTIONS_INFO.Status when 'R' then 'Returned' when 'P' then 'Pending' when 'C' then 'Cleared' end Status, FM_PROPERTY_TRANSACTIONS_INFO.payment_id,case FM_PROPERTY_TRANSACTIONS_INFO.PAYMODE when 'C' then 'Cash' when 'Q' then 'Cheque' when 'R' then 'RTGS' when 'N' then 'NEFT' end PAYMODE, FM_PROPERTY_TRANSACTIONS_INFO.Flag, FM_PROPERTY_RENT_PAYMENT_SCHEDULE.payment_desc, convert(varchar(10), FM_PROPERTY_RENT_PAYMENT_SCHEDULE.pay_date, 103) as pay_date from FM_PROPERTY_TRANSACTIONS_INFO INNER JOIN FM_PROPERTY_RENT_PAYMENT_SCHEDULE on FM_PROPERTY_TRANSACTIONS_INFO.payment_id = FM_PROPERTY_RENT_PAYMENT_SCHEDULE.payment_id inner join FM_PROPERTY_MASTER on FM_PROPERTY_TRANSACTIONS_INFO.PROPERTY_ID = FM_PROPERTY_MASTER.property_id  where FM_PROPERTY_TRANSACTIONS_INFO.STATUS != 'I'" + "    and ('" + function + "' ='0' or '" + function + "' is null " + "or FM_PROPERTY_TRANSACTIONS_INFO.function_id =cast( ISNULL('" + function + "',0) as int) ) " +
                        "and('" + location + "' = '0' or '" + location + "' is null " +
                        "or FM_PROPERTY_TRANSACTIONS_INFO.LOCATION_ID = cast( ISNULL('" + location + "',0) as int))" +
                        "and('" + property + "' = '0' or '" + property + "' is null " +
                        "or FM_PROPERTY_MASTER.property_code = ('" + property + "'))";

                if (SplitID != "")
                {
                    query = query + "and FM_PROPERTY_TRANSACTIONS_INFO.Property_Split_ID=" + SplitID + "";
                }

                query = query + "and FM_PROPERTY_TRANSACTIONS_INFO.FLAG='R' )temp";

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
        [Route("quickrecipt_Pending_payment/{function}/{location}/{propertyid}")]
        public string quickrecipt_Pending_payment(string function, string location, string propertyid)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";

                query = "select rent_id, isnull(PDC_NO, 1) as 'NoPDC',receipt_ref,payment_desc,payment_id,pay_amount,convert(varchar(10), pay_date, 103) as pay_date,amount_paid_mode,balance_pay from FM_PROPERTY_RENT_PAYMENT_SCHEDULE where STATUS != 'I' and(balance_pay > 0 or balance_pay is null) and function_id ='" + function + "' and location_id = '" + location + "' and property_id = '" + propertyid + "' and SUBSTRING(convert(varchar, pay_date, 106),4,3)= SUBSTRING(convert(varchar, GETDATE(), 106), 4, 3) order by payment_id";


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
        [Route("getadditionalchargegrid/{property}")]
        public string getadditionalchargegrid(string property)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select Distinct DAMAGE_ID,PROPERTY_ID,DAMAGE_DESCRIPTION,AMOUNT,case when PAYMODE=N'C' then 'CASH' when PAYMODE=N'Q' then 'CHEQUE' END AS PAYMODE,BANK_NAME,case when CHEQUE_NO=N'0' then ''  when CHEQUE_NO!=N'0' then convert(varchar(10),CHEQUE_NO) end as CHEQUE_NO,case convert(varchar(10),CHEQUE_DATE,103)when '01/01/1900' then ''else convert(varchar(10),CHEQUE_DATE,103)  end as CHEQUE_DATE,AMOUNT,REMARKS,case when  STATUS =N'P' then 'Pending' when STATUS =N'C'  then 'Cleared'  end as STATUS,ACTIONTAKEN,case when  TRANSACTION_STATUS=N'P' then 'Pending'  when  TRANSACTION_STATUS=N'C' then 'Cleared'  when  TRANSACTION_STATUS=N'R' then 'Return' end as  TRANSACTION_STATUS,balance_pay,CONVERT(VARCHAR(10),DUE_DATE,103) AS DUE_DATE  from FM_PROPERTY_DAMAGE_DETAILS where  PROPERTY_ID='"+ property + "' AND FLAG='P'";

               

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
                //query = "select ISSUEDESCRIPTION text , CATEGORY_ID VAL FROM  CAMS_USERREQUEST_REASON where category_id='"+ cat + "' and sub_category_id='"+ subcat + "' and status='A' order by text asc ";
                //shy

                query = "select ISSUEDESCRIPTION text , CATEGORY_ID VAL,ISSUEID FROM  CAMS_USERREQUEST_REASON where category_id='" + cat + "' and sub_category_id='" + subcat + "' and status='A' order by text asc ";

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
        [Route("getreceiptdetails/{propertyid}/{rentalid}")]
        public string getreceiptdetails(string propertyid, string rentalid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT BO_FUNCTION_MASTER.FUNCTION_DESC,BO_BRANCH_MASTER.BRANCH_DESC,BO_BRANCH_LOCATION_MASTER.LOCATION_DESC, FM_PROPERTY_MASTER.PROPERTY_CODE,BO_BRANCH_LOCATION_MASTER.LOCATION_ID,FM_PROPERTY_MASTER.PROPERTY_DESC,FM_PROPERTY_MASTER.PROPERTY_CURRENCY, FM_PROPERTY_RENTAL_MASTER.RENTAL_ID,FM_PROPERTY_RENTAL_MASTER.RENTAL_CODE FROM FM_PROPERTY_MASTER WITH (NOLOCK) INNER JOIN BO_FUNCTION_MASTER WITH (NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID =FM_PROPERTY_MASTER.FUNCTION_ID   INNER JOIN BO_BRANCH_MASTER WITH (NOLOCK) ON BO_BRANCH_MASTER.FUNCTION_ID=FM_PROPERTY_MASTER.FUNCTION_ID  AND BO_BRANCH_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID    INNER JOIN BO_BRANCH_LOCATION_MASTER WITH (NOLOCK) ON BO_BRANCH_LOCATION_MASTER.FUNCTION_ID=FM_PROPERTY_MASTER.FUNCTION_ID  AND BO_BRANCH_LOCATION_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID AND BO_BRANCH_LOCATION_MASTER.LOCATION_ID=FM_PROPERTY_MASTER.LOCATION_ID  INNER JOIN FM_PROPERTY_RENTAL_MASTER WITH (NOLOCK) ON FM_PROPERTY_RENTAL_MASTER.FUNCTION_ID =FM_PROPERTY_MASTER.FUNCTION_ID  AND FM_PROPERTY_RENTAL_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID AND FM_PROPERTY_RENTAL_MASTER.LOCATION_ID =FM_PROPERTY_MASTER.LOCATION_ID  AND FM_PROPERTY_RENTAL_MASTER.PROPERTY_ID=FM_PROPERTY_MASTER.PROPERTY_ID WHERE FM_PROPERTY_RENTAL_MASTER.STATUS='A' AND FM_PROPERTY_RENTAL_MASTER.RENTAL_ID='"+ rentalid + "' AND FM_PROPERTY_MASTER.PROPERTY_ID='"+ propertyid + "' ";

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
        [Route("getreceiptdetailsgrid/{propertyid}/{rentalid}")]
        public string getreceiptdetailsgrid(string propertyid, string rentalid)
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    if (propertyid.ToString() == "0" || propertyid.ToString() == "" || propertyid.ToString() == string.Empty || propertyid.ToString() == null)
                    {
                        propertyid = "0";
                    }
                    if (rentalid.ToString() == "0" || rentalid.ToString() == "" || rentalid.ToString() == string.Empty || rentalid.ToString() == null)
                    {
                        rentalid = "0";
                    }
                   

                    string Logdata1 = string.Empty;
                    string sql = "MBL_SP_Recipt_summary_FM"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PropertyID", propertyid);
                    cmd.Parameters.AddWithValue("@RENTID", rentalid);
                    

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
        [Route("getpaymenthistorydetailsgrid/{propertyid}")]
        public string getpaymenthistorydetailsgrid(string propertyid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select FMP.property_id ,case when FMP.BANK_NAME is null or  FMP.BANK_NAME = '' then 'CASH' ELSE  FMP.BANK_NAME END AS 'BANKNAME' ,case when FMP.CHEQUE_NO is null or  FMP.CHEQUE_NO = '' then 'CASH' ELSE  FMP.CHEQUE_NO END AS 'CHEQUENO' ,case when convert(varchar(10),FMP.CHEQUE_Date, 103) is null or  convert(varchar(10),FMP.CHEQUE_Date, 103) = '' then 'CASH' ELSE  convert(varchar(10),FMP.CHEQUE_Date, 103) END AS 'CHEQUEDate' ,case when FMP.PAYMODE = 'C' then 'CASH' when FMP.PAYMODE = 'Q' then 'CHEQUE' when FMP.PAYMODE = 'R' then 'RTGS' when FMP.PAYMODE = 'N' then 'NEFT'  END AS 'PAYMODE' ,case FMP.STATUS when 'C' then 'Cleared' when 'P' then 'Pending' when 'R' then 'Returned' else FMP.STATUS end as status,0 AS transaction_id,FMP.Remarks, 0 AS 'payment_id','R' AS 'Flag',FMP.amount,FMPR.PAYMENT_DESC +'-'+ convert(varchar,year(FMP.Pay_date)) as 'PAYMENT_DESC',FMPO.exp_month + '-' +FMPO.exp_description +'-'+ convert(varchar,year(FMP.Pay_date)) as Descr  into #Temp from FM_PROPERTY_TRANSACTIONS_INFO  FMP LEFT join FM_PROPERTY_RENT_PAYMENT_SCHEDULE FMPR on FMP.property_id = FMPR.property_id and FMP.payment_id = FMPR.payment_id LEFT join FM_PROPERTY_OTHER_EXPENSE_SCHEDULE FMPO on FMP.property_id = FMPO.property_id and FMPO.opex_id = FMP.payment_id where 1=1 and FMP.property_id='" + propertyid + "' AND FMP.AMOUNT >= 0   SELECT distinct e.property_id,e.BANKNAME,e.CHEQUENO,sum(e.amount) AS 'Amount',sum(e.amount) AS 'pay_amount',status,CHEQUEDate,payment_id,PAYMODE,Remarks,transaction_id,Flag, r.[payment_desc] FROM  #Temp e   CROSS APPLY  (  SELECT isnull(r.PAYMENT_DESC,'') + '  ' + isnull( r.DESCR,'') FROM #Temp r where  e.property_id = r.property_id and e.BANKNAME = r.BANKNAME and e.chequeno = r.chequeno    FOR XML PATH('')	   ) r ([payment_desc]) group by e.property_id,e.BANKNAME,e.chequeno,r.[payment_desc],CHEQUEDate,PAYMODE,transaction_id,Flag,payment_id,Remarks,status";


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
        [Route("getpaymentdetailsreports/{functionid}/{branchid}/{locationid}/{propertycode}/{customer}/{fromdate}/{todate}/{status}/{paymentmode}/{chequeno}")]
        public string getpaymentdetailsreports(string functionid, string branchid, string locationid, string propertycode, string customer, string fromdate, string todate, string status, string paymentmode, string chequeno)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT Branch,Location,Propertycode,Customer,Description,Currency,Amount,PayMode,Bank,Reference,ChequeDate,Status FROM FMPAYMENTDETAILS where 1=1";

                if (functionid != "" && functionid != "0")
                {
                    query = query + " and FUNCTION_ID='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and BRANCH_ID='" + branchid + "'";
                }
                if (locationid != "" && locationid != "0")
                {
                    query = query + " and LOCATION_ID='" + locationid + "'";
                }
                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and PropertyCode='" + propertycode + "'";
                }
                if (customer != "" && customer != "0")
                {
                    query = query + " and Customer='" + customer + "'";
                }
                if (fromdate != "" && fromdate != "0")
                {
                    query = query + " and ChequeDate>=CONVERT(varchar, convert(datetime,'" + fromdate + "', 103),103)";
                }
                if (todate != "" && todate != "0")
                {
                    query = query + " and ChequeDate<=CONVERT(varchar, convert(datetime,'" + todate + "', 103),103)";
                }
                if (status != "" && status != "0")
                {
                    query = query + " and status='" + status + "'";
                }
                if (paymentmode != "" && paymentmode != "0")
                {
                    query = query + " and PAYMODE='" + paymentmode + "'";
                }
                if (chequeno != "" && chequeno != "0")
                {
                    query = query + " and Reference='" + chequeno + "'";
                }




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
        [Route("getpropertylistreports/{functionid}/{branchid}/{locationid}/{propertycode}/{Propertydesc}/{fromdate}/{todate}/{status}/{PropertyType}/{PropertyOwner}/{PropertyNature}")]
        public string getpropertylistreports(string functionid, string branchid, string locationid, string propertycode, string Propertydesc, string fromdate, string todate, string status, string PropertyType, string PropertyOwner, string PropertyNature)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //query = "select [Function],Branch,Location,Code,  Description,[Property Type],status,[Property Owner],[Total Floor],[Property On Floor],PropertyNature,CustomerName,Fromdate,Todate from FMPropertylist where 1=1";
                //Added by sankari
                query = "select[Function],Branch,Location,Code,  Description,[Property Type] as [PropertyType],status,[Property Owner] as [PropertyOwner],[Total Floor] as [TotalFloor],[Property On Floor] as [PropertyOnFloor],PropertyNature,CustomerName,Fromdate,Todate from FMPropertylist where 1 = 1";

                if (functionid != "" && functionid != "0")
                {
                    query = query + " and function_id='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and BRANCH_id='" + branchid + "'";
                }
                if (locationid != "" && locationid != "0")
                {
                    query = query + " and LOCATION_id='" + locationid + "'";
                }
                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and Code='" + propertycode + "'";
                }
                if (Propertydesc != "" && Propertydesc != "0")
                {
                    query = query + " and Description='" + Propertydesc + "'";
                }
                if (fromdate != "" && fromdate != "0")
                {
                    query = query + " and FromDate>=CONVERT(varchar, convert(datetime,'" + fromdate + "', 103),103)";
                }
                if (todate != "" && todate != "0")
                {
                    query = query + " and Todate<=CONVERT(varchar, convert(datetime,'" + todate + "', 103),103)";
                }
                if (status != "" && status != "0")
                {
                    query = query + " and status='" + status + "'";
                }
                if (PropertyType != "" && PropertyType != "0")
                {
                    query = query + " and [Property Type]='" + PropertyType + "'";
                }
                if (PropertyOwner != "" && PropertyOwner != "0")
                {
                    query = query + " and [Property Owner]='" + PropertyOwner + "'";
                }
                if (PropertyNature != "" && PropertyNature != "0")
                {
                    query = query + " and PropertyNature='" + PropertyNature + "'";
                }




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
        [Route("getpropertyissueledger/{functionid}/{branchid}/{locationid}/{propertycode}/{Propertydesc}/{fromdate}/{todate}/{status}/{Customer}/{AssignedTo}")]
        public string getpropertyissueledger(string functionid, string branchid, string locationid, string propertycode, string Propertydesc, string fromdate, string todate, string status, string Customer, string AssignedTo)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //query = "select Branch,Location,[Property Code] as PropertyCode,[Property Desc] as Description,Customer,PMR_REference as [Issue Ref],IssueDesc,Criticality,AssignedTo,TargetDate,CompletionDateTime,Status from fmissueledger where 1=1";
                //Added by sankari
                query = "select Branch, Location,[Property Code] as PropertyCode,[Property Desc] as Description,Customer,PMR_REference as [IssueRef],IssueDesc,Criticality,AssignedTo,TargetDate,CompletionDateTime,Status from fmissueledger where 1 = 1";

                if (functionid != "" && functionid != "0")
                {
                    query = query + " and FUNCTION_ID='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and branch_ID='" + branchid + "'";
                }
                if (locationid != "" && locationid != "0")
                {
                    query = query + " and LOCATION_id='" + locationid + "'";
                }
                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and [Property Code]='" + propertycode + "'";
                }
                if (Propertydesc != "" && Propertydesc != "0")
                {
                    query = query + " and [Property Desc]='" + Propertydesc + "'";
                }
                if (fromdate != "" && fromdate != "0")
                {
                    query = query + " and TargetDate>=CONVERT(varchar, convert(datetime,'" + fromdate + "', 103),103)";
                }
                if (todate != "" && todate != "0")
                {
                    query = query + " and TargetDate<=CONVERT(varchar, convert(datetime,'" + todate + "', 103),103)";
                }
                if (status != "" && status != "0")
                {
                    query = query + " and status='" + status + "'";
                }
                if (Customer != "" && Customer != "0")
                {
                    query = query + " and Customer='" + Customer + "'";
                }
                if (AssignedTo != "" && AssignedTo != "0")
                {
                    query = query + " and AssignedTo='" + AssignedTo + "'";
                }



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
        [Route("getdocumentexpiryreport/{functionid}/{branchid}/{propertycode}/{issuedate}/{expirydate}/{clientcode}/{clientname}")]
        public string getdocumentexpiryreport(string functionid, string branchid, string propertycode, string issuedate, string expirydate, string clientcode, string clientname)

        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //query = "SELECT Branch,Location,[Property Code],PropertyDesc,ClientCode,CustomerName AS ClientName,[From Date],[To Date], [Document Type],[Issuing Autority],Reference,[Issue Date],[Expiry Date],[Expiry Days] FROM [FMDocumentExpiry] where 1=1";
                //Added by sankari
                query = "SELECT Branch, Location,[Property Code] as  [PropertyCode],PropertyDesc,ClientCode,CustomerName AS ClientName,[From Date] as [FromDate],[To Date] as [ToDate], [Document Type] as [DocumentType],[Issuing Autority] as [IssuingAutority],Reference,[Issue Date] as [IssueDate],[Expiry Date] as [ExpiryDate],[Expiry Days] as [ExpiryDays] FROM[FMDocumentExpiry] where 1 = 1";
                if (functionid != "" && functionid != "0")
                {
                    query = query + " and Function_id='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and BRANCH_ID='" + branchid + "'";
                }

                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and [Property Code]='" + propertycode + "'";
                }

                if (issuedate != "" && issuedate != "0")
                {
                    query = query + " and [Issue Date]=CONVERT(varchar, convert(datetime,'" + issuedate + "',103),103)";
                }
                if (expirydate != "" && expirydate != "0")
                {
                    query = query + " and [Expiry Date]=CONVERT(varchar, convert(datetime,'" + expirydate + "',103),103)";
                }
                if (clientcode != "" && clientcode != "0")
                {
                    query = query + " and ClientCode='" + clientcode + "'";
                }
                if (clientname != "" && clientname != "0")
                {
                    query = query + " and CustomerName='" + clientname + "'";
                }




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
        [Route("getpropertycontactlistreport/{functionid}/{branchid}/{propertycode}/{Propertydesc}")]
        public string getpropertycontactlistreport(string functionid, string branchid, string propertycode, string Propertydesc)

        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //Added by sankari
                query = "select  function_desc as 'Function',branch_desc as 'Branch',unique_ref as [UniqueRef],property_code as 'PropertyCode',property_desc as 'PropertyDesc', BO_PARAMETER.text as ContactCategory,Name,Address,Res_Phone,   Off_Phone,Email, Others1,Others2,Others3 from FM_PROPERTY_CONTACT_DETAILS   inner join BO_PARAMETER   on FM_PROPERTY_CONTACT_DETAILS.category_code=BO_PARAMETER.code  and BO_PARAMETER.type='PropertyContactCategory' and FM_PROPERTY_CONTACT_DETAILS.function_id=BO_PARAMETER.FUNCTION_ID inner join FM_PROPERTY_MASTER on FM_PROPERTY_CONTACT_DETAILS.property_id=FM_PROPERTY_MASTER.property_id  inner join BO_FUNCTION_MASTER on  FM_PROPERTY_CONTACT_DETAILS.function_id=BO_FUNCTION_MASTER.function_id  inner join BO_BRANCH_MASTER on  FM_PROPERTY_CONTACT_DETAILS.branch_id=BO_BRANCH_MASTER.branch_id where 1 = 1";

                if (functionid != "" && functionid != "0")
                {
                    query = query + " and FM_PROPERTY_CONTACT_DETAILS.function_id='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and FM_PROPERTY_CONTACT_DETAILS.branch_id='" + branchid + "'";
                }

                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and FM_PROPERTY_MASTER.property_code='" + propertycode + "'";
                }

                if (Propertydesc != "" && Propertydesc != "0")
                {
                    query = query + " and FM_PROPERTY_MASTER.property_desc='" + Propertydesc + "'";
                }





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
        [Route("getpropertyadditionalcharger/{functionid}/{branchid}/{locationid}/{propertycode}/{Description}/{ChequeDate}")]
        public string getpropertyadditionalcharger(string functionid, string branchid, string locationid, string propertycode, string Description, string ChequeDate)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //query = "Select FUNCTION_ID, BRANCH_ID, Branch, LOCATION_ID, Location, Customer, (cast(Propertycode as nvarchar(max)) + '~' + Description)  as CodeDesc,Currency,	Amount,	PAYMODE ,BANK   ,Reference,	ChequeDate from FMPAYMENTDETAILS where 1=1";
                query = "SELECT * FROM FMPAYMENTDETAILS where 1=1";

                if (functionid != "" && functionid != "0")
                {
                    query = query + " and FUNCTION_ID='" + functionid + "'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and branch_ID='" + branchid + "'";
                }
                if (locationid != "" && locationid != "0")
                {
                    query = query + " and LOCATION_id='" + locationid + "'";
                }
                if (propertycode != "" && propertycode != "0")
                {
                    query = query + " and [Propertycode]='" + propertycode + "'";
                }

                if (Description != "" && Description != "0")
                {
                    query = query + " and [Description]='" + Description + "'";
                }

                if (ChequeDate != "" && ChequeDate != "0")
                {
                    //query = query + " and [ChequeDate]='" + ChequeDate + "'";
                    query = query + " and ChequeDate>=CONVERT(varchar, convert(datetime,'" + ChequeDate + "', 103),103)";
                }


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
                   // ds.Tables[0].Columns.Add("Issueid");//shylaja


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
                  //  newrow["Issueid"] = data.Issueid;//shylaja

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

                                string strQry = "Exec CAMS_UIR_Save " + data.branchid + "," + data.functionid + "," + row4["pmr_asset_reference"] + "," + row4["pmr_reference"] + "," + row4["amd_activity_id"] + ",'" + row4["pm_due_date"] + "','" + row4["pmr_pm_type"].ToString() + "','" + row4["pmr_details"].ToString() + "','" + row4["problemdescr"] + "','" + row4["Priority"] + "','" + row4["Problem_Type"] + "','" + row4["Problem_Service_Type"] + "','" + row4["Created_by"] + "','" + row4["ipaddress"] + "','" + wkno + "','" + row4["ASSET_DURATION"] + "','" + row4["ASSET_OWNER"]  + "','" + row4["pmr_requested_by"]  + "','','','','A','Insert'";

                               // +"','" + row4["Issueid"]//shy

                                SqlCommand cmd4 = new SqlCommand(strQry, dbConn);
                                var reader4 = cmd4.ExecuteReader();
                                System.Data.DataTable results4 = new System.Data.DataTable();
                                results4.Load(reader4);

                                string strsqlupdate = "update BO_slno_parameter set serial_no='"+ S_NO + "' where type='URWorkOrderNumber'";

                                SqlCommand cmd7 = new SqlCommand(strsqlupdate, dbConn);
                                var reader7 = cmd7.ExecuteReader();
                                System.Data.DataTable results7 = new System.Data.DataTable();
                                results7.Load(reader7);


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

                // var result = (new { recordsets = Logdata1 });
                // return Ok("Issue raised successfully. Issue ref number :"+ result);

                return Ok(Logdata1);


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
                query = "select * from bo_branch_master inner join BO_BRANCH_ACCESS on BO_BRANCH_ACCESS.BRANCH_ID=bo_branch_master.BRANCH_ID and bo_branch_master.FUNCTION_ID=BO_BRANCH_ACCESS.FUNCTION_ID where bo_branch_master.Status='A' and BO_BRANCH_ACCESS.USER_ID='" + userid + "' and BO_BRANCH_ACCESS.FUNCTION_ID='"+ function_id + "'";


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
                query = "SELECT distinct CAMS_LAST_MAINTENANCE.branch_id,ASSET_REF_NO as refno,CAMS_PARAMETER_BRANCH.VAL, CAMS_PARAMETER_BRANCH.text as store_name,CAMS_ASSET_MASTER.ASSET_CODE as pmm_asset_code, CAMS_ASSET_MASTER.ASSET_DESCRIPTION as Asset_name, (case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC else  CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_DESC   end ) as amd_activity_desc, CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID as pm_asset_reference,CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY as pm_frequency,   case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then  CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)    when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)   else CONVERT(VARCHAR(5), cams_asset_request.ASSET_DURATION,108) end  as amd_maintenance_duration, CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID as amd_activity_id,convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_LAST_MAINTENANCE, 103) as CAMS_last_maintenance, convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_NEXT_MAINTENANCE, 103) as pm_next_maintenance,(case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'T' then 'Time Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'C' then 'Counter Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'B' then 'BreakDown' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'I' then 'Improvement' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'S' then 'Shutdown'  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'G' then 'General' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'D' then 'Conditional' end) as pm_frequency_mode,CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ASSIGNEDTO,Default_User_Type,isnull(IS_DEFAULT_USER, 'Y') as 'IS_DEFAULT_USER',tum_user_name,tum_user_name as Assigned_To,CAMS_TASKS_ASSIGNED.CAMS_USER_ID,case CAMS_ASSET_REQUEST.asset_start_status when 'Y' then 'Started' when 'S' then 'Started' when 'P' then 'Pending' when 'C' then 'Completed' when 'I' then 'Cancelled' else 'New' end as Realease_status,case when CAMS_MAINTENANCE_DETAILS.Criticality is not null then BPCR.text else BPCR_P.TEXT end as 'Criticality',ASSET_NEXT_MAINTENANCE,CMD_CREATED_ON FROM CAMS_LAST_MAINTENANCE WITH(NOLOCK) left outer JOIN CAMS_MAINTENANCE_DETAILS  WITH(NOLOCK) ON CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ID = CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID and CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_STATUS != 'S' and CAMS_MAINTENANCE_DETAILS.FUNCTION_ID = CAMS_MAINTENANCE_DETAILS.FUNCTION_ID  AND CAMS_MAINTENANCE_DETAILS.CMD_ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID INNER JOIN  CAMS_ASSET_MASTER WITH(NOLOCK) ON CAMS_LAST_MAINTENANCE.FUNCTION_ID = CAMS_ASSET_MASTER.FUNCTION_ID AND CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID = CAMS_ASSET_MASTER.ASSET_ID left outer join CAMS_ASSET_REQUEST with(nolock) on CAMS_ASSET_REQUEST.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and CAMS_ASSET_REQUEST.ASSET_PMR_REFERENCE = CAMS_LAST_MAINTENANCE.ASSET_REF_NO AND CAMS_ASSET_REQUEST.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID AND CAMS_ASSET_REQUEST.ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID  inner JOIN  CAMS_PARAMETER_BRANCH WITH(NOLOCK) ON CAMS_ASSET_MASTER.FUNCTION_ID = CAMS_PARAMETER_BRANCH.FUNCTION_ID   AND CAMS_ASSET_MASTER.ASSET_DEPARTMENT = CAMS_PARAMETER_BRANCH.val and CAMS_PARAMETER_BRANCH.type = 'bo_Team' left outer join CAMS_TASKS_ASSIGNED on CAMS_TASKS_ASSIGNED.CAMS_REF_NO = CAMS_LAST_MAINTENANCE.ASSET_REF_NO  and CAMS_TASKS_ASSIGNED.FUNCTION_ID = CAMS_ASSET_REQUEST.FUNCTION_ID left outer join BO_USER_MASTER  on TUM_USER_ID = CAMS_USER_ID and BO_USER_MASTER.FUNCTION_ID = CAMS_TASKS_ASSIGNED.FUNCTION_ID left outer join BO_PARAMETER  BPCR with(nolock) on BPCR.VAL = CAMS_MAINTENANCE_DETAILS.Criticality and BPCR.type = 'Criticality' and BPCR.status = 'A' left join BO_PARAMETER BPCR_P with(nolock) on convert(varchar, BPCR_P.VAL)= CAMS_LAST_MAINTENANCE.priority  and BPCR_P.type = 'Criticality' and BPCR_P.status = 'A' INNER JOIN BO_BRANCH_MASTER ON BO_BRANCH_MASTER.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID AND BO_BRANCH_MASTER.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID INNER JOIN BO_FUNCTION_ACCESS WITH(NOLOCK) ON BO_FUNCTION_ACCESS.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and BO_FUNCTION_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = BO_FUNCTION_ACCESS.FUNCTION_ID INNER JOIN BO_BRANCH_ACCESS WITH(NOLOCK) ON BO_BRANCH_ACCESS.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID AND BO_BRANCH_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY and BO_BRANCH_ACCESS.BRANCH_ID = BO_BRANCH_MASTER.BRANCH_ID INNER JOIN fm_property_master on CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID=fm_property_master.ASSET_ID and substring([asset_sbs_no], PatIndex('%[0-9]%', [asset_sbs_no]), len([asset_sbs_no]))= fm_property_master.property_id and cams_asset_master.asset_sbs_no is not null and asset_sbs_no not like '%Batc%' and asset_sbs_no != '' and asset_sbs_no not like '%Seria%' and asset_sbs_no not like '%Lenov%' and asset_sbs_no not like '%Non%' where CAMS_LAST_MAINTENANCE.FUNCTION_ID = '" + strfunction + "' and CAMS_LAST_MAINTENANCE.BRANCH_ID = '"+ strbranch + "' and fm_property_master.property_id='"+propertyid+"'";







                //query = "SELECT distinct CAMS_LAST_MAINTENANCE.branch_id,ASSET_REF_NO as refno,CAMS_PARAMETER_BRANCH.VAL, CAMS_PARAMETER_BRANCH.text as store_name,CAMS_ASSET_MASTER.ASSET_CODE as pmm_asset_code, CAMS_ASSET_MASTER.ASSET_DESCRIPTION as Asset_name, (case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DESC else  CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_DESC   end ) as amd_activity_desc, CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID as pm_asset_reference,CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY as pm_frequency,   case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='C' then  CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)    when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE='T' then CONVERT(VARCHAR(8),CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_DURATION,108)   else CONVERT(VARCHAR(5), cams_asset_request.ASSET_DURATION,108) end  as amd_maintenance_duration, CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID as amd_activity_id,convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_LAST_MAINTENANCE, 103) as CAMS_last_maintenance, convert(varchar(10), CAMS_LAST_MAINTENANCE.ASSET_NEXT_MAINTENANCE, 103) as pm_next_maintenance,(case when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'T' then 'Time Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'C' then 'Counter Based' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'B' then 'BreakDown' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'I' then 'Improvement' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'S' then 'Shutdown'  when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'G' then 'General' when CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_MODE = 'D' then 'Conditional' end) as pm_frequency_mode,CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ASSIGNEDTO,Default_User_Type,isnull(IS_DEFAULT_USER, 'Y') as 'IS_DEFAULT_USER',tum_user_name,tum_user_name as Assigned_To,CAMS_TASKS_ASSIGNED.CAMS_USER_ID,case CAMS_ASSET_REQUEST.asset_start_status when 'Y' then 'Started' when 'S' then 'Started' when 'P' then 'Pending' when 'C' then 'Completed' when 'I' then 'Cancelled' else 'New' end as Realease_status,case when CAMS_MAINTENANCE_DETAILS.Criticality is not null then BPCR.text else BPCR_P.TEXT end as 'Criticality',ASSET_NEXT_MAINTENANCE,CMD_CREATED_ON FROM CAMS_LAST_MAINTENANCE WITH(NOLOCK) left outer JOIN CAMS_MAINTENANCE_DETAILS  WITH(NOLOCK) ON CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_ID = CAMS_LAST_MAINTENANCE.ASSET_ACTIVITY_ID and CAMS_MAINTENANCE_DETAILS.CMD_ACTIVITY_STATUS != 'S' and CAMS_MAINTENANCE_DETAILS.FUNCTION_ID = CAMS_MAINTENANCE_DETAILS.FUNCTION_ID AND CAMS_MAINTENANCE_DETAILS.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID AND CAMS_MAINTENANCE_DETAILS.CMD_ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID INNER JOIN  CAMS_ASSET_MASTER WITH(NOLOCK) ON CAMS_LAST_MAINTENANCE.FUNCTION_ID = CAMS_ASSET_MASTER.FUNCTION_ID AND CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID = CAMS_ASSET_MASTER.ASSET_ID AND CAMS_LAST_MAINTENANCE.BRANCH_ID = CAMS_ASSET_MASTER.BRANCH_ID left outer join CAMS_ASSET_REQUEST with(nolock) on CAMS_ASSET_REQUEST.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and CAMS_ASSET_REQUEST.ASSET_PMR_REFERENCE = CAMS_LAST_MAINTENANCE.ASSET_REF_NO AND CAMS_ASSET_REQUEST.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID AND CAMS_ASSET_REQUEST.ASSET_ID = CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID  inner JOIN  CAMS_PARAMETER_BRANCH WITH(NOLOCK) ON CAMS_ASSET_MASTER.FUNCTION_ID = CAMS_PARAMETER_BRANCH.FUNCTION_ID   AND CAMS_ASSET_MASTER.ASSET_DEPARTMENT = CAMS_PARAMETER_BRANCH.val and CAMS_PARAMETER_BRANCH.type = 'bo_Team' left outer join CAMS_TASKS_ASSIGNED on CAMS_TASKS_ASSIGNED.CAMS_REF_NO = CAMS_LAST_MAINTENANCE.ASSET_REF_NO  and CAMS_TASKS_ASSIGNED.FUNCTION_ID = CAMS_ASSET_REQUEST.FUNCTION_ID left outer join BO_USER_MASTER  on TUM_USER_ID = CAMS_USER_ID and BO_USER_MASTER.FUNCTION_ID = CAMS_TASKS_ASSIGNED.FUNCTION_ID left outer join BO_PARAMETER  BPCR with(nolock) on BPCR.VAL = CAMS_MAINTENANCE_DETAILS.Criticality and BPCR.type = 'Criticality' and BPCR.status = 'A' left join BO_PARAMETER BPCR_P with(nolock) on convert(varchar, BPCR_P.VAL)= CAMS_LAST_MAINTENANCE.priority  and BPCR_P.type = 'Criticality' and BPCR_P.status = 'A' INNER JOIN BO_BRANCH_MASTER ON BO_BRANCH_MASTER.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID AND BO_BRANCH_MASTER.BRANCH_ID = CAMS_LAST_MAINTENANCE.BRANCH_ID INNER JOIN BO_FUNCTION_ACCESS WITH(NOLOCK) ON BO_FUNCTION_ACCESS.FUNCTION_ID = CAMS_LAST_MAINTENANCE.FUNCTION_ID and BO_FUNCTION_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = BO_FUNCTION_ACCESS.FUNCTION_ID INNER JOIN BO_BRANCH_ACCESS WITH(NOLOCK) ON BO_BRANCH_ACCESS.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID AND BO_BRANCH_ACCESS.USER_ID = CAMS_LAST_MAINTENANCE.CREATED_BY and BO_BRANCH_ACCESS.BRANCH_ID = BO_BRANCH_MASTER.BRANCH_ID INNER JOIN fm_property_master on CAMS_LAST_MAINTENANCE.ASSET_ASSET_ID=fm_property_master.ASSET_ID and substring([asset_sbs_no], PatIndex('%[0-9]%', [asset_sbs_no]), len([asset_sbs_no]))= fm_property_master.property_id and cams_asset_master.asset_sbs_no is not null and asset_sbs_no not like '%Batc%' and asset_sbs_no != '' and asset_sbs_no not like '%Seria%' and asset_sbs_no not like '%Lenov%' and asset_sbs_no not like '%Non%' where CAMS_LAST_MAINTENANCE.FUNCTION_ID = '" + strfunction + "' and CAMS_LAST_MAINTENANCE.BRANCH_ID = '" + strbranch + "' and fm_property_master.property_id='" + propertyid + "'";

                /////inner join CAMS_USERREQUEST_REASON CUR ON 1 = 1 AND CAMS_LAST_MAINTENANCE.ASSET_FREQUENCY_VALUE = CUR.CATEGORY_ID AND CAMS_LAST_MAINTENANCE.ASSET_LAST_MAINTENANCE_TYPE = CUR.ISSUEID
                ///,CUR.ISSUEDESCRIPTION AS Issuecategory 



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
        [Route("getPropertyrent/{functionid}/{branchid}/{locationid}/{propertyid}")]
        public string getPropertyrent(string functionid,string branchid,string locationid,string propertyid)
        {

            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select fm_property_master.function_id,fm_property_master.Branch_id,fm_property_master.location_id,fm_property_master.property_id,fm_property_master.property_code,fm_property_master.property_desc,FM_PROPERTY_RENTAL_MASTER.rental_id,FM_PROPERTY_RENTAL_MASTER.rental_code  from fm_property_master inner join FM_PROPERTY_RENTAL_MASTER on FM_PROPERTY_RENTAL_MASTER.property_id=fm_property_master.property_id where 1=1";

                if (functionid !="" && functionid !="0")
                {
                    query = query + " and  fm_property_master.function_id='"+functionid+"'";
                }
                if (branchid != "" && branchid != "0")
                {
                    query = query + " and  fm_property_master.Branch_id='" + branchid + "'";
                }
                if (locationid != "" && locationid != "0")
                {
                    query = query + " and  fm_property_master.location_id='" + locationid + "'";
                }
                if (propertyid != "" && propertyid != "0")
                {
                    query = query + " and  fm_property_master.property_id='" + propertyid + "'";
                }


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
        [Route("getPropertycode/{code}/{functionid}/{branchid}/{locationid}/{usercode}")]
        public string getPropertycode(string code,string functionid,string branchid,string locationid,string usercode)
        {


            string usertype = string.Empty;
            string custid = string.Empty;
            string Logdata1 = string.Empty; 

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();



                string query2 = "";
                query2 = "select Tum_user_type from bo_user_master where tum_user_code='"+usercode+"' ";
                SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                var reader2 = cmd2.ExecuteReader();
                System.Data.DataTable results2 = new System.Data.DataTable();
                results2.Load(reader2);

                for (int i2 = 0; i2 < results2.Rows.Count; i2++)
                {
                    DataRow row1 = results2.Rows[i2];
                    usertype =row1[0].ToString();
                }


                if (usertype == "1")
                {
                    string query1 = "";
                    query1 = "select fm_property_master.function_id,fm_property_master.Branch_id,fm_property_master.location_id,fm_property_master.property_id,fm_property_master.property_code,fm_property_master.property_desc,fm_property_master.property_ownership,fm_property_master.ASSET_ID,CAMS_ASSET_MASTER.ASSET_CODE,CAMS_ASSET_MASTER.ASSET_DESCRIPTION,fm_property_master.property_building_name from fm_property_master inner join CAMS_ASSET_MASTER on CAMS_ASSET_MASTER.asset_id=fm_property_master.asset_id  left join FM_PROPERTY_RENTAL_MASTER  on FM_PROPERTY_MASTER.property_id=FM_PROPERTY_RENTAL_MASTER.property_id and FM_PROPERTY_MASTER.function_id =FM_PROPERTY_RENTAL_MASTER.function_id and FM_PROPERTY_MASTER.branch_id = FM_PROPERTY_RENTAL_MASTER.branch_id and FM_PROPERTY_MASTER.location_id = FM_PROPERTY_RENTAL_MASTER.location_id where fm_property_master.property_code like '%" + code + "%' and fm_property_master.function_id = '" + functionid + "' and fm_property_master.Branch_id = '" + branchid + "' and fm_property_master.location_id = '" + locationid + "' and FM_PROPERTY_MASTER.status = 'A' and FM_PROPERTY_RENTAL_MASTER.status != 'I' ";


                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    if (results1.Rows.Count == 0)
                    {
                        string st = "No data found";

                        Logdata1 = new JavaScriptSerializer().Serialize(st);
                    }
                    else
                    {
                        Logdata1 = DataTableToJSONWithStringBuilder(results1);
                    }

                }

                else
                {


                    string querycust = "";
                    querycust = "select Cust_ID from fm_customer_master where cust_ref='" + usercode + "' ";
                    SqlCommand cmdcust = new SqlCommand(querycust, dbConn);
                    var readercust = cmdcust.ExecuteReader();
                    System.Data.DataTable resultscust = new System.Data.DataTable();
                    resultscust.Load(readercust);

                    for (int i2 = 0; i2 < resultscust.Rows.Count; i2++)
                    {
                        DataRow row1 = resultscust.Rows[i2];
                        custid = row1[0].ToString();
                    }







                    string query = "";
                    //query = "select * from fm_property_master where property_code like '%"+ code + "%' and function_id='"+ functionid + "' and Branch_id='"+ branchid + "' and location_id='"+ locationid + "' ";

                    //query = "select fm_property_master.function_id,fm_property_master.Branch_id,fm_property_master.location_id,fm_property_master.property_id,fm_property_master.property_code,fm_property_master.property_desc,fm_property_master.property_ownership,fm_property_master.ASSET_ID,CAMS_ASSET_MASTER.ASSET_CODE,CAMS_ASSET_MASTER.ASSET_DESCRIPTION,fm_property_master.property_building_name from fm_property_master inner join CAMS_ASSET_MASTER on CAMS_ASSET_MASTER.asset_id=fm_property_master.asset_id where fm_property_master.property_code like '%" + code + "%' and fm_property_master.function_id='" + functionid + "' and fm_property_master.Branch_id='" + branchid + "' and fm_property_master.location_id='" + locationid + "' ";
                    //2DEC shy


                    query = "select fm_property_master.function_id,fm_property_master.Branch_id,fm_property_master.location_id,fm_property_master.property_id,fm_property_master.property_code,fm_property_master.property_desc,fm_property_master.property_ownership,fm_property_master.ASSET_ID,CAMS_ASSET_MASTER.ASSET_CODE,CAMS_ASSET_MASTER.ASSET_DESCRIPTION,fm_property_master.property_building_name from fm_property_master inner join CAMS_ASSET_MASTER on CAMS_ASSET_MASTER.asset_id=fm_property_master.asset_id  left join FM_PROPERTY_RENTAL_MASTER  on FM_PROPERTY_MASTER.property_id=FM_PROPERTY_RENTAL_MASTER.property_id and FM_PROPERTY_MASTER.function_id =FM_PROPERTY_RENTAL_MASTER.function_id and FM_PROPERTY_MASTER.branch_id = FM_PROPERTY_RENTAL_MASTER.branch_id and FM_PROPERTY_MASTER.location_id = FM_PROPERTY_RENTAL_MASTER.location_id where fm_property_master.property_code like '%" + code + "%' and fm_property_master.function_id = '" + functionid + "' and fm_property_master.Branch_id = '" + branchid + "' and fm_property_master.location_id = '" + locationid + "' and FM_PROPERTY_MASTER.status = 'A' and FM_PROPERTY_RENTAL_MASTER.status != 'I' and FM_PROPERTY_RENTAL_MASTER.customer_id='"+ custid + "'";


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
                }
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (Logdata1);
        }


        [HttpGet]
        [Route("getPropertycodebybranch/{code}/{functionid}/{branchid}")]
        public string getPropertycodebybranch(string code, string functionid, string branchid)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select * from fm_property_master where property_code like '%" + code + "%' and function_id='" + functionid + "' and Branch_id='" + branchid + "' ";

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
        [Route("getadditionalcharges/{strfunction}/{strbranch}/{locationid}/{propertyid}/{rentid}")]
        public string getadditionalcharges(string strfunction, string strbranch, string locationid, string propertyid, string rentid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                string sql = "MBL_FM_Get_Aditional_Charge";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION_ID", strfunction);
                cmd.Parameters.AddWithValue("@BRANCH_ID", strbranch);
                cmd.Parameters.AddWithValue("@LOCATION_ID", locationid);
                cmd.Parameters.AddWithValue("@PROPERTY_ID", propertyid);
                cmd.Parameters.AddWithValue("@RENTAL_ID", rentid);

                cmd.ExecuteNonQuery();
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();
                return Logdata1;
                //query = "SELECT FM_PROPERTY_MASTER.Branch_id,BO_FUNCTION_MASTER.FUNCTION_DESC,BO_BRANCH_MASTER.BRANCH_DESC,BO_BRANCH_LOCATION_MASTER.LOCATION_DESC, FM_PROPERTY_MASTER.PROPERTY_CODE,BO_BRANCH_LOCATION_MASTER.LOCATION_ID,FM_PROPERTY_MASTER.PROPERTY_ID,FM_PROPERTY_MASTER.PROPERTY_DESC,FM_PROPERTY_MASTER.PROPERTY_CURRENCY, FM_PROPERTY_RENTAL_MASTER.RENTAL_ID,FM_PROPERTY_RENTAL_MASTER.RENTAL_CODE FROM FM_PROPERTY_MASTER WITH (NOLOCK) INNER JOIN BO_FUNCTION_MASTER WITH (NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID =FM_PROPERTY_MASTER.FUNCTION_ID   INNER JOIN BO_BRANCH_MASTER WITH (NOLOCK) ON BO_BRANCH_MASTER.FUNCTION_ID=FM_PROPERTY_MASTER.FUNCTION_ID  AND BO_BRANCH_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID    INNER JOIN BO_BRANCH_LOCATION_MASTER WITH (NOLOCK) ON BO_BRANCH_LOCATION_MASTER.FUNCTION_ID=FM_PROPERTY_MASTER.FUNCTION_ID  AND BO_BRANCH_LOCATION_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID AND BO_BRANCH_LOCATION_MASTER.LOCATION_ID=FM_PROPERTY_MASTER.LOCATION_ID  INNER JOIN FM_PROPERTY_RENTAL_MASTER WITH (NOLOCK) ON FM_PROPERTY_RENTAL_MASTER.FUNCTION_ID =FM_PROPERTY_MASTER.FUNCTION_ID  AND FM_PROPERTY_RENTAL_MASTER.BRANCH_ID=FM_PROPERTY_MASTER.BRANCH_ID AND FM_PROPERTY_RENTAL_MASTER.LOCATION_ID =FM_PROPERTY_MASTER.LOCATION_ID  AND FM_PROPERTY_RENTAL_MASTER.PROPERTY_ID=FM_PROPERTY_MASTER.PROPERTY_ID WHERE 1=1 AND FM_PROPERTY_RENTAL_MASTER.STATUS='A' " +
                //    "and   ('" + strfunction + "' = '0' or '" + strfunction + "' is null or " +
                //    "BO_BRANCH_LOCATION_MASTER.FUNCTION_ID = '" + strfunction + "') and('" + strbranch + "' = '0' or '" + strbranch + "' is null or " +"FM_PROPERTY_MASTER.BRANCH_ID = '" + strbranch + "') and('" + locationid + "' = '0' or '" + locationid + "' is null or " +  "FM_PROPERTY_MASTER.LOCATION_ID = '" + locationid + "') and('" + propertyid + "' = '0' or '" + propertyid + "' is null or " + "FM_PROPERTY_MASTER.property_id = '" + propertyid + "') and('" + rentid + "' = '0' or '" + rentid + "' is null or " + "FM_PROPERTY_RENTAL_MASTER.rental_id = '" + rentid + "') and ISNULL(FM_PROPERTY_MASTER.property_id,0) >'0' and ISNULL " + " ( FM_PROPERTY_RENTAL_MASTER.rental_id,0) > '0' order by ISNULL(FM_PROPERTY_MASTER.property_id,0)";



                ////if (strfunction !="" && strfunction != "0")
                ////{
                ////    query = query + " AND FM_PROPERTY_MASTER.function_id='" + strfunction + "'";
                ////}
                ////if (strbranch != "" && strbranch != "0")
                ////{
                ////    query = query + " AND FM_PROPERTY_MASTER.Branch_id='" + strbranch + "'";
                ////}
                ////if (locationid != "" && locationid != "0")
                ////{
                ////    query = query + " AND FM_PROPERTY_MASTER.location_id='" + locationid + "'";
                ////}
                ////if (propertyid != "" && propertyid != "0")
                ////{
                ////    query = query + " AND FM_PROPERTY_MASTER.PROPERTY_ID='" + propertyid + "'";
                ////}
                ////if (rentid != "" && rentid != "0")
                ////{
                ////    query = query + " AND FM_PROPERTY_RENTAL_MASTER.RENTAL_ID='" + rentid + "'";
                ////}



                //SqlCommand cmd = new SqlCommand(query, dbConn);
                //var reader = cmd.ExecuteReader();
                //System.Data.DataTable results = new System.Data.DataTable();
                //results.Load(reader);
                //Logdata1 = DataTableToJSONWithStringBuilder(results);
                //dbConn.Close();

                ////var result = (new { recordsets = Logdata1 });
                //return Logdata1;
            }
        }

        //30Nov

        [HttpGet]
        [Route("Get_Additional_charge/{FUNCTION_ID}/{BRANCH_ID}/{LOCATION_ID}/{PROPERTY_ID}/{RENTAL_ID}")]
        public string Get_Additional_charge(string FUNCTION_ID, string BRANCH_ID, string LOCATION_ID, string PROPERTY_ID, string RENTAL_ID)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";

                string sql = "MBL_FM_Additional_charge";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION_ID", FUNCTION_ID);
                cmd.Parameters.AddWithValue("@BRANCH_ID", BRANCH_ID);
                cmd.Parameters.AddWithValue("@LOCATION_ID", LOCATION_ID);
                cmd.Parameters.AddWithValue("@PROPERTY_ID", PROPERTY_ID);
                cmd.Parameters.AddWithValue("@RENTAL_ID", RENTAL_ID);

                cmd.ExecuteNonQuery();
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }

                dbConn.Close();
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
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString().Trim() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString().Trim() + "\":" + "\"" + table.Rows[i][j].ToString().Trim() + "\"");
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
