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




        [HttpGet]
        [Route("Pendingsearchs11")]
        public string Pendingsearchs11(string strfunction, string branch, string fdate, string tdate, string Status, string strUserId, string UserType, string drpcategory, string drptype, string TASKTYPE, string AssetCode)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
               {
                    dbConn.Open();

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

                    DataTable dt = new DataTable();
                apd.Fill(dt);

                return JsonConvert.SerializeObject(dt, Formatting.Indented);








                //DataSet ds1 = new DataSet();
                //    string Logdata1 = string.Empty;
                //    DataSet DS = new DataSet();
                //    using (SqlConnection dbConn = new SqlConnection(strconn))
                //    {
                //        dbConn.Open();
                //        string sql = "CAMS__SEARCHS_PENDINGDETAIL";
                //        SqlCommand cmd = new SqlCommand(sql, dbConn);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        // cmd.Parameters.AddWithValue("@loginUserId", loginUserId);
                //        cmd.Parameters.AddWithValue("@strfunction", strfunction);
                //        cmd.Parameters.AddWithValue("@branch", branch);
                //        cmd.Parameters.AddWithValue("@fdate", fdate);
                //        cmd.Parameters.AddWithValue("@tdate", tdate);
                //        cmd.Parameters.AddWithValue("@Status", Status);
                //        cmd.Parameters.AddWithValue("@strUserId", strUserId);
                //        cmd.Parameters.AddWithValue("@UserType", UserType);
                //        cmd.Parameters.AddWithValue("@drpcategory", drpcategory);
                //        cmd.Parameters.AddWithValue("@drptype", drptype);
                //    cmd.Parameters.AddWithValue("@TASKTYPE", TASKTYPE);
                //          cmd.Parameters.AddWithValue("@AssetCode", AssetCode);
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        da.Fill(ds1);

                //        var reader = cmd.ExecuteReader();
                //        System.Data.DataTable results = new System.Data.DataTable();
                //        results.Load(reader);
                //        //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //        for (int i = 0; i < results.Rows.Count; i++)
                //        {
                //            DataRow row = results.Rows[i];
                //            Logdata1 = DataTableToJSONWithStringBuilder(results);
                //        }
                //        return Logdata1;

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
        public string CAMSPENDING_COMPLTED_SEARCH(string strfunction, string branch, string fdate, string tdate, string Status, string strUserId, string UserType, string drpcategory, string drptype, string TASKTYPE)//, string PrDesc, string PrCode string loginUserId,, string UserType
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

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

                    cmd.Parameters.AddWithValue("@strUserId", strUserId);
                    cmd.Parameters.AddWithValue("@UserType", UserType);
                    cmd.Parameters.AddWithValue("@drpcategory", drpcategory);
                    cmd.Parameters.AddWithValue("@drptype", drptype);

                    cmd.Parameters.AddWithValue("@TASKTYPE", TASKTYPE);

                   // cmd.Parameters.AddWithValue("@AssetCode", AssetCode);

                    DataTable dt = new DataTable();
                    apd.Fill(dt);

                    return JsonConvert.SerializeObject(dt, Formatting.Indented);








                    //DataSet ds1 = new DataSet();
                    //    string Logdata1 = string.Empty;
                    //    DataSet DS = new DataSet();
                    //    using (SqlConnection dbConn = new SqlConnection(strconn))
                    //    {
                    //        dbConn.Open();
                    //        string sql = "CAMS__SEARCHS_PENDINGDETAIL";
                    //        SqlCommand cmd = new SqlCommand(sql, dbConn);
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        // cmd.Parameters.AddWithValue("@loginUserId", loginUserId);
                    //        cmd.Parameters.AddWithValue("@strfunction", strfunction);
                    //        cmd.Parameters.AddWithValue("@branch", branch);
                    //        cmd.Parameters.AddWithValue("@fdate", fdate);
                    //        cmd.Parameters.AddWithValue("@tdate", tdate);
                    //        cmd.Parameters.AddWithValue("@Status", Status);
                    //        cmd.Parameters.AddWithValue("@strUserId", strUserId);
                    //        cmd.Parameters.AddWithValue("@UserType", UserType);
                    //        cmd.Parameters.AddWithValue("@drpcategory", drpcategory);
                    //        cmd.Parameters.AddWithValue("@drptype", drptype);
                    //    cmd.Parameters.AddWithValue("@TASKTYPE", TASKTYPE);
                    //          cmd.Parameters.AddWithValue("@AssetCode", AssetCode);
                    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //        da.Fill(ds1);

                    //        var reader = cmd.ExecuteReader();
                    //        System.Data.DataTable results = new System.Data.DataTable();
                    //        results.Load(reader);
                    //        //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    //        for (int i = 0; i < results.Rows.Count; i++)
                    //        {
                    //            DataRow row = results.Rows[i];
                    //            Logdata1 = DataTableToJSONWithStringBuilder(results);
                    //        }
                    //        return Logdata1;

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
