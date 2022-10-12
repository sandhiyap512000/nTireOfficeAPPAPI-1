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



        //[HttpPost]
        //[Route("get_training_details")]
        //public async Task<ActionResult<Property>> get_training_details(Property data)
        //{
        //    // string struser = data.user_lower;

        //    List<Property> Logdata = new List<Property>();
        //    string Logdata1 = string.Empty;
        //    var logdata = "";
        //    var strtoken = "";
        //    // var result = "";

        //    int REF_ID;
        //    int S_NO;
        //    int wkno;    
        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {


        //        dbConn.Open();
        //        string query = "select cast(isnull(max(isnull(ASSET_REF_NO,0)),0)+1 as decimal) from CAMS_LAST_MAINTENANCE with (nolock) where 1=1  and FUNCTION_ID='"+data.functionid+"'  and BRANCH_ID='"+data.branchid + "'";

        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);

        //        for (int i = 0; i < results.Rows.Count; i++)
        //        {
        //            Property log = new Property();
        //            DataRow row = results.Rows[i];
                   
        //            REF_ID = Convert.ToInt32(row[0]);
        //            DataSet ds = new DataSet();
        //            DataRow newrow = ds.Tables[0].NewRow();
        //            newrow.BeginEdit();
        //            int count = 1;
        //            string controltext = "";

        //            newrow["Approval_Required"] = "Y";
        //            newrow["pmr_asset_reference"] = data.assetid;
                    
        //            newrow["pmr_reference"] = REF_ID;
                   
        //            newrow["Priority"] = data.Priority;
                   
        //            newrow["asset_activity_id"] = "0";
        //            newrow["pm_due_date"] = data.pm_due_date;
        //            newrow["pmr_requested_by"] = data.userid;
        //            newrow["pmr_pm_type"] = data.drpPMType;
        //            newrow["pmr_counter_reading"] = "0";
        //            newrow["pmr_details"] = data.txtDetails;
        //            newrow["division_cd"] = data.branchid;
        //            newrow["category"] = "0";
        //            newrow["act_desc"] = data.txtDetails;
        //            newrow["amd_activity_id"] = "0";
        //            newrow["ASSET_DURATION"] = "00:00";
        //            newrow["ASSET_OWNER"] = data.assetownerid;
        //            newrow["Created_by"] = data.userid;

        //           string strIpAddress = "::1";
        //            newrow["ipaddress"] = strIpAddress;
                    
        //                newrow["pmr_shutdown_request"] = "N";
                    
        //            newrow.EndEdit();
        //            ds.Tables[0].Rows.Add(newrow);


        //            string query1 = "select serial_no+1 from BO_SLNO_PARAMETER where type='URWorkOrderNumber'";

        //            SqlCommand cmd1 = new SqlCommand(query1, dbConn);
        //            var reader1 = cmd.ExecuteReader();
        //            System.Data.DataTable results1 = new System.Data.DataTable();
        //            results1.Load(reader1);

        //            for (int j = 0; j < results1.Rows.Count; j++)
        //            {
        //                Property log1 = new Property();
        //                DataRow row1 = results1.Rows[i];

        //                S_NO = Convert.ToInt32(row1[0]);

        //                string query2 = "select serial_no+1 from BO_SLNO_PARAMETER where type='URWorkOrderNumber'";

        //                SqlCommand cmd2 = new SqlCommand(query1, dbConn);
        //                var reader2 = cmd.ExecuteReader();
        //                System.Data.DataTable results2 = new System.Data.DataTable();
        //                results2.Load(reader2);

        //                for (int k = 0; k < results2.Rows.Count; k++)
        //                {
        //                    Property log2 = new Property();
        //                    DataRow row2 = results2.Rows[i];

        //                    S_NO = Convert.ToInt32(row2[0]);

        //                    string sql11 = "select prefix + '' + serial_no  + '' + isnull(suffix,'') from BO_slno_parameter where type='URWorkOrderNumber' and slno_domain='" + data.functionid + "'";

        //                    SqlCommand cmd3 = new SqlCommand(query1, dbConn);
        //                    var reader3 = cmd.ExecuteReader();
        //                    System.Data.DataTable results3 = new System.Data.DataTable();
        //                    results3.Load(reader3);


        //                    for (int a = 0; a < results3.Rows.Count; a++)
        //                    {
        //                        Property log3 = new Property();
        //                        DataRow row3 = results3.Rows[i];

        //                        wkno = Convert.ToInt32(row3[0]);

        //                        DataRow row4 = ds.Tables[0].Rows[0];

        //                        string strQry = "Exec CAMS_UIR_Save " + branch + "," + Company + "," + row4["pmr_asset_reference"] + "," + row4["pmr_reference"] + "," + row4["amd_activity_id"] + ",'" + row4["pm_due_date"] + "','" + row4["pmr_pm_type"].ToString() + "','" + row4["pmr_details"].ToString() + "','" + row4["problemdescr"] + "','" + row4["Priority"] + "','" + row4["Problem_Type"] + "','" + row4["Problem_Service_Type"] + "','" + row4["Created_by"] + "','" + row4["ipaddress"] + "','" + workod + "','" + row4["ASSET_DURATION"] + "','" + row4["ASSET_OWNER"] + "','" + row4["pmr_requested_by"] + "','','','','A','Insert'";

        //                    }


        //                    }




        //            }

        //            }
        //            Logdata1 = DataTableToJSONWithStringBuilder(results);
        //        dbConn.Close();

        //        var result = (new { recordsets = Logdata1 });
        //        return Ok(Logdata1);


        //    }
        //}




        #endregion


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
