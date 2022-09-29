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









        [HttpPost]
        [Route("assetserreqdept")]
        public async Task<ActionResult<CAMS>> assetserreqdept(CAMS data)
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
                query = "SELECT cam.ASSET_ID,cam.ASSET_USER,bp.val as dpid,cam.ASSET_CODE,bp.Text,cam.ASSET_DESCRIPTION FROM CAMS_ASSET_MASTER as cam inner join BO_PARAMETER as bp on bp.VAL=cam.ASSET_DEPARTMENT  where cam.ASSET_CODE='" + data.assetcode + "' and cam.BRANCH_ID='" + data.branchid + "' and bp.TYPE='BO_TEAM' and bp.function_id='1' and bp.status='A'";

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
        [Route("assetservreqlistview")]
        public async Task<ActionResult<CAMS>> assetservreqlistview(CAMS data)
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
                query = "select distinct CAMS_ASSET_SERVICE.REPLACEMENT_ASSET_ID, CAMS_ASSET_MASTER.asset_code as ASSET_CODE1,CAMS_ASSET_MASTER.ASSET_DESCRIPTION  ,CAMS_ASSET_SERVICE.FUNCTION_ID, HISTORY_ID,BO_BRANCH_MASTER.BRANCH_DESC as branch,CAMS_ASSET_SERVICE.ASSET_CODE,CAMS_ASSET_SERVICE.ASSET_ID,CAMS_ASSET_SERVICE.VENDOR_CODE as vendor,CONVERT(VARCHAR(10),DATE_OF_SERVICE,103) as DATE_OF_SERVICE,CONVERT(VARCHAR(10),EXPECTED_DATE_OF_DELIVERY,103) as'EXPECTED_DATE_OF_DELIVERY',convert(numeric(18,2),EXPECTED_EXPENSES) as EXPECTED_EXPENSES,case CAMS_ASSET_SERVICE.STATUS when 'N' then 'New' when 'P' then 'Pending' when 'A' then 'Approved' when 'D' then 'Denied' end as status,mode,INSURANCECOMPANY,AMOUNTINSURED,CONVERT(VARCHAR(10),WARRANTYDATE,103) as WARRANTYDATE,CAMS_ASSET_SERVICE.CREATED_ON createddate from CAMS_ASSET_SERVICE inner  join BO_BRANCH_MASTER with(nolock)on BO_BRANCH_MASTER.BRANCH_ID=CAMS_ASSET_SERVICE.BRANCH_ID and BO_BRANCH_MASTER.FUNCTION_ID=CAMS_ASSET_SERVICE.FUNCTION_ID left outer join  ERP_VENDOR_MASTER with(nolock)on ERP_VENDOR_MASTER.vendor_id=CAMS_ASSET_SERVICE.VENDOR_ID and ERP_VENDOR_MASTER.function_id=CAMS_ASSET_SERVICE.FUNCTION_ID Join CAMS_ASSET_MASTER on CAMS_ASSET_MASTER.ASSET_ID = CAMS_ASSET_SERVICE.asset_id and CAMS_ASSET_MASTER.FUNCTION_ID=CAMS_ASSET_SERVICE.FUNCTION_ID and CAMS_ASSET_MASTER.BRANCH_ID=CAMS_ASSET_SERVICE.BRANCH_ID where CAMS_ASSET_SERVICE.RELEASE in ('P','A','N','D') and CAMS_ASSET_SERVICE.MODE='S' and CAMS_ASSET_SERVICE.BRANCH_ID='" + data.branchid + "' order by CAMS_ASSET_SERVICE.CREATED_ON desc";

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
        [Route("assetservreqlistviewfilter")]
        public async Task<ActionResult<CAMS>> assetservreqlistviewfilter(CAMS data)
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
                query = "select distinct CAMS_ASSET_SERVICE.REPLACEMENT_ASSET_ID, CAMS_ASSET_MASTER.asset_code as ASSET_CODE1,CAMS_ASSET_MASTER.ASSET_DESCRIPTION  ,CAMS_ASSET_SERVICE.FUNCTION_ID, HISTORY_ID,BO_BRANCH_MASTER.BRANCH_DESC as branch,CAMS_ASSET_SERVICE.ASSET_CODE,CAMS_ASSET_SERVICE.ASSET_ID,CAMS_ASSET_SERVICE.VENDOR_CODE as vendor,CONVERT(VARCHAR(10),DATE_OF_SERVICE,103) as DATE_OF_SERVICE,CONVERT(VARCHAR(10),EXPECTED_DATE_OF_DELIVERY,103) as'EXPECTED_DATE_OF_DELIVERY',convert(numeric(18,2),EXPECTED_EXPENSES) as EXPECTED_EXPENSES,case CAMS_ASSET_SERVICE.STATUS when 'N' then 'New' when 'P' then 'Pending' when 'A' then 'Approved' when 'D' then 'Denied' end as status,mode,INSURANCECOMPANY,AMOUNTINSURED,CONVERT(VARCHAR(10),WARRANTYDATE,103) as WARRANTYDATE,CAMS_ASSET_SERVICE.CREATED_ON createddate from CAMS_ASSET_SERVICE inner  join BO_BRANCH_MASTER with(nolock)on BO_BRANCH_MASTER.BRANCH_ID=CAMS_ASSET_SERVICE.BRANCH_ID and BO_BRANCH_MASTER.FUNCTION_ID=CAMS_ASSET_SERVICE.FUNCTION_ID left outer join  ERP_VENDOR_MASTER with(nolock)on ERP_VENDOR_MASTER.vendor_id=CAMS_ASSET_SERVICE.VENDOR_ID and ERP_VENDOR_MASTER.function_id=CAMS_ASSET_SERVICE.FUNCTION_ID Join CAMS_ASSET_MASTER on CAMS_ASSET_MASTER.ASSET_ID = CAMS_ASSET_SERVICE.asset_id and CAMS_ASSET_MASTER.FUNCTION_ID=CAMS_ASSET_SERVICE.FUNCTION_ID and CAMS_ASSET_MASTER.BRANCH_ID=CAMS_ASSET_SERVICE.BRANCH_ID where CAMS_ASSET_SERVICE.RELEASE in ('P','A','N','D') and CAMS_ASSET_SERVICE.MODE='S' and CAMS_ASSET_SERVICE.BRANCH_ID='" + data.branchid + "'";

                var flag = 0;
                if (data.assetcode != null)
                {
                    if (flag == 0)
                    {
                        query = query + " AND CAMS_ASSET_MASTER.asset_code='" + data.assetcode + "'";
                        flag = 1;
                    }
                    else
                    {
                        query = query + " AND CAMS_ASSET_MASTER.asset_code='" + data.assetcode + "'";
                    }
                }
                if (data.vendorcode != null)
                {
                    if (flag == 0)
                    {
                        query = query + " AND  CAMS_ASSET_SERVICE.VENDOR_CODE='" + data.vendorcode + "'";
                        flag = 1;
                    }
                    else
                    {
                        query = query + " AND CAMS_ASSET_SERVICE.VENDOR_CODE='" + data.vendorcode + "'";
                    }
                }
                if (data.datofservice != null)
                {
                    if (flag == 0)
                    {
                        query = query + " AND  DATE_OF_SERVICE='" + data.datofservice + "'";
                         flag = 1;
                    }
                    else
                    {
                        query = query + " AND  DATE_OF_SERVICE='" + data.datofservice + "'";
                    }
                }
                query = query + "order by CAMS_ASSET_SERVICE.CREATED_ON desc";





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
        [Route("assetservicelistreplace")]
        public async Task<ActionResult<CAMS>> assetservicelistreplace(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            DataSet DS = new DataSet();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "CAMS_ASSET_getAssetsforreplcaseasset";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strstatus","A");
                cmd.Parameters.AddWithValue("@Branch",data.branchid);
                cmd.Parameters.AddWithValue("@strfunction",data.functionid);
                cmd.Parameters.AddWithValue("@assettype","0");
                cmd.Parameters.AddWithValue("@AssetReference","");
                cmd.Parameters.AddWithValue("@code",data.assetcode);
                cmd.Parameters.AddWithValue("@desc","");
                cmd.Parameters.AddWithValue("@dept","");
                cmd.Parameters.AddWithValue("@pageIndex","0");
                cmd.Parameters.AddWithValue("@pageSize","20");
                cmd.Parameters.AddWithValue("@sortExpression", "currentdate DESC");
                cmd.Parameters.AddWithValue("@alphaname","");
                cmd.Parameters.AddWithValue("@Category","0");
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
                return Ok(Logdata1);

            }
        }




        [HttpPost]
        [Route("assetdetailslist")]
        public async Task<ActionResult<CAMS>> assetdetailslist(CAMS data)
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
                query = "SELECT cam.ImageUrl,cam.asset_latitude,cam.asset_longitude,bumm.TUM_USER_NAME,casm.SUB_CATEGORY_DESC,cam.TYPE,cam.ASSET_ID,cam.ASSET_CODE,cam.ASSET_DESCRIPTION,cam.ASSET_VALUE,cam.ASSET_BRAND,cam.ASSET_MODE,cam.ASSET_PURCHASE_DATE,cam.ASSET_WARRANTY_TILL,cam.CAMS_ASSET_MANUFACTURER,cam.ASSET_RESIDUAL_VALUE,cam.ASSET_DEPRECIATION_TYPE,cam.ASSET_DEPRECIATION_PERCENTAGE,cam.ASSET_REMARKS,cam.ASSET_APPREQ_USERINITIATED,cam.ASSET_COUNTER_ENABLED,cam.ASSET_INSTALLATION_DATE,cam.ASSET_INSTALLED_BY,cam.ASSET_CERTIFICATE_ISSUED,cam.ASSET_WORKING_CONDITION,bum.TUM_USER_CODE,bop.TEXT as cnme,bop.VAL as cval FROM CAMS_ASSET_MASTER as cam INNER JOIN BO_PARAMETER as bop ON bop.VAL=cam.ASSET_CATEGORY LEFT OUTER JOIN BO_USER_MASTER as bum on bum.TUM_USER_ID=cam.ASSET_USER LEFT OUTER JOIN CAMS_ASSET_SUBCATEGORY_MASTER casm on casm.SUB_CATEGORY_ID=cam.ASSET_TYPE LEFT OUTER JOIN BO_USER_MASTER as bumm on bumm.TUM_USER_ID=cam.ASSET_OWNER_ID WHERE bop.FUNCTION_ID=1 and  bop.TYPE='InfCategory' and cam.ASSET_CODE='" + data.assetcode + "' and cam.BRANCH_ID='" + data.branchid + "'";

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
        [Route("assetcodelist")]
        public async Task<ActionResult<CAMS>> assetcodelist(CAMS data)
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
                query = "SELECT ASSET_CODE FROM CAMS_ASSET_MASTER";

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
        [Route("assetreconciliationnew")]
        public async Task<ActionResult<CAMS>> assetreconciliationnew(CAMS data)
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
                query = "INSERT INTO CAMS_PHYSICAL_INVENTORY(FUNCTION_ID,BRANCH_ID,ASSET_ID,DEP_ID,CREATED_BY,MODE,CREATED_ON,UPDATED_ON) VALUES(" + data.functionidrec + "," + data.branchidu + "," + data.assetidrec + "," + data.deprtid + "," + data.assetuser + ",'A','" + data.recrdte + "','" + data.recrdte + "')";

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
        [Route("reconassetdetail")]
        public async Task<ActionResult<CAMS>> reconassetdetail(CAMS data)
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
                query = "SELECT cam.ImageUrl,cam.asset_latitude,cam.asset_longitude,bumm.TUM_USER_NAME,casm.SUB_CATEGORY_DESC,cam.TYPE,cam.ASSET_ID,cam.ASSET_CODE,cam.ASSET_DESCRIPTION,cam.ASSET_VALUE,cam.ASSET_BRAND,cam.ASSET_MODE,cam.ASSET_PURCHASE_DATE,cam.ASSET_WARRANTY_TILL,cam.CAMS_ASSET_MANUFACTURER,cam.ASSET_RESIDUAL_VALUE,cam.ASSET_DEPRECIATION_TYPE,cam.ASSET_DEPRECIATION_PERCENTAGE,cam.ASSET_REMARKS,cam.ASSET_APPREQ_USERINITIATED,cam.ASSET_COUNTER_ENABLED,cam.ASSET_INSTALLATION_DATE,cam.ASSET_INSTALLED_BY,cam.ASSET_CERTIFICATE_ISSUED,cam.ASSET_WORKING_CONDITION,bum.TUM_USER_CODE,bop.TEXT as cnme,bop.VAL as cval FROM CAMS_ASSET_MASTER as cam INNER JOIN BO_PARAMETER as bop ON bop.VAL=cam.ASSET_CATEGORY LEFT OUTER JOIN BO_USER_MASTER as bum on bum.TUM_USER_ID=cam.ASSET_USER LEFT OUTER JOIN CAMS_ASSET_SUBCATEGORY_MASTER casm on casm.SUB_CATEGORY_ID=cam.ASSET_TYPE LEFT OUTER JOIN BO_USER_MASTER as bumm on bumm.TUM_USER_ID=cam.ASSET_OWNER_ID WHERE bop.FUNCTION_ID=1 and  bop.TYPE='InfCategory' and cam.ASSET_CODE='" + data.assetcodeu + "' and cam.BRANCH_ID='" + data.branchidu + "'";

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
        [Route("assetreq")]
        public async Task<ActionResult<CAMS>> assetreq(CAMS data)
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
                query = "SELECT cam.ASSET_ID,cam.ASSET_USER,bp.val as dpid,cam.ASSET_CODE,bp.Text,cam.ASSET_DESCRIPTION FROM CAMS_ASSET_MASTER as cam inner join BO_PARAMETER as bp on bp.VAL=cam.ASSET_DEPARTMENT  where cam.ASSET_CODE='" + data.assetcodeu + "' and cam.BRANCH_ID='" + data.branchidu + "' and bp.TYPE='BO_TEAM' and bp.function_id='1' and bp.status='A'";

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
        [Route("vendorcodelist")]
        public async Task<ActionResult<CAMS>> vendorcodelist(CAMS data)
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
                query = "SELECT Vendor_Code,vendor_id,Vendor_Name FROM ERP_VENDOR_MASTER erpm WHERE  function_id='1'";

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
        [Route("assetservicevendorlist")]
        public async Task<ActionResult<CAMS>> assetservicevendorlist(CAMS data)
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
                query = "SELECT * FROM ERP_VENDOR_MASTER erpm WHERE erpm.Vendor_Code='" + data.vendorcode + "' AND function_id='" + data.functionid + "'";

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
        [Route("assetcodereplace")]
        public async Task<ActionResult<CAMS>> assetcodereplace(CAMS data)
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
                query = "SELECT ASSET_CODE FROM CAMS_ASSET_MASTER where ASSET_USER IS NULL";

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
        [Route("assetservicelist")]
        public async Task<ActionResult<CAMS>> assetservicelist(CAMS data)
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
               // string Logdata1 = string.Empty;
                string sql = "CAMS_GETCAMSServiceDetails"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@loginUserId", loginUserId);
                cmd.Parameters.AddWithValue("@FUNCTION_ID", data.functionid);
                cmd.Parameters.AddWithValue("@BRANCH_ID", data.branchid);
                cmd.Parameters.AddWithValue("@ASSET_ID", "");
                cmd.Parameters.AddWithValue("@ASSET_CODE", data.assetcode);

                cmd.Parameters.AddWithValue("@MODE", "S");

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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("assettransfertobranch")]
        public async Task<ActionResult<CAMS>> assettransfertobranch(CAMS data)
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
                query = "(SELECT BRANCH_ID,BRANCH_DESC from BO_BRANCH_MASTER WHERE FUNCTION_ID=" + data.functionidrep + ") order by BRANCH_DESC asc";

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
        [Route("assettransferfrombranch")]
        public async Task<ActionResult<CAMS>> assettransferfrombranch(CAMS data)
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
                query = "(SELECT cam.ASSET_ID,cam.ASSET_DEPARTMENT,cam.ASSET_OWNER_ID,cam.ASSET_CATEGORY,bom.BRANCH_ID,bom.BRANCH_DESC from BO_BRANCH_MASTER as bom INNER JOIN CAMS_ASSET_MASTER as cam on cam.BRANCH_ID=bom.BRANCH_ID WHERE cam.FUNCTION_ID=" + data.functionidrep + " AND cam.ASSET_CODE='" + data.fassetcode + "')";

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
        [Route("assettransferupdatebranch")]
        public async Task<ActionResult<CAMS>> assettransferupdatebranch(CAMS data)
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
                query = "INSERT INTO CAMS_ASSET_TRANSFER_MASTER (FUNCTION_ID,CAT_ASSET_ID,CAT_FROM_BRANCH_ID,CAT_TO_BRANCH_ID,CAT_FROM_DEPARTMENT_ID,CAT_TO_DEPARTMENT_ID,CAT_FROM_ASSET_OWNER_ID,CAT_TO_ASSET_OWNER_ID,CREATED_ON,UPDATED_ON,CREATED_BY,STATUS,CAT_CATEGORY_ID,Asset_Transfer_type,Total_Assets,transfertype)VALUES('" + data.functionidrep + "','" + data.assetid + "','" + data.oldbranchid + "','" + data.ubranchid + "','" + data.assetdepart + "','0','" + data.assetownerid + "','0','" + data.dateins + "','" + data.dateins + "','" + data.createbytf + "','P','" + data.assetcategory + "','M',1,'2');select Scope_Identity()";

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
        [Route("assetlocationzone")]
        public async Task<ActionResult<CAMS>> assetlocationzone(CAMS data)
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
                query = "SELECT DISTINCT ZONE_ID,ZONE_DESC from BO_ZONE_MASTER WHERE FUNCTION_ID=" + data.functionidrep + "";

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
        [Route("assetlocationarea")]
        public async Task<ActionResult<CAMS>> assetlocationarea(CAMS data)
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
                query = "select VAL, TEXT from BO_PARAMETER where type='BO_DEPTLOCATION' and BO_PARAMETER.FUNCTION_ID=" + data.functionidrep + "";

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
        [Route("assetlocationregion")]
        public async Task<ActionResult<CAMS>> assetlocationregion(CAMS data)
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
                query = "SELECT region_id,region_desc from BO_REGION_MASTER WHERE FUNCTION_ID=" + data.functionidrep + " AND zone_id=" + data.zoneid + "";

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
        [Route("assetlocationdeparment")]
        public async Task<ActionResult<CAMS>> assetlocationdeparment(CAMS data)
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
                query = "select VAL, TEXT from BO_PARAMETER where type='BO_TEAM' and BO_PARAMETER.FUNCTION_ID=" + data.functionidrep + " and Status='A'";

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
        [Route("assetlocationbranch")]
        public async Task<ActionResult<CAMS>> assetlocationbranch(CAMS data)
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
                query = "SELECT BRANCH_ID,BRANCH_DESC from BO_BRANCH_MASTER WHERE FUNCTION_ID=" + data.functionidrep + " AND ZONE_ID=" +data.zoneid + " AND region_id=" + data.regionid + "";

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
        [Route("assetloctionfilterfinal")]
        public async Task<ActionResult<CAMS>> assetloctionfilterfinal(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                var pzoneid = data.fzoneid;
                var pregionid = data.fregionid;
                var pbranchid = data.fbranchid;
                var passetcatid = data.fassetcatid;
                var passetsubcatid = data.fassetsubcatid;

                dbConn.Open();
                string query = "";
                query = "SELECT CAMS_ASSET_MASTER.FUNCTION_ID,BO_ZONE_MASTER.ZONE_ID,BO_ZONE_MASTER.ZONE_DESC AS 'Zone',BO_REGION_MASTER.region_id,BO_REGION_MASTER.region_desc as 'Region',CAMS_ASSET_MASTER.BRANCH_ID,BO_BRANCH_MASTER.BRANCH_DESC as 'Branch',CAMS_ASSET_MASTER.ASSET_CATEGORY,CAMS_ASSET_MASTER.ASSET_TYPE,BO_PARAMETER.TEXT as 'Category',CAMS_ASSET_SUBCATEGORY_MASTER.SUB_CATEGORY_DESC as 'SubCategore',CAMS_ASSET_SUBCATEGORY_MASTER.SUB_CATEGORY_ID,COUNT(CAMS_ASSET_MASTER.ASSET_ID) as Qty, CAMS_ASSET_MASTER.STATUS as 'Status' FROM CAMS_ASSET_MASTER WITH(NOLOCK) inner join BO_FUNCTION_MASTER with(nolock) on BO_FUNCTION_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID inner join BO_PARAMETER with(nolock) on BO_PARAMETER.TYPE='INFCATEGORY' and BO_PARAMETER.VAL=CAMS_ASSET_MASTER.ASSET_CATEGORY and BO_PARAMETER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID and BO_PARAMETER.STATUS='A' inner join CAMS_ASSET_SUBCATEGORY_MASTER with(nolock) on CAMS_ASSET_SUBCATEGORY_MASTER.CATEGORY_ID=CAMS_ASSET_MASTER.ASSET_CATEGORY and CAMS_ASSET_SUBCATEGORY_MASTER.SUB_CATEGORY_ID=CAMS_ASSET_MASTER.ASSET_TYPE and CAMS_ASSET_SUBCATEGORY_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID INNER JOIN   BO_BRANCH_MASTER WITH (NOLOCK) ON BO_BRANCH_MASTER.BRANCH_ID=CAMS_ASSET_MASTER.BRANCH_ID   AND BO_BRANCH_MASTER.STATUS='A'   AND   BO_BRANCH_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID       INNER JOIN BO_ZONE_MASTER WITH (NOLOCK) ON BO_ZONE_MASTER.ZONE_ID=BO_BRANCH_MASTER.ZONE_ID AND BO_ZONE_MASTER.ZONE_STATUS='A'   AND   BO_ZONE_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID INNER JOIN BO_REGION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID=BO_REGION_MASTER.function_id AND BO_ZONE_MASTER.ZONE_ID=BO_REGION_MASTER.zone_id AND BO_BRANCH_MASTER.region_id=BO_REGION_MASTER.region_id  where 1=1 AND CAMS_ASSET_MASTER.FUNCTION_ID=" + data.functionidrep + "";


                var flag = 0;
                if (pzoneid != null && pregionid != null && pbranchid != null)
                {
                    if (flag == 0)
                    {
                         query = query + " AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " AND BO_REGION_MASTER.region_id=" + data.fregionid + "";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " AND BO_REGION_MASTER.region_id=" + data.fregionid + "";
                    }
                }
                if (pzoneid != null)
                {
                    if (flag == 0)
                    {
                         query = query + "  AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + "  AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " ";
                    }
                }
                if (pregionid != null)
                {
                    if (flag == 0)
                    {
                         query = query + "  AND BO_REGION_MASTER.region_id=" + data.fregionid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND BO_REGION_MASTER.region_id=" + data.fregionid + " ";
                    }
                }
                if (pbranchid != null)
                {
                    if (flag == 0)
                    {
                         query = query + "  AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " ";
                    }
                }
                if (passetcatid != null)
                {
                    if (flag == 0)
                    {
                         query = query + "  AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " ";
                    }
                }
                if (passetsubcatid != null)
                {
                    if (flag == 0)
                    {
                         query = query + "  AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                    }
                }
                if (passetcatid != null && passetsubcatid != null)
                {
                    if (flag == 0)
                    {
                         query = query + " AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                    }
                }
                if (pzoneid != null && pregionid != null && pbranchid != null && passetcatid != null && passetsubcatid != null)
                {
                    if (flag == 0)
                    {
                         query = query + " AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " AND BO_REGION_MASTER.region_id=" + data.fregionid + " AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                         flag = 1;
                    }
                    else
                    {
                         query = query + " AND CAMS_ASSET_MASTER.BRANCH_ID=" + data.fbranchid + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " AND BO_REGION_MASTER.region_id=" + data.fregionid + " AND CAMS_ASSET_MASTER.ASSET_CATEGORY=" + data.fassetcatid + " AND CAMS_ASSET_MASTER.ASSET_TYPE=" + data.fassetsubcatid + " ";
                    }
                }
                query = query + " group by  CAMS_ASSET_MASTER.FUNCTION_ID,BO_ZONE_MASTER.ZONE_DESC ,BO_REGION_MASTER.region_desc ,BO_BRANCH_MASTER.BRANCH_DESC,BO_PARAMETER.TEXT,CAMS_ASSET_SUBCATEGORY_MASTER.SUB_CATEGORY_DESC ,CAMS_ASSET_SUBCATEGORY_MASTER.SUB_CATEGORY_ID,CAMS_ASSET_MASTER.ASSET_MODE,BO_ZONE_MASTER.ZONE_ID,BO_REGION_MASTER.region_id,CAMS_ASSET_MASTER.BRANCH_ID,CAMS_ASSET_MASTER.ASSET_CATEGORY,CAMS_ASSET_MASTER.ASSET_TYPE,CAMS_ASSET_MASTER.STATUS ";
        // console.log(fserinslistf);





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
        [Route("assettransferfrombranchnew")]
        public async Task<ActionResult<CAMS>> assettransferfrombranchnew(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            System.Data.DataTable results2 = new System.Data.DataTable();
            string Logdata1 = string.Empty;
            int ASSET_ID;
            int CAT_HISTORY_ID;
            //var logdata = "";
            //var strtoken = "";
            //// var result = "";
            //int ASSET_USER;
            //int ASSET_CATEGORY;

            //int BRANCH_ID;
            //int FUNCTION_ID;

            string output = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select CAMS_ASSET_MASTER.ASSET_CATEGORY,CAMS_ASSET_MASTER.FUNCTION_ID,BRANCH_ID,ASSET_CODE,ASSET_ID,ASSET_SERIAL_NO,ASSET_OWNER_ID,isnull(BO_USER_MASTER.TUM_USER_CODE,'')  as'Assetownercode',ASSET_DEPARTMENT,'' as TO_DEPARTMENT_ID,''as Remarks,CAMS_PARAMETER_BRANCH.TEXT as departmentdescription,ASSET_DESCRIPTION from CAMS_ASSET_MASTER left outer join BO_USER_MASTER with(nolock)on BO_USER_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID and BO_USER_MASTER.TUM_BRANCH_ID=CAMS_ASSET_MASTER.BRANCH_ID and BO_USER_MASTER.TUM_USER_ID=CAMS_ASSET_MASTER.ASSET_OWNER_ID inner join CAMS_PARAMETER_BRANCH with(nolock)on CAMS_PARAMETER_BRANCH.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID AND  CAMS_PARAMETER_BRANCH.VAL=CAMS_ASSET_MASTER.ASSET_DEPARTMENT and CAMS_PARAMETER_BRANCH.TYPE='bo_team' where CAMS_ASSET_MASTER.STATUS='A' and CAMS_ASSET_MASTER.ASSET_CODE='" + data.fassetcode + "' and CAMS_ASSET_MASTER.BRANCH_ID='" + data.branch + "' ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {

                    CAMS log = new CAMS();
                    DataRow row = results.Rows[i];
                    //ASSET_USER = Convert.ToInt32(row[0]);
                   // ASSET_CATEGORY = Convert.ToInt32(row[1]);
                    ASSET_ID = Convert.ToInt32(row[4]);
                   // BRANCH_ID = Convert.ToInt32(row[2]);
                   // FUNCTION_ID = Convert.ToInt32(row[3]);

                    string query1 = "";
                    query1 = "SELECT  TOP(1) CAT_HISTORY_ID FROM CAMS_ASSET_TRANSFER_Detail WHERE CAT_ASSET_ID='" + ASSET_ID + "' ORDER BY CAT_HISTORY_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    for (int i1 = 0; i < results1.Rows.Count; i++)
                    {
                        DataRow row1 = results1.Rows[i];
                        CAT_HISTORY_ID = Convert.ToInt32(row1[0]);
                        string query2 = "";
                        query2 = "SELECT  *  FROM CAMS_ASSET_TRANSFER_MASTER WHERE CAT_HISTORY_ID='" + CAT_HISTORY_ID + "' and STATUS IN('A','P') ";

                        SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                        var reader2 = cmd2.ExecuteReader();
                       
                        results2.Load(reader2);
                        

                    }

                           
                }

                if (results2.Rows.Count >= 1)
                {
                    Logdata1 = "This Asset Already Transferred";
                    dbConn.Close();


                }
                else
                {
                    Logdata1 = DataTableToJSONWithStringBuilder(results2);
                    dbConn.Close();
                }

                //Logdata1 = DataTableToJSONWithStringBuilder(results2);
                //dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);
            }


        }



        [HttpPost]
        [Route("assettransferupdatebranchnew")]
        public async Task<ActionResult<CAMS>> assettransferupdatebranchnew(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            System.Data.DataTable results2 = new System.Data.DataTable();
            System.Data.DataTable results3 = new System.Data.DataTable();
            string Logdata1 = string.Empty;
            int ASSET_ID;
            int CAT_HISTORY_ID;
            int lasttransfericeidvv;
            int WF_CONFIG_ID;



            string output = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "INSERT INTO CAMS_ASSET_TRANSFER_MASTER (FUNCTION_ID,CAT_ASSET_ID,CAT_FROM_BRANCH_ID,CAT_TO_BRANCH_ID,CAT_FROM_DEPARTMENT_ID,CAT_TO_DEPARTMENT_ID,CAT_FROM_ASSET_OWNER_ID,CAT_TO_ASSET_OWNER_ID,CREATED_ON,UPDATED_ON,CREATED_BY,STATUS,CAT_CATEGORY_ID,Asset_Transfer_type,Total_Assets,transfertype)VALUES('" + data.functionidrep + "','" + data.assetid + "','" + data.oldbranchid + "','" + data.oldbranchid + "','" + data.assetdepart + "','0','" + data.assetownerid + "','0','" + data.dateins + "','" + data.dateins + "','" + data.createbytf + "','P','" + data.assetcategory + "','M',1,'1');select Scope_Identity() ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {

                    CAMS log = new CAMS();
                    DataRow row = results.Rows[i];

                    lasttransfericeidvv = Convert.ToInt32(row[0]);


                    string query1 = "";
                    query1 = "INSERT INTO CAMS_ASSET_TRANSFER_Detail (FUNCTION_ID,CAT_ASSET_ID,CAT_FROM_BRANCH_ID,CAT_TO_BRANCH_ID,CAT_FROM_DEPARTMENT_ID,CAT_TO_DEPARTMENT_ID,CAT_FROM_ASSET_OWNER_ID,CAT_TO_ASSET_OWNER_ID,CREATED_ON,UPDATED_ON,CREATED_BY,STATUS,CAT_CATEGORY_ID,CAT_HISTORY_ID,Remarks,transfertype)VALUES ('" + data.functionidrep + "','" + data.assetid + "','" + data.oldbranchid + "','" + data.oldbranchid + "','" + data.assetdepart + "','0','" + data.assetownerid + "','0','" + data.dateins + "','" + data.dateins + "','" + data.assetownerid + "','P','0','" + lasttransfericeidvv + "' ,'" + data.remarks + "','2' )";
                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);

                    string query2 = "";
                    query2 = "select WF_CONFIG_ID from BO_WORKFLOW_CONFIGURATIONS where table_name like '%CAMS_ASSET_TRANSFER_MASTER%' and status='A' and Function_ID='1' ";
                    SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                    var reader2 = cmd2.ExecuteReader();
                  
                    results2.Load(reader2);

                    for (int i2 = 0; i < results2.Rows.Count; i++)
                    {
                        DataRow row1 = results2.Rows[i];
                        WF_CONFIG_ID = Convert.ToInt32(row1[0]);

                        string query3 = "";
                        query3 = "exec usp_WF_ApprovalUsers 'CAMS_ASSET_TRANSFER_MASTER','CAT_HISTORY_ID','0','0','0','','" + lasttransfericeidvv + "','','' ,'','' ,'1' ,'" + data.createbytf + "' ,'STATUS','P' ,'" + WF_CONFIG_ID + "' ";
                        SqlCommand cmd3 = new SqlCommand(query3, dbConn);
                        var reader3 = cmd3.ExecuteReader();

                        results3.Load(reader3);
                
                    }
     

                    }

               
                Logdata1 = DataTableToJSONWithStringBuilder(results2);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);
            }

        }






        [HttpPost]
        [Route("assetreconciliationrep")]
        public async Task<ActionResult<CAMS>> assetreconciliationrep(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
           
            var functionidrep = data.functionidrep;
            var rfdate = data.rfdate;
            var rtdate = data.rtdate;
            var rassetcode = data.rassetcode;
            var rstatus = data.rstatus;
            var rbranch = data.rbranch;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "set dateformat DMY;SELECT distinct cams_PI_Reconciliation_Report.Asset_Code,cams_PI_Reconciliation_Report.Asset_description,cams_PI_Reconciliation_Report.Branch,cams_PI_Reconciliation_Report.Department,cams_PI_Reconciliation_Report.Last_PI as 'ScanDate',cams_PI_Reconciliation_Report.status as 'Status'  FROM cams_PI_Reconciliation_Report inner join CAMS_ASSET_MASTER with(nolock)on CAMS_ASSET_MASTER.ASSET_ID=cams_PI_Reconciliation_Report.ASSETID";

                var flag = 0;
                if (rfdate != "" && rtdate != "")
                {
                    if (flag == 0)
                    {
                       
                      query=query+ " AND cast(cams_PI_Reconciliation_Report.Last_PI as date) BETWEEN CAST('" + data.rfdate + "' AS DATETIME2) and CAST('" + data.rtdate + "' AS DATETIME2) ";
                        
                         flag = 1;
                    }
                    else
                    {
                        //varquery=query+ " AND CAMS_PHYSICAL_INVENTORY.UPDATED_ON BETWEEN Cast('" + data.rfdate + " 00:00:00.000' as datetime) and Cast('" + data.rtdate + " 00:00:00.000' as datetime)";
                      query=query+ " AND cast(cams_PI_Reconciliation_Report.Last_PI as date) BETWEEN '" + data.rfdate + "' and '" + data.rtdate + "'";
                        // console.log(reconrep);
                    }
                }

                if (rassetcode != "")
                {
                    if (flag == 0)
                    {
                      query=query+ " AND cams_PI_Reconciliation_Report.Asset_Code='" + data.rassetcode + "'";
                         flag = 1;
                    }
                    else
                    {
                      query=query+ "AND cams_PI_Reconciliation_Report.Asset_Code='" + data.rassetcode + "'";
                    }
                }

                query = query + "order by ScanDate desc";



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
        [Route("assetdepatmentwiselocation")]
        public async Task<ActionResult<CAMS>> assetdepatmentwiselocation(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            var pzoneid = data.fzoneid;
            var pregionid = data.fregionid;
            var pbranchid = data.fbranchid;
           
            var depatid = data.depatmentid;
            var locatid = data.locationidd;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select DISTINCT  BO_FUNCTION_MASTER.FUNCTION_DESC as 'Function', BRANCH_DESC Branch ,BO_PARAMETER1.TEXT Department,BO_PARAMETER.TEXT Location, count (Asset_id) AssetCount   from CAMS_ASSET_MASTER  Join BO_PARAMETER on BO_PARAMETER.TYPE ='BO_DEPTLOCATION'  and CAMS_ASSET_MASTER.FUNCTION_ID =BO_PARAMETER.FUNCTION_ID and BO_PARAMETER.VAL=ASSET_LocationId Join Bo_branch_master on Bo_branch_master.FUNCTION_ID=CAMS_ASSET_MASTER.Function_id and CAMS_ASSET_MASTER.BRANCH_ID=Bo_branch_master.BRANCH_ID left Join BO_PARAMETER as BO_PARAMETER1 on BO_PARAMETER1.TYPE='BO_TEAM' and BO_PARAMETER1.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID and CAMS_ASSET_MASTER.BRANCH_ID=Bo_branch_master.BRANCH_ID and BO_PARAMETER1.val=CAMS_ASSET_MASTER.ASSET_DEPARTMENT and BO_PARAMETER1.STATUS='A' inner join BO_FUNCTION_MASTER on BO_FUNCTION_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.FUNCTION_ID INNER JOIN BO_ZONE_MASTER ON BO_ZONE_MASTER.ZONE_ID=BO_BRANCH_MASTER.ZONE_ID AND ZONE_STATUS='A'   AND   BO_ZONE_MASTER.FUNCTION_ID=CAMS_ASSET_MASTER.function_id where  CAMS_ASSET_MASTER.STATUS='A'";

                var flag = 0;
                if (pzoneid != null)
                {
                    if (flag == 0)
                    {
                        query = query + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " ";
                         flag = 1;
                    }
                    else
                    {
                        query = query + " AND BO_ZONE_MASTER.ZONE_ID=" + data.fzoneid + " ";
                    }
                }
                if (pbranchid != null)
                {
                    if (flag == 0)
                    {
                        query = query + " AND Bo_branch_master.BRANCH_ID=" + data.fbranchid + " ";
                         flag = 1;
                    }
                    else
                    {
                        query = query + " AND Bo_branch_master.BRANCH_ID=" + data.fbranchid + "  ";
                    }
                }
                if (depatid != null )
                {
                    if (flag == 0)
                    {
                        query = query + " AND ASSET_DEPARTMENT=" + data.depatmentid + " ";
                         flag = 1;
                    }
                    else
                    {
                        query = query + " AND ASSET_DEPARTMENT=" + data.depatmentid + "  ";
                    }
                }
                if (locatid != null )
                {
                    if (flag == 0)
                    {
                        query = query + " AND ASSET_LocationId=" + data.locationidd + " ";
                         flag = 1;
                    }
                    else
                    {
                        query = query + " AND ASSET_LocationId=" + data.locationidd + "  ";
                    }
                }
                query = query + " Group by BO_FUNCTION_MASTER.FUNCTION_DESC , BRANCH_DESC ,BO_PARAMETER1.TEXT ,BO_PARAMETER.TEXT ";
             



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
        [Route("Pendingsearchs11")]
        public string Pendingsearchs11(string strfunction, string branch, string fdate, string tdate, string Status, string strUserId, string UserType, string drpcategory, string drptype, string TASKTYPE, string AssetCode)//, string PrDesc, string PrCode string loginUserId,, string UserType
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
                    if (strUserId.ToString() == "0" || strUserId.ToString() == "" || strUserId.ToString() == string.Empty || strUserId.ToString() == null)
                    {
                        strUserId = "0";
                    }
                    if (branch.ToString() == "0" || branch.ToString() == "" || branch.ToString() == string.Empty || branch.ToString() == null)
                    {
                        branch = "0";
                    }
                    if (fdate.ToString() == "0" || fdate.ToString() == "" || fdate.ToString() == string.Empty || fdate.ToString() == "null")
                    {
                        fdate = null;
                    }
                    if (tdate.ToString() == "0" || tdate.ToString() == "" || tdate.ToString() == string.Empty || tdate.ToString() == "null")
                    {
                        tdate = null;
                    }
                    if (Status.ToString() == "0" || Status.ToString() == "" || Status.ToString() == string.Empty || Status.ToString() == "null")
                    {
                        Status = null;
                    }
                    if (drpcategory.ToString() == "0" || drpcategory.ToString() == "" || drpcategory.ToString() == string.Empty || drpcategory.ToString() == "null")
                    {
                        drpcategory = null;
                    }
                    if (drptype.ToString() == "0" || drptype.ToString() == "" || drptype.ToString() == string.Empty || drptype.ToString() == "null")
                    {
                        drptype =null;
                    }
                    if (TASKTYPE.ToString() == "0" || TASKTYPE.ToString() == "" || TASKTYPE.ToString() == string.Empty || TASKTYPE.ToString() == "null")
                    {
                        TASKTYPE = null;
                    }
                    if (AssetCode.ToString() == "0" || AssetCode.ToString() == "" || AssetCode.ToString() == string.Empty || AssetCode.ToString() == "null")
                    {
                        AssetCode = null;
                    }

                    string Logdata1 = string.Empty;
                string sql = "CAMS__SEARCHS_PENDINGDETAIL"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@loginUserId", loginUserId);
                cmd.Parameters.AddWithValue("@strfunction", strfunction);
                cmd.Parameters.AddWithValue("@branch", branch);
                cmd.Parameters.AddWithValue("@fdate", fdate);
                cmd.Parameters.AddWithValue("@tdate", tdate);
               
                cmd.Parameters.AddWithValue("@Status", Status);
               
                cmd.Parameters.AddWithValue("@strUserId", strUserId);
                cmd.Parameters.AddWithValue("@UserType", UserType);
                cmd.Parameters.AddWithValue("@drpcategory", drpcategory);
                cmd.Parameters.AddWithValue("@drptype", drptype);

                cmd.Parameters.AddWithValue("@TASKTYPE", TASKTYPE);
               
                cmd.Parameters.AddWithValue("@AssetCode", AssetCode);
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





        [HttpGet]
        [Route("CAMSPENDING_COMPLTED_SEARCH")]
        public string CAMSPENDING_COMPLTED_SEARCH(string strfunction, string branch, string fdate, string tdate, string Status, string drpcategory, string drptype, string TASKTYPE)//, string PrDesc, string PrCode string loginUserId,, string UserType
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
                    if (fdate.ToString() == "0" || fdate.ToString() == "" || fdate.ToString() == string.Empty || fdate.ToString() == "null")
                    {
                        fdate =null;
                    }
                    if (tdate.ToString() == "0" || tdate.ToString() == "" || tdate.ToString() == string.Empty || tdate.ToString() == "null")
                    {
                        tdate = null;
                    }
                    if (Status.ToString() == "0" || Status.ToString() == "" || Status.ToString() == string.Empty || Status.ToString() == "null")
                    {
                        Status = null;
                    }
                    if (drpcategory.ToString() == "0" || drpcategory.ToString() == "" || drpcategory.ToString() == string.Empty || drpcategory.ToString() == "null")
                    {
                        drpcategory = null;
                    }
                    if (drptype.ToString() == "0" || drptype.ToString() == "" || drptype.ToString() == string.Empty || drptype.ToString() == "null")
                    {
                        drptype = null;
                    }
                    if (TASKTYPE.ToString() == "0" || TASKTYPE.ToString() == "" || TASKTYPE.ToString() == string.Empty || TASKTYPE.ToString() == "null")
                    {
                        TASKTYPE = null;
                    }

                    string Logdata1 = string.Empty;
                    string sql = "CAMS_PENDINGDETAIL_COMPLETED_SEARCH"; //"CAMS_PENDINGDETAIL_SEARCHS1";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@loginUserId", loginUserId);
                    cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@fdate", fdate);
                    cmd.Parameters.AddWithValue("@tdate", tdate);

                    cmd.Parameters.AddWithValue("@Status", Status);

                  //  cmd.Parameters.AddWithValue("@strUserId", strUserId);
                  //  cmd.Parameters.AddWithValue("@UserType", UserType);
                    cmd.Parameters.AddWithValue("@drpcategory", drpcategory);
                    cmd.Parameters.AddWithValue("@drptype", drptype);

                    cmd.Parameters.AddWithValue("@TASKTYPE", TASKTYPE);

                    // cmd.Parameters.AddWithValue("@AssetCode", AssetCode);
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






        [HttpPost]
        [Route("assetlocationcategory")]
        public async Task<ActionResult<CAMS>> assetlocationcategory(CAMS data)
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
                query = "SELECT  FUNCTION_ID, VAL, TEXT  FROM  BO_PARAMETER with(nolock) where 1=1  and BO_PARAMETER.FUNCTION_ID=" + data.functionidrep + " and TYPE='InfCategory' and status='A'  ORDER BY TEXT asc ";

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
        [Route("assetlocationsubcategory")]
        public async Task<ActionResult<CAMS>> assetlocationsubcategory(CAMS data)
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
                query = "SELECT SUB_CATEGORY_ID,SUB_CATEGORY_DESC FROM CAMS_ASSET_SUBCATEGORY_MASTER WHERE FUNCTION_ID=" + data.functionidrep + " AND CATEGORY_ID=" + data.categoryid + " order by SUB_CATEGORY_DESC ASC";

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
        [Route("refrencemax")]
        public async Task<ActionResult<CAMS>> refrencemax(CAMS data)
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
                query = "select cast(isnull(max(isnull(ASSET_REF_NO,0)),0)+1 as decimal) as refnum from CAMS_LAST_MAINTENANCE with (nolock) where 1=1";

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
        [Route("assetreqcategory")]
        public async Task<ActionResult<CAMS>> assetreqcategory(CAMS data)
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
                query = "SELECT ASSET_TYPE,ASSET_USER,ASSET_CATEGORY,ASSET_ID,BRANCH_ID,FUNCTION_ID FROM CAMS_ASSET_MASTER  where ASSET_CODE='" + data.assetcodeu + "' and BRANCH_ID='" + data.branchidu + "' ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    CAMS log = new CAMS();
                    DataRow row = results.Rows[i];
                   int ASSET_TYPE = Convert.ToInt32(row[0]);
                   int ASSET_CATEGORY = Convert.ToInt32(row[2]);
                  
                    string query1 = "";
                    query1 = "SELECT cur.ISSUEID,cur.ISSUEDESCRIPTION FROM  CAMS_USERREQUEST_REASON as cur where CATEGORY_ID=" + ASSET_CATEGORY + " and SUB_CATEGORY_ID=" + ASSET_TYPE + " and FUNCTION_ID=1";
                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                   
                    results1.Load(reader1);
                }





                    Logdata1 = DataTableToJSONWithStringBuilder(results1);
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("assetreqinsert")]
        public async Task<ActionResult<CAMS>> assetreqinsert(CAMS data)
        {
            // string struser = data.user_lower;

            List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            int ASSET_USER;
            int ASSET_CATEGORY;
            int ASSET_ID;
            int BRANCH_ID;
            int FUNCTION_ID;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT ASSET_USER,ASSET_CATEGORY,ASSET_ID,BRANCH_ID,FUNCTION_ID FROM CAMS_ASSET_MASTER  where ASSET_CODE='" + data.assetcoder + "' and BRANCH_ID='" + data.branchidr + "'  ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {

                    CAMS log = new CAMS();
                    DataRow row = results.Rows[i];
                     ASSET_USER = Convert.ToInt32(row[0]);
                    ASSET_CATEGORY = Convert.ToInt32(row[1]);
                    ASSET_ID= Convert.ToInt32(row[2]);
                    BRANCH_ID = Convert.ToInt32(row[2]);
                    FUNCTION_ID= Convert.ToInt32(row[3]);

                    string query1 = "";
                    query1 = "INSERT INTO CAMS_LAST_MAINTENANCE(BRANCH_ID,FUNCTION_ID,ASSET_ASSET_ID,   ASSET_STATUS,  ASSET_FREQUENCY_MODE,  ASSET_ACTIVITY_DESC,  priority,ASSET_ACTIVITY_ID,ASSET_NEXT_MAINTENANCE,ASSET_REF_NO,CREATED_ON) VALUES (" + BRANCH_ID + "," + FUNCTION_ID + ",'" + ASSET_ID + "', 'P',  'U',  '" + data.reqdetail + "',  " + data.priority + ",0,'" + data.reqdate1 + "','" + data.refmaxno + "'," + data.reqdate + ")";
                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                   
                }

                string query2 = "";
                query2= "select (prefix + '' + serial_no  + '' + isnull(suffix,'')) as wkno from BO_slno_parameter where type='URWorkOrderNumber' and slno_domain='1' ";

                SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                var reader2 = cmd2.ExecuteReader();
                System.Data.DataTable results2 = new System.Data.DataTable();
                results2.Load(reader2);
                for (int i = 0; i < results2.Rows.Count; i++)
                {

                    CAMS log = new CAMS();
                    DataRow row = results2.Rows[i];
                    int wkno = Convert.ToInt32(row[0]);
                    for (int i1 = 0; i1 < results.Rows.Count; i1++)
                    {


                        DataRow row1 = results.Rows[i];
                        ASSET_USER = Convert.ToInt32(row1[0]);
                        ASSET_CATEGORY = Convert.ToInt32(row1[1]);
                        ASSET_ID = Convert.ToInt32(row1[2]);
                        BRANCH_ID = Convert.ToInt32(row1[2]);
                        FUNCTION_ID = Convert.ToInt32(row1[3]);



                        string query3 = "";
                        query3 = "INSERT INTO CAMS_ASSET_REQUEST(BRANCH_ID,FUNCTION_ID,ASSET_ID,ASSET_ACTIVITY_ID,ASSET_DETAILS,ASSET_DUE_DATE,ASSET_STATUS,ASSET_USER_ID,ASSET_PMR_REFERENCE,ASSET_CATEGORY,ASSET_PM_TYPE,ASSET_REQUESTED_BY,ASSET_DURATION,ASSET_WORKORDNO) VALUES (" + BRANCH_ID + "," + FUNCTION_ID + ",'" + ASSET_ID + "',0,'" + data.reqdetail + "','" + data.reqdate1 + "','A'," + ASSET_USER + ",'" + data.refmaxno + "'," + ASSET_CATEGORY + ",'U','" + data.assetreqid + "','00:00','" + wkno + "')";
                        SqlCommand cmd3 = new SqlCommand(query3, dbConn);
                        var reader3 = cmd3.ExecuteReader();



                        string query4 = "";
                        query4 = "Update BO_SLNO_PARAMETER set serial_no = serial_no + 1 where type = N'URWorkOrderNumber'";
                        SqlCommand cmd4 = new SqlCommand(query4, dbConn);
                        var reader4 = cmd4.ExecuteReader();


                        string query5 = "";
                        query5 = "INSERT INTO CAMS_TASKS_ASSIGNED(FUNCTION_ID, CAMS_USER_ID, created_on) VALUES(" + FUNCTION_ID + ", " + ASSET_USER + ", '" + data.reqdate + "')";
                        SqlCommand cmd5 = new SqlCommand(query5, dbConn);
                        var reader5 = cmd5.ExecuteReader();


                    }
                }


                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    dbConn.Close();

                    var result = (new { recordsets = Logdata1 });
                    return Ok(Logdata1);
                }

            
        }


        [HttpPost]
        [Route("assetservicecategory")]
        public async Task<ActionResult<CAMS>> assetservicecategory(CAMS data)
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
                query = "SELECT DISTINCT VAL AS VALUE,TEXT AS TEXT,TYPE FROM BO_PARAMETER WITH (NOLOCK) WHERE STATUS = 'A' AND TYPE like 'ServiceCategory%' AND FUNCTION_ID='" + data.functionid + "' ";

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
