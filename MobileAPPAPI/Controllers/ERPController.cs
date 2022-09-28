using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Nancy.Json;
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
    public class ERPController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();

        //node source starts


        [HttpPost]
        [Route("list_po_order")]
        public async Task<ActionResult<ERP>> list_po_order(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_PO_ORDER_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                cmd.Parameters.AddWithValue("@PONUMBER", data.ponumber);
                cmd.Parameters.AddWithValue("@VENDORCODE", data.vendorcode);
                cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                cmd.Parameters.AddWithValue("@TODATE", data.todate);
                cmd.Parameters.AddWithValue("@STATUS", data.status);
                cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                cmd.Parameters.AddWithValue("@usertype", data.usertype);
                cmd.Parameters.AddWithValue("@userid", data.userid);
                cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex);
                cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize);
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);
                cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }







        [HttpPost]
        [Route("listvendor_items")]
        public async Task<ActionResult<ERP>> listvendor_items(ERP data)
        {
           // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select Vendor_Code,vendor_id,Vendor_Name from ERP_VENDOR_MASTER where status='A'";

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
        [Route("getAllBranches")]
        public async Task<ActionResult<ERP>> getAllBranches(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select BRANCH_CODE, BRANCH_ID  from BO_BRANCH_MASTER where STATUS='A'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { recordsets = logdata });
                return Ok(Logdata1);
            }
        }




        [HttpPost]
        [Route("vendor_detail")]
        public async Task<ActionResult<ERP>> vendor_detail(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from PROCUREMENTVENDORMASTER where Code='" + data.code+"'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //for (int i = 0; i < results.Rows.Count; i++)
                //{
                //    DataRow row = results.Rows[i];
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //    logdata = DataTableToJSONWithStringBuilder(results);

                //    dbConn.Close();
                //}
                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);

           
            }
        }



        [HttpPost]
        [Route("vendors_quotations")]
        public async Task<ActionResult<ERP>> vendors_quotations(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_VENDORQUOTATION_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                cmd.Parameters.AddWithValue("@FROMDATE", "");
                cmd.Parameters.AddWithValue("@TODATE", "");
                cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                cmd.Parameters.AddWithValue("@VENDORID", "");
                cmd.Parameters.AddWithValue("@QUOTEREF", "");
                cmd.Parameters.AddWithValue("@STATUS", 0);
                cmd.Parameters.AddWithValue("@PAGEINDEX", 0);
                cmd.Parameters.AddWithValue("@PAGESIZE", 500);
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", "item_id");
                cmd.Parameters.AddWithValue("@ALPHANAME", "");
                cmd.Parameters.AddWithValue("@MODE", 2);
                cmd.Parameters.AddWithValue("@VENDORCODE", "");
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }






        [HttpPost]
        [Route("get_vendorid")]
        public async Task<ActionResult<ERP>> get_vendorid(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            string vendorid = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "ERP_VALIDATE_VENDORID";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FUNCTIOND", data.FUNCTION_ID);
                cmd.Parameters.AddWithValue("@USERCODE", data.usercode);
               
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    vendorid =row["vendor_id"].ToString();
                 
                }

                string query = "";
                query = "select * from ERP_VENDOR_MASTER where vendor_id='" + vendorid + "'";

                SqlCommand cmd1 = new SqlCommand(query, dbConn);
                var reader1 = cmd.ExecuteReader();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results1.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();

               
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }



        [HttpPost]
        [Route("vendor_category")]
        public async Task<ActionResult<ERP>> vendor_category(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'VendorCategory' and Status = 'A'";

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
        [Route("company_category")]
        public async Task<ActionResult<ERP>> company_category(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'PROC_CompanyCategory' and Status = 'A'";

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
        [Route("company_type")]
        public async Task<ActionResult<ERP>> company_type(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'BusinessType' and Status = 'A'";

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
        [Route("vendor_country")]
        public async Task<ActionResult<ERP>> vendor_country(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where TYPE = 'PROC_Country' and Status = 'A'";

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
        [Route("vendor_region")]
        public async Task<ActionResult<ERP>> vendor_region(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT region_desc AS TEXT,region_id AS VALUE FROM BO_REGION_MASTER WITH(NOLOCK) WHERE STATUS='A'";

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
        [Route("list_vendors_items")]
        public async Task<ActionResult<ERP>> list_vendors_items(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select vendor_id from ERP_VENDOR_MASTER where Vendor_Code='" + data.vendorcode+"'";

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

        //node source end



        //new method starts

        //PRS search

        [HttpPost]
        [Route("get_PRS_search")]
        public async Task<ActionResult<ERP>> get_PRS_search(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
        
            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    if (data.functionid.ToString() == "0" || data.functionid.ToString() == "" || data.functionid.ToString() == string.Empty || data.functionid.ToString() == null)
                    {
                        data.functionid = "0";
                    }
                    if (data.branchid.ToString() == "0" || data.branchid.ToString() == "" || data.branchid.ToString() == string.Empty || data.branchid.ToString() == null)
                    {
                        data.branchid = "0";
                    }
                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "0";
                    }
                    if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    {
                        data.fromdate = "0";
                    }
                    if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    {
                        data.todate = "0";
                    }
                    if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    {
                        data.reuestdate = "0";
                    }
                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }
                    if (data.currentstatus.ToString() == "0" || data.currentstatus.ToString() == "" || data.currentstatus.ToString() == string.Empty || data.currentstatus.ToString() == null)
                    {
                        data.currentstatus = "0";
                    }
                    if (data.reqtype.ToString() == "0" || data.reqtype.ToString() == "" || data.reqtype.ToString() == string.Empty || data.reqtype.ToString() == null)
                    {
                        data.reqtype = "0";
                    }
                    if (data.requser.ToString() == "0" || data.requser.ToString() == "" || data.requser.ToString() == string.Empty || data.requser.ToString() == null)
                    {
                        data.requser = "0";
                    }
                    if (data.usertype.ToString() == "0" || data.usertype.ToString() == "" || data.usertype.ToString() == string.Empty || data.usertype.ToString() == null)
                    {
                        data.usertype = "0";
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "0";
                    }
                    if (data.qutype.ToString() == "0" || data.qutype.ToString() == "" || data.qutype.ToString() == string.Empty || data.qutype.ToString() == null)
                    {
                        data.qutype = "0";
                    }
                    if (data.prsref.ToString() == "0" || data.prsref.ToString() == "" || data.prsref.ToString() == string.Empty || data.prsref.ToString() == null)
                    {
                        data.prsref = "0";
                    }
                    if (data.menuid.ToString() == "0" || data.menuid.ToString() == "" || data.menuid.ToString() == string.Empty || data.menuid.ToString() == null)
                    {
                        data.menuid = "0";
                    }
                    if (data.userid.ToString() == "0" || data.userid.ToString() == "" || data.userid.ToString() == string.Empty || data.userid.ToString() == null)
                    {
                        data.userid = "0";
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    string sql = "MBL_erp_prs_getdetails";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@REQUESTDATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@CURRENTSTATUS", data.currentstatus);
                    cmd.Parameters.AddWithValue("@REQTYPE", data.reqtype);

                    cmd.Parameters.AddWithValue("@MENUID", data.menuid);
                    cmd.Parameters.AddWithValue("@USERTYPE", data.usertype);
                    cmd.Parameters.AddWithValue("@REQUSER", data.requser);
                    cmd.Parameters.AddWithValue("@USERID", data.userid);

                    cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                    cmd.Parameters.AddWithValue("@QUTYPE", data.qutype);

                    cmd.Parameters.AddWithValue("@prsref", data.prsref);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize1);
                    cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        logdata = DataTableToJSONWithStringBuilder(results);

                        dbConn.Close();
                    }
                    var result = (new { logdata });
                    return Ok(Logdata1);
                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }







        //PRS Insert and Update

        [HttpPost]
        [Route("get_PRS_Insert_Update")]
        public async Task<ActionResult<ERP>> get_PRS_Insert_Update(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var stroutput = "";
            string strprscode = "";
            var strprsid = "";
            // var result = "";

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {

                    if (data.functionid.ToString() == "0" || data.functionid.ToString() == "" || data.functionid.ToString() == string.Empty || data.functionid.ToString() == null)
                    {
                        data.functionid = "0";
                    }
                    //if (data.prsid.ToString() == "0" || data.prsid.ToString() == "" || data.prsid.ToString() == string.Empty || data.prsid.ToString() == null)
                    //{
                    //    data.prsid = "0";
                    //}
                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }
                    if (data.createdby.ToString() == "0" || data.createdby.ToString() == "" || data.createdby.ToString() == string.Empty || data.createdby.ToString() == null)
                    {
                        data.createdby = "0";
                    }
                    if (data.ipaddress.ToString() == "0" || data.ipaddress.ToString() == "" || data.ipaddress.ToString() == string.Empty || data.ipaddress.ToString() == null)
                    {
                        data.ipaddress = "0";
                    }
                    if (data.reasonpurchase.ToString() == "0" || data.reasonpurchase.ToString() == "" || data.reasonpurchase.ToString() == string.Empty || data.reasonpurchase.ToString() == null)
                    {
                        data.reasonpurchase = "0";
                    }
                    if (data.netamount.ToString() == "0" || data.netamount.ToString() == "" || data.netamount.ToString() == string.Empty || data.netamount.ToString() == null)
                    {
                        data.netamount = "0";
                    }

                    if (data.currency.ToString() == "0" || data.currency.ToString() == "" || data.currency.ToString() == string.Empty || data.currency.ToString() == null)
                    {
                        data.currency = "0";
                    }
                    if (data.requestcomments.ToString() == "0" || data.requestcomments.ToString() == "" || data.requestcomments.ToString() == string.Empty || data.requestcomments.ToString() == null)
                    {
                        data.requestcomments = "0";
                    }
                    if (data.isbid.ToString() == "0" || data.isbid.ToString() == "" || data.isbid.ToString() == string.Empty || data.isbid.ToString() == null)
                    {
                        data.isbid = "0";
                    }
                    if (data.prstype.ToString() == "0" || data.prstype.ToString() == "" || data.prstype.ToString() == string.Empty || data.prstype.ToString() == null)
                    {
                        data.prstype = "0";
                    }
                    if (data.branchid.ToString() == "0" || data.branchid.ToString() == "" || data.branchid.ToString() == string.Empty || data.branchid.ToString() == null)
                    {
                        data.branchid = "0";
                    }
                    if (data.prsref.ToString() == "0" || data.prsref.ToString() == "" || data.prsref.ToString() == string.Empty || data.prsref.ToString() == null)
                    {
                        data.prsref = "0";
                    }
                    if (data.userid.ToString() == "0" || data.userid.ToString() == "" || data.userid.ToString() == string.Empty || data.userid.ToString() == null)
                    {
                        data.userid = "0";
                    }
                    if (data.requestby.ToString() == "0" || data.requestby.ToString() == "" || data.requestby.ToString() == string.Empty || data.requestby.ToString() == null)
                    {
                        data.requestby = "0";
                    }
                    if (data.requestdate.ToString() == "0" || data.requestdate.ToString() == "" || data.requestdate.ToString() == string.Empty || data.requestdate.ToString() == null)
                    {
                        data.requestdate = "0";
                    }

                    if (data.requettype.ToString() == "0" || data.requettype.ToString() == "" || data.requettype.ToString() == string.Empty || data.requettype.ToString() == null)
                    {
                        data.requettype = "0";
                    }


                    if (data.issinglevendor.ToString() == "0" || data.issinglevendor.ToString() == "" || data.issinglevendor.ToString() == string.Empty || data.issinglevendor.ToString() == null)
                    {
                        data.issinglevendor = "0";
                    }

                    if (data.orderpriority.ToString() == "0" || data.orderpriority.ToString() == "" || data.orderpriority.ToString() == string.Empty || data.orderpriority.ToString() == null)
                    {
                        data.orderpriority = "0";
                    }

                    dbConn.Open();
                    if (data.prsid == "" || data.prsid == null)
                    {

                        string query = "";
                        query = "select max(prs_id) as maxid from ERP_PRS_MASTER";

                        SqlCommand cmd = new SqlCommand(query, dbConn);
                        var reader = cmd.ExecuteReader();
                        System.Data.DataTable results = new System.Data.DataTable();
                        results.Load(reader);

                        for (int i = 0; i < results.Rows.Count; i++)
                        {
                            DataRow row = results.Rows[i];
                            strprsid = row[0].ToString() + 1;
                        }
                    }



                    if (data.prscode == "" || data.prscode == null)
                    {

                        DataSet dsprscode = new DataSet();
                        string sqlprscode = "ERP_PRS_ISCONFIG";
                        SqlCommand cmdprscode = new SqlCommand(sqlprscode, dbConn);


                        cmdprscode.CommandType = CommandType.StoredProcedure;

                        cmdprscode.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                        cmdprscode.Parameters.AddWithValue("@TYPE", "Purchaserequisition");

                        cmdprscode.ExecuteNonQuery();
                        var prscodereader = cmdprscode.ExecuteReader();
                        System.Data.DataTable resultsprscode = new System.Data.DataTable();
                        resultsprscode.Load(prscodereader);
                        //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                        for (int i = 0; i < resultsprscode.Rows.Count; i++)
                        {
                            DataRow rowprscode = resultsprscode.Rows[i];
                            strprscode = rowprscode[0].ToString();


                        }





                        DataSet dsuserdetails = new DataSet();
                        string sql = "MBL_ERP_PRS_SAVEDATA";
                        SqlCommand cmd1 = new SqlCommand(sql, dbConn);


                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.AddWithValue("@FUNCTION_ID", data.functionid);
                        cmd1.Parameters.AddWithValue("@PRS_ID", data.prsid);
                        cmd1.Parameters.AddWithValue("@STATUS", data.status);
                        cmd1.Parameters.AddWithValue("@CREATED_BY", data.createdby);
                        cmd1.Parameters.AddWithValue("@IPADDRESS", data.ipaddress);
                        cmd1.Parameters.AddWithValue("@REASON_PURCHASE", data.reasonpurchase);
                        cmd1.Parameters.AddWithValue("@NETAMOUNT", data.netamount);
                        cmd1.Parameters.AddWithValue("@CURRENCY", data.currency);
                        cmd1.Parameters.AddWithValue("@REQUEST_COMMENTS", data.requestcomments);
                        cmd1.Parameters.AddWithValue("@IS_BID", data.isbid);

                        cmd1.Parameters.AddWithValue("@PRS_TYPE", data.prstype);
                        cmd1.Parameters.AddWithValue("@BRANCH_ID", data.branchid);
                        cmd1.Parameters.AddWithValue("@PRS_REF", data.prsref);
                        cmd1.Parameters.AddWithValue("@PRS_CATEGORY", data.userid);

                        cmd1.Parameters.AddWithValue("@PRS_CODE", data.prscode);
                        cmd1.Parameters.AddWithValue("@REQUESTED_BY", data.requestby);
                        cmd1.Parameters.AddWithValue("@REQUESTED_DATE", "");

                        cmd1.Parameters.AddWithValue("@REQUEST_TYPE", data.requettype);
                        cmd1.Parameters.AddWithValue("@IS_SINGLE_VENDOR", data.issinglevendor);
                        cmd1.Parameters.AddWithValue("@ORDER_PRIORITY", data.orderpriority);
                        cmd1.ExecuteNonQuery();
                        var reader1 = cmd1.ExecuteReader();
                        System.Data.DataTable results1 = new System.Data.DataTable();
                        results1.Load(reader1);
                        //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                        for (int i = 0; i < results1.Rows.Count; i++)
                        {
                            DataRow row1 = results1.Rows[i];
                            strprscode = row1[0].ToString();

                            dbConn.Close();
                        }
                        if (strprscode != string.Empty)
                        {
                            stroutput = "Inserted successfully";

                        }
                        var result = (new { logdata });
                        return Ok(stroutput);
                    }

                    else
                    {
                        DataSet dsuserdetails = new DataSet();
                        string sql = "MBL_ERP_PRS_SAVEDATA";
                        SqlCommand cmd1 = new SqlCommand(sql, dbConn);


                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Parameters.AddWithValue("@FUNCTION_ID", data.functionid);
                        cmd1.Parameters.AddWithValue("@PRS_ID", strprsid);
                        cmd1.Parameters.AddWithValue("@STATUS", data.status);
                        cmd1.Parameters.AddWithValue("@CREATED_BY", data.createdby);
                        cmd1.Parameters.AddWithValue("@IPADDRESS", data.ipaddress);
                        cmd1.Parameters.AddWithValue("@REASON_PURCHASE", data.reasonpurchase);
                        cmd1.Parameters.AddWithValue("@NETAMOUNT", data.netamount);
                        cmd1.Parameters.AddWithValue("@CURRENCY", data.currency);
                        cmd1.Parameters.AddWithValue("@REQUEST_COMMENTS", data.requestcomments);
                        cmd1.Parameters.AddWithValue("@IS_BID", data.isbid);

                        cmd1.Parameters.AddWithValue("@PRS_TYPE", data.prstype);
                        cmd1.Parameters.AddWithValue("@BRANCH_ID", data.branchid);
                        cmd1.Parameters.AddWithValue("@PRS_REF", data.prsref);
                        cmd1.Parameters.AddWithValue("@PRS_CATEGORY", data.userid);

                        cmd1.Parameters.AddWithValue("@PRS_CODE", strprscode);
                        cmd1.Parameters.AddWithValue("@REQUESTED_BY", data.requestby);
                        cmd1.Parameters.AddWithValue("@REQUESTED_DATE", "");

                        cmd1.Parameters.AddWithValue("@REQUEST_TYPE", data.requettype);
                        cmd1.Parameters.AddWithValue("@IS_SINGLE_VENDOR", data.issinglevendor);
                        cmd1.Parameters.AddWithValue("@ORDER_PRIORITY", data.orderpriority);
                        cmd1.ExecuteNonQuery();
                        var reader1 = cmd1.ExecuteReader();
                        System.Data.DataTable results1 = new System.Data.DataTable();
                        results1.Load(reader1);
                        //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                        for (int i = 0; i < results1.Rows.Count; i++)
                        {
                            DataRow row1 = results1.Rows[i];
                            strprscode = row1[0].ToString();

                            dbConn.Close();
                        }
                        if (strprscode != string.Empty)
                        {
                            stroutput = "Inserted successfully";

                        }
                        var result = (new { logdata });
                        return Ok(stroutput);
                    }
                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
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
