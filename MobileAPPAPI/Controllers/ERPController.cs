﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Nancy.Json;
using Nancy.Json.Simple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
//using nTireAdminLib.BLL;
namespace MobileAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ERPController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();
        public static SQLAccesLayer objSQLAccesLayer = new SQLAccesLayer();

        static string JsonString = string.Empty;
        DataSet dstSubDelivery = new DataSet();
        //node source starts

        //   nTireAdminLib.BLL.WorkFlow workflow = new nTireAdminLib.BLL.WorkFlow();


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
                cmd.Parameters.AddWithValue("@PAGEINDEX", "0");
                cmd.Parameters.AddWithValue("@PAGESIZE", "20");
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", "po_reference");
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
                query = "select * from PROCUREMENTVENDORMASTER where Code='" + data.code + "'";

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
                    vendorid = row["vendor_id"].ToString();

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
                query = "select vendor_id from ERP_VENDOR_MASTER where Vendor_Code='" + data.vendorcode + "'";

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
                        data.functionid = "";
                    }
                    if (data.branchid.ToString() == "0" || data.branchid.ToString() == "" || data.branchid.ToString() == string.Empty || data.branchid.ToString() == null)
                    {
                        data.branchid = "";
                    }
                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "";
                    }
                    if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    {
                        data.fromdate = "";
                    }
                    if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    {
                        data.todate = "";
                    }
                    if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    {
                        data.reuestdate ="";
                    }
                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "";
                    }
                    if (data.currentstatus.ToString() == "0" || data.currentstatus.ToString() == "" || data.currentstatus.ToString() == string.Empty || data.currentstatus.ToString() == null)
                    {
                        data.currentstatus = "";
                    }
                    if (data.reqtype.ToString() == "0" || data.reqtype.ToString() == "" || data.reqtype.ToString() == string.Empty || data.reqtype.ToString() == null)
                    {
                        data.reqtype = "";
                    }
                    if (data.requser.ToString() == "0" || data.requser.ToString() == "" || data.requser.ToString() == string.Empty || data.requser.ToString() == null)
                    {
                        data.requser = "";
                    }
                    if (data.usertype.ToString() == "0" || data.usertype.ToString() == "" || data.usertype.ToString() == string.Empty || data.usertype.ToString() == null)
                    {
                        data.usertype = "";
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "";
                    }
                    if (data.qutype.ToString() == "0" || data.qutype.ToString() == "" || data.qutype.ToString() == string.Empty || data.qutype.ToString() == null)
                    {
                        data.qutype = "";
                    }
                    if (data.prsref.ToString() == "0" || data.prsref.ToString() == "" || data.prsref.ToString() == string.Empty || data.prsref.ToString() == null)
                    {
                        data.prsref = "";
                    }
                    if (data.menuid.ToString() == "0" || data.menuid.ToString() == "" || data.menuid.ToString() == string.Empty || data.menuid.ToString() == null)
                    {
                        data.menuid = "";
                    }
                    if (data.userid.ToString() == "0" || data.userid.ToString() == "" || data.userid.ToString() == string.Empty || data.userid.ToString() == null)
                    {
                        data.userid = "";
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    string sql = "erp_prs_getdetails";

                   // string sql = "erp_prs_getdetails";

                    // string sql = "erp_prs_getdetails1";
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
                   // cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        //logdata = DataTableToJSONWithStringBuilder(results);

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


        [HttpPost]
        [Route("get_PRS_search1")]
        public async Task<ActionResult<ERP>> get_PRS_search1(ERP data)
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
                    //string sql = "erp_prs_getdetails";

                    // string sql = "erp_prs_getdetails1";

                    string sql = "erp_prs_getdetails";
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
                    // cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        //logdata = DataTableToJSONWithStringBuilder(results);

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
        public async Task<ActionResult<ERP>> get_PRS_Insert_Update(dynamic data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var stroutput = "";
            string strprscode = "";
            string strprsid = "";
            string prsid = string.Empty;
            string currency = string.Empty;
            string prs_detailID = string.Empty;
            string wfno = string.Empty;
            // var result = "";
            //  string stroutput = string.Empty;


            //declare variable for both method

            string prscategory = string.Empty;
            string prsdetail = string.Empty;

            string functionid = string.Empty;

            string prscode = string.Empty;
            string status = string.Empty;
            string createdby = string.Empty;
            string ipaddress = string.Empty;
            string reasonpurchase = string.Empty;
            string netamount = string.Empty;

            string requestcomments = string.Empty;
            string isbid = string.Empty;
            string prstype = string.Empty;
            string branchid = string.Empty;
            string prsref = string.Empty;
            string userid = string.Empty;
            string requestby = string.Empty;
            string requestdate = string.Empty;
            string requettype = string.Empty;
            string issinglevendor = string.Empty;
            string orderpriority = string.Empty;
            string release = string.Empty;

            string itemid = string.Empty;
            string i_function_id = string.Empty;
            string required_qty = string.Empty;
            string UOM = string.Empty;
            string expected_cost = string.Empty;
            string exp_date = string.Empty;

            string created_by = string.Empty;

            string unit_price = string.Empty;
            string Limit = string.Empty;
            string Availlimit = string.Empty;
            string BalanceLimit = string.Empty;
            string CATEGORY = string.Empty;
            string TAX1 = string.Empty;
            string TAX2 = string.Empty;
            string TAX1DESC = string.Empty;
            string TAX2DESC = string.Empty;
            string OTHERCHARGES = string.Empty;
            string item_short_desc = string.Empty;
            string REMARKS = string.Empty;
            string CategoryID = string.Empty;
            string SubCategoryID = string.Empty;
            string prsDetailID = string.Empty;
            string FreightVALUE = string.Empty;
            string FreightID = string.Empty;
            string RecoveryVALUE = string.Empty;
            string RecoveryID = string.Empty;
            string BDC = string.Empty;
            string PTM = string.Empty;
            string ACC = string.Empty;
            string CPC = string.Empty;
            string flag = string.Empty;

            string pk_column_name1 = string.Empty;

            string pk_column_name2 = string.Empty;

            string pk_column_name3 = string.Empty;

            string pk_column_name4 = string.Empty;

            string pk_column_name5 = string.Empty;


            string STATUS_COLUMN = string.Empty;

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {




                    var serializedObject = JsonConvert.SerializeObject(data).ToString();


                    var obj = JsonConvert.DeserializeObject<JObject>(data.ToString());


                    JObject obj1 = JsonConvert.DeserializeObject<JObject>(data.ToString());





                    JObject obj_parent = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent1 = obj_parent.GetValue("prsdetail")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent1)
                    {
                        JProperty p1 = obj_parent1.Property(item.Key);

                        if (item.Key == "prscategory")
                        {
                            prscategory = item.Value.ToString();
                        }

                        if (item.Key == "functionid")
                        {
                            functionid = item.Value.ToString();
                        }
                        if (item.Key == "prsid")
                        {
                            prsid = item.Value.ToString();
                        }
                        if (item.Key == "prscode")
                        {
                            prscode = item.Value.ToString();
                            if (prscode!="")
                            {
                                strprscode = prscode;
                            }

                        }
                        if (item.Key == "status")
                        {
                            status = item.Value.ToString();
                        }
                        if (item.Key == "createdby")
                        {
                            createdby = item.Value.ToString();
                        }
                        if (item.Key == "ipaddress")
                        {
                            ipaddress = item.Value.ToString();
                        }
                        if (item.Key == "reasonpurchase")
                        {
                            reasonpurchase = item.Value.ToString();
                        }
                        if (item.Key == "netamount")
                        {
                            netamount = item.Value.ToString();
                        }
                        if (item.Key == "currency")
                        {
                            currency = item.Value.ToString();
                        }
                        if (item.Key == "requestcomments")
                        {
                            requestcomments = item.Value.ToString();
                        }
                        if (item.Key == "isbid")
                        {
                            isbid = item.Value.ToString();
                        }
                        if (item.Key == "prstype")
                        {
                            prstype = item.Value.ToString();

                        }
                        if (item.Key == "branchid")
                        {
                            branchid = item.Value.ToString();
                        }
                        if (item.Key == "prsref")
                        {
                            prsref = item.Value.ToString();
                        }
                        if (item.Key == "userid")
                        {
                            userid = item.Value.ToString();
                        }
                        if (item.Key == "requestby")
                        {
                            requestby = item.Value.ToString();
                        }
                        if (item.Key == "requestdate")
                        {
                            requestdate = item.Value.ToString();
                        }
                        if (item.Key == "requettype")
                        {
                            requettype = item.Value.ToString();
                        }
                        if (item.Key == "issinglevendor")
                        {
                            issinglevendor = item.Value.ToString();
                        }
                        if (item.Key == "orderpriority")
                        {
                            orderpriority = item.Value.ToString();
                        }
                        if (item.Key == "release")
                        {
                            release = item.Value.ToString();
                        }
                    }

                    if (prscategory == "U")
                    {
                        if (prsid == "" || prsid == null)
                        {

                            dbConn.Open();
                           

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

                            dbConn.Close();
                        }
                        else
                        {
                            dbConn.Open();
                           
                            string status1 = string.Empty;
                            string query = "";
                            query = "select status from ERP_PRS_MASTER where  prs_id='" + prsid + "'";

                            SqlCommand cmd = new SqlCommand(query, dbConn);
                            var reader = cmd.ExecuteReader();
                            System.Data.DataTable results = new System.Data.DataTable();
                            results.Load(reader);

                            for (int i = 0; i < results.Rows.Count; i++)
                            {
                                DataRow row = results.Rows[i];
                                status1 = row[0].ToString();
                            }
                            if (status1 == "N" && status=="P")
                            {
                                string wfno_sql = "update erp_prs_master set status='P', request_comments='"+ requestcomments + "',priority='"+ orderpriority + "' where prs_id='" + prsid + "' and function_id=1";
                                SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                var reader3 = cmd3.ExecuteReader();
                                System.Data.DataTable results3 = new System.Data.DataTable();
                                results3.Load(reader3);
                                stroutput = "Updated successfully";
                            }
                            else
                            {
                                string wfno_sql = "update erp_prs_master set status='" + status + "', request_comments='" + requestcomments + "' ,priority='" + orderpriority + "' where prs_id='" + prsid + "' and function_id=1";
                                SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                var reader3 = cmd3.ExecuteReader();
                                System.Data.DataTable results3 = new System.Data.DataTable();
                                results3.Load(reader3);
                                stroutput = "Updated successfully";
                            }



                          



                            dbConn.Close();
                        }













                    }

                    if (prscategory == "I")
                    {



                        if (prsid == "" || prsid == null)
                        {

                            dbConn.Open();

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

                            dbConn.Close();
                        }
                        else
                        {
                            dbConn.Open();
                            string status1 = string.Empty;
                            string query = "";
                            query = "select status from ERP_PRS_MASTER where  prs_id='" + prsid + "'";

                            SqlCommand cmd = new SqlCommand(query, dbConn);
                            var reader = cmd.ExecuteReader();
                            System.Data.DataTable results = new System.Data.DataTable();
                            results.Load(reader);

                            for (int i = 0; i < results.Rows.Count; i++)
                            {
                                DataRow row = results.Rows[i];
                                status1 = row[0].ToString();
                            }
                            if (status1 == "N" && status == "P")
                            {
                                string wfno_sql = "update erp_prs_master set status='P' where prs_id='" + prsid + "' and function_id=1";
                                SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                var reader3 = cmd3.ExecuteReader();
                                System.Data.DataTable results3 = new System.Data.DataTable();
                                results3.Load(reader3);
                                stroutput = "Updated successfully";
                            }

                            dbConn.Close();
                        }



                        if (prscode == "" || prscode == null)
                        {

                            dbConn.Open();
                            DataSet dsprscode = new DataSet();
                            string sqlprscode = "ERP_PRS_ISCONFIG";
                            SqlCommand cmdprscode = new SqlCommand(sqlprscode, dbConn);


                            cmdprscode.CommandType = CommandType.StoredProcedure;

                            cmdprscode.Parameters.AddWithValue("@FUNCTIONID", functionid);
                            cmdprscode.Parameters.AddWithValue("@TYPE", "Purchaserequisition");

                            //cmdprscode.ExecuteNonQuery();
                            var prscodereader = cmdprscode.ExecuteReader();
                            System.Data.DataTable resultsprscode = new System.Data.DataTable();
                            resultsprscode.Load(prscodereader);
                            //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                            for (int i = 0; i < resultsprscode.Rows.Count; i++)
                            {
                                DataRow rowprscode = resultsprscode.Rows[i];
                                strprscode = rowprscode[0].ToString();


                            }


                            if (string.IsNullOrEmpty(currency.ToString()))
                            {
                                if (itemid.ToString() == "0")
                                {
                                    currency = currency.ToString();
                                }
                                else
                                {
                                    string query = "";
                                    query = "select currency from ERP_ITEM_MASTER INNER JOIN USERACCESS WITH (NOLOCK) ON USERACCESS.FUNCTION_ID=ERP_ITEM_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID = ERP_ITEM_MASTER.CREATED_BY where item_id='" + itemid + "'";

                                    SqlCommand cmd = new SqlCommand(query, dbConn);
                                    var reader = cmd.ExecuteReader();
                                    System.Data.DataTable results = new System.Data.DataTable();
                                    results.Load(reader);

                                    for (int i = 0; i < results.Rows.Count; i++)
                                    {
                                        DataRow row = results.Rows[i];
                                        currency = row[0].ToString() + 1;
                                    }

                                    //shy
                                }
                            }
                            else
                            {
                                currency = currency.ToString();
                            }


                            DataSet dsuserdetails = new DataSet();
                           // string sql = "ERP_PRS_SAVEDATA";
                            string sql = "erp_prs_savedata";
                            SqlCommand cmd1 = new SqlCommand(sql, dbConn);
                            cmd1.CommandType = CommandType.StoredProcedure;

                            cmd1.Parameters.AddWithValue("@FUNCTION_ID", functionid.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_ID", strprsid.ToString());
                            cmd1.Parameters.AddWithValue("@CREATED_BY", createdby.ToString());
                            cmd1.Parameters.AddWithValue("@REASON_PURCHASE", reasonpurchase.ToString());
                            cmd1.Parameters.AddWithValue("@STATUS", status.ToString());
                            cmd1.Parameters.AddWithValue("@NETAMOUNT", netamount.ToString());
                            cmd1.Parameters.AddWithValue("@REQUEST_COMMENTS", requestcomments.ToString());
                          //  cmd1.Parameters.AddWithValue("@IS_BID", isbid.ToString());
                            cmd1.Parameters.AddWithValue("@IS_BID", "N");
                            cmd1.Parameters.AddWithValue("@PRS_TYPE", prstype.ToString());
                            cmd1.Parameters.AddWithValue("@BRANCH_ID", branchid.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_REF", prsref.ToString());
                            cmd1.Parameters.AddWithValue("@PRS_CATEGORY", "");
                            cmd1.Parameters.AddWithValue("@PRS_CODE", strprscode.ToString());
                            cmd1.Parameters.AddWithValue("@REQUESTED_BY", requestby.ToString());
                            cmd1.Parameters.AddWithValue("@REQUESTED_DATE", requestdate.ToString());
                            cmd1.Parameters.AddWithValue("@REQUEST_TYPE", requettype.ToString());
                            cmd1.Parameters.AddWithValue("@CURRENCY", currency);
                            cmd1.Parameters.AddWithValue("@IPADDRESS", ipaddress.ToString());
                           // cmd1.Parameters.AddWithValue("@IS_SINGLE_VENDOR", issinglevendor.ToString());
                            cmd1.Parameters.AddWithValue("@IS_SINGLE_VENDOR", "N");
                            cmd1.Parameters.AddWithValue("@ORDER_PRIORITY", orderpriority.ToString());

                            //cmd1.ExecuteNonQuery();
                            var reader1 = cmd1.ExecuteReader();
                            System.Data.DataTable results1 = new System.Data.DataTable();
                            results1.Load(reader1);
                            //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                            for (int i = 0; i < results1.Rows.Count; i++)
                            {
                                DataRow row1 = results1.Rows[i];
                                prsid = row1[0].ToString();


                            }
                            if (strprscode != string.Empty)
                            {
                                stroutput = "Inserted successfully";

                            }
                            //var result = (new { logdata });
                            //return Ok(stroutput);
                            dbConn.Close();
                        }
                    }




                    if (status != "D")
                    {
                        JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                        JObject obj_parent2 = obj_parents.GetValue("prsdetail")[0] as JObject;


                        foreach (KeyValuePair<string, JToken> item in obj_parent2)
                        {
                            JProperty p2 = obj_parent2.Property(item.Key);



                            if (item.Key == "Itemsdetail")
                            {
                                var Itemsdetail = item.Value.ToString();

                                JArray array = JArray.Parse(Itemsdetail);
                                JArray jsonArray = JArray.Parse(Itemsdetail);

                                foreach (JObject content in array.Children<JObject>())
                                {
                                    foreach (JProperty prop in content.Properties())
                                    {
                                        string Name = prop.Name.ToString().Trim();
                                        string Value = prop.Value.ToString().Trim();
                                        //if (Name == "prsid")
                                        //{
                                        //    prsid = Value.ToString();
                                        //}
                                        if (Name == "itemid")
                                        {
                                            itemid = Value.ToString();
                                            if (itemid == "")
                                            {
                                                itemid = "0";
                                            }
                                        }
                                        if (Name == "i_function_id")
                                        {
                                            i_function_id = Value.ToString();
                                            if (i_function_id == "")
                                            {
                                                i_function_id = "0";
                                            }
                                        }
                                        if (Name == "required_qty")
                                        {
                                            required_qty = Value.ToString();
                                            if (required_qty == "")
                                            {
                                                required_qty = "0";
                                            }
                                        }
                                        if (Name == "UOM")
                                        {
                                            UOM = Value.ToString();
                                            if (UOM == "")
                                            {
                                                UOM = "0";
                                            }
                                        }
                                        if (Name == "expected_cost")
                                        {
                                            expected_cost = Value.ToString();
                                            if (expected_cost == "")
                                            {
                                                expected_cost = "0";
                                            }
                                        }
                                        if (Name == "exp_date")
                                        {
                                            exp_date = Value.ToString();
                                            if (exp_date == "")
                                            {
                                                exp_date = "0";
                                            }
                                        }
                                        if (Name == "status")
                                        {
                                            status = Value.ToString();
                                            if (status == "")
                                            {
                                                status = "0";
                                            }
                                        }
                                        if (Name == "created_by")
                                        {
                                            created_by = Value.ToString();
                                            if (created_by == "")
                                            {
                                                created_by = "0";
                                            }
                                        }
                                        if (Name == "ipaddress")
                                        {
                                            ipaddress = Value.ToString();
                                            if (ipaddress == "")
                                            {
                                                ipaddress = "0";
                                            }
                                        }
                                        if (Name == "unit_price")
                                        {
                                            unit_price = Value.ToString();
                                            if (unit_price == "")
                                            {
                                                unit_price = "0";
                                            }
                                        }
                                        if (Name == "Limit")
                                        {
                                            Limit = Value.ToString();
                                            if (Limit == "")
                                            {
                                                Limit = "0";
                                            }
                                        }
                                        if (Name == "Availlimit")
                                        {
                                            Availlimit = Value.ToString();
                                            if (Availlimit == "")
                                            {
                                                Availlimit = "0";
                                            }
                                        }
                                        if (Name == "BalanceLimit")
                                        {
                                            BalanceLimit = Value.ToString();
                                            if (BalanceLimit == "")
                                            {
                                                BalanceLimit = "0";
                                            }
                                        }
                                        if (Name == "CATEGORY")
                                        {
                                            CATEGORY = Value.ToString();
                                            if (CATEGORY == "")
                                            {
                                                CATEGORY = "0";
                                            }
                                        }
                                        if (Name == "TAX1")
                                        {
                                            TAX1 = Value.ToString();
                                            if (TAX1 == "")
                                            {
                                                TAX1 = "0";
                                            }
                                        }
                                        if (Name == "TAX2")
                                        {
                                            TAX2 = Value.ToString();
                                            if (TAX2 == "")
                                            {
                                                TAX2 = "0";
                                            }
                                        }
                                        if (Name == "TAX1DESC")
                                        {
                                            TAX1DESC = Value.ToString();
                                            if (TAX1DESC == "")
                                            {
                                                TAX1DESC = "0";
                                            }
                                        }
                                        if (Name == "TAX2DESC")
                                        {
                                            TAX2DESC = Value.ToString();
                                            if (TAX2DESC == "")
                                            {
                                                TAX2DESC = "0";
                                            }
                                        }
                                        if (Name == "OTHERCHARGES")
                                        {
                                            OTHERCHARGES = Value.ToString();
                                            if (OTHERCHARGES == "")
                                            {
                                                OTHERCHARGES = "0";
                                            }
                                        }
                                        if (Name == "item_short_desc")
                                        {
                                            item_short_desc = Value.ToString();
                                            if (item_short_desc == "")
                                            {
                                                item_short_desc = "0";
                                            }
                                        }
                                        if (Name == "REMARKS")
                                        {
                                            REMARKS = Value.ToString();
                                            if (REMARKS == "")
                                            {
                                                REMARKS = "0";
                                            }
                                        }
                                        if (Name == "CategoryID")
                                        {
                                            CategoryID = Value.ToString();
                                            if (CategoryID == "")
                                            {
                                                CategoryID = "0";
                                            }

                                        }
                                        if (Name == "SubCategoryID")
                                        {
                                            SubCategoryID = Value.ToString();
                                            if (SubCategoryID == "")
                                            {
                                                SubCategoryID = "0";
                                            }
                                        }
                                        if (Name == "prsDetailID")
                                        {
                                            prsDetailID = Value.ToString();
                                            if (prsDetailID == "")
                                            {
                                                prsDetailID = "0";
                                            }
                                        }
                                        if (Name == "FreightVALUE")
                                        {
                                            FreightVALUE = Value.ToString();
                                            if (FreightVALUE == "")
                                            {
                                                FreightVALUE = "0";
                                            }
                                        }
                                        if (Name == "FreightID")
                                        {
                                            FreightID = Value.ToString();
                                            if (FreightID == "")
                                            {
                                                FreightID = "0";
                                            }
                                        }
                                        if (Name == "RecoveryVALUE")
                                        {
                                            RecoveryVALUE = Value.ToString();
                                            if (RecoveryVALUE == "")
                                            {
                                                RecoveryVALUE = "0";
                                            }
                                        }
                                        if (Name == "RecoveryID")
                                        {
                                            RecoveryID = Value.ToString();
                                            if (RecoveryID == "")
                                            {
                                                RecoveryID = "0";
                                            }
                                        }
                                        if (Name == "BDC")
                                        {
                                            BDC = Value.ToString();
                                            if (BDC == "")
                                            {
                                                BDC = "0";
                                            }
                                        }
                                        if (Name == "PTM")
                                        {
                                            PTM = Value.ToString();
                                            if (PTM == "")
                                            {
                                                PTM = "0";
                                            }
                                        }
                                        if (Name == "ACC")
                                        {
                                            ACC = Value.ToString();
                                            if (ACC == "")
                                            {
                                                ACC = "0";
                                            }
                                        }
                                        if (Name == "CPC")
                                        {
                                            CPC = Value.ToString();
                                            if (CPC == "")
                                            {
                                                CPC = "0";
                                            }
                                        }
                                        if (Name == "flag")
                                        {
                                            flag = Value.ToString();
                                            if (flag == "")
                                            {
                                                flag = "0";
                                            }
                                        }

                                    }



                                    if (itemid != null && itemid !="" && itemid !="null" && itemid != string.Empty)
                                    {
                                        string itemID = itemid;
                                        dbConn.Open();
                                        // string prs_id = item.prsid;
                                        if (itemid != null)
                                        {

                                            string query = "";
                                            query = "delete from erp_prs_details where item_id = " + itemID + " and prs_id='" + prsid + "'";

                                            SqlCommand cmd = new SqlCommand(query, dbConn);
                                            var reader = cmd.ExecuteReader();
                                            System.Data.DataTable results = new System.Data.DataTable();
                                            results.Load(reader);


                                        }
                                        dbConn.Close();
                                    }




                                    if (prsid != null && prsid != string.Empty && prsid != "-1")
                                    {
                                        if (flag.ToString() != "D")
                                        {

                                            dbConn.Open();
                                            DataSet dsuserdetails = new DataSet();
                                            //string sql = "ERP_PRS_DETAILS_INSERT_UPDATE";

                                            string sql = "ERP_PRS_DETAILS_INSERT_UPDATE";
                                            SqlCommand objcommand1 = new SqlCommand(sql, dbConn);


                                            objcommand1.CommandType = CommandType.StoredProcedure;
                                            objcommand1.CommandType = CommandType.StoredProcedure;
                                            objcommand1.Parameters.AddWithValue("@FUNCTION_ID", i_function_id.ToString());
                                            objcommand1.Parameters.AddWithValue("@PRS_ID", prsid);
                                            objcommand1.Parameters.AddWithValue("@ITEM_ID", itemid.ToString());
                                            objcommand1.Parameters.AddWithValue("@REQUIRED_QTY", required_qty.ToString());
                                            objcommand1.Parameters.AddWithValue("@UOM", UOM.ToString());
                                            objcommand1.Parameters.AddWithValue("@EXPECTED_COST", expected_cost.ToString());
                                            objcommand1.Parameters.AddWithValue("@EXP_DATE", exp_date.ToString());
                                            objcommand1.Parameters.AddWithValue("@STATUS", status.ToString());
                                            objcommand1.Parameters.AddWithValue("@CREATED_BY", created_by.ToString());
                                            objcommand1.Parameters.AddWithValue("@IPADDRESS", ipaddress.ToString());
                                            objcommand1.Parameters.AddWithValue("@UNIT_PRICE", unit_price.ToString());
                                            objcommand1.Parameters.AddWithValue("@LIMIT", Limit.ToString());
                                            objcommand1.Parameters.AddWithValue("@AVAILED", Availlimit.ToString());
                                            objcommand1.Parameters.AddWithValue("@BALANCE_LIMIT", BalanceLimit.ToString());
                                            objcommand1.Parameters.AddWithValue("@CURRENCY", currency);
                                            objcommand1.Parameters.AddWithValue("@CATEGORY", CATEGORY.ToString());
                                            objcommand1.Parameters.AddWithValue("@TAX1", TAX1.ToString());
                                            objcommand1.Parameters.AddWithValue("@TAX2", TAX2.ToString());
                                            objcommand1.Parameters.AddWithValue("@TAX1DESC", TAX1DESC.ToString());
                                            objcommand1.Parameters.AddWithValue("@TAX2DESC", TAX2DESC.ToString());
                                            objcommand1.Parameters.AddWithValue("@OTHERCHARGES", OTHERCHARGES.ToString());
                                            objcommand1.Parameters.AddWithValue("@ITEMDESC", item_short_desc.ToString());
                                            objcommand1.Parameters.AddWithValue("@REMARKS", REMARKS.ToString());
                                            objcommand1.Parameters.AddWithValue("@CategoryID", CategoryID.ToString());
                                            objcommand1.Parameters.AddWithValue("@SubCategoryID", SubCategoryID.ToString());
                                            if (prsDetailID.ToString() != null && prsDetailID.ToString() != string.Empty)
                                                objcommand1.Parameters.AddWithValue("@prsDetailID", prsDetailID.ToString());
                                            else
                                                objcommand1.Parameters.AddWithValue("@prsDetailID", "0");

                                            objcommand1.Parameters.AddWithValue("@FreightVALUE", FreightVALUE.ToString());
                                            objcommand1.Parameters.AddWithValue("@FreightID", FreightID.ToString());
                                            objcommand1.Parameters.AddWithValue("@RecoveryVALUE", RecoveryVALUE.ToString());
                                            objcommand1.Parameters.AddWithValue("@RecoveryID", RecoveryID.ToString());
                                            objcommand1.Parameters.AddWithValue("@BDC", BDC.ToString());
                                            objcommand1.Parameters.AddWithValue("@PTM", PTM.ToString());
                                            objcommand1.Parameters.AddWithValue("@ACC", ACC.ToString());
                                            objcommand1.Parameters.AddWithValue("@CPC", CPC.ToString());



                                            var reader1 = objcommand1.ExecuteReader();
                                            System.Data.DataTable results1 = new System.Data.DataTable();
                                            results1.Load(reader1);
                                            //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                                            for (int i = 0; i < results1.Rows.Count; i++)
                                            {
                                                DataRow row1 = results1.Rows[i];
                                                prs_detailID = row1[0].ToString();


                                            }

                                            dbConn.Close();
                                        }
                                    }

                                    if (prs_detailID != "")
                                    {




                                        dbConn.Open();



                                        DataSet ds1 = new DataSet();
                                        string netbasecurrency = "update ERP_PRS_MASTER set netamount=" + netamount + " WHERE PRS_ID='" + prsid + "' and function_id='" + i_function_id + "'";
                                        SqlCommand cmd = new SqlCommand(netbasecurrency, dbConn);
                                        var reader = cmd.ExecuteReader();
                                        System.Data.DataTable results = new System.Data.DataTable();
                                        results.Load(reader);
                                        if (release.ToString() == "true")
                                        {
                                            string wf_config_id = "select wf_config_id from BO_workflow_configurations where table_name like '%ERP_PRS_MASTER%' and status='A' and function_id='" + i_function_id + "'";
                                            SqlCommand cmd2 = new SqlCommand(wf_config_id, dbConn);
                                            var reader2 = cmd2.ExecuteReader();
                                            System.Data.DataTable results2 = new System.Data.DataTable();
                                            results2.Load(reader2);

                                            for (int i = 0; i < results2.Rows.Count; i++)
                                            {
                                                DataRow row = results2.Rows[i];
                                                wf_config_id = row[0].ToString();
                                            }

                                            // wf_config_id = objSql.getString(wf_config_id);
                                            if (wf_config_id != null && wf_config_id != "")
                                            {

                                                string wffun = i_function_id;
                                                string WorkFlowTable = "ERP_PRS_MASTER";
                                                string PK1 = prsid;
                                                string PK2 = null;
                                                string PK3 = null;
                                                string PK4 = null;
                                                string PK5 = null;
                                                string User = created_by;

                                                //workflow insert



                                                string wf_insert = "select pk_column_name1,pk_column_name2,pk_column_name3,pk_column_name4,pk_column_name5,STATUS_COLUMN from BO_WORKFLOW_CONFIGURATIONS with (nolock) where WF_CONFIG_ID='" + wf_config_id + "'";
                                                SqlCommand cmdwf = new SqlCommand(wf_insert, dbConn);
                                                var readerwf = cmdwf.ExecuteReader();
                                                System.Data.DataTable resultswf = new System.Data.DataTable();
                                                resultswf.Load(readerwf);

                                                for (int i = 0; i < resultswf.Rows.Count; i++)
                                                {
                                                    DataRow row = resultswf.Rows[i];
                                                    pk_column_name1 = resultswf.Rows[i]["pk_column_name1"].ToString();
                                                    pk_column_name2 = resultswf.Rows[i]["pk_column_name2"].ToString();
                                                    pk_column_name3 = resultswf.Rows[i]["pk_column_name3"].ToString();
                                                    pk_column_name4 = resultswf.Rows[i]["pk_column_name4"].ToString();
                                                    pk_column_name5 = resultswf.Rows[i]["pk_column_name5"].ToString();
                                                    STATUS_COLUMN = resultswf.Rows[i]["STATUS_COLUMN"].ToString();

                                                }


                                                if (pk_column_name1 != "" && STATUS_COLUMN != "")
                                                {
                                                    string wf_insert2 = "exec usp_WF_ApprovalUsers '" + WorkFlowTable + "','" + pk_column_name1 + "','" + pk_column_name2 + "','" + pk_column_name3 + "','" + pk_column_name4 + "','" + pk_column_name5 + "','" + PK1 + "','" + PK2 + "','" + PK3 + "' ,'" + PK4 + "','" + PK5 + "' ,'" + wffun + "' ,'" + User + "' ,'" + STATUS_COLUMN + "','" + status + "' ,'" + wf_config_id + "'";
                                                    SqlCommand cmdwf2 = new SqlCommand(wf_insert2, dbConn);
                                                    var readerwf2 = cmdwf2.ExecuteReader();
                                                    System.Data.DataTable resultsef2 = new System.Data.DataTable();
                                                    resultsef2.Load(readerwf2);
                                                }


                                                string wfno_sql = "select workflow_no from bo_workflow_details where module_id='260' and pk_value1='" + prsid + "' ";
                                                SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                                var reader3 = cmd3.ExecuteReader();
                                                System.Data.DataTable results3 = new System.Data.DataTable();
                                                results3.Load(reader3);
                                                for (int i = 0; i < results3.Rows.Count; i++)
                                                {
                                                    DataRow row = results3.Rows[i];
                                                    wfno = row[0].ToString();
                                                }

                                                if (wfno != "")
                                                {

                                                    string sql2 = "select *,BO_USER_MASTER.tum_user_name,ERP_ITEM_MASTER.item_short_Desc,bo_parameter.TEXT as UOMdesc,cur.code as currencydesc,ERP_PRS_MASTER.prs_code,convert(varchar, erp_prs_details.created_on, 103) as rfpdate from erp_prs_details inner join ERP_PRS_MASTER on ERP_PRS_MASTER.prs_id=erp_prs_details.prs_id  inner join BO_USER_MASTER on BO_USER_MASTER.tum_user_id=erp_prs_details.created_by and BO_USER_MASTER.FUNCTION_ID='1' inner join ERP_ITEM_MASTER on ERP_ITEM_MASTER.item_id = erp_prs_details.item_id inner join bo_parameter on bo_parameter.val = erp_prs_details.UOM and bo_parameter.type = 'UOM' and bo_parameter.FUNCTION_ID = '1' inner join bo_parameter cur on cur.val = erp_prs_details.currency and cur.type = 'currency' and cur.function_id = '1' where erp_prs_details.prs_id ='" + prsid + "' ";
                                                    SqlCommand cmd4 = new SqlCommand(sql2, dbConn);
                                                    var reader4 = cmd4.ExecuteReader();
                                                    System.Data.DataTable results4 = new System.Data.DataTable();
                                                    // ds1.Load(reader4);
                                                    results4.Load(reader4);




                                                }



                                            }
                                            else
                                            {
                                                string wfno_sql = "update erp_prs_master set status='P' where prs_id='" + prsid + "' and function_id=1";
                                                SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                                                var reader3 = cmd3.ExecuteReader();
                                                System.Data.DataTable results3 = new System.Data.DataTable();
                                                results3.Load(reader3);


                                            }



                                        }
                                        dbConn.Close();
                                    }

                                }
                            }





                        }
                    }
                }
                string space = " ";
                logdata = stroutput + space + space + "PRSCODE :" + strprscode;

                var result = (new { logdata });
                return Ok(logdata);
            }

            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }



        //PRS delete
        [HttpPost]
        [Route("get_PRS_Delete")]
        public async Task<ActionResult<ERP>> get_PRS_Delete(ERP data)
        {
            // string struser = data.user_lower;

            // List<CAMS> Logdata = new List<CAMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            DataSet DS = new DataSet();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                //string sql = "ERP_PRS_DELETE";
                string sql = "ERP_PRS_DELETE";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PRSID", data.prsid);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //for (int i = 0; i < results.Rows.Count; i++)
                //{
                //    DataRow row = results.Rows[i];
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //}

                Logdata1 = "Deleted Successfully";
                return Ok(Logdata1);

            }
        }


        //prs code autocompletion

        [HttpGet]
        [Route("getPRScode/{code}")]
        public string getPRScode(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select prs_id,prs_code ,concat(prs_id,' - ',prs_code) as prsdetails from ERP_PRS_MASTER where PRS_CODE like'" + code + "%'";

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










        //RFQ search


        [HttpPost]
        [Route("get_RFQ_search")]
        public async Task<ActionResult<ERP>> get_RFQ_search(ERP data)
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

                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "0";
                    }
                    if (data.itemcode.ToString() == "0" || data.itemcode.ToString() == "" || data.itemcode.ToString() == string.Empty || data.itemcode.ToString() == null)
                    {
                        data.itemcode = "0";
                    }

                    if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    {
                        data.reuestdate = "0";
                    }


                    if (data.rfqcode.ToString() == "0" || data.rfqcode.ToString() == "" || data.rfqcode.ToString() == string.Empty || data.rfqcode.ToString() == null)
                    {
                        data.rfqcode = "0";
                    }

                    if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    {
                        data.fromdate = "0";
                    }
                    if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    {
                        data.todate = "0";
                    }
                    if (data.rfqfromdate.ToString() == "0" || data.rfqfromdate.ToString() == "" || data.rfqfromdate.ToString() == string.Empty || data.rfqfromdate.ToString() == null)
                    {
                        data.rfqfromdate = "0";
                    }
                    if (data.rfqtodate.ToString() == "0" || data.rfqtodate.ToString() == "" || data.rfqtodate.ToString() == string.Empty || data.rfqtodate.ToString() == null)
                    {
                        data.rfqtodate = "0";
                    }

                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }

                    if (data.mode.ToString() == "0" || data.mode.ToString() == "" || data.mode.ToString() == string.Empty || data.mode.ToString() == null)
                    {
                        data.mode = "0";
                    }
                    if (data.pageindex1.ToString() == "0" || data.pageindex1.ToString() == "" || data.pageindex1.ToString() == string.Empty || data.pageindex1.ToString() == null)
                    {
                        data.pageindex1 = 0;
                    }
                    if (data.pagesize1.ToString() == "0" || data.pagesize1.ToString() == "" || data.pagesize1.ToString() == string.Empty || data.pagesize1.ToString() == null)
                    {
                        data.pagesize1 = 0;
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "0";
                    }



                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    //string sql = "erp_prs_getdetails";
                    string sql = "erp_prs_getdetails";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                    cmd.Parameters.AddWithValue("@REQUESTED_DATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@RFQFromDate", data.rfqfromdate);
                    cmd.Parameters.AddWithValue("@RFQToDate", data.rfqtodate);
                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@MODE", data.mode);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize1);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.alphaname);


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


        //get Item code auto filling details

        [HttpGet]
        [Route("getItemcode/{code}")]
        public string getItemcode(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                //query = "select function_id, item_id, item_Code, item_short_Desc, item_long_desc from ERP_ITEM_MASTER where item_code like  '%" + code + "%'";




                query = "select function_id, item_id, item_Code, item_short_Desc, item_long_desc,concat(item_code,'-',item_short_Desc) itemdetails from ERP_ITEM_MASTER where item_code like  '" + code + "%' or item_short_Desc like '" + code + "%'";

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


        //get Item detials

        [HttpGet]
        [Route("getItemDetail/{code}")]
        public string getItemDetail(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select * from ERP_ITEM_MASTER where item_code = '" + code + "' and function_id=1 and Status='A'";

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


        //order priority dropdown
        [HttpGet]
        [Route("getOrderPriority")]
        public string getOrderPriority()
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select type,text,val,sequence from bo_parameter where TYPE='Order Priority' and FUNCTION_ID=1 and status='A'";

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

        //rfq pending search

        [HttpPost]
        [Route("searchRFQLists")]
        public async Task<ActionResult<ERP>> searchRFQLists(ERP data)
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


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    //string sql = "ERP_RFQ_getvendorevaluationRFQ";

                    string sql = "ERP_RFQ_getvendorevaluationRFQ";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                    cmd.Parameters.AddWithValue("@REQUESTED_DATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@RFQFromDate", data.rfqfromdate);
                    cmd.Parameters.AddWithValue("@RFQToDate", data.rfqtodate);
                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@MODE", "2");
                    cmd.Parameters.AddWithValue("@PAGEINDEX", "0");
                    cmd.Parameters.AddWithValue("@PAGESIZE", "20");
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", "prs_id DESC");
                    cmd.Parameters.AddWithValue("@ALPHANAME", "");
                    cmd.Parameters.AddWithValue("@UserId", "1");
                    cmd.Parameters.AddWithValue("@UserType", "1");

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


        //Pending Qoutation Search Sankari

        [HttpPost]
        [Route("get_pending_quotation_search")]
        public async Task<ActionResult<ERP>> get_pending_quotation_search(ERP data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            string todate = string.Empty;
            string fromdate = string.Empty;

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    if (data.FUNCTIONIDP.ToString() == "0" || data.FUNCTIONIDP.ToString() == "" || data.FUNCTIONIDP.ToString() == string.Empty || data.FUNCTIONIDP.ToString() == null)
                    {
                        data.FUNCTIONIDP = "0";
                    }
                    if (data.BRANCHIDP.ToString() == "0" || data.BRANCHIDP.ToString() == "" || data.BRANCHIDP.ToString() == string.Empty || data.BRANCHIDP.ToString() == null)
                    {
                        data.BRANCHIDP = "0";
                    }
                    if (data.RFQCODEP.ToString() == "0" || data.RFQCODEP.ToString() == "" || data.RFQCODEP.ToString() == string.Empty || data.RFQCODEP.ToString() == null)
                    {
                        data.RFQCODEP = "0";
                    }
                    //if (data.FROMDATEP.ToString() == "0" || data.FROMDATEP.ToString() == "" || data.FROMDATEP.ToString() == string.Empty || data.FROMDATEP.ToString() == null)
                    //{
                    //    data.FROMDATEP = "0";
                    //}
                    //else
                    //{
                    //    string date = data.FROMDATEP;
                    //    //DateTime dt = DateTime.Parse(date);
                    //    //dt.ToString("dd/MM/yyyy");
                    //    //fromdate = dt.ToString();

                    //    fromdate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


                    //}
                    //if (data.TODATEP.ToString() == "0" || data.TODATEP.ToString() == "" || data.TODATEP.ToString() == string.Empty || data.TODATEP.ToString() == null)
                    //{
                    //    data.TODATEP = "0";
                    //}
                    //else
                    //{
                    //    string date = data.TODATEP;
                    //    //DateTime dt = DateTime.Parse(date);
                    //    //dt.ToString("dd/MM/yyyy");
                    //    todate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //}




                    //if (data.FROMDATEP.ToString() == "0" || data.FROMDATEP.ToString() == "" || data.FROMDATEP.ToString() == string.Empty || data.FROMDATEP.ToString() == null)
                    //{
                    //    data.FROMDATEP = "quote_date >= CONVERT(varchar, convert(datetime,'" + data.FROMDATEP + "', 103),103)";
                    //    // data.FROMDATEP = "0";
                    //}
                    //if (data.TODATEP.ToString() == "0" || data.TODATEP.ToString() == "" || data.TODATEP.ToString() == string.Empty || data.TODATEP.ToString() == null)
                    //{
                    //    // data.TODATEP = "0";
                    //    data.TODATEP = "quote_date >= CONVERT(varchar, convert(datetime,'" + data.TODATEP + "', 103),103)";

                    //}


                    if (data.FROMDATEP.ToString() == "0" || data.FROMDATEP.ToString() == "" || data.FROMDATEP.ToString() == string.Empty || data.FROMDATEP.ToString() == null)
                    {
                        data.FROMDATEP = "";
                    }
                    if (data.TODATEP.ToString() == "0" || data.TODATEP.ToString() == "" || data.TODATEP.ToString() == string.Empty || data.TODATEP.ToString() == null)
                    {
                        data.TODATEP = "";
                    }

                    if (data.ITEMCODEP.ToString() == "0" || data.ITEMCODEP.ToString() == "" || data.ITEMCODEP.ToString() == string.Empty || data.ITEMCODEP.ToString() == null)
                    {
                        data.ITEMCODEP = "0";
                    }
                    if (data.VENDORIDP.ToString() == "0" || data.VENDORIDP.ToString() == "" || data.VENDORIDP.ToString() == string.Empty || data.VENDORIDP.ToString() == null)
                    {
                        data.VENDORIDP = "0";
                    }
                    if (data.STATUSP.ToString() == "0" || data.STATUSP.ToString() == "" || data.STATUSP.ToString() == string.Empty || data.STATUSP.ToString() == null)
                    {
                        data.STATUSP = "0";
                    }
                    if (data.PAGEINDEXP.ToString() == "0" || data.PAGEINDEXP.ToString() == "" || data.PAGEINDEXP.ToString() == string.Empty || data.PAGEINDEXP.ToString() == null)
                    {
                        data.PAGEINDEXP = 0;
                    }
                    if (data.PAGESIZEP.ToString() == "0" || data.PAGESIZEP.ToString() == "" || data.PAGESIZEP.ToString() == string.Empty || data.PAGESIZEP.ToString() == null)
                    {
                        data.PAGESIZEP = 0;
                    }
                    if (data.SORTEXPRESSIONP.ToString() == "0" || data.SORTEXPRESSIONP.ToString() == "" || data.SORTEXPRESSIONP.ToString() == string.Empty || data.SORTEXPRESSIONP.ToString() == null)
                    {
                        data.SORTEXPRESSIONP = "0";
                    }
                    if (data.ALPHANAMEP.ToString() == "0" || data.ALPHANAMEP.ToString() == "" || data.ALPHANAMEP.ToString() == string.Empty || data.ALPHANAMEP.ToString() == null)
                    {
                        data.ALPHANAMEP = "0";
                    }
                    if (data.modep.ToString() == "0" || data.modep.ToString() == "" || data.modep.ToString() == string.Empty || data.modep.ToString() == null)
                    {
                        data.modep = "0";
                    }

                    string strFDate = string.Empty;
                    string strTDate = string.Empty;
                    if (data.FROMDATEP.ToString() != "0" && data.FROMDATEP.ToString() != "" && data.FROMDATEP.ToString() != string.Empty && data.FROMDATEP.ToString() != null)
                    {
                        strFDate = Convert.ToDateTime(data.FROMDATEP.ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        strFDate = data.FROMDATEP.ToString();
                    }
                    if (data.TODATEP.ToString() != "0" && data.TODATEP.ToString() != "" && data.TODATEP.ToString() != string.Empty && data.TODATEP.ToString() != null)
                    {



                        strTDate = Convert.ToDateTime(data.TODATEP.ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        strTDate = data.TODATEP.ToString();
                    }

                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                 //string sql = "ERP_VENDORQUOTATION_SUMMARY";

                    string sql = "ERP_VENDORQUOTATION_SUMMARY";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.FUNCTIONIDP);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.BRANCHIDP);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.RFQCODEP);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.FROMDATEP);
                    cmd.Parameters.AddWithValue("@TODATE",data.TODATEP);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.ITEMCODEP);
                    cmd.Parameters.AddWithValue("@VENDORID", data.VENDORIDP);
                    cmd.Parameters.AddWithValue("@QUOTEREF", data.QUOTEREFP);

                    cmd.Parameters.AddWithValue("@STATUS", data.STATUSP);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXP);

                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEP);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONP);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEP);
                    cmd.Parameters.AddWithValue("@MODE", data.modep);
                    cmd.Parameters.AddWithValue("@VENDORCODE", data.VENDORCODEP);


                    //EXEC ERP_VENDORQUOTATION_SUMMARY '1','1','0','','','0','0','','0','0','20','QUOTE_id desc','0','2',''
                    //Added Catid and subcatid
                    //cmd.ExecuteNonQuery();
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



        #region
        //sowmi

        [HttpGet]
        [Route("Getodersummary")]
        public string Getodersummary(string strbranch, string function, string ponumber, string vendorcode, string fromdate, string todate, string status, string itemcode, string usertype, string userid, int pageIndex, int pageSize, string sortExpression, string alphaname, string prscode = null)

        {
            try
            {
                string fromdate1 = string.Empty;
                string todate1 = string.Empty;
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();



                    if (strbranch.ToString() == "0" || strbranch.ToString() == "" || strbranch.ToString() == string.Empty || strbranch.ToString() == null)
                    {
                        strbranch = "";
                    }
                    if (function.ToString() == "0" || function.ToString() == "" || function.ToString() == string.Empty || function.ToString() == null)
                    {
                        function = "";
                    }
                    if (ponumber.ToString() == "0" || ponumber.ToString() == "" || ponumber.ToString() == string.Empty || ponumber.ToString() == "null")
                    {
                        ponumber = "";
                    }
                    if (vendorcode.ToString() == "0" || vendorcode.ToString() == "" || vendorcode.ToString() == string.Empty || vendorcode.ToString() == "null")
                    {
                        vendorcode = "";
                    }
                    if (fromdate.ToString() == ""|| fromdate.ToString() == "0"|| fromdate.ToString() == "null"|| fromdate.ToString() == string.Empty)
                    {
                        fromdate = "";
                    }
                    else
                    {
                        string date = fromdate;
                    
                        fromdate1 = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (todate.ToString() == "0" || todate.ToString() == "" || todate.ToString() == string.Empty || todate.ToString() == "null")
                    {
                        todate = "";
                    }
                    else
                    {
                        string date = todate;

                        todate1 = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (status.ToString() == "0" || status.ToString() == "" || status.ToString() == string.Empty || status.ToString() == "null")
                    {
                        status = "";
                    }
                    //if (itemcode.ToString() == "0" || itemcode.ToString() == "" || itemcode.ToString() == string.Empty || itemcode.ToString() == null)
                    //{
                    itemcode = "";
                    // }
                    if (usertype.ToString() == "0" || usertype.ToString() == "" || usertype.ToString() == string.Empty || usertype.ToString() == "null" || usertype.ToString() == null)
                    {
                        usertype = "";
                    }
                    if (userid.ToString() == "0" || userid.ToString() == "" || userid.ToString() == string.Empty || userid.ToString() == null)
                    {
                        userid = "0";
                    }
                    //if (pageIndex.ToString() == "0" || pageIndex.ToString() == "" || pageIndex.ToString() == string.Empty || pageIndex.ToString() == null)
                    //{
                    pageIndex = 0;
                    // }
                    //if (sortExpression.ToString() == "0" || sortExpression.ToString() == "" || sortExpression.ToString() == string.Empty || sortExpression.ToString() == null)
                    //{
                    sortExpression = "po_id desc";
                    //}
                    //if (alphaname.ToString() == "0" || alphaname.ToString() == "" || alphaname.ToString() == string.Empty || alphaname.ToString() == null)
                    //{
                    alphaname = "";
                    //}
                    //if (pageSize.ToString() == "0" || pageSize.ToString() == "" || pageSize.ToString() == string.Empty || pageSize.ToString() == null)
                    //{
                    pageSize = 25;
                    // }

                    //if (prscode.ToString() == "0" || prscode.ToString() == "" || prscode.ToString() == string.Empty || prscode.ToString() == null)
                    //{
                    prscode = "";
                    //}
                    // if (UserId.ToString() == "0" || UserId.ToString() == "" || UserId.ToString() == string.Empty || UserId.ToString() == "null")
                    // {
                    //   UserId = "";
                    //}


                    string Logdata1 = string.Empty;



                   // string sql = "ERP_PO_ORDER_SUMMARY";

                    string sql = "ERP_PO_ORDER_SUMMARY";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BRANCHID", strbranch);
                    cmd.Parameters.AddWithValue("@FUNCTIONID", function);
                    cmd.Parameters.AddWithValue("@PONUMBER", ponumber);
                    cmd.Parameters.AddWithValue("@VENDORCODE", vendorcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", fromdate1);
                    cmd.Parameters.AddWithValue("@TODATE", todate1);
                    cmd.Parameters.AddWithValue("@STATUS", status);
                    cmd.Parameters.AddWithValue("@ITEMCODE", itemcode);
                    cmd.Parameters.AddWithValue("@usertype", usertype);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", pageIndex);
                    cmd.Parameters.AddWithValue("@PAGESIZE", pageSize);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", sortExpression);
                    cmd.Parameters.AddWithValue("@ALPHANAME", alphaname);
                    cmd.Parameters.AddWithValue("@PRSCODE", prscode);
                    //cmd.Parameters.AddWithValue("@userid ", UserId);
                    //cmd.Parameters.AddWithValue("@struserType", struserType)
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    //cmd.Parameters.AddWithValue("@type", "CP");

                    //cmd.ExecuteNonQuery();

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

        #endregion


        [HttpGet]
        [Route("get_Quotation_Items/{Po_id}/{item_code}")]
        public string get_Quotation_Items(string Po_id, string item_code)
        {


            string Logdata1 = string.Empty;
            string po_id = Po_id;
            string itemcode1 = string.Empty;
            string strPrsCode1 = string.Empty;
            string strPrsCode = string.Empty;
            string strFunction = "1";
            //  var Po_id = Request.QueryString["PRSID"];
            if (item_code != "" && item_code != null)
            {
                itemcode1 = item_code;
                string[] codes = itemcode1.Split('~');
                itemcode1 = codes[0].ToString();
            }
            else
            {
                itemcode1 = item_code;
            }
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                if (itemcode1 == "Service" || itemcode1 == "ServiceS")
                {
                    string item_id = "0";// objCommon.getString("select item_id from ERP_Item_master where item_code='" + item_code + "'");
                                         // string strPrsCode1 = objCommon.getString("select rfq_id from ERP_RFQ_ITEMS_DELIVERY_DETAILS where po_id='" + Po_id + "'");

                    string query = "select rfq_id from ERP_RFQ_ITEMS_DELIVERY_DETAILS where po_id='" + po_id + "'";

                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        strPrsCode1 = row[0].ToString();
                    }





                    if (strPrsCode1 != "")
                    {

                        string query1 = "select prs_id from ERP_PRS_DETAILS where rfqid='" + strPrsCode1 + "'";

                        SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                        var reader1 = cmd1.ExecuteReader();
                        System.Data.DataTable results1 = new System.Data.DataTable();
                        results1.Load(reader1);

                        for (int i = 0; i < results1.Rows.Count; i++)
                        {
                            DataRow row = results1.Rows[i];
                            strPrsCode = row[0].ToString();
                        }



                        //string strPrsCode = objCommon.getString("select prs_id from ERP_PRS_DETAILS where rfqid='" + strPrsCode1 + "'");

                        dstSubDelivery = getSubDelShow1(strFunction, Po_id, item_id, item_code, strPrsCode);
                        // ViewState["VCTDetails"] = dstSubDelivery;
                    }
                    
                }
                else
                {
                    string item_id = "0";

                    string query1 = "select item_id from ERP_Item_master where item_code='" + itemcode1 + "' and status='A'";

                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);

                    for (int i = 0; i < results1.Rows.Count; i++)
                    {
                        DataRow row = results1.Rows[i];
                        item_id = row[0].ToString();
                    }



                    string query2 = "select rfq_id from ERP_RFQ_ITEMS_DELIVERY_DETAILS where po_id='" + Po_id + "'";

                    SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                    var reader2 = cmd2.ExecuteReader();
                    System.Data.DataTable results2 = new System.Data.DataTable();
                    results2.Load(reader1);

                    for (int i = 0; i < results2.Rows.Count; i++)
                    {
                        DataRow row = results2.Rows[i];
                        strPrsCode1 = row[0].ToString();
                    }





                    //string item_id = objCommon.getString("select item_id from ERP_Item_master where item_code='" + item_code + "'");
                    //string strPrsCode1 = objCommon.getString("select rfq_id from ERP_RFQ_ITEMS_DELIVERY_DETAILS where po_id='" + Po_id + "'");
                    if (strPrsCode1 != "")
                    {
                        string query3 = "select prs_id from ERP_PRS_DETAILS where rfqid='" + strPrsCode1 + "'";

                        SqlCommand cmd3 = new SqlCommand(query1, dbConn);
                        var reader3 = cmd3.ExecuteReader();
                        System.Data.DataTable results3 = new System.Data.DataTable();
                        results3.Load(reader3);

                        for (int i = 0; i < results3.Rows.Count; i++)
                        {
                            DataRow row = results3.Rows[i];
                            strPrsCode = row[0].ToString();
                        }



                        //string strPrsCode = objCommon.getString("select prs_id from ERP_PRS_DETAILS where rfqid='" + strPrsCode1 + "'");


                        //  ViewState["VCTDetails"] = dstSubDelivery;
                    }
                    dstSubDelivery = getSubDelShow1(strFunction, Po_id, item_id, itemcode1, strPrsCode);
                }
                DataTable firstTable = dstSubDelivery.Tables[0];


                Logdata1 = DataTableToJSONWithStringBuilder(firstTable);
            }




            return (Logdata1);

        }












        public DataSet getSubDelShow1(string functionid, string Po_Id, string ItemId, string item_code, string strPrsCode)
        {
            try
            {
                string sqlpo = "";
                DataSet dspo = new DataSet();
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {

                    if (ItemId != "0" && ItemId != "")
                    {
                        //sqlpo = "select DISTINCT PSUB.Address,PSUB.po_subdelvery_id,convert(int,ERP_PO_DETAILS.required_qty) as required_qty,ERP_PO_DETAILS.Amount,ERP_VENDOR_MASTER.VENDOR_NAME,ERP_PO_MASTER.po_number, PSUB.FUNCTION_ID ,PSUB.po_id ,ERP_RFQ_MASTER.RFQID,ERP_RFQ_MASTER.RFQCode,ERP_item_master.item_Code,ERP_item_master.item_short_Desc,ERP_item_master.item_id,convert(int,PSUB.required_qty) as required_qty1,BO_PARAMETER.TEXT AS UOM,convert(varchar(10),PSUB.delivery_before,103) as delivery_before,PSUB.status,PSUB.created_by,PSUB.created_on,PSUB.lst_upd_by,PSUB  .lst_upd_on,PSUB.ipaddress,  PSUB.rfq_id as 'prs_id','' as 'item_desc'  from ERP_PO_ITEMS_DELIVERY_DETAILS PSUB INNER JOIN ERP_PO_MASTER ON ERP_PO_MASTER.PO_ID=PSUB.PO_ID inner join ERP_PO_DETAILS with(nolock) on ERP_PO_DETAILS.po_id=ERP_PO_MASTER.po_id INNER JOIN ERP_item_master WITH (NOLOCK)  ON PSUB.function_id=ERP_item_master.function_id AND  PSUB.item_id=ERP_item_master.item_id and ERP_PO_DETAILS.ITEM_ID= PSUB.item_id LEFT join ERP_VENDOR_MASTER with(nolock) on ERP_VENDOR_MASTER.vendor_id=ERP_PO_MASTER.vendor_id  Left Outer JOIN ERP_RFQ_MASTER WITH(NOLOCK)  ON  PSUB.function_id=ERP_RFQ_MASTER.function_id AND  PSUB.rfq_id=ERP_RFQ_MASTER.RFQID  INNER JOIN BO_PARAMETER  WITH(NOLOCK) ON  PSUB.function_id=BO_PARAMETER.function_id AND  PSUB.UOM=BO_PARAMETER.VAL WHERE  BO_PARAMETER.TYPE='UOM'   and PSUB.FUNCTION_ID='" + functionid + "' and PSUB.po_id='" + Po_Id + "' and PSUB.item_id='" + ItemId + "' and PSUB.item_detailed_description='" + Hf_item_desc.Value + "' and PSUB.STATUS='A'";

                        sqlpo = "select DISTINCT PSUB.UOM,PSUB.Address,PSUB.RFQ_subdelvery_id,convert(int, ERP_RFQ_DETAILS.QUANTITY) as required_qty,'0' AS Amount, '' AS VENDOR_NAME,ERP_RFQ_MASTER.RFQCode AS 'po_number', ERP_RFQ_MASTER.FUNCTION_ID ,ERP_RFQ_MASTER.RFQID AS 'po_id' ,ERP_RFQ_MASTER.RFQID,ERP_RFQ_MASTER.RFQCode,                ERP_item_master.item_Code,ERP_item_master.item_short_Desc,ERP_item_master.item_id,convert(int, PSUB.required_qty) as required_qty1,convert(varchar(10), PSUB.delivery_before, 103) as delivery_before,PSUB.status,PSUB.created_by,PSUB.created_on,                PSUB.lst_upd_by,PSUB.lst_upd_on,PSUB.ipaddress,  PSUB.rfq_id as 'prs_id','' as 'item_desc'  , erp_prs_master.prs_code as 'prscode'  from ERP_RFQ_MASTER                LEFT JOIN ERP_RFQ_DETAILS WITH(NOLOCK)  ON ERP_RFQ_DETAILS.function_id = ERP_RFQ_MASTER.function_id AND ERP_RFQ_DETAILS.RFQID = ERP_RFQ_MASTER.RFQID                LEFT JOIN ERP_item_master WITH(NOLOCK)  ON ERP_RFQ_DETAILS.function_id = ERP_item_master.function_id  AND ERP_RFQ_DETAILS.itemid = ERP_item_master.item_id  INNER JOIN  ERP_RFQ_ITEMS_DELIVERY_DETAILS PSUB WITH(NOLOCK)  ON PSUB.function_id = ERP_RFQ_MASTER.function_id AND PSUB.po_id = ERP_RFQ_MASTER.RFQID AND PSUB.function_id = ERP_item_master.function_id AND PSUB.item_id = ERP_item_master.item_id   and PSUB.STATUS = 'A' left join erp_prs_details WITH(NOLOCK) on erp_prs_details.rfqid = PSUB.rfq_id left join erp_prs_master WITH(NOLOCK) on erp_prs_details.prs_id=erp_prs_master.prs_id  WHERE 1 = 1   and ERP_RFQ_MASTER.FUNCTION_ID = '" + functionid + "'  and ERP_RFQ_DETAILS.rfqid = '" + Po_Id + "'  and ERP_RFQ_DETAILS.itemid = '" + ItemId + "'";




                    }
                    else
                    {
                        if (ItemId == null || ItemId == string.Empty || ItemId == "")
                        {
                            ItemId = "0";
                        }
                        //sqlpo = "select DISTINCT PSUB.Address,PSUB.po_subdelvery_id,convert(int,ERP_PO_DETAILS.required_qty) as required_qty,ERP_PO_DETAILS.Amount,ERP_VENDOR_MASTER.VENDOR_NAME,ERP_PO_MASTER.po_number, PSUB.FUNCTION_ID ,PSUB.po_id ,ERP_RFQ_MASTER.RFQID,ERP_RFQ_MASTER.RFQCode,ERP_item_master.item_Code,ERP_item_master.item_short_Desc,ERP_item_master.item_id,convert(int,PSUB.required_qty) as required_qty1,BO_PARAMETER.TEXT AS UOM,convert(varchar(10),PSUB.delivery_before,103) as delivery_before,PSUB.status,PSUB.created_by,PSUB.created_on,PSUB.lst_upd_by,PSUB  .lst_upd_on,PSUB.ipaddress,  '' as 'prs_id','' as 'item_desc'  from ERP_PO_ITEMS_DELIVERY_DETAILS PSUB INNER JOIN ERP_PO_MASTER ON ERP_PO_MASTER.PO_ID=PSUB.PO_ID LEFT  join ERP_PO_DETAILS with(nolock) on ERP_PO_DETAILS.po_id=ERP_PO_MASTER.po_id and ERP_PO_DETAILS.item_id=PSUB.item_id and ERP_PO_DETAILS.item_detailed_description=PSUB.item_detailed_description LEFT JOIN ERP_item_master WITH (NOLOCK)  ON PSUB.function_id=ERP_item_master.function_id AND  PSUB.item_id=ERP_item_master.item_id LEFT join ERP_VENDOR_MASTER with(nolock) on ERP_VENDOR_MASTER.vendor_id=ERP_PO_MASTER.vendor_id  Left Outer JOIN ERP_RFQ_MASTER WITH(NOLOCK)  ON  PSUB.function_id=ERP_RFQ_MASTER.function_id AND  PSUB.rfq_id=ERP_RFQ_MASTER.RFQID  INNER JOIN BO_PARAMETER  WITH(NOLOCK) ON  PSUB.function_id=BO_PARAMETER.function_id AND  PSUB.UOM=BO_PARAMETER.VAL WHERE  BO_PARAMETER.TYPE='UOM'  and PSUB.FUNCTION_ID='" + functionid + "' and PSUB.po_id='" + Po_Id + "' and PSUB.item_id='" + ItemId + "' and PSUB.item_detailed_description='" + Hf_item_desc.Value + "' and PSUB.STATUS='A'";

                        sqlpo = "select DISTINCT PSUB.Address,PSUB.RFQ_subdelvery_id,convert(int, ERP_RFQ_DETAILS.QUANTITY) as required_qty,'0' AS Amount, '' AS VENDOR_NAME, ERP_RFQ_MASTER.RFQCode AS 'po_number', ERP_RFQ_MASTER.FUNCTION_ID ,ERP_RFQ_MASTER.RFQID AS 'po_id' ,ERP_RFQ_MASTER.RFQID,ERP_RFQ_MASTER.RFQCode,                ERP_item_master.item_Code,ERP_item_master.item_short_Desc,ERP_item_master.item_id,convert(int, PSUB.required_qty) as required_qty1,                BO_PARAMETER.TEXT AS UOM,convert(varchar(10), PSUB.delivery_before, 103) as delivery_before,PSUB.status,PSUB.created_by,PSUB.created_on,                PSUB.lst_upd_by,PSUB.lst_upd_on,PSUB.ipaddress,  PSUB.rfq_id as 'prs_id','' as 'item_desc', erp_prs_master.prs_code as 'prscode'   from ERP_RFQ_MASTER                LEFT JOIN ERP_RFQ_DETAILS WITH(NOLOCK)  ON ERP_RFQ_DETAILS.function_id = ERP_RFQ_MASTER.function_id AND ERP_RFQ_DETAILS.RFQID = ERP_RFQ_MASTER.RFQID                LEFT JOIN ERP_item_master WITH(NOLOCK)  ON ERP_RFQ_DETAILS.function_id = ERP_item_master.function_id AND ERP_RFQ_DETAILS.itemid = ERP_item_master.item_id                LEFT JOIN  ERP_RFQ_ITEMS_DELIVERY_DETAILS PSUB WITH(NOLOCK)  ON PSUB.function_id = ERP_RFQ_MASTER.function_id AND PSUB.rfq_id = ERP_RFQ_MASTER.RFQID                                 and PSUB.STATUS = 'A' left join erp_prs_details WITH(NOLOCK) on erp_prs_details.rfqid = PSUB.rfq_id left join erp_prs_master WITH(NOLOCK) on erp_prs_details.prs_id=erp_prs_master.prs_id                LEFT JOIN BO_PARAMETER WITH(NOLOCK) ON PSUB.function_id = BO_PARAMETER.function_id AND PSUB.UOM = BO_PARAMETER.VAL AND BO_PARAMETER.TYPE = 'UOM'                WHERE 1 = 1   and ERP_RFQ_MASTER.FUNCTION_ID = '" + functionid + "'  and ERP_RFQ_DETAILS.rfqid = '" + Po_Id + "'  and ERP_RFQ_DETAILS.itemid = '" + ItemId + "'";
                        //AND PSUB.function_id = ERP_item_master.function_id AND PSUB.item_id = ERP_item_master.item_id
                        //and PSUB.item_detailed_description = '" + Hf_item_desc.Value + "'
                    }


                    SqlDataAdapter objSda = new SqlDataAdapter(sqlpo, dbConn);
                    // objSda.SelectCommand.CommandTimeout = 1000;
                    objSda.Fill(dspo);


                    return dspo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }



        //Raise RFQ _RFQ COde Generate[Increement]

        [HttpGet]
        [Route("getraiserfq")]
        public string getraiserfq()
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";

                query = " SELECT MAX(RFQID)+1 FROM ERP_RFQ_MASTER  INNER JOIN USERACCESS WITH(NOLOCK) ON USERACCESS.FUNCTION_ID = ERP_RFQ_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID = ERP_RFQ_MASTER.CREATED_BY";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();
                return Logdata1;
            }
        }


        //Raised RFQ Details viewed


        [HttpPost]
        [Route("RaisedRFQDetails")]
        public async Task<ActionResult<ERP>> RaisedRFQDetails(ERP data)
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

                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "";
                    }
                    if (data.itemcode.ToString() == "0" || data.itemcode.ToString() == "" || data.itemcode.ToString() == string.Empty || data.itemcode.ToString() == null)
                    {
                        data.itemcode = "";
                    }

                    //if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    //{
                    //  //  data.reuestdate = "0";
                    //    data.reuestdate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.reuestdate + "', 103),103)";
                    //}




                    if (data.rfqcode.ToString() == "0" || data.rfqcode.ToString() == "" || data.rfqcode.ToString() == string.Empty || data.rfqcode.ToString() == null)
                    {
                        data.rfqcode = "";
                    }

                    //if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    //{
                    //    data.fromdate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.fromdate + "', 103),103)";
                    //   // data.fromdate = "0";
                    //}
                    //if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    //{
                    //   // data.todate = "0";
                    //    data.todate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.todate + "', 103),103)";

                    //}
                    //if (data.rfqfromdate.ToString() == "0" || data.rfqfromdate.ToString() == "" || data.rfqfromdate.ToString() == string.Empty || data.rfqfromdate.ToString() == null)
                    //{

                    //          data.rfqfromdate = "created_on >= CONVERT(varchar, convert(datetime,'" + data.rfqfromdate + "', 103),103)";

                    //  //  data.rfqfromdate = "0";
                    //}
                    //if (data.rfqtodate.ToString() == "0" || data.rfqtodate.ToString() == "" || data.rfqtodate.ToString() == string.Empty || data.rfqtodate.ToString() == null)
                    //{
                    //    // data.rfqtodate = "0";

                    //    data.rfqtodate = "created_on >= CONVERT(varchar, convert(datetime,'" + data.rfqtodate + "', 103),103)";
                    //}

                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "";
                    }

                    if (data.mode.ToString() == "0" || data.mode.ToString() == "" || data.mode.ToString() == string.Empty || data.mode.ToString() == null)
                    {
                        data.mode = "";
                    }
                    if (data.pageindex1.ToString() == "0" || data.pageindex1.ToString() == "" || data.pageindex1.ToString() == string.Empty || data.pageindex1.ToString() == null)
                    {
                        data.pageindex1 = 0;
                    }
                    if (data.pagesize1.ToString() == "0" || data.pagesize1.ToString() == "" || data.pagesize1.ToString() == string.Empty || data.pagesize1.ToString() == null)
                    {
                        data.pagesize1 = 0;
                    }
                    if (data.alphaname.ToString() == "0" || data.alphaname.ToString() == "" || data.alphaname.ToString() == string.Empty || data.alphaname.ToString() == null)
                    {
                        data.alphaname = "0";
                    }



                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                   // string sql = "MBLE_ERP_RFQ_getvendorevaluationRFQs";

                    string sql = "ERP_RFQ_getvendorevaluationRFQs";

                    // String sql = "ERP_RFQ_getvendorevaluationRFQ";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    // EXEC ERP_RFQ_getvendorevaluationRFQs '1','','','','','','','','','RFQ Raised','2','0','20','prs_id DESC','','286','1'


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                    cmd.Parameters.AddWithValue("@REQUESTED_DATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);
                    cmd.Parameters.AddWithValue("@RFQFromDate", data.rfqfromdate);
                    cmd.Parameters.AddWithValue("@RFQToDate", data.rfqtodate);
                    //cmd.Parameters.AddWithValue("@STATUS", "RFQ Raised");
                    //cmd.Parameters.AddWithValue("@MODE", "2");
                    //cmd.Parameters.AddWithValue("@PAGEINDEX","0");
                    //cmd.Parameters.AddWithValue("@PAGESIZE", "20");
                    //cmd.Parameters.AddWithValue("@SORTEXPRESSION", "prs_id DESC");

                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@MODE",data.mode);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE",data.pagesize1);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);
                    cmd.Parameters.AddWithValue("@ALPHANAME", "");
                    cmd.Parameters.AddWithValue("@UserId", data.userid);
                    cmd.Parameters.AddWithValue("@UserType", "1");


                  //  cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                        //logdata = DataTableToJSONWithStringBuilder(results);

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


        //CancelledRFQ List




        [HttpPost]
        [Route("CancelledRFQDetails")]
        public async Task<ActionResult<ERP>> CancelledRFQDetails(ERP data)
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

                    if (data.prscode.ToString() == "0" || data.prscode.ToString() == "" || data.prscode.ToString() == string.Empty || data.prscode.ToString() == null)
                    {
                        data.prscode = "0";
                    }
                    if (data.itemcode.ToString() == "0" || data.itemcode.ToString() == "" || data.itemcode.ToString() == string.Empty || data.itemcode.ToString() == null)
                    {
                        data.itemcode = "0";
                    }

                    //if (data.reuestdate.ToString() == "0" || data.reuestdate.ToString() == "" || data.reuestdate.ToString() == string.Empty || data.reuestdate.ToString() == null)
                    //{
                    //  //  data.reuestdate = "0";
                    //    data.reuestdate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.reuestdate + "', 103),103)";
                    //}




                    if (data.rfqcode.ToString() == "0" || data.rfqcode.ToString() == "" || data.rfqcode.ToString() == string.Empty || data.rfqcode.ToString() == null)
                    {
                        data.rfqcode = "0";
                    }

                    //if (data.fromdate.ToString() == "0" || data.fromdate.ToString() == "" || data.fromdate.ToString() == string.Empty || data.fromdate.ToString() == null)
                    //{
                    //    data.fromdate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.fromdate + "', 103),103)";
                    //   // data.fromdate = "0";
                    //}
                    //if (data.todate.ToString() == "0" || data.todate.ToString() == "" || data.todate.ToString() == string.Empty || data.todate.ToString() == null)
                    //{
                    //   // data.todate = "0";
                    //    data.todate = "requested_Date >= CONVERT(varchar, convert(datetime,'" + data.todate + "', 103),103)";

                    //}

                    if (data.status.ToString() == "0" || data.status.ToString() == "" || data.status.ToString() == string.Empty || data.status.ToString() == null)
                    {
                        data.status = "0";
                    }

                    if (data.mode.ToString() == "0" || data.mode.ToString() == "" || data.mode.ToString() == string.Empty || data.mode.ToString() == null)
                    {
                        data.mode = "0";
                    }
                    if (data.pageindex1.ToString() == "0" || data.pageindex1.ToString() == "" || data.pageindex1.ToString() == string.Empty || data.pageindex1.ToString() == null)
                    {
                        data.pageindex1 = 0;
                    }
                    if (data.pagesize1.ToString() == "0" || data.pagesize1.ToString() == "" || data.pagesize1.ToString() == string.Empty || data.pagesize1.ToString() == null)
                    {
                        data.pagesize1 = 0;
                    }



                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                   // string sql = "MBLE_ERP_RFQ_GETCANCEL";
                    string sql = "ERP_RFQ_GETCANCEL";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);

                    // EXEC ERP_RFQ_GETCANCEL '1','','','','','','','A','2',0,20
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                    cmd.Parameters.AddWithValue("@PRSCODE", data.prscode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.itemcode);
                    cmd.Parameters.AddWithValue("@REQUESTED_DATE", data.reuestdate);
                    cmd.Parameters.AddWithValue("@RFQCODE", data.rfqcode);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", data.todate);

                    cmd.Parameters.AddWithValue("@STATUS", data.status);
                    cmd.Parameters.AddWithValue("@MODE", data.mode);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.pageindex1);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.pagesize1);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.sortexpression);



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



        //My approval type selection Dropdown

        [HttpGet]
        [Route("RequestType")]
        public string RequestType(string prefixText, string sunctionid)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select WF_CONFIG_DESC,WF_CONFIG_ID  from bo_workflow_configurations where WF_CONFIG_DESC in('Asset Transfer','Asset Service','ERP Purchase Request','ERP PO Request','ERP Confirm Vendor','Tendor','Supplier Registration','ERP Purchase Payment Invoice','Inter Location Transfer1','ERP MRS','Budget Approval','Asset Request') and FUNCTION_ID ='" + sunctionid + "' and status='A'";

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


        //My approval screen search

        //getMailBoxHistory in old

        [HttpGet]
        [Route("myapprovalsearch")]
        public string myapprovalsearch(string strFunction, string strConfigId, string Username, string strWorkFlowNo, string strFromDate, string strToDate, string strWFstatus, string strMode, string strUserId, string strusertype)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string dataval = "";
                //DataTable _dtBaseQuery = new DataTable();
                string strQuery = "";
                string[] _query = null;
                DataSet ds = new DataSet();
                string QuerySecond = string.Empty;
                string PKColumn = string.Empty;
                try
                {


                    if (strFunction.ToString() == "0" || strFunction.ToString() == "" || strFunction.ToString() == string.Empty || strFunction.ToString() == "null")
                    {
                        strFunction = "";
                    }
                    if (strConfigId.ToString() == "0" || strConfigId.ToString() == "" || strConfigId.ToString() == string.Empty || strConfigId.ToString() == "null")
                    {
                        strConfigId = "";
                    }
                    if (Username.ToString() == "0" || Username.ToString() == "" || Username.ToString() == string.Empty || Username.ToString() == "null")
                    {
                        Username = "";
                    }
                    if (strWorkFlowNo.ToString() == "0" || strWorkFlowNo == "" || strWorkFlowNo.ToString() == string.Empty || strWorkFlowNo.ToString() == "null")
                    {
                        strWorkFlowNo = "";
                    }
                    if (strFromDate == "0" || strFromDate == "" || strFromDate == string.Empty || strFromDate.ToString() == "null")
                    {
                        strFromDate = "";
                    }
                    if (strToDate.ToString() == "0" || strToDate.ToString() == "" || strToDate.ToString() == string.Empty || strToDate.ToString() =="null")
                    {
                        strToDate = "";
                    }
                    if (strWFstatus.ToString() == "0" || strWFstatus.ToString() == "" || strWFstatus.ToString() == string.Empty || strWFstatus.ToString() == "null")
                    {
                        strWFstatus = "";
                    }
                    if (strMode.ToString() == "0" || strMode.ToString() == "" || strMode.ToString() == string.Empty || strMode.ToString() == "null")
                    {
                        strMode = "";
                    }
                    if (strUserId.ToString() == "0" || strUserId.ToString() == "" || strUserId.ToString() == string.Empty || strUserId.ToString() == "null")
                    {
                        strUserId = "";

                    }
                    if (strusertype.ToString() == "0" || strusertype.ToString() == "" || strusertype.ToString() == string.Empty || strusertype.ToString() == "null")
                    {
                        strusertype = "";

                    }









                    string query = "select  BASEQUERY,PK_COLUMN_NAME1 from BO_WORKFLOW_CONFIGURATIONS WHERE WF_CONFIG_ID='" + strConfigId + "' and FUNCTION_ID='" + strFunction + "'";

                    SqlCommand cmd1 = new SqlCommand(query, dbConn);
                    var reader = cmd1.ExecuteReader();
                    System.Data.DataTable _dtBaseQuery = new System.Data.DataTable();
                    _dtBaseQuery.Load(reader);
                    if (!string.IsNullOrEmpty(_dtBaseQuery.Rows[0]["BASEQUERY"].ToString()))
                    {
                        _query = _dtBaseQuery.Rows[0]["BASEQUERY"].ToString().ToUpper().Split(new string[] { "FROM" }, StringSplitOptions.None);
                        PKColumn = _dtBaseQuery.Rows[0]["PK_COLUMN_NAME1"].ToString();
                    }

                    strQuery = _query == null || string.IsNullOrEmpty(_query[0].Trim()) ? "set dateformat dmy;select distinct" : "set dateformat dmy;" + _query[0] + ",";

                    if (_query != null && _query.Length > 1)
                    {
                        QuerySecond = _query[1].Trim().ToString();
                        if (!strQuery.ToString().ToUpper().Contains("SELECT DISTINCT"))
                        {
                            strQuery = strQuery.ToString().ToUpper().Replace("SELECT", "SELECT DISTINCT");
                        }
                    }
                    else
                    {
                        strQuery = "";
                        QuerySecond = "";
                        PKColumn = "";
                    }
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo; // sudish
                    strQuery = strQuery.ToString();
                    strQuery = textInfo.ToTitleCase(strQuery.ToLower());
                    string quer = "BO_WORKFLOW_GETAPPROVAL_DETAILS";
                    SqlCommand cmd = new SqlCommand(quer, dbConn);



                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@strFunction", strFunction);
                    cmd.Parameters.AddWithValue("@strConfigId", strConfigId);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@strWorkFlowNo", strWorkFlowNo);
                    cmd.Parameters.AddWithValue("@strFromDate", strFromDate);
                    cmd.Parameters.AddWithValue("@strToDate", strToDate);
                    cmd.Parameters.AddWithValue("@strWFstatus", strWFstatus);
                    cmd.Parameters.AddWithValue("@strMode", strMode);
                    cmd.Parameters.AddWithValue("@strUserId", strUserId);
                    cmd.Parameters.AddWithValue("@strusertype", strusertype);
                    cmd.Parameters.AddWithValue("@Query", strQuery);
                    cmd.Parameters.AddWithValue("@QuerySecond", QuerySecond);
                    cmd.Parameters.AddWithValue("@PkColumn", PKColumn);


                    cmd.ExecuteNonQuery();
                    var reader1 = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader1);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);


                        dbConn.Close();
                    }
                    //var result = (new { logdata });
                    return Logdata1;










                    //SqlParameter strFunctionid;
                    //strFunctionid = cmd.Parameters.Add("@strFunction", SqlDbType.VarChar);
                    //strFunctionid.Value = strFunction;
                    //strFunctionid.Direction = ParameterDirection.Input;


                    //SqlParameter strConfigIdval;
                    //strConfigIdval = cmd.Parameters.Add("@strConfigId", SqlDbType.VarChar);
                    //strConfigIdval.Value = strConfigId;
                    //strConfigIdval.Direction = ParameterDirection.Input;


                    //SqlParameter Usernameval;
                    //Usernameval = cmd.Parameters.Add("@Username", SqlDbType.VarChar);
                    //Usernameval.Value = Username;
                    //Usernameval.Direction = ParameterDirection.Input;


                    //SqlParameter strWorkFlowNoval;
                    //strWorkFlowNoval = cmd.Parameters.Add("@strWorkFlowNo", SqlDbType.VarChar);
                    //strWorkFlowNoval.Value = strWorkFlowNo;
                    //strWorkFlowNoval.Direction = ParameterDirection.Input;


                    //SqlParameter strFromDateval;
                    //strFromDateval = cmd.Parameters.Add("@strFromDate", SqlDbType.VarChar);
                    //strFromDateval.Value = strFromDate;
                    //strFromDateval.Direction = ParameterDirection.Input;


                    //SqlParameter strToDateval;
                    //strToDateval = cmd.Parameters.Add("@strToDate", SqlDbType.VarChar);
                    //strToDateval.Value = strToDate;
                    //strToDateval.Direction = ParameterDirection.Input;


                    //SqlParameter strWFstatusval;
                    //strWFstatusval = cmd.Parameters.Add("@strWFstatus", SqlDbType.VarChar);
                    //strWFstatusval.Value = strWFstatus;
                    //strWFstatusval.Direction = ParameterDirection.Input;


                    //SqlParameter strModeval;
                    //strModeval = cmd.Parameters.Add("@strMode", SqlDbType.VarChar);
                    //strModeval.Value = strMode;
                    //strModeval.Direction = ParameterDirection.Input;


                    //SqlParameter strUserIdval;
                    //strUserIdval = cmd.Parameters.Add("@strUserId", SqlDbType.VarChar);
                    //strUserIdval.Value = strUserId;
                    //strUserIdval.Direction = ParameterDirection.Input;


                    //SqlParameter strusertypeval;
                    //strusertypeval = cmd.Parameters.Add("@strusertype", SqlDbType.VarChar);
                    //strusertypeval.Value = strusertype;
                    //strusertypeval.Direction = ParameterDirection.Input;

                    //SqlParameter strquerys;
                    //strquerys = cmd.Parameters.Add("@Query", SqlDbType.VarChar);
                    //strquerys.Value = strQuery;
                    //strquerys.Direction = ParameterDirection.Input;


                    //SqlParameter strquerysecond;
                    //strquerysecond = cmd.Parameters.Add("@QuerySecond", SqlDbType.VarChar);
                    //strquerysecond.Value = QuerySecond;
                    //strquerysecond.Direction = ParameterDirection.Input;


                    //SqlParameter strpkcolumn;
                    //strpkcolumn = cmd.Parameters.Add("@PkColumn", SqlDbType.VarChar);
                    //strpkcolumn.Value = PKColumn;
                    //strpkcolumn.Direction = ParameterDirection.Input;

                    ////cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.ExecuteNonQuery();

                    //var reader1 = cmd.ExecuteReader();
                    //System.Data.DataTable results = new System.Data.DataTable();
                    //results.Load(reader1);
                    ////string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    //for (int i = 0; i < results.Rows.Count; i++)
                    //{
                    //    DataRow row = results.Rows[i];
                    //    Logdata1 = DataTableToJSONWithStringBuilder(results);


                    //    dbConn.Close();
                    //}


                }
                catch (Exception ex)
                {
                    Logdata1 = ex.ToString();
                }
            }

            return Logdata1;
        }




        //sowmi-01/11/22

        [HttpGet]
        [Route("GETISSUESUMMARY")]
        public string GETISSUESUMMARY(string functionID, string branchID, string itemCode, string itemRef, string srRef, string iltRef, string fromdate, string todate, string status, string alphaname, string sortExpression, int pageIndex, int pageSize, string searchType)

        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();



                    if (functionID.ToString() == "0" || functionID.ToString() == "" || functionID.ToString() == string.Empty || functionID.ToString() == null)
                    {
                        functionID = "";
                    }
                    if (branchID.ToString() == "0" || branchID.ToString() == "" || branchID.ToString() == string.Empty || branchID.ToString() == null)
                    {
                        branchID = "";
                    }
                    if (itemCode.ToString() == "0" || itemCode.ToString() == "" || itemCode.ToString() == string.Empty || itemCode.ToString() == "null")
                    {
                        itemCode = "";
                    }
                    if (itemRef.ToString() == "0" || itemRef.ToString() == "" || itemRef.ToString() == string.Empty || itemRef.ToString() == "null")
                    {
                        itemRef = "";
                    }
                    if (iltRef.ToString() == "0" || iltRef.ToString() == "" || iltRef.ToString() == string.Empty || iltRef.ToString() == "null")
                    {
                        iltRef = "";
                    }
                    //if ( fromdate.ToString() == "")
                    //{
                    fromdate = "";
                    //}
                    if (todate.ToString() == "0" || todate.ToString() == "" || todate.ToString() == string.Empty || todate.ToString() == "null")
                    {
                        todate = "";
                    }
                    // if (srRef.ToString() == "0" || srRef.ToString() == "" || srRef.ToString() == string.Empty || srRef.ToString() == "null")
                    //{
                    srRef = "";
                    //}
                    //if (itemcode.ToString() == "0" || itemcode.ToString() == "" || itemcode.ToString() == string.Empty || itemcode.ToString() == null)
                    //{
                    // itemcode = "";
                    // }
                    if (status.ToString() == "0" || status.ToString() == "" || status.ToString() == string.Empty || status.ToString() == "null" || status.ToString() == null)
                    {
                        status = "";
                    }
                    //if (searchType.ToString() == "0" || searchType.ToString() == "" || searchType.ToString() == string.Empty || searchType.ToString() == null)
                    {
                        searchType = "0";
                    }
                    //if (pageIndex.ToString() == "0" || pageIndex.ToString() == "" || pageIndex.ToString() == string.Empty || pageIndex.ToString() == null)
                    //{
                    pageIndex = 0;
                    // }
                    //if (sortExpression.ToString() == "0" || sortExpression.ToString() == "" || sortExpression.ToString() == string.Empty || sortExpression.ToString() == null)
                    //{
                    // sortExpression = "";
                    //}
                    //if (alphaname.ToString() == "0" || alphaname.ToString() == "" || alphaname.ToString() == string.Empty || alphaname.ToString() == null)
                    //{
                    alphaname = "";
                    //}
                    //if (pageSize.ToString() == "0" || pageSize.ToString() == "" || pageSize.ToString() == string.Empty || pageSize.ToString() == null)
                    //{
                    pageSize = 25;
                    // }

                    //if (prscode.ToString() == "0" || prscode.ToString() == "" || prscode.ToString() == string.Empty || prscode.ToString() == null)
                    //{
                    //  prscode = "";
                    //}
                    // if (UserId.ToString() == "0" || UserId.ToString() == "" || UserId.ToString() == string.Empty || UserId.ToString() == "null")
                    // {
                    //   UserId = "";
                    //}


                    string Logdata1 = string.Empty;


                    string sql = "ERP_MI_GETISSUESUMMARY";

                   // string sql = "ERP_MI_GETISSUESUMMARY";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);


                    SqlDataAdapter apd = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FUNCTIONID", functionID);
                    cmd.Parameters.AddWithValue("@BRANCHID", branchID);
                    cmd.Parameters.AddWithValue("@ITEM_CODE", itemCode);
                    cmd.Parameters.AddWithValue("@ITEM_REF", itemRef);
                    cmd.Parameters.AddWithValue("@ILT_REF", iltRef);
                    cmd.Parameters.AddWithValue("@SR_REF", srRef);
                    cmd.Parameters.AddWithValue("@FROMDATE", fromdate);
                    cmd.Parameters.AddWithValue("@TODATE", todate);
                    cmd.Parameters.AddWithValue("@STATUS", status);
                    cmd.Parameters.AddWithValue("@ALPHANAME", alphaname);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", "item_short_desc");
                    cmd.Parameters.AddWithValue("@PAGEINDEX", pageIndex);
                    cmd.Parameters.AddWithValue("@PAGESIZE", pageSize);
                    cmd.Parameters.AddWithValue("@SEARCH_TYPE", "searchType");
                    //cmd.Parameters.AddWithValue("@ALPHANAME", alphaname);
                    //cmd.Parameters.AddWithValue("@PRSCODE", prscode);
                    //cmd.Parameters.AddWithValue("@userid ", UserId);
                    //cmd.Parameters.AddWithValue("@struserType", struserType);
                    //cmd.Parameters.AddWithValue("@userid", userid);
                    //cmd.Parameters.AddWithValue("@type", "CP");

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


        //Material Requistion Search by shankari



        [HttpPost]
        [Route("MaterialRewuistionDetails")]
        public async Task<ActionResult<ERP>> MaterialRewuistionDetails(ERP data)
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


                    if (data.FUNCTIONIDM.ToString() == "0" || data.FUNCTIONIDM.ToString() == "" || data.FUNCTIONIDM.ToString() == string.Empty || data.FUNCTIONIDM.ToString() == null || data.FUNCTIONIDM.ToString() == "null")
                    {
                        data.FUNCTIONIDM = "0";
                    }

                    if (data.BRANCHM.ToString() == "0" || data.BRANCHM.ToString() == "" || data.BRANCHM.ToString() == string.Empty || data.BRANCHM.ToString() == null || data.BRANCHM.ToString() == "null")
                    {
                        data.BRANCHM = "";
                    }
                    if (data.MRSCODEM.ToString() == "0" || data.MRSCODEM.ToString() == "" || data.MRSCODEM.ToString() == string.Empty || data.MRSCODEM.ToString() == null || data.MRSCODEM.ToString() == "null")
                    {
                        data.MRSCODEM = "";
                    }




                    if (data.ITEMCODEM.ToString() == "0" || data.ITEMCODEM.ToString() == "" || data.ITEMCODEM.ToString() == string.Empty || data.ITEMCODEM.ToString() == null || data.ITEMCODEM.ToString() == "null" )
                    {
                        data.ITEMCODEM = "";
                    }



                    if (data.DATEFROMM.ToString() == "0" || data.DATEFROMM.ToString() == "" || data.DATEFROMM.ToString() == string.Empty || data.DATEFROMM.ToString() == null || data.DATEFROMM.ToString() == "null")
                    {
                        data.DATEFROMM = "";
                    }

                    if (data.DATETOM.ToString() == "0" || data.DATETOM.ToString() == "" || data.DATETOM.ToString() == string.Empty || data.DATETOM.ToString() == null || data.DATETOM.ToString() == "null")
                    {
                        data.DATETOM = "";
                    }
                    if (data.STATUSM.ToString() == "0" || data.STATUSM.ToString() == "" || data.STATUSM.ToString() == string.Empty || data.STATUSM.ToString() == null || data.STATUSM.ToString() == "null")
                    {
                        data.STATUSM = "";
                    }
                    if (data.CUTSTATUSM.ToString() == "0" || data.CUTSTATUSM.ToString() == "" || data.CUTSTATUSM.ToString() == string.Empty || data.CUTSTATUSM.ToString() == null || data.CUTSTATUSM.ToString() == "null")
                    {
                        data.CUTSTATUSM = "";
                    }
                    if (data.MENUIDM.ToString() == "0" || data.MENUIDM.ToString() == "" || data.MENUIDM.ToString() == string.Empty || data.MENUIDM.ToString() == null || data.MENUIDM.ToString() == "null")
                    {
                        data.MENUIDM = "";
                    }
                    if (data.PAGEINDEXM.ToString() == "0" || data.PAGEINDEXM.ToString() == "" || data.PAGEINDEXM.ToString() == string.Empty || data.PAGEINDEXM.ToString() == null || data.PAGEINDEXM.ToString() == "null")
                    {
                        data.PAGEINDEXM = 0;
                    }
                    if (data.PAGESIZEM.ToString() == "0" || data.PAGESIZEM.ToString() == "" || data.PAGESIZEM.ToString() == string.Empty || data.PAGESIZEM.ToString() == null || data.PAGESIZEM.ToString() == "null")
                    {
                        data.PAGESIZEM = 0;
                    }
                    if (data.SORTEXPRESSIONM.ToString() == "0" || data.SORTEXPRESSIONM.ToString() == "" || data.SORTEXPRESSIONM.ToString() == string.Empty || data.SORTEXPRESSIONM.ToString() == null || data.SORTEXPRESSIONM.ToString() == "null")
                    {
                        data.SORTEXPRESSIONM = "0";
                    }

                    if (data.ALPHANAMEM.ToString() == "0" || data.ALPHANAMEM.ToString() == "" || data.ALPHANAMEM.ToString() == string.Empty || data.ALPHANAMEM.ToString() == null|| data.ALPHANAMEM.ToString() == "null")
                    {
                        data.ALPHANAMEM = "";
                    }
                    if (data.USERTYPEM.ToString() == "0" || data.USERTYPEM.ToString() == "" || data.USERTYPEM.ToString() == string.Empty || data.USERTYPEM.ToString() == null || data.USERTYPEM.ToString() == "null")
                    {
                        data.USERTYPEM = "";
                    }
                    if (data.USERIDM.ToString() == "0" || data.USERIDM.ToString() == "" || data.USERIDM.ToString() == string.Empty || data.USERIDM.ToString() == null || data.USERIDM.ToString() == "null")
                    {
                        data.USERIDM = "";
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    //string sql = "MBLE_ERP_MRS_GET_SUMMARYDATA"

                         string sql = "ERP_MRS_GET_SUMMARYDATA";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    // exec ERP_MRS_GET_SUMMARYDATA @FUNCTIONID='1',@BRANCH='1',@MRSCODE='',@ITEMCODE='',@DATEFROM='',@DATETO='',@STATUS='0',@CUTSTATUS='<<Select>>',@MENUID='',@PAGEINDEX='0',@PAGESIZE='10',@SORTEXPRESSION='mrs_code',@ALPHANAME='',@USERTYPE='1',@USERID='286'


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.FUNCTIONIDM);
                    cmd.Parameters.AddWithValue("@BRANCH", data.BRANCHM);
                    cmd.Parameters.AddWithValue("@MRSCODE", data.MRSCODEM);
                    cmd.Parameters.AddWithValue("@ITEMCODE", data.ITEMCODEM);
                    cmd.Parameters.AddWithValue("@DATEFROM", data.DATEFROMM);
                    cmd.Parameters.AddWithValue("@DATETO", data.DATETOM);
                    cmd.Parameters.AddWithValue("@STATUS", data.STATUSM);
                    cmd.Parameters.AddWithValue("@CUTSTATUS", data.CUTSTATUSM);
                    cmd.Parameters.AddWithValue("@MENUID", data.MENUIDM);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXM);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEM);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONM);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEM);
                    cmd.Parameters.AddWithValue("@USERTYPE", data.USERTYPEM);
                    cmd.Parameters.AddWithValue("@USERID", data.USERIDM);



                    //cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    if (results.Rows.Count > 0)
                    {
                        for (int i = 0; i < results.Rows.Count; i++)
                        {
                            DataRow row = results.Rows[i];
                            Logdata1 = DataTableToJSONWithStringBuilder(results);
                            logdata = DataTableToJSONWithStringBuilder(results);

                            dbConn.Close();
                        }
                    }
                    else
                    {
                        Logdata1 = "No Data Found";
                       // return logdata; 
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



        [HttpGet]
        [Route("getrfqsingledata/{RFQID}")]
        public string getrfqsingledata(string RFQID)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT * FROM erp_rfq_master where RFQID='" + RFQID + "'";


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
        [Route("get_edit_prs/{prscode}/{functionid}")]
        public string get_edit_prs(string prscode, string functionid)
        {
            string Logdata1 = string.Empty;
            string Logdata2 = string.Empty;
            string prsid = string.Empty;
            string alphaname = "";
            string sortExpression = "item_short_Desc";
            string pageSize = "20";
            string pageIndex = "0";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //query = "select erp.function_id,branch_id,prs_id,prs_category,prs_code,prs_ref,prs_type,bo.TUM_USER_NAME as requested_by,requested_Date,request_type,request_comments,reason_purchase,netamount,IS_BID,currency,status,erp.created_by,erp.created_on,erp.lst_upd_by,erp.lst_upd_on,erp.ipaddress,IS_SINGLE_VENDOR,PRIORITY from Erp_prs_master erp left join bo_user_master bo on erp.function_id = bo.FUNCTION_ID and bo.tum_user_id = erp.requested_by where prs_code = '" + prscode + "'";


                query = "select prs.function_id,prs.branch_id,prs.prs_id,prs.prs_category,prs.prs_code,prs.prs_ref,prs.prs_type, (select um.TUM_USER_NAME as requested_by from BO_USER_MASTER um where um.TUM_USER_ID = prs.requested_by ) requested_by ,prs.requested_Date,prs.request_type,prs.request_comments,prs.reason_purchase,prs.netamount,prs.IS_BID,prs.currency,prs.status,CASE WHEN prd.status = 'F' AND prs.status = 'A' AND prd.ITEM_ID <> '0' THEN 'Stock Fully Updated' WHEN prd.status = 'U' AND prs.status = 'A' AND prd.ITEM_ID <> '0' THEN 'Stock Partially Updated' WHEN prd.status = 'G' AND prs.status = 'A' AND prd.ITEM_ID = '0' THEN 'GRN Done' WHEN prd.status = 'S' AND prs.status = 'A' AND prd.ITEM_ID <> '0' THEN 'Shipment Done' WHEN prd.status = 'O' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'PO Raised' WHEN prd.status = 'C' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Vendor Confirmed' WHEN prd.status = 'Q' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Quotation Released' WHEN prd.status = 'V' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Vendor Evaluated' WHEN prd.status = 'R' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'RFQ Raised' WHEN prd.status = 'P' AND prs.status = 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Approved' WHEN prd.status = 'P' AND prs.status = 'D' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Denied' WHEN(prd.status = 'P' AND prs.status = 'P' AND prs.status <> 'A' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') OR(prd.status = 'N' AND prs.status = 'P'  AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0'))) THEN 'Pending' WHEN prd.status = 'N' AND prs.status = 'N' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'New' WHEN prd.status = 'X' AND prs.status = 'X' AND(prd.ITEM_ID = '0' OR prd.ITEM_ID <> '0') THEN 'Cancelled' END AS CURRENT_STATUS,  prs.created_by,prs.created_on,prs.lst_upd_by,prs.lst_upd_on,prs.ipaddress,IS_SINGLE_VENDOR,PRIORITY from Erp_prs_master prs join ERP_PRS_DETAILS prd on(prs.prs_id= prd.prs_id) where prs.prs_code = '" + prscode + "'";


                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    // prsid = row[0].ToString();
                    prsid = results.Rows[i]["prs_id"].ToString();
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }


               // ERP_PRS_GETITEM2

                string StrSql = "EXEC ERP_PRS_GETITEM2  '" + functionid + "', '" + prsid + "', '" + alphaname + "', '" + sortExpression + "', '" + pageIndex + "', '" + pageSize + "'";

                SqlCommand cmd1 = new SqlCommand(StrSql, dbConn);
                var reader1 = cmd1.ExecuteReader();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results1.Load(reader1);
                for (int i = 0; i < results1.Rows.Count; i++)
                {
                    DataRow row = results1.Rows[i];
                    //prsid = row[0].ToString();
                    Logdata2 = DataTableToJSONWithStringBuilder(results1);
                }

                //ading two json into single json array

                var javaScriptSerializer = new JavaScriptSerializer();
                var prsDetails = javaScriptSerializer.DeserializeObject(Logdata1);//json1
                var ItemDetails = javaScriptSerializer.DeserializeObject(Logdata2);//json2

                var resultJson = javaScriptSerializer.Serialize(new { PrsDetails = prsDetails, ItemDetails = ItemDetails });


                //var result = (new { recordsets = Logdata1 });
                return resultJson;
            }
        }


        //ManageRFQ get

        [HttpGet]
        [Route("get_Manage_RFQ/{RFQcode}")]
        public string get_Manage_RFQ(string RFQcode)
        {
            string Logdata1 = string.Empty;
            string Logdata2 = string.Empty;
            string rfqid = string.Empty;
            string alphaname = "";
            string sortExpression = "item_short_Desc";
            string pageSize = "10";
            string pageIndex = "0";

            string strfun = "1";
            string strcode = "RFQ/" + RFQcode;
            string strdate = "";
            string stritemcode = "";
            string mode = "";
            string status = "";
            string fromdate = "";
            string todate = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();




                string query = "";
                query = "select RFQID from ERP_RFQ_MASTER where RFQCode= '" + strcode + "'";


                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    // prsid = row[0].ToString();
                    rfqid = results.Rows[i]["RFQID"].ToString();
                    // Logdata1 = DataTableToJSONWithStringBuilder(results);
                }


                // ERP_VENDOR_EVALUATION_SUMMARY

                mode = "1";

                string StrSql = "exec ERP_VENDOR_EVALUATION_SUMMARY '" + strfun + "', '" + strcode.Trim() + "','" + strdate.Trim() + "','" + stritemcode.Trim() + "','" + mode + "','" + status + "','" + fromdate.Trim() + "','" + todate.Trim() + "','" + pageIndex + "','" + pageSize + "','" + sortExpression + "','" + alphaname + "' ";

                SqlCommand cmd1 = new SqlCommand(StrSql, dbConn);
                var reader1 = cmd1.ExecuteReader();
                System.Data.DataTable results1 = new System.Data.DataTable();
                results1.Load(reader1);
                for (int i = 0; i < results1.Rows.Count; i++)
                {
                    DataRow row = results1.Rows[i];
                    //prsid = row[0].ToString();
                    Logdata2 = DataTableToJSONWithStringBuilder(results1);
                }

                //ading two json into single json array

                var javaScriptSerializer = new JavaScriptSerializer();
                var prsDetails = javaScriptSerializer.DeserializeObject(Logdata1);//json1
                var ItemDetails = javaScriptSerializer.DeserializeObject(Logdata2);//json2

                var resultJson = javaScriptSerializer.Serialize(new { ItemDetails = ItemDetails });


                //var result = (new { recordsets = Logdata1 });
                return resultJson;
            }
        }


        //Get Vendor Dedails for selected item
        //Find in Manage RFQ page

        [HttpGet]
        [Route("get_Find_vendor")]
        public string get_Find_vendor(string functionid, string branch, string itemCategory, string itemSubCategory, string rFQCode, string keyword, string VendorCode, string Brand, string Model, string qtyval, string ItemID)
        {
            string Logdata1 = string.Empty;
            string Logdata2 = string.Empty;
            string rfqid = string.Empty;
            string alphaname = "";
            string sortExpression = "item_short_Desc";
            string PageSize = "10";
            string pageIndex = "0";

            string strfun = "1";
            // string strcode = "RFQ/" + RFQcode;

            string SingleVendor = "N";

            string Field1 = "Y";
            string Field2 = "Y";
            string Field3 = "Y";
            string Field4 = "Y";
            string Field5 = "Y";
            string Field6 = "Y";
            string Field7 = "Y";
            string Field8 = "Y";
            string Field9 = "Y";
            string Field10 = "Y";
            string Field11 = "Y";
            string Field12 = "Y";
            string Field13 = "Y";
            string Field14 = "Y";
            string Field15 = "Y";
            string Field16 = "Y";
            string Field17 = "Y";
            string Field18 = "Y";
            string Field19 = "Y";
            string Field20 = "Y";
            string CustomFilter = "N";
            string CatType = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();



               // string strsql = "EXEC ERP_GETVENDORDETAILS";
                string strsql = "EXEC ERP_GETVENDORDETAILS";
                if (functionid != null && functionid != "" && functionid !="null")
                {
                    strsql += " @FUNCTIONID=" + functionid;

                }
                if (pageIndex != null && pageIndex !="" && pageIndex != "null")
                {
                    strsql += " ,@PAGEINDEX=" + pageIndex;

                }
                if (PageSize != null && PageSize !="" && PageSize != "null")
                {
                    strsql += " ,@PAGESIZE=" + PageSize;
                }
                if (itemCategory != "" && itemCategory != null && itemCategory != "null")
                {
                    strsql += " ,@Category='" + itemCategory + "'";
                }
                if (rFQCode != "" && rFQCode != null && rFQCode != "null")
                {
                    rFQCode = "RFQ/" + rFQCode;
                    strsql += " ,@RFQCode='" + rFQCode + "'";
                }
                if (itemSubCategory != "" && itemSubCategory != null && itemSubCategory != "null")
                {
                    strsql += " ,@SubCategory='" + itemSubCategory + "'";
                }
                if (keyword != "" && keyword != null && keyword != "null")
                {
                    strsql += " ,@KEYWORD='" + keyword + "'";
                }

                //below vendorCode added by thiru 
                if (VendorCode != "" && VendorCode != null && VendorCode != "null")
                {
                    strsql += " ,@VendorCode='" + VendorCode + "'";
                }


                if (Brand != "" && Brand !=null && Brand != "null")
                {
                    strsql += " ,@Brand='" + Brand + "'";
                }
                if (Model != "" && Model != null && Model != "null")
                {
                    strsql += " ,@Model='" + Model + "'";
                }
                if (SingleVendor != "" && SingleVendor != null && SingleVendor != "null")
                {
                    strsql += " ,@SINGLEVENDOR='" + SingleVendor + "'";
                }
                if (qtyval != "" && qtyval !=null && qtyval != "null")
                {
                    strsql += " ,@QTY='" + qtyval + "'";
                }

                //Custom Values

                if (Field1.ToString().Trim() != "" && Field1.ToString().Trim() != "N")
                {
                    strsql += " ,@Field1='" + Field1.ToString().Trim() + "'";

                }
                if (Field2.ToString().Trim() != "" && Field2.ToString().Trim() != "N")
                {
                    strsql += " ,@Field2='" + Field2.ToString().Trim() + "'";

                }
                if (Field3.ToString().Trim() != "" && Field3.ToString().Trim() != "N")
                {
                    strsql += " ,@Field3='" + Field3.ToString().Trim() + "'";

                }
                if (Field4.ToString().Trim() != "" && Field4.ToString().Trim() != "N")
                {
                    strsql += " ,@Field4='" + Field4.ToString().Trim() + "'";

                }
                if (Field5.ToString().Trim() != "" && Field5.ToString().Trim() != "N")
                {
                    strsql += " ,@Field5='" + Field5.ToString().Trim() + "'";

                }
                if (Field6.ToString().Trim() != "" && Field6.ToString().Trim() != "N")
                {
                    strsql += " ,@Field6='" + Field6.ToString().Trim() + "'";

                }
                if (Field7.ToString().Trim() != "" && Field7.ToString().Trim() != "N")
                {
                    strsql += " ,@Field7='" + Field7.ToString().Trim() + "'";

                }
                if (Field8.ToString().Trim() != "" && Field8.ToString().Trim() != "N")
                {
                    strsql += " ,@Field8='" + Field8.ToString().Trim() + "'";

                }
                if (Field9.ToString().Trim() != "" && Field9.ToString().Trim() != "N")
                {
                    strsql += " ,@Field9='" + Field9.ToString().Trim() + "'";

                }
                if (Field10.ToString().Trim() != "" && Field10.ToString().Trim() != "N")
                {
                    strsql += " ,@Field10='" + Field10.ToString().Trim() + "'";

                }
                if (Field11.ToString().Trim() != "" && Field11.ToString().Trim() != "N")
                {
                    strsql += " ,@Field11='" + Field11.ToString().Trim() + "'";

                }
                if (Field12.ToString().Trim() != "" && Field12.ToString().Trim() != "N")
                {
                    strsql += " ,@Field12='" + Field12.ToString().Trim() + "'";

                }
                if (Field13.ToString().Trim() != "" && Field13.ToString().Trim() != "N")
                {
                    strsql += " ,@Field13='" + Field13.ToString().Trim() + "'";

                }
                if (Field14.ToString().Trim() != "" && Field14.ToString().Trim() != "N")
                {
                    strsql += " ,@Field14='" + Field14.ToString().Trim() + "'";

                }
                if (Field15.ToString().Trim() != "" && Field15.ToString().Trim() != "N")
                {
                    strsql += " ,@Field15='" + Field15.ToString().Trim() + "'";

                }
                if (Field16.ToString().Trim() != "" && Field16.ToString().Trim() != "N")
                {
                    strsql += " ,@Field16='" + Field16.ToString().Trim() + "'";

                }
                if (Field17.ToString().Trim() != "" && Field17.ToString().Trim() != "N")
                {
                    strsql += " ,@Field17='" + Field17.ToString().Trim() + "'";

                }
                if (Field18.ToString().Trim() != "" && Field18.ToString().Trim() != "N")
                {
                    strsql += " ,@Field18='" + Field18.ToString().Trim() + "'";

                }
                if (Field19.ToString().Trim() != "" && Field19.ToString().Trim() != "N")
                {
                    strsql += " ,@Field19='" + Field19.ToString().Trim() + "'";

                }
                if (Field20.ToString().Trim() != "" && Field20.ToString().Trim() != "N")
                {
                    strsql += " ,@Field20='" + Field20.ToString().Trim() + "'";

                }

                if (CustomFilter == "Y")
                {
                    strsql += " ,@CustomFilter='" + CustomFilter + "'";
                }


                if (ItemID != "" && ItemID != null && ItemID != "null")
                {
                    if (ItemID.ToString() != "0")
                    {
                        CatType = "Items";
                        strsql += " ,@Type='" + CatType + "'";
                    }
                    else
                    {
                        CatType = "Service";
                    }
                }
               // strsql += " ,@branch='" + branch + "'";

                //End Custom Values



                SqlCommand cmd = new SqlCommand(strsql, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    // prsid = row[0].ToString();
                    //  rfqid = results.Rows[i]["RFQID"].ToString();
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                }






                //ading two json into single json array

                var javaScriptSerializer = new JavaScriptSerializer();
                var prsDetails = javaScriptSerializer.DeserializeObject(Logdata1);//json1
                var VendorDetails = javaScriptSerializer.DeserializeObject(Logdata1);//json2

                var resultJson = javaScriptSerializer.Serialize(new { VendorDetails = VendorDetails });


                //var result = (new { recordsets = Logdata1 });
                return resultJson;
            }
        }












        [HttpPost]
        [Route("MaterialRequistion_Insert_Update")]
        public async Task<ActionResult<ERP>> MaterialRequistion_Insert_Update(dynamic data)
        {
            // string struser = data.user_lower;

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var stroutput = "";
            string mrs_id = "";
            string strmrsid = "";
            string prsid = string.Empty;
            string currency = string.Empty;
            string prs_detailID = string.Empty;
            string wfno = string.Empty;
            string result = string.Empty;



            string ret = string.Empty;
            string Outputval = string.Empty;


            string strmrscode = "";
            string Outputval1 = string.Empty;
            string strUserID = string.Empty;
            string Result = string.Empty;
            //declare variable for both method
            string product_id = string.Empty;
            string campaign_id = string.Empty;
            string strMode = string.Empty;
            string FUNCTION_ID = string.Empty;
            string BRANCH_ID = string.Empty;
            string BRANCH_DESC = string.Empty;
            string REQUEST_REFERENCE = string.Empty;
            string REQUEST_REASON = string.Empty;
            string MRS_CODE = string.Empty;

            string EXP_DATE = string.Empty;
            string MRS_DATE = string.Empty;
            string ITEM_CODE = string.Empty;
            string ITEM_SHORT_DESC = string.Empty;
            string Current_Status = string.Empty;
            string STATUS = string.Empty;
            string REQUIRED_QTY = string.Empty;
            string STOCK_QTY = string.Empty;
            string ReservedQty = string.Empty;
            string Issued_Qty = string.Empty;
            string Pending_Qty = string.Empty;
            string Priority = string.Empty;

            string CurrentQty = string.Empty;
            string RequiredQty = string.Empty;


            string requested_by = string.Empty;
            string requested_Date = string.Empty;
            string request_reference = string.Empty;
            string request_reason = string.Empty;
            string CREATED_BY = string.Empty;
            string LST_UPD_BY = string.Empty;
            string IPADDRESS = string.Empty;
            string Order_Priority = string.Empty;
            string CostCode = string.Empty;
            string VechileNO = string.Empty;


            string CPC = string.Empty;
            string ACC = string.Empty;
            string PTM = string.Empty;
            string BDC = string.Empty;
            string item_detailed_description = string.Empty;
            string remarks = string.Empty;
            string UserID = string.Empty;
            string unitprice = string.Empty;
            string item_id = string.Empty;
            //string item_id = null;
            string flag = string.Empty;
            string val = string.Empty;
            string ChkRelease = string.Empty;
            string netamount = string.Empty;
            int mrsdetail_id = 0;
            int required_qty = 0;
            int issued_qty = 0;
            int returned_qty = 0;
            int cancelled_qty = 0;
            int UOM = 0;
            string wo_number = string.Empty;
            string jc_number = string.Empty;



            string pk_column_name1 = string.Empty;

            string pk_column_name2 = string.Empty;

            string pk_column_name3 = string.Empty;

            string pk_column_name4 = string.Empty;

            string pk_column_name5 = string.Empty;


            string STATUS_COLUMN = string.Empty;

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();
                    var serializedObject = JsonConvert.SerializeObject(data).ToString();
                    var obj = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj1 = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent1 = obj_parent.GetValue("mrsdetail")[0] as JObject;
                    foreach (KeyValuePair<string, JToken> item in obj_parent1)
                    {
                        JProperty p1 = obj_parent1.Property(item.Key);

                        if (item.Key == "FUNCTION_ID")
                        {
                            FUNCTION_ID = item.Value.ToString();
                        }
                        if (item.Key == "BRANCH_ID")
                        {
                            BRANCH_ID = item.Value.ToString();
                        }
                        if (item.Key == "BRANCH_DESC")
                        {
                            BRANCH_DESC = item.Value.ToString();
                        }
                        if (item.Key == "mrs_id")
                        {
                            // mrs_id = item.Value.ToString();
                            mrs_id = strmrsid;

                        }
                        if (item.Key == "MRS_CODE")
                        {
                            // mrs_id = item.Value.ToString();
                            MRS_CODE = strmrsid;

                        }

                        if (item.Key == "requested_by")
                        {
                            requested_by = item.Value.ToString();
                        }
                        if (item.Key == "requested_Date")
                        {
                            requested_Date = item.Value.ToString();
                        }

                        if (item.Key == "REQUEST_REFERENCE")
                        {
                            REQUEST_REFERENCE = item.Value.ToString();
                        }
                        if (item.Key == "REQUEST_REASON")
                        {
                            REQUEST_REASON = item.Value.ToString();
                        }
                        if (item.Key == "netamount")
                        {
                            netamount = item.Value.ToString();
                            if (netamount == "")
                            {
                                netamount = "0";
                            }
                        }
                        if (item.Key == "STATUS")
                        {
                            STATUS = item.Value.ToString();
                        }

                        if (item.Key == "CREATED_BY")
                        {
                            CREATED_BY = item.Value.ToString();
                            if (CREATED_BY == "")
                            {
                                CREATED_BY = "0";
                            }
                        }
                        if (item.Key == "LST_UPD_BY")
                        {
                            LST_UPD_BY = item.Value.ToString();
                            if (LST_UPD_BY == "")
                            {
                                LST_UPD_BY = "0";
                            }
                        }
                        if (item.Key == "IPADDRESS")
                        {
                            IPADDRESS = item.Value.ToString();
                            if (IPADDRESS == "")
                            {
                                IPADDRESS = "0";
                            }
                        }


                        if (item.Key == "Order_Priority")
                        {
                            // MRS_CODE = item.Value.ToString();
                            Order_Priority = item.Value.ToString();
                        }
                        if (item.Key == "CostCode")
                        {
                            // MRS_CODE = item.Value.ToString();
                            CostCode = item.Value.ToString();
                        }

                        //if (item.Key == "product_id")
                        //{
                        //    product_id = item.Value.ToString();
                        //    if (product_id == "")
                        //    {
                        //        product_id = "0";
                        //    }
                        //}
                        //if (item.Key == "campaign_id")
                        //{
                        //    campaign_id = item.Value.ToString();
                        //    if (campaign_id == "")
                        //    {
                        //        campaign_id = "0";
                        //    }
                        //}

                        //if (item.Key == "EXP_DATE")
                        //{
                        //    EXP_DATE = item.Value.ToString();
                        //}
                        //if (item.Key == "MRS_DATE")
                        //{
                        //    MRS_DATE = item.Value.ToString();
                        //}
                        //if (item.Key == "Current_Status")
                        //{
                        //    Current_Status = item.Value.ToString();
                        //}
                        //if (item.Key == "REQUIRED_QTY")
                        //{
                        //    REQUIRED_QTY = item.Value.ToString();
                        //}
                        //if (item.Key == "RequiredQty")
                        //{
                        //    RequiredQty = item.Value.ToString();
                        //}
                        //if (item.Key == "BDC")
                        //{
                        //    BDC = item.Value.ToString();
                        //}
                        //if (item.Key == "PTM")
                        //{
                        //    PTM = item.Value.ToString();
                        //}
                        //if (item.Key == "ACC")
                        //{
                        //    ACC = item.Value.ToString();
                        //}
                        //if (item.Key == "CPC")
                        //{
                        //    CPC = item.Value.ToString();
                        //}
                        if (item.Key == "flag")
                        {
                            flag = item.Value.ToString();
                        }


                    }
                    //if (mrs_id == "")
                    //{
                    //    mrs_id = "0";
                    //}
                    //else
                    //{
                    string query = "";
                    query = "select max(mrs_id +1) as maxid from ERP_MRS_MASTER";

                    SqlCommand cmd = new SqlCommand(query, dbConn);
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        strmrsid = row[0].ToString();
                        mrs_id = strmrsid;
                    }
                    if (MRS_CODE == "")
                    {

                        DataSet dsmrscode = new DataSet();
                        string sqlmrscode = "ERP_MRS_ISCONFIG";
                        //string sqlmrscode = "ERP_MRS_DETAILS";

                        SqlCommand cmdmrscode = new SqlCommand(sqlmrscode, dbConn);


                        cmdmrscode.CommandType = CommandType.StoredProcedure;

                        cmdmrscode.Parameters.AddWithValue("@FUNCTIONID", FUNCTION_ID);
                        cmdmrscode.Parameters.AddWithValue("@TYPE", "MaterialRequisitionMaster");

                        cmdmrscode.ExecuteNonQuery();
                        var mrscodereader = cmdmrscode.ExecuteReader();
                        System.Data.DataTable resultsmrscode = new System.Data.DataTable();
                        resultsmrscode.Load(mrscodereader);
                        //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                        for (int i = 0; i < resultsmrscode.Rows.Count; i++)
                        {
                            DataRow rowmrscode = resultsmrscode.Rows[i];
                            strmrscode = rowmrscode[0].ToString();


                        }

                        MRS_CODE = strmrscode;


                        // retmrs = objdata.savedetails(ds);
                    }



                    //if (val == "" || ret == "S")
                    //{
                    // mrs_id = ("EXEC ERP_MRS_SAVEMASTER");

                    DataSet dsuserdetails = new DataSet();
                    string sqlMRS = "ERP_MRS_SAVEMASTER";

                   // string sqlMRS = "ERP_MRS_SAVEMASTER1";
                    SqlCommand objcommandMrs = new SqlCommand(sqlMRS, dbConn);


                    objcommandMrs.CommandType = CommandType.StoredProcedure;


                    objcommandMrs.Parameters.AddWithValue("@FUNCTION_ID", FUNCTION_ID.ToString());
                    objcommandMrs.Parameters.AddWithValue("@BRANCH_ID", BRANCH_ID.ToString());
                    objcommandMrs.Parameters.AddWithValue("@MRS_ID", strmrsid.ToString());
                    objcommandMrs.Parameters.AddWithValue("@mrs_code", MRS_CODE.ToString());
                    objcommandMrs.Parameters.AddWithValue("@requested_by", requested_by.ToString());
                    objcommandMrs.Parameters.AddWithValue("@requested_Date", requested_Date.ToString());
                    objcommandMrs.Parameters.AddWithValue("@request_reference", request_reference.ToString());
                    objcommandMrs.Parameters.AddWithValue("@request_reason", request_reason.ToString());
                    objcommandMrs.Parameters.AddWithValue("@netamount", netamount.ToString());
                    objcommandMrs.Parameters.AddWithValue("@STATUS", STATUS.ToString());
                    objcommandMrs.Parameters.AddWithValue("@created_by", CREATED_BY.ToString());
                    objcommandMrs.Parameters.AddWithValue("@LST_UPD_BY", LST_UPD_BY.ToString());
                    objcommandMrs.Parameters.AddWithValue("@IPADDRESS", IPADDRESS.ToString());
                    objcommandMrs.Parameters.AddWithValue("@PRIORITY", Order_Priority.ToString());
                    objcommandMrs.Parameters.AddWithValue("@CostCode", CostCode.ToString());
                    objcommandMrs.Parameters.AddWithValue("@FLAG", flag.ToString());
                    //objcommand1.Parameters.AddWithValue("@Remarks", remarks.ToString());
                    //objcommand1.Parameters.AddWithValue("@ITEM_DETAILED_DESCRIPTION", item_detailed_description.ToString());
                    //objcommand1.Parameters.AddWithValue("@BDC", BDC.ToString());
                    //objcommand1.Parameters.AddWithValue("@PTM", PTM.ToString());
                    //objcommand1.Parameters.AddWithValue("@ACC", ACC.ToString());
                    //objcommand1.Parameters.AddWithValue("@CPC", CPC.ToString());
                    //objcommand1.Parameters.AddWithValue("@VechileNO", VechileNO.ToString());
                    //objcommandMrs.ExecuteNonQuery();

                    //....
                    var readerMRS = objcommandMrs.ExecuteReader();
                    //var readerMRS = objcommandMrs.ExecuteNonQuery();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(readerMRS);

                    //var reader1 = objcommandMrs.ExecuteReader();
                    //System.Data.DataTable results1 = new System.Data.DataTable();
                    //results1.Load(reader);


                    //var reader1 = objcommandMrs.ExecuteReader();
                    //objcommandMrs.ExecuteNonQuery();
                    //System.Data.DataTable results1 = new System.Data.DataTable();
                    //results1.Load(reader1);

                    //for (int i = 0; i < results1.Rows.Count; i++)
                    //{
                    //    DataRow row1 = results1.Rows[i];
                    //    mrs_id = row1[0].ToString();

                    //}



                    if (ChkRelease.ToString() == "true")
                    {
                        string wf_config_id = "select wf_config_id from BO_workflow_configurations where table_name like '%ERP_MRS%' and status='A' and function_id='" + FUNCTION_ID + "'";
                        SqlCommand cmd2 = new SqlCommand(wf_config_id, dbConn);
                        var reader2 = cmd2.ExecuteReader();
                        System.Data.DataTable results2 = new System.Data.DataTable();
                        results2.Load(reader2);

                        for (int i = 0; i < results2.Rows.Count; i++)
                        {
                            DataRow row = results2.Rows[i];
                            wf_config_id = row[0].ToString();
                        }

                        // wf_config_id = objSql.getString(wf_config_id);
                        if (wf_config_id != null && wf_config_id != "")
                        {

                            string wffun = FUNCTION_ID;
                            string WorkFlowTable = "ERP_MRS_MASTER";
                            string PK1 = prsid;
                            string PK2 = null;
                            string PK3 = null;
                            string PK4 = null;
                            string PK5 = null;
                            string User = CREATED_BY;

                            //workflow insert



                            string wf_insert = "select pk_column_name1,pk_column_name2,pk_column_name3,pk_column_name4,pk_column_name5,STATUS_COLUMN from BO_WORKFLOW_CONFIGURATIONS with (nolock) where WF_CONFIG_ID='" + wf_config_id + "'";
                            SqlCommand cmdwf = new SqlCommand(wf_insert, dbConn);
                            var readerwf = cmdwf.ExecuteReader();
                            System.Data.DataTable resultswf = new System.Data.DataTable();
                            resultswf.Load(readerwf);

                            for (int i = 0; i < resultswf.Rows.Count; i++)
                            {
                                DataRow row = resultswf.Rows[i];
                                pk_column_name1 = resultswf.Rows[i]["pk_column_name1"].ToString();
                                pk_column_name2 = resultswf.Rows[i]["pk_column_name2"].ToString();
                                pk_column_name3 = resultswf.Rows[i]["pk_column_name3"].ToString();
                                pk_column_name4 = resultswf.Rows[i]["pk_column_name4"].ToString();
                                pk_column_name5 = resultswf.Rows[i]["pk_column_name5"].ToString();
                                STATUS_COLUMN = resultswf.Rows[i]["STATUS_COLUMN"].ToString();

                            }
                        }
                        else
                        {
                            string wfno_sql = "update ERP_MRS_MASTER set status='A' WHERE MRS_ID='" + prsid + "' and function_id='" + FUNCTION_ID + "'";
                            SqlCommand cmd3 = new SqlCommand(wfno_sql, dbConn);
                            var reader3 = cmd3.ExecuteReader();
                            System.Data.DataTable results3 = new System.Data.DataTable();
                            results3.Load(reader3);


                        }



                    }


                    //}


                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent2 = obj_parents.GetValue("mrsdetail")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent2)
                    {
                        JProperty p2 = obj_parent2.Property(item.Key);

                        if (item.Key == "MRSDdetail")
                        {
                            var Itemsdetail = item.Value.ToString();

                            JArray array = JArray.Parse(Itemsdetail);
                            JArray jsonArray = JArray.Parse(Itemsdetail);

                            foreach (JObject content in array.Children<JObject>())
                            {
                                foreach (JProperty prop in content.Properties())
                                {
                                    string Name = prop.Name.ToString().Trim();
                                    string Value = prop.Value.ToString().Trim();



                                    if (Name == "FUNCTION_ID")
                                    {
                                        FUNCTION_ID = Value.ToString();
                                    }

                                    if (Name == "mrsdetail_id")
                                    {
                                        mrsdetail_id = Convert.ToInt32(Value);
                                    }

                                    //if (Name == "MRS_ID")
                                    //{
                                    //    // mrs_id = item.Value.ToString();
                                    //    mrs_id = strmrsid;

                                    //}
                                    if (Name == "item_id")
                                    {
                                        // mrs_id = item.Value.ToString();
                                        item_id = Value.ToString();

                                    }

                                    if (Name == "RequiredQty")
                                    {
                                        RequiredQty = Value.ToString();
                                    }
                                    if (Name == "issued_qty	")
                                    {
                                        issued_qty = 0;
                                    }
                                    if (Name == "returned_qty")
                                    {
                                        returned_qty = 0;
                                    }
                                    if (Name == "cancelled_qty")
                                    {
                                        cancelled_qty = 0;
                                    }

                                    if (Name == "EXP_DATE")
                                    {
                                        EXP_DATE = Value.ToString();
                                        // EXP_DATE = strmrscode;
                                    }
                                    if (Name == "CREATED_BY")
                                    {
                                        CREATED_BY = Value.ToString();
                                    }
                                    if (Name == "LST_UPD_BY")
                                    {
                                        LST_UPD_BY = Value.ToString();
                                    }

                                    if (Name == "IPADDRESS")
                                    {
                                        IPADDRESS = Value.ToString();
                                    }
                                    if (Name == "STATUS")
                                    {
                                        STATUS = Value.ToString();
                                    }
                                    if (Name == "unitprice")
                                    {
                                        unitprice = item.Value.ToString();
                                    }
                                    if (Name == "netamount")
                                    {
                                        netamount = Value.ToString();
                                    }

                                    if (Name == "flag")
                                    {
                                        flag = Value.ToString();
                                    }

                                    if (Name == "remarks")
                                    {
                                        remarks = Value.ToString();
                                    }

                                    if (Name == "item_detailed_description")
                                    {
                                        item_detailed_description = Value.ToString();
                                    }

                                    if (Name == "prs_id")
                                    {
                                        prsid = Value.ToString();
                                    }

                                    if (Name == "BDC")
                                    {
                                        BDC = Value.ToString();
                                    }
                                    if (Name == "PTM")
                                    {
                                        PTM = Value.ToString();
                                    }
                                    if (Name == "ACC")
                                    {
                                        ACC = Value.ToString();
                                    }
                                    if (Name == "CPC")
                                    {
                                        CPC = Value.ToString();
                                    }
                                    if (Name == "VechileNO")
                                    {
                                        VechileNO = Value.ToString();
                                    }

                                    //   ds.Tables[0].Rows.Add(row);
                                    string retmrs = string.Empty;


                                    //if (Outputval != "")
                                    //{
                                    //}
                                    //mrs_id = Outputval.ToString();
                                    //ret = "S";




                                    //btnsave




                                    //    //    string approveStatus1 = "select ,BO_USER_MASTER.TUM_USER_NAME from ERP_MRS_MASTER INNER JOIN BO_USER_MASTER ON BO_USER_MASTER.TUM_USER_ID = ERP_MRS_MASTER.requested_by  WHERE ERP_MRS_MASTER.mrs_id='" + mrs_id + "' and ERP_MRS_MASTER.function_id='" + FUNCTION_ID + "' select ERP_MRS_DETAILS.,ERP_ITEM_MASTER.ITEM_CODE,ERP_ITEM_MASTER.ITEM_LONG_DESC  from ERP_MRS_DETAILS  INNER JOIN ERP_ITEM_MASTER ON ERP_MRS_DETAILS.ITEM_ID = ERP_ITEM_MASTER.ITEM_ID WHERE mrs_id='" + mrs_id + "'";
                                    //    //    SqlCommand cmdwf = new SqlCommand(approveStatus1, dbConn);
                                    //    //    var readerwf = cmdwf.ExecuteReader();
                                    //    //    System.Data.DataTable resultswf = new System.Data.DataTable();
                                    //    //    resultswf.Load(readerwf);
                                    //    //    Outputval1 = resultswf.ToString();
                                    //    //}
                                    //}

                                    //if (MRS_CODE == "")
                                    //{

                                    //    DataSet dsmrscode = new DataSet();
                                    //    string sqlmrscode = "ERP_MRS_ISCONFIG";
                                    //    SqlCommand cmdmrscode = new SqlCommand(sqlmrscode, dbConn);


                                    //    cmdmrscode.CommandType = CommandType.StoredProcedure;

                                    //    cmdmrscode.Parameters.AddWithValue("@FUNCTIONID", FUNCTION_ID);
                                    //    cmdmrscode.Parameters.AddWithValue("@TYPE", "MaterialRequisitionMaster");

                                    //    cmdmrscode.ExecuteNonQuery();
                                    //    var mrscodereader = cmdmrscode.ExecuteReader();
                                    //    System.Data.DataTable resultsmrscode = new System.Data.DataTable();
                                    //    resultsmrscode.Load(mrscodereader);
                                    //    //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                                    //    for (int i = 0; i < resultsmrscode.Rows.Count; i++)
                                    //    {
                                    //        DataRow rowmrscode = resultsmrscode.Rows[i];
                                    //        strmrscode = rowmrscode[0].ToString();


                                    //    }

                                    //    MRS_CODE = strmrscode;


                                    //    // retmrs = objdata.savedetails(ds);
                                    //}



                                }
                                if (strUserID != "")
                                {



                                    string approveStatus1 = "select ,BO_USER_MASTER.TUM_USER_NAME from ERP_MRS_MASTER INNER JOIN BO_USER_MASTER ON BO_USER_MASTER.TUM_USER_ID = ERP_MRS_MASTER.requested_by  WHERE ERP_MRS_MASTER.mrs_id='" + mrs_id + "' and ERP_MRS_MASTER.function_id='" + FUNCTION_ID + "' select ERP_MRS_DETAILS.,ERP_ITEM_MASTER.ITEM_CODE,ERP_ITEM_MASTER.ITEM_LONG_DESC  from ERP_MRS_DETAILS  INNER JOIN ERP_ITEM_MASTER ON ERP_MRS_DETAILS.ITEM_ID = ERP_ITEM_MASTER.ITEM_ID WHERE mrs_id='" + mrs_id + "'";
                                    SqlCommand cmdwf = new SqlCommand(approveStatus1, dbConn);
                                    var readerwf = cmdwf.ExecuteReader();
                                    System.Data.DataTable resultswf = new System.Data.DataTable();
                                    resultswf.Load(readerwf);
                                    Outputval1 = resultswf.ToString();
                                }
                                if (MRS_CODE != "")
                                {
                                    //retmrs = objdata.savedetails(ds);
                                    DataSet dsuserdetails1 = new DataSet();
                                    //string sql1 = "ERP_MRS_SAVE_DETAILS";
                                    string sql1 = "ERP_MRS_SAVE_DETAILS";
                                    SqlCommand objcommandDet = new SqlCommand(sql1, dbConn);

                                    objcommandDet.CommandType = CommandType.StoredProcedure;
                                    objcommandDet.Parameters.AddWithValue("@FUNCTION_ID", FUNCTION_ID.ToString());
                                    objcommandDet.Parameters.AddWithValue("@mrsdetail_id", mrsdetail_id.ToString());
                                    objcommandDet.Parameters.AddWithValue("@MRS_ID", mrs_id.ToString());

                                    objcommandDet.Parameters.AddWithValue("@ITEM_ID", item_id.ToString());
                                    objcommandDet.Parameters.AddWithValue("@required_qty", RequiredQty.ToString());
                                    objcommandDet.Parameters.AddWithValue("@issued_qty", 0);
                                    objcommandDet.Parameters.AddWithValue("@returned_qty", 0);
                                    objcommandDet.Parameters.AddWithValue("@cancelled_qty", 0);
                                    objcommandDet.Parameters.AddWithValue("@UOM", "");
                                    objcommandDet.Parameters.AddWithValue("@wo_number", 0);
                                    objcommandDet.Parameters.AddWithValue("@jc_number", 0);
                                    objcommandDet.Parameters.AddWithValue("@exp_date", EXP_DATE.ToString());

                                    objcommandDet.Parameters.AddWithValue("@STATUS", STATUS.ToString());
                                    objcommandDet.Parameters.AddWithValue("@CREATED_BY", CREATED_BY.ToString());
                                    objcommandDet.Parameters.AddWithValue("@LST_UPD_BY", LST_UPD_BY.ToString());
                                    objcommandDet.Parameters.AddWithValue("@IPADDRESS", IPADDRESS.ToString());

                                    objcommandDet.Parameters.AddWithValue("@item_detailed_description", item_detailed_description.ToString());
                                    objcommandDet.Parameters.AddWithValue("@prs_id", prsid.ToString());
                                    objcommandDet.Parameters.AddWithValue("@remarks", remarks.ToString());
                                    //objcommandDet.Parameters.AddWithValue("@NETAMOUNT", netamount.ToString());
                                    objcommandDet.Parameters.AddWithValue("@flag", flag.ToString());
                                    //objcommandDet.Parameters.AddWithValue("@ORDER_PRIORITY", Order_Priority.ToString());
                                    //objcommandDet.ExecuteNonQuery();
                                    var readerdet = objcommandDet.ExecuteReader();
                                    System.Data.DataTable results3 = new System.Data.DataTable();
                                    results3.Load(readerdet);
                                    //Outputval = results2.ToString();
                                    //for (int i = 0; i < results1.Rows.Count; i++)
                                    //{
                                    //    DataRow row1 = results1.Rows[i];
                                    //    mrs_id = row1[0].ToString();

                                    //}



                                }
                                stroutput = "Successfully inserted";
                                //var result = (new { logdata });
                                return Ok(stroutput);
                            }
                        }

                    }
                    //var result = (new { logdata });

                }
                return Ok(stroutput);
            }



            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }


        //Raise RFQ POST on NOV 5
        #region

        //shylaja 


        [HttpPost]
        [Route("RaiseRFQ")]
        public async Task<ActionResult<ERP>> RaiseRFQ(dynamic data)
        {
            string prsids = "", itemids = "", ItemCodeAmount = "", ItemCode = "", Add = "", quotdate = "", rowid = "", itemid = "", itemcode = "";
            string RFQCODE = "", RFQCODE1 = "", qty = "", branch = "", userid = "", ipaddress = "", Created_by = "", CheckIsSingleVendor = "", BranchID = "", RFQCode2="", CHECK="";
            int checkprs = 0, checkedcount = 0, CheckYCount = 0, CheckNCount = 0, CheckSingleCount = 0;
            string strBuyer = "";
           // int countt = 0;
            string Mode = string.Empty;
            string Qty = string.Empty;
            string RFQID = "";
             int countt = 0;
            string stroutput = string.Empty;
            string prscode = string.Empty;
            var logdata = "";
            string prsid = string.Empty;
            string itemdesc1 = string.Empty;
            int count = 0;
            string prs_id = "";
            int cnt = 0;
            int PRS_ID = 0;
            System.Data.DataTable results1 = new System.Data.DataTable();

            try
            {

                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();

                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent2 = obj_parents.GetValue("RFQ_raise")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent2)
                    {
                        JProperty p2 = obj_parent2.Property(item.Key);



                        if (item.Key == "RFQ_details")
                        {
                            var RFQ_details = item.Value.ToString();

                            JArray array = JArray.Parse(RFQ_details);
                            JArray jsonArray = JArray.Parse(RFQ_details);

                            foreach (JObject content in array.Children<JObject>())
                            {
                                foreach (JProperty prop in content.Properties())
                                {
                                    string Name = prop.Name.ToString().Trim();
                                    string Value = prop.Value.ToString().Trim();




                                    if (Name == "quotdate")
                                    {
                                        quotdate = Value.ToString();
                                    }

                                    if (Name == "prs_id")
                                    {
                                        prsid = Value.ToString();
                                    }
                                    if (Name == "item_id")
                                    {
                                        itemid = Value.ToString();
                                    }
                                    if (Name == "item_Code")
                                    {
                                        ItemCode = Value.ToString();
                                    }
                                    if (Name == "required_qty")
                                    {
                                        qty = Value.ToString();
                                    }
                                    if (Name == "branch_id")
                                    {
                                        branch = Value.ToString();
                                    }
                                    if (Name == "item_short_Desc")
                                    {
                                        itemdesc1 = Value.ToString();
                                    }
                                    if (Name == "rowid")
                                    {
                                        rowid = Value.ToString();
                                    }
                                    if (Name == "userid")
                                    {
                                        userid = Value.ToString();
                                    }
                                    if (Name == "ipaddress")
                                    {
                                        ipaddress = Value.ToString();
                                    }
                                    if (Name == "Created_by")
                                    {
                                        Created_by = Value.ToString();
                                    }
                                    if (Name == "RFQCode2")
                                    {
                                        RFQCode2 = Value.ToString();
                                    }

                                }

                                //  quotdate = data.quotdate.Text.ToString();

                                prs_id += prsid.ToString() + ",";

                                prsids += prsid.ToString() + '|';
                                itemids += itemid.ToString() + '|';
                                itemcode += ItemCode.ToString() + '|';
                                ItemCodeAmount += ItemCode.ToString() + '~' + itemid.ToString() + '~' + qty.ToString() + '~' + branch.ToString() + '~' + prsid.ToString() + '~' + itemdesc1.ToString() + '~' + rowid.ToString() + '|';
                                checkedcount++;
                                PRS_ID = Convert.ToInt32(prsid.ToString());
                                int ITEM_ID = Convert.ToInt32(itemid.ToString());
                                BranchID = branch.ToString();
                                CheckIsSingleVendor = "N";
                                checkprs = PRS_ID;



                                if (CheckIsSingleVendor != null && CheckIsSingleVendor != "")
                                {
                                    if (CheckIsSingleVendor == "Y")
                                    {
                                        CheckYCount = CheckYCount + 1;
                                    }
                                    else
                                        if (CheckIsSingleVendor == "N")
                                    {
                                        CheckNCount = CheckNCount + 1;
                                    }
                                }
                                else
                                {
                                    // shylaja for icrisat purpose
                                    CheckNCount = CheckNCount + 1;
                                }

                                string strSqltemp1 = "exec ERP_RFQ_get_prs_id  '" + prsid + "','" + itemid + "'";


                                SqlCommand cmdtempp = new SqlCommand(strSqltemp1, dbConn);
                                var readerp = cmdtempp.ExecuteReader();
                                System.Data.DataTable resultstempp = new System.Data.DataTable();
                                resultstempp.Load(readerp);
                                for (int i = 0; i < resultstempp.Rows.Count; i++)
                                {
                                    DataRow row = resultstempp.Rows[i];
                                    CHECK = row[0].ToString();
                                    // RFQCODE1 = "RFQ/" + RFQCODE + "AT";

                                }

                                if (CHECK == "")
                                {
                                   // int countt = 0;
                                    count++;
                                    checkprs = PRS_ID;
                                }
                                cnt += 1;
                                CheckSingleCount = CheckSingleCount + 1;
                            }
                        }
                        //}



                   

                        if (cnt == 1)
                        {
                            string[] prs = prs_id.Split(',');
                            if (prs.Length > 0)
                                prs_id = prs[0].ToString();
                        }


                        if (count == 0 && (CheckSingleCount == CheckYCount || CheckSingleCount == CheckNCount))
                        {
                            string RFQ = "", ClubRFQ = "";

                            if (itemcode != null && itemcode != "")
                            {
                                string[] str = itemcode.Split('|');
                                if (str != null && str.Length > 0)
                                {
                                    for (int i = 0; i < str.Length; i++)
                                    {
                                        RFQ = str[i].ToString();
                                        if (RFQ != null && RFQ != "")
                                        {
                                            ClubRFQ += RFQ + " ";
                                        }
                                    }
                                    if (ClubRFQ != null && ClubRFQ != "")
                                    {
                                        string FG = "";
                                        ClubRFQ = ClubRFQ.TrimEnd(' ');
                                        //  HF_RFQs.Value = ClubRFQ;

                                        string[] stt = ClubRFQ.Split(' ');
                                        if (stt != null && stt.Length > 0)
                                        {
                                            for (int j = 0; j < stt.Length; j++)
                                            {
                                                string tobematched = stt[j];
                                                string sentence = ClubRFQ;

                                                foreach (Match match in Regex.Matches(sentence, tobematched))
                                                {
                                                   // int countt = 0;
                                                    //countt++;
                                                    Add = stt[j];
                                                }
                                                if (!FG.Contains(Add))
                                                {
                                                    FG += Add + ",";
                                                }
                                            }
                                            if (FG != null && FG != "")
                                            {
                                                FG = FG.TrimEnd(',');
                                            }
                                        }
                                        if (FG != null && FG != "" && ItemCodeAmount != null && ItemCodeAmount != "")
                                        {
                                            ArrayList ArrlstItemCode = new ArrayList();
                                            ArrayList ArrlstItemID = new ArrayList();
                                            ArrayList ArrlstAmount = new ArrayList();
                                            ArrayList ArrlstBranchID = new ArrayList();
                                            ArrayList ArrlstPRSID = new ArrayList();
                                            ArrayList ArrlstItemDesc = new ArrayList();
                                            ArrayList ArrlstPRSRowid = new ArrayList();

                                            string[] AmountSplit = ItemCodeAmount.Split('|');

                                            if (AmountSplit != null && AmountSplit.Length > 0)
                                            {
                                                for (int k = 0; k < AmountSplit.Length; k++)
                                                {
                                                    if (AmountSplit[k].ToString() != null && AmountSplit[k].ToString() != "")
                                                    {
                                                        string[] stt2 = AmountSplit[k].Split('~');
                                                        if (stt2 != null && stt2.Length > 0)
                                                        {
                                                            ArrlstItemCode.Add(stt2[0].ToString());
                                                            ArrlstItemID.Add(stt2[1].ToString());
                                                            ArrlstAmount.Add(stt2[2].ToString());
                                                            ArrlstBranchID.Add(stt2[3].ToString());
                                                            ArrlstPRSID.Add(stt2[4].ToString());
                                                            ArrlstItemDesc.Add(stt2[5].ToString().TrimEnd(','));
                                                            ArrlstPRSRowid.Add(stt2[6].ToString());
                                                        }
                                                    }
                                                }
                                            }
                                            if (ArrlstItemCode != null && ArrlstItemID != null && ArrlstAmount != null)
                                            {
                                                //objNewquotation.InsertMultiItemsTemp(ArrlstItemCode, ArrlstItemID, ArrlstBranchID, ArrlstAmount, "INSERTTEMP", ArrlstItemDesc);

                                                for (int i = 0; i < ArrlstItemCode.Count; i++)
                                                {

                                                    string inserttemp = "INSERTTEMP";
                                                    string strSql = "EXEC ERP_INSERT_MULTIITEMS '" + ArrlstItemCode[i].ToString() + "','" + ArrlstItemID[i].ToString() + "','" + ArrlstBranchID[i].ToString() + "','" + ArrlstAmount[i].ToString() + "','" + "INSERTTEMP" + "','" + ArrlstItemDesc[i].ToString() + "'";


                                                    SqlCommand cmd = new SqlCommand(strSql, dbConn);
                                                    var reader = cmd.ExecuteReader();
                                                    System.Data.DataTable results = new System.Data.DataTable();
                                                    results.Load(reader);
                                                }

                                            }


                                            //if (countt == 0)
                                            //{



                                            //    string str2 = "exec ERP_RFQ_MAX1_RFQID";


                                            //    SqlCommand cmd1 = new SqlCommand(str2, dbConn);
                                            //    var reader1 = cmd1.ExecuteReader();
                                            //    //System.Data.DataTable results1 = new System.Data.DataTable();
                                            //    results1.Load(reader1);

                                            //    for (int i = 0; i < results1.Rows.Count; i++)
                                            //    {
                                            //        DataRow row = results1.Rows[i];
                                            //        RFQCODE = row[0].ToString();
                                            //        RFQCODE1 = "RFQ/" + RFQCODE + "AT";

                                            //    }
                                            //}
                                            //int countt = 0;
                                            string IDADD = "", SingleVendor = "";
                                            // Mode = "",
                                            for (int index = 0; index < ArrlstItemID.Count; index++)
                                            {
                                                string ItemIDD = ArrlstItemID[index].ToString();
                                                string BranchIDD = ArrlstBranchID[index].ToString();
                                                string PRSIDD = ArrlstPRSID[index].ToString();
                                                string itemdesc = ArrlstItemDesc[index].ToString();
                                                string prsrowid = ArrlstPRSRowid[index].ToString();
                                                Mode = "GETTEMP";


                                                string strSqltemp = "EXEC ERP_INSERT_MULTIITEMS '','" + ItemIDD + "','" + BranchID + "','','" + Mode + "','" + itemdesc + "'";


                                                SqlCommand cmdtemp = new SqlCommand(strSqltemp, dbConn);
                                                var reader = cmdtemp.ExecuteReader();
                                                System.Data.DataTable resultstemp = new System.Data.DataTable();
                                                resultstemp.Load(reader);
                                                for (int i = 0; i < resultstemp.Rows.Count; i++)
                                                {
                                                    DataRow row = resultstemp.Rows[i];
                                                    //RFQCODE = row[0].ToString();
                                                    Qty = row["AMOUNT"].ToString();

                                                }

                                                if (countt == 0)
                                                {
                                                    string str2 = "exec ERP_RFQ_MAX1_RFQID";


                                                    SqlCommand cmd1 = new SqlCommand(str2, dbConn);
                                                    var reader1 = cmd1.ExecuteReader();
                                                    //System.Data.DataTable results1 = new System.Data.DataTable();
                                                    results1.Load(reader1);

                                                    for (int i = 0; i < results1.Rows.Count; i++)
                                                    {
                                                        DataRow row = results1.Rows[i];
                                                        RFQCODE = row[0].ToString();
                                                        RFQCODE1 = "RFQ/" + RFQCODE + "AT";

                                                    }
                                                }

                                                if (resultstemp.Rows.Count > 0)
                                                {



                                                    if (CheckSingleCount == CheckYCount)
                                                    {
                                                        SingleVendor = "Y";
                                                    }
                                                    else
                                                    if (CheckSingleCount == CheckNCount)
                                                    {
                                                        SingleVendor = "N";
                                                    }


                                                    if (!IDADD.Contains(itemdesc.ToUpper().Trim()))
                                                    {
                                                        Mode = "A";

                                                        //ERP_RFQ_MASTER_INSERT_NEW
                                                        if (countt==0)
                                                        {


                                                            string strSql = "EXEC ERP_RFQ_MASTER_INSERT_NEW '1','" + BranchID + "','" + RFQCODE1 + "','" + PRSIDD + "','A','" + Created_by + "','" + ipaddress + "','1','" + ItemIDD + "','" + quotdate + "','" + Qty + "','" + countt + "','" + Mode + "','" + SingleVendor + "','" + prsrowid + "'";
                                                            SqlCommand cmdstrSql = new SqlCommand(strSql, dbConn);
                                                            var readerstrSql = cmdstrSql.ExecuteReader();
                                                            System.Data.DataTable resultsstrSql = new System.Data.DataTable();
                                                            resultsstrSql.Load(readerstrSql);

                                                            for (int i = 0; i < results1.Rows.Count; i++)
                                                            {
                                                                DataRow row = results1.Rows[i];
                                                                RFQID = row[i].ToString().ToString();
                                                            }
                                                        }
                                                        else
                                                        {

                                                            string strSql = "EXEC ERP_RFQ_MASTER_INSERT_NEW '1','" + BranchID + "','" + RFQCODE + "','" + PRSIDD + "','A','" + Created_by + "','" + ipaddress + "','1','" + ItemIDD + "','" + quotdate + "','" + Qty + "','" + countt + "','" + Mode + "','" + SingleVendor + "','" + prsrowid + "'";
                                                            SqlCommand cmdstrSql = new SqlCommand(strSql, dbConn);
                                                            var readerstrSql = cmdstrSql.ExecuteReader();
                                                            System.Data.DataTable resultsstrSql = new System.Data.DataTable();
                                                            resultsstrSql.Load(readerstrSql);

                                                            for (int i = 0; i < results1.Rows.Count; i++)
                                                            {
                                                                DataRow row = results1.Rows[i];
                                                                RFQID = row[i].ToString().ToString();
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        Mode = "S";
                                                        string strSql = "EXEC ERP_RFQ_MASTER_INSERT_NEW '1','" + BranchID + "','" + RFQCODE1 + "','" + PRSIDD + "','A','" + Created_by + "','" + ipaddress + "','1','" + ItemIDD + "','" + quotdate + "','" + Qty + "','" + countt + "','" + Mode + "','" + SingleVendor + "','" + prsrowid + "'";
                                                        SqlCommand cmdstrSql = new SqlCommand(strSql, dbConn);
                                                        var readerstrSql = cmdstrSql.ExecuteReader();
                                                        System.Data.DataTable resultsstrSql = new System.Data.DataTable();
                                                        resultsstrSql.Load(readerstrSql);

                                                        for (int i = 0; i < results1.Rows.Count; i++)
                                                        {
                                                            DataRow row = results1.Rows[i];
                                                            RFQID = results1.Rows[i]["RFQID"].ToString();
                                                        }
                                                    }
                                                    IDADD += itemdesc.ToUpper().Trim() + ",";
                                                    // countt = countt + 1;
                                                }
                                                countt = countt + 1;
                                            }
                                            string Str1 = "DROP TABLE RFQTemp";
                                            SqlCommand cmdStr1 = new SqlCommand(Str1, dbConn);
                                            var readerStr1 = cmdStr1.ExecuteReader();
                                            System.Data.DataTable resultsStr1 = new System.Data.DataTable();
                                            resultsStr1.Load(readerStr1);
                                        }
                                        else
                                            if (ItemCodeAmount != null && ItemCodeAmount != "")
                                        {
                                            ArrayList ArrlstItemCode = new ArrayList();
                                            ArrayList ArrlstItemID = new ArrayList();
                                            ArrayList ArrlstAmount = new ArrayList();
                                            ArrayList ArrlstBranchID = new ArrayList();
                                            ArrayList ArrlstPRSID = new ArrayList();

                                            string[] AmountSplit = ItemCodeAmount.Split('|');

                                            if (AmountSplit != null && AmountSplit.Length > 0)
                                            {
                                                for (int k = 0; k < AmountSplit.Length; k++)
                                                {
                                                    if (AmountSplit[k].ToString() != null && AmountSplit[k].ToString() != "")
                                                    {
                                                        string[] stt2 = AmountSplit[k].Split('~');
                                                        if (stt2 != null && stt2.Length > 0)
                                                        {
                                                            ArrlstItemCode.Add(stt2[0].ToString());
                                                            ArrlstItemID.Add(stt2[1].ToString());
                                                            ArrlstAmount.Add(stt2[2].ToString());
                                                            ArrlstBranchID.Add(stt2[3].ToString());
                                                            ArrlstPRSID.Add(stt2[4].ToString());
                                                        }
                                                    }
                                                }
                                                if (ArrlstItemCode != null && ArrlstItemID != null && ArrlstAmount != null)
                                                {


                                                    string strSql = "EXEC ERP_RFQ_MASTER_INSERT_NEW '1','" + BranchID + "','" + RFQCODE + "','" + prsid + "','A','" + Created_by + "','" + ipaddress + "','1','" + itemid + "','" + quotdate + "','" + Qty + "','" + countt + "','" + Mode + "','N','" + rowid + "'";
                                                    SqlCommand cmdstrSql = new SqlCommand(strSql, dbConn);
                                                    var readerstrSql = cmdstrSql.ExecuteReader();
                                                    System.Data.DataTable resultsstrSql = new System.Data.DataTable();
                                                    resultsstrSql.Load(readerstrSql);

                                                    for (int i = 0; i < resultsstrSql.Rows.Count; i++)
                                                    {
                                                        DataRow row = resultsstrSql.Rows[i];
                                                        RFQID = resultsstrSql.Rows[i]["RFQID"].ToString();
                                                    }
                                                    string Str1 = "DROP TABLE RFQTemp";
                                                    SqlCommand cmddrp = new SqlCommand(Str1, dbConn);
                                                    var readerdrp = cmddrp.ExecuteReader();
                                                    System.Data.DataTable resultsdrp = new System.Data.DataTable();
                                                    resultsdrp.Load(readerdrp);
                                                }
                                            }
                                        }
                                    }
                                }



                                string rfq = "select RFQCode from ERP_RFQ_MASTER INNER JOIN USERACCESS WITH (NOLOCK) ON USERACCESS.FUNCTION_ID=ERP_RFQ_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID=ERP_RFQ_MASTER.CREATED_BY  where ERP_RFQ_MASTER.RFQID ='" + RFQID + "'";
                                SqlCommand cmdrfq = new SqlCommand(rfq, dbConn);
                                var readerrfq = cmdrfq.ExecuteReader();
                                System.Data.DataTable resultsrfq = new System.Data.DataTable();
                                resultsrfq.Load(readerrfq);

                                for (int i = 0; i < resultsrfq.Rows.Count; i++)
                                {
                                    DataRow row = resultsrfq.Rows[i];
                                    RFQCODE = resultsrfq.Rows[i]["RFQCODE"].ToString();
                                }


                                stroutput = "RFQ Raised Successfully : " + RFQCODE1 + ".";
                            }

                        }
                        else if (count > 0)
                        {
                            if (checkprs != 0)
                            {
                                string strsql = "exec ERP_RFQ_getprsCode '" + prs_id + "' ";
                                SqlCommand cmdd = new SqlCommand(strsql, dbConn);
                                var readerr = cmdd.ExecuteReader();
                                System.Data.DataTable resultss = new System.Data.DataTable();
                                resultss.Load(readerr);

                                for (int i = 0; i < resultss.Rows.Count; i++)
                                {
                                    DataRow row = resultss.Rows[i];
                                    prscode = resultss.Rows[i]["prs_code"].ToString();
                                }
                                stroutput = "RFQ Already Raised for " + prscode + ".";
                                // PopulateGrid();
                            }
                            else
                            {
                                stroutput = "RFQ Already Raised";

                            }
                        }
                        else
                            if (CheckSingleCount != CheckYCount && CheckSingleCount != CheckNCount)
                        {
                            stroutput = "Selected all PRS should be Single Vendor or It Should be Non Single Vendor";
                        }
                    }


                    logdata = stroutput;

                    var result = (new { logdata });
                    return Ok(logdata);


                    // return Ok(json);
                }
            }

            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }

        }

        #endregion

        #region

        //ADD vendor SHY

        [HttpPost]
        [Route("Add_Vendor")]
        public async Task<ActionResult<ERP>> Add_Vendor(dynamic data)
        {

            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            string functionid = "";
            string rfqcode = "";
            string vendorid = "";
            string itemid = "";
            string itemcategory = "";
            string itemsubcategory = "";
            string brand = "";
            string model = "";
            string rating = "";
            string mode = "";
            string userid = "";
            string ipaddress = "";
            string unitprice = "";
            string netamount = "";
            string discount = "";
            string taxes = "";
            string transportcharges = "";
            string fromqty = "";
            string toqty = "";
            string leadtime = "";
            string itemcode = "";
            string itemdesc1 = "";
            string tamount = "";
            string qty = "";
            string vendoritemid = "";
            string prsid = "", rowid = "";

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))

                {
                    dbConn.Open();

                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent2 = obj_parents.GetValue("Add_vendor")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent2)
                    {
                        JProperty p2 = obj_parent2.Property(item.Key);



                        if (item.Key == "Vendor_details")
                        {
                            var Vendor_details = item.Value.ToString();

                            JArray array = JArray.Parse(Vendor_details);
                            JArray jsonArray = JArray.Parse(Vendor_details);

                            foreach (JObject content in array.Children<JObject>())
                            {
                                foreach (JProperty prop in content.Properties())
                                {
                                    string Name = prop.Name.ToString().Trim();
                                    string Value = prop.Value.ToString().Trim();



                                    if (Name == "functionid")
                                    {
                                        if (Value.ToString()=="")
                                        {
                                            functionid = null;
                                        }
                                        else
                                        {
                                            functionid = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "rfqcode")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            rfqcode = null;
                                        }
                                        else
                                        {
                                            rfqcode = Value.ToString();
                                        }
                                        
                                    }

                                    if (Name == "vendorid")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            vendorid = null;
                                        }
                                        else
                                        {
                                            vendorid = Value.ToString();

                                        }
                                       
                                    }
                                    if (Name == "itemid")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            itemid = null;

                                        }
                                        else
                                        {
                                            itemid = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "itemcategory")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            itemcategory = null;
                                        }
                                        else
                                        {
                                            itemcategory = Value.ToString();

                                        }
                                       
                                    }
                                    if (Name == "itemsubcategory")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            itemsubcategory = null;
                                        }
                                        else
                                        {
                                            itemsubcategory = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "brand")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            brand = "";
                                        }
                                        else
                                        {
                                            brand = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "model")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            model = "";
                                        }
                                        else
                                        {
                                            model = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "rating")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            rating = "";
                                        }
                                        else
                                        {
                                            rating = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "mode")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            mode = "";
                                        }
                                        else
                                        {
                                            mode = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "ipaddress")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            ipaddress = "";
                                        }
                                        else
                                        {
                                            ipaddress = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "unitprice")
                                    {
                                        if (Value.ToString() == "")
                                        {
                                            unitprice = null;
                                        }
                                        else
                                        {
                                            unitprice = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "netamount")
                                    {
                                        if (Value.ToString() == "")
                                        {
                                            netamount = null;
                                        }
                                        else
                                        {
                                            netamount = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "discount")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            discount = null;
                                        }
                                        else
                                        {
                                            discount = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "taxes")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            taxes = null;
                                        }
                                        else
                                        {
                                            taxes = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "transportcharges")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            transportcharges = null;
                                        }
                                        else
                                        {
                                            transportcharges = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "fromqty")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            fromqty = null;
                                        }
                                        else
                                        {
                                            fromqty = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "toqty")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            toqty = null;
                                        }
                                        else
                                        {
                                            toqty = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "leadtime")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            leadtime = null;
                                        }
                                        else
                                        {
                                            leadtime = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "itemcode")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            itemcode = null;
                                        }
                                        else
                                        {
                                            itemcode = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "itemdesc1")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            itemdesc1 = null;
                                        }
                                        else
                                        {
                                            itemdesc1 = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "tamount")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            tamount = null;
                                        }
                                        else
                                        {
                                            tamount = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "qty")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            qty = null;
                                        }
                                        else
                                        {
                                            qty = Value.ToString();
                                        }
                                      
                                    }
                                    if (Name == "vendoritemid")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            vendoritemid = null;
                                        }
                                        else
                                        {
                                            vendoritemid = Value.ToString();
                                        }
                                       
                                    }
                                    if (Name == "prsid")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            prsid = null;
                                        }
                                        else
                                        {
                                            prsid = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "rOW_NUM")
                                    {
                                        if (Value.ToString() == "")
                                        {
                                            rowid = "";
                                        }
                                        else
                                        {
                                            rowid = Value.ToString();
                                        }
                                        
                                    }
                                    if (Name == "userid")
                                    {

                                        if (Value.ToString() == "")
                                        {
                                            userid = null;
                                        }
                                        else
                                        {
                                            userid = Value.ToString();

                                        }
                                        
                                    }
                                }

                                DataSet dsuserdetails = new DataSet();

                                //string sql = "ERP_ADDVENDORTEMP";

                                string sql = "ERP_ADDVENDORTEMP";
                                SqlCommand cmd = new SqlCommand(sql, dbConn);


                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@FUNCTIONID", functionid);
                                cmd.Parameters.AddWithValue("@RFQCODE", rfqcode);
                                cmd.Parameters.AddWithValue("@VENDORID", vendorid);
                                cmd.Parameters.AddWithValue("@ITEMID", itemid);
                                cmd.Parameters.AddWithValue("@ITEMCATEGORY", itemcategory);
                                cmd.Parameters.AddWithValue("@ITEMSUBCATEGORY", itemsubcategory);
                                cmd.Parameters.AddWithValue("@BRAND", brand);
                                cmd.Parameters.AddWithValue("@MODEL", model);
                                cmd.Parameters.AddWithValue("@RATING", rating);
                                cmd.Parameters.AddWithValue("@MODE", mode);
                                cmd.Parameters.AddWithValue("@USERID", userid);
                                cmd.Parameters.AddWithValue("@IPADDRESS", "");
                                //cmd.Parameters.AddWithValue("@UNIT_PRICE", Convert.ToDecimal(unitprice));
                                //cmd.Parameters.AddWithValue("@NET_PRICE_PER_UNIT", Convert.ToDecimal(netamount));
                                //cmd.Parameters.AddWithValue("@DISCOUNT", Convert.ToDecimal(discount));
                                //cmd.Parameters.AddWithValue("@TAXES_AND_LEVIES ", Convert.ToDecimal(taxes));
                                //cmd.Parameters.AddWithValue("@TRANSPORT_CHARGES", Convert.ToDecimal(transportcharges));


                                cmd.Parameters.AddWithValue("@UNIT_PRICE", unitprice);
                                cmd.Parameters.AddWithValue("@NET_PRICE_PER_UNIT", netamount);
                                cmd.Parameters.AddWithValue("@DISCOUNT", discount);
                                cmd.Parameters.AddWithValue("@TAXES_AND_LEVIES ", taxes);
                                cmd.Parameters.AddWithValue("@TRANSPORT_CHARGES", transportcharges);
                                cmd.Parameters.AddWithValue("@FROM_QTY", fromqty);
                                cmd.Parameters.AddWithValue("@TO_QTY", toqty);
                                cmd.Parameters.AddWithValue("@LEAD_TIME", leadtime);
                                cmd.Parameters.AddWithValue("@SINGLE", "N");
                                cmd.Parameters.AddWithValue("@ITEMCODE", itemcode);
                                cmd.Parameters.AddWithValue("@ITEMDESC", itemdesc1);
                                cmd.Parameters.AddWithValue("@AMOUNT", tamount);
                                cmd.Parameters.AddWithValue("@REQQTY", qty);
                                cmd.Parameters.AddWithValue("@ROWID", rowid);
                                cmd.Parameters.AddWithValue("@VENDORITEMID", vendoritemid);
                                cmd.Parameters.AddWithValue("@PRSDETAILSID", prsid);


                               // cmd.ExecuteNonQuery();
                                var reader = cmd.ExecuteReader();
                                System.Data.DataTable results = new System.Data.DataTable();
                                results.Load(reader);
                                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                                if (results.Rows.Count > 0)
                                {
                                    for (int i = 0; i < results.Rows.Count; i++)
                                    {
                                        DataRow row = results.Rows[i];
                                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                                        logdata = DataTableToJSONWithStringBuilder(results);

                                        dbConn.Close();
                                    }

                                }
                                else
                                {
                                    Logdata1 = "Successfully Added Vendor ";

                                }



                            }
                        }
                    }
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }

        #endregion

        //VENDOR GRID BINDING DATA


        [HttpGet]
        [Route("getvendor_RFQ")]
        public string getvendor_RFQ(string functionId, string rfqcode)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                //ERP_GETVENDORTEMPDETAILS1
                string strsql = "EXEC ERP_GETVENDORTEMPDETAILS @FUNCTIONID='" + functionId + "',@RFQCODE='" + rfqcode + "'";
                SqlCommand cmd = new SqlCommand(strsql, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();
                return Logdata1;
            }
        }

        //delete added vendor in raise RFQ


        [HttpGet]
        [Route("deletevendor_RFQ/{functionId}/{rfqcode}/{vendorid}")]
        public string deletevendor_RFQ(string functionId, string rfqcode,string vendorid)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";

                string strsql = "Delete from ERP_VENDOR_TEMPDETAILS where RFQCode='" + rfqcode + "' and FUNCTION_ID='" + functionId + "' AND VENDOR_ID='" + vendorid + "'";
                SqlCommand cmd = new SqlCommand(strsql, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                Logdata1 = DataTableToJSONWithStringBuilder(results);
                dbConn.Close();
                return Logdata1;
            }
        }


        //shankari

        //issue details Search

        //ERP_MI_GETSTOCKDETAILS



        [HttpPost]
        [Route("MaterialIssueDetailsPopupSearch")]
        public async Task<ActionResult<ERP>> MaterialIssueDetailsPopupSearch(ERP data)
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


                    if (data.FUNCTIONIDMIS.ToString() == "0" || data.FUNCTIONIDMIS.ToString() == "" || data.FUNCTIONIDMIS.ToString() == string.Empty || data.FUNCTIONIDMIS.ToString() == null)
                    {
                        data.FUNCTIONIDMIS = "0";
                    }

                    if (data.BRANCHIDMIS.ToString() == "0" || data.BRANCHIDMIS.ToString() == "" || data.BRANCHIDMIS.ToString() == string.Empty || data.BRANCHIDMIS.ToString() == null)
                    {
                        data.BRANCHIDMIS = "0";
                    }
                    if (data.LOCATION_IDMIS.ToString() == "0" || data.LOCATION_IDMIS.ToString() == "" || data.LOCATION_IDMIS.ToString() == string.Empty || data.LOCATION_IDMIS.ToString() == null)
                    {
                        data.LOCATION_IDMIS = "0";
                    }




                    if (data.BINMIS.ToString() == "0" || data.BINMIS.ToString() == "" || data.BINMIS.ToString() == string.Empty || data.BINMIS.ToString() == null)
                    {
                        data.BINMIS = "0";
                    }



                    if (data.ITEM_IDMIS.ToString() == "0" || data.ITEM_IDMIS.ToString() == "" || data.ITEM_IDMIS.ToString() == string.Empty || data.ITEM_IDMIS.ToString() == null)
                    {
                        data.ITEM_IDMIS = "0";
                    }

                    if (data.ALPHANAMEMIS.ToString() == "0" || data.ALPHANAMEMIS.ToString() == "" || data.ALPHANAMEMIS.ToString() == string.Empty || data.ALPHANAMEMIS.ToString() == null)
                    {
                        data.ALPHANAMEMIS = "0";
                    }
                    if (data.SORTEXPRESSIONMIS.ToString() == "0" || data.SORTEXPRESSIONMIS.ToString() == "" || data.SORTEXPRESSIONMIS.ToString() == string.Empty || data.SORTEXPRESSIONMIS.ToString() == null)
                    {
                        data.SORTEXPRESSIONMIS = "0";
                    }
                    if (data.PAGEINDEXMIS.ToString() == "0" || data.PAGEINDEXMIS.ToString() == "" || data.PAGEINDEXMIS.ToString() == string.Empty || data.PAGEINDEXMIS.ToString() == null)
                    {
                        data.PAGEINDEXMIS = 0;
                    }
                    if (data.PAGESIZEMIS.ToString() == "0" || data.PAGESIZEMIS.ToString() == "" || data.PAGESIZEMIS.ToString() == string.Empty || data.PAGESIZEMIS.ToString() == null)
                    {
                        data.PAGESIZEMIS = 0;
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                   // string sql = "ERP_MI_GETSTOCKDETAILS";

                    string sql = "ERP_MI_GETSTOCKDETAILS";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);

                    //EXEC ERP_MI_GETSTOCKDETAILS @FUNCTIONID='1',@BRANCHID='1',@LOCATION_ID='12',@BIN='36',@ITEM_ID='1272',@PAGEINDEX='0',@PAGESIZE='10',@SORTEXPRESSION='batch_serial_no desc',@ALPHANAME='' ,@STOREE='165',@RACKE='5953'

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.FUNCTIONIDMIS);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.BRANCHIDMIS);
                    cmd.Parameters.AddWithValue("@LOCATION_ID", data.LOCATION_IDMIS);
                    cmd.Parameters.AddWithValue("@BIN", data.BINMIS);
                    cmd.Parameters.AddWithValue("@ITEM_ID", data.ITEM_IDMIS);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEMIS);

                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONMIS);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXMIS);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEMIS);
                    cmd.Parameters.AddWithValue("@STOREE", data.STOREEMIS);
                    cmd.Parameters.AddWithValue("@RACKE", data.RACKEMIS);



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





        //Material Issue Search Payload
        //Location L
        //Material Requition Slip - MR
        //Sales Return SR
        //Sort Expression - item_short_desc
        //


        [Route("MaterialIssueAllDetails")]
        public async Task<ActionResult<ERP>> MaterialIssueAllDetails(ERP data)
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


                    if (data.FUNCTIONIDMI.ToString() == "0" || data.FUNCTIONIDMI.ToString() == "" || data.FUNCTIONIDMI.ToString() == string.Empty || data.FUNCTIONIDMI.ToString() == null)
                    {
                        data.FUNCTIONIDMI = "0";
                    }

                    if (data.BRANCHIDMI.ToString() == "0" || data.BRANCHIDMI.ToString() == "" || data.BRANCHIDMI.ToString() == string.Empty || data.BRANCHIDMI.ToString() == null)
                    {
                        data.BRANCHIDMI = "0";
                    }
                    //if (data.ITEM_CODEMI.ToString() == "0" || data.ITEM_CODEMI.ToString() == "" || data.ITEM_CODEMI.ToString() == string.Empty || data.ITEM_CODEMI.ToString() == null)
                    //{
                    //    data.ITEM_CODEMI = "0";
                    //}




                    if (data.ITEM_REFMI.ToString() == "0" || data.ITEM_REFMI.ToString() == "" || data.ITEM_REFMI.ToString() == string.Empty || data.ITEM_REFMI.ToString() == null)
                    {
                        data.ITEM_REFMI = "";
                    }



                    if (data.ILT_REFMI.ToString() == "0" || data.ILT_REFMI.ToString() == "" || data.ILT_REFMI.ToString() == string.Empty || data.ILT_REFMI.ToString() == null)
                    {
                        data.ILT_REFMI = "";
                    }

                    if (data.SR_REFMI.ToString() == "0" || data.SR_REFMI.ToString() == "" || data.SR_REFMI.ToString() == string.Empty || data.SR_REFMI.ToString() == null)
                    {
                        data.SR_REFMI = "";
                    }
                    if (data.FROMDATEMI.ToString() == "0" || data.FROMDATEMI.ToString() == "" || data.FROMDATEMI.ToString() == string.Empty || data.FROMDATEMI.ToString() == null)
                    {
                        data.FROMDATEMI = "";
                    }
                    if (data.TODATEMI.ToString() == "0" || data.TODATEMI.ToString() == "" || data.TODATEMI.ToString() == string.Empty || data.TODATEMI.ToString() == null)
                    {
                        data.TODATEMI = "";
                    }
                    if (data.STATUSMI.ToString() == "0" || data.STATUSMI.ToString() == "" || data.STATUSMI.ToString() == string.Empty || data.STATUSMI.ToString() == null)
                    {
                        data.STATUSMI = "";
                    }
                    if (data.ALPHANAMEMI.ToString() == "0" || data.ALPHANAMEMI.ToString() == "" || data.ALPHANAMEMI.ToString() == string.Empty || data.ALPHANAMEMI.ToString() == null)
                    {
                        data.ALPHANAMEMI = "";
                    }
                    if (data.SORTEXPRESSIONMI.ToString() == "0" || data.SORTEXPRESSIONMI.ToString() == "" || data.SORTEXPRESSIONMI.ToString() == string.Empty || data.SORTEXPRESSIONMI.ToString() == null)
                    {
                        data.SORTEXPRESSIONMI = "";
                    }
                    if (data.PAGEINDEXMI.ToString() == "0" || data.PAGEINDEXMI.ToString() == "" || data.PAGEINDEXMI.ToString() == string.Empty || data.PAGEINDEXMI.ToString() == null)
                    {
                        data.PAGEINDEXMI = 0;
                    }

                    if (data.PAGESIZEMI.ToString() == "0" || data.PAGESIZEMI.ToString() == "" || data.PAGESIZEMI.ToString() == string.Empty || data.PAGESIZEMI.ToString() == null)
                    {
                        data.PAGESIZEMI = 0;
                    }
                    if (data.SEARCH_TYPEMI.ToString() == "0" || data.SEARCH_TYPEMI.ToString() == "" || data.SEARCH_TYPEMI.ToString() == string.Empty || data.SEARCH_TYPEMI.ToString() == null)
                    {
                        data.SEARCH_TYPEMI = "MI";
                    }


                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                   // string sql = "ERP_MATISSUE_GETISSUESUMMARY";

                    string sql = "ERP_MI_GETISSUESUMMARY";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);

                    //EXEC ERP_MI_GETISSUESUMMARYS '1','1','','', '','', '','','0', '', 'item_short_desc','0', '20' ,  'MI'

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.FUNCTIONIDMI);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.BRANCHIDMI);
                    cmd.Parameters.AddWithValue("@ITEM_CODE", data.ITEM_CODEMI);
                    cmd.Parameters.AddWithValue("@ITEM_REF", data.ITEM_REFMI);
                    cmd.Parameters.AddWithValue("@ILT_REF", data.ILT_REFMI);
                    cmd.Parameters.AddWithValue("@SR_REF", data.SR_REFMI);

                    cmd.Parameters.AddWithValue("@FROMDATE", data.FROMDATEMI);
                    cmd.Parameters.AddWithValue("@TODATE", data.TODATEMI);
                    cmd.Parameters.AddWithValue("@STATUS", data.STATUSMI);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEMI);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONMI);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXMI);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEMI);
                    cmd.Parameters.AddWithValue("@SEARCH_TYPE", data.SEARCH_TYPEMI);
                    //cmd.Parameters.AddWithValue("@RBY", data.RBYMI);
                    //cmd.Parameters.AddWithValue("@STORE", data.STOREMI);


                    //cmd.ExecuteNonQuery();
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


        //sang Inter Location Transfer

        [HttpPost]
        [Route("InterLocationTransferSummary")]
        public async Task<ActionResult<ERP>> InterLocationTransferSummary(ERP data)
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


                    if (data.FUNCTIONIDILT.ToString() == "0" || data.FUNCTIONIDILT.ToString() == "" || data.FUNCTIONIDILT.ToString() == string.Empty || data.FUNCTIONIDILT.ToString() == null)
                    {
                        data.FUNCTIONIDILT = "0";
                    }

                    if (data.BRANCHIDILT.ToString() == "0" || data.BRANCHIDILT.ToString() == "" || data.BRANCHIDILT.ToString() == string.Empty || data.BRANCHIDILT.ToString() == null)
                    {
                        data.BRANCHIDILT = "0";
                    }
                    if (data.FROMDATEILT.ToString() == "0" || data.FROMDATEILT.ToString() == "" || data.FROMDATEILT.ToString() == string.Empty || data.FROMDATEILT.ToString() == null)
                    {
                        data.FROMDATEILT = "0";
                    }




                    if (data.TODATEILT.ToString() == "0" || data.TODATEILT.ToString() == "" || data.TODATEILT.ToString() == string.Empty || data.TODATEILT.ToString() == null)
                    {
                        data.TODATEILT = "0";
                    }



                    if (data.STATUSILT.ToString() == "0" || data.STATUSILT.ToString() == "" || data.STATUSILT.ToString() == string.Empty || data.STATUSILT.ToString() == null)
                    {
                        data.STATUSILT = "0";
                    }

                    if (data.MODEILT.ToString() == "0" || data.MODEILT.ToString() == "" || data.MODEILT.ToString() == string.Empty || data.MODEILT.ToString() == null)
                    {
                        data.MODEILT = "0";
                    }
                    if (data.STRITEMCODEILT.ToString() == "0" || data.STRITEMCODEILT.ToString() == "" || data.STRITEMCODEILT.ToString() == string.Empty || data.STRITEMCODEILT.ToString() == null)
                    {
                        data.STRITEMCODEILT = "0";
                    }
                    if (data.ALPHANAMEILT.ToString() == "0" || data.ALPHANAMEILT.ToString() == "" || data.ALPHANAMEILT.ToString() == string.Empty || data.ALPHANAMEILT.ToString() == null)
                    {
                        data.ALPHANAMEILT = "0";
                    }
                    if (data.SORTEXPRESSIONILT.ToString() == "0" || data.SORTEXPRESSIONILT.ToString() == "" || data.SORTEXPRESSIONILT.ToString() == string.Empty || data.SORTEXPRESSIONILT.ToString() == null)
                    {
                        data.SORTEXPRESSIONILT = "0";
                    }


                    if (data.PAGEINDEXILT.ToString() == "0" || data.PAGEINDEXILT.ToString() == "" || data.PAGEINDEXILT.ToString() == string.Empty || data.PAGEINDEXILT.ToString() == null)
                    {
                        data.PAGEINDEXILT = 0;
                    }
                    if (data.PAGESIZEILT.ToString() == "0" || data.PAGESIZEILT.ToString() == "" || data.PAGESIZEILT.ToString() == string.Empty || data.PAGESIZEILT.ToString() == null)
                    {
                        data.PAGESIZEILT = 0;
                    }
                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    //string sql = "ERP_ILT_GETINTERLOCATIONMASTERSUMMARY";

                    string sql = "ERP_ILT_GETINTERLOCATIONMASTERSUMMARY";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);

                    //EXEC ERP_ILT_GETINTERLOCATIONMASTERSUMMARY @FUNCTION_ID = '1',@BRANCHID = '1',@FROMDATE = '',@TODATE = '',@STATUS = '',@MODE = 'IN',@INTERREF = '',@STRITEMCODE = '',@ALPHANAME = '',@SORTEXPRESSION = 'CodeDesc',@PAGEINDEX = '0',@PAGESIZE = '20'

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTION_ID", data.FUNCTIONIDILT);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.BRANCHIDILT);
                    cmd.Parameters.AddWithValue("@FROMDATE", data.FROMDATEILT);
                    cmd.Parameters.AddWithValue("@TODATE", data.TODATEILT);
                    cmd.Parameters.AddWithValue("@STATUS", data.STATUSILT);
                    cmd.Parameters.AddWithValue("@MODE", data.MODEILT);
                    cmd.Parameters.AddWithValue("@INTERREF", data.INTERREFILT);

                    cmd.Parameters.AddWithValue("@STRITEMCODE", data.STRITEMCODEILT);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEILT);
                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONILT);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXILT);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEILT);



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


        //sang InterLocationTransfer Branch Get 


        [HttpGet]
        [Route("GetBranch/{FunID}")]
        public dynamic GetBranch(int FunID)
        {
            List<ERP> Logdata = new List<ERP>();
            string Logdata1 = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";


                query = "select distinct BO_BRANCH_MASTER.BRANCH_ID,BO_BRANCH_MASTER.BRANCH_DESC from BO_BRANCH_MASTER INNER JOIN BO_FUNCTION_ACCESS WITH(NOLOCK) ON BO_FUNCTION_ACCESS.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = BO_FUNCTION_ACCESS.FUNCTION_ID where 1 = 1AND BO_BRANCH_MASTER.FUNCTION_ID = '" + FunID + "'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                Logdata1 = DataTableToJSONWithStringBuilder(results);

                dbConn.Close();
            }
            return Logdata1;
        }



        [HttpPost]
        [Route("InterLocation_Insert_Update")]
        public async Task<ActionResult<ERP>> InterLocation_Insert_Update(dynamic data)
        {
            string status = ""; string ChkRelease = ""; string branch_to = ""; string branch_id = ""; string FUNCTION_ID = ""; string itrfid = "";
            string btnSave = ""; string itrf_id = ""; string itrf_ref = ""; string isconfig = ""; string itrf_date = ""; string trf_mode = ""; string remarks = ""; string IPADDRESS = ""; string CREATED_BY = ""; string LST_UPD_BY = ""; string itrfid1 = ""; string LST_UPD_ON = ""; string Outputval = "";


            string function_id = ""; string ITEM_QTY = ""; string remarkss = ""; string statuss = ""; string created_by = "";
            string created_on = ""; string lst_upd_by = ""; string lst_upd_on = ""; string ipaddress = ""; string branch_ids = "";
            string item_detailed_description = "";
            int id = 0;
            int dt = 2;
            int dt1 = 1;
            int i = 0;
            int inew = 0;
            string itemFromFlag = "";
            string strMessage = "";
            string ItrfTransferNo = "";
            DataRow row = null;
            int id1 = 1;
            int dtb = 1;
            DataSet dsInterLocationTransfer = new DataSet();
            DataSet dstILTMaster = new DataSet();
            DataSet dsStockUpdation = new DataSet();
            CultureInfo objCulture;
            string culture = "";
            ArrayList err = new ArrayList();
            DataSet ds1 = new DataSet();
            DataSet dsBranch = new DataSet();
            string strMode = "";
            string CREATED_ON = "";
            string type = "InterLocationTransfer";
            string itrfno = "";
            //string LST_UPD_BY = "";
            string BRANCH_ID = "";
            string ILT_ID = "";


            string pk_column_name1 = string.Empty;

            string pk_column_name2 = string.Empty;

            string pk_column_name3 = string.Empty;

            string pk_column_name4 = string.Empty;

            string pk_column_name5 = string.Empty;


            string STATUS_COLUMN = string.Empty;

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    dbConn.Open();
                    var serializedObject = JsonConvert.SerializeObject(data).ToString();
                    var obj = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj1 = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent1 = obj_parent.GetValue("MasterInterlocation")[0] as JObject;
                    foreach (KeyValuePair<string, JToken> item in obj_parent1)


                    {
                        JProperty p1 = obj_parent1.Property(item.Key);



                        if (item.Key == "FUNCTION_ID")
                        {
                            FUNCTION_ID = item.Value.ToString();
                        }




                        if (item.Key == "branch_id")
                        {
                            branch_id = item.Value.ToString();
                        }
                        if (item.Key == "branch_to")
                        {
                            branch_to = item.Value.ToString();

                        }

                        //if (ChkRelease == "true")
                        //{
                        //    if (item.Key == "status")
                        //    {
                        //        status = "N";
                        //    }

                        //}



                        //if (status != "" && status != null)
                        //{
                        //    if (status == "D")
                        //    {
                        //        if (item.Key == "status")
                        //        {
                        //            status = item.Value.ToString();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (item.Key == "status")
                        //        {
                        //            status = item.Value.ToString();

                        //        }
                        //    }
                        //}


                        if (FUNCTION_ID != "")
                        {
                            if (itrf_ref == "")
                            {

                                if (btnSave != "Initiate")
                                {
                                    DataSet dsmrscode = new DataSet();
                                    string sqlmrscode = "ERP_PRS_ISCONFIG";
                                    SqlCommand cmdmrscode = new SqlCommand(sqlmrscode, dbConn);


                                    cmdmrscode.CommandType = CommandType.StoredProcedure;

                                    cmdmrscode.Parameters.AddWithValue("@FUNCTIONID", FUNCTION_ID);
                                    cmdmrscode.Parameters.AddWithValue("@TYPE", "InterLocationTransfer");

                                    cmdmrscode.ExecuteNonQuery();
                                    var mrscodereader = cmdmrscode.ExecuteReader();
                                    System.Data.DataTable resultsmrscode = new System.Data.DataTable();
                                    resultsmrscode.Load(mrscodereader);
                                    //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                                    for (int l = 0; l < resultsmrscode.Rows.Count; l++)
                                    {
                                        DataRow rowmrscode = resultsmrscode.Rows[l];
                                        isconfig = rowmrscode[0].ToString();


                                    }

                                    itrf_ref = isconfig;
                                }
                            }

                        }
                        else
                        {
                            itrf_ref = isconfig;
                        }
                        //if (item.Key == "itrf_ref")
                        //{
                        //    itrf_ref = item.Value.ToString();

                        //}



                        if (item.Key == "itrf_date")
                        {
                            itrf_date = item.Value.ToString();
                        }
                        if (item.Key == "trf_mode")
                        {
                            trf_mode = item.Value.ToString();
                        }
                        trf_mode = "Intra";

                        if (item.Key == "remarks")
                        {
                            remarks = item.Value.ToString();
                        }
                        if (item.Key == "status")
                        {
                            status = item.Value.ToString();
                        }

                        if (item.Key == "CREATED_BY")
                        {
                            CREATED_BY = item.Value.ToString();
                        }
                        if (item.Key == "CREATED_ON")
                        {
                            CREATED_ON = item.Value.ToString();
                        }
                        if (item.Key == "LST_UPD_BY")
                        {
                            LST_UPD_BY = item.Value.ToString();
                        }
                        if (item.Key == "LST_UPD_ON")
                        {
                            LST_UPD_ON = item.Value.ToString();
                        }

                        if (item.Key == "IPADDRESS")
                        {
                            IPADDRESS = item.Value.ToString();
                        }
                        //if (item.Key == "ILT_ID")
                        //{
                        //    ILT_ID = item.Value.ToString();
                        //}



                        if (item.Key == "itrf_id")
                        {
                            itrf_id = item.Value.ToString();
                        }

                        if (btnSave != "Initiate")
                        {

                            //dbConn.Open();

                            string query = "";
                            query = "select max(ILT_ID) as maxid from ERP_ILT_MASTER";

                            SqlCommand cmd = new SqlCommand(query, dbConn);
                            var reader = cmd.ExecuteReader();
                            System.Data.DataTable results = new System.Data.DataTable();
                            results.Load(reader);

                            for (int k = 0; k < results.Rows.Count; k++)
                            {
                                DataRow row1 = results.Rows[k];
                                itrfid = row1[0].ToString() + 1;
                            }

                            //dbConn.Close();
                        }






                        if (btnSave != "Initiate")
                        {


                            if (FUNCTION_ID == "" && FUNCTION_ID == "0" && BRANCH_ID == "" && BRANCH_ID == "0" && ILT_ID == "" && ILT_ID == "0")
                            {

                                DataSet dsuserdetails1 = new DataSet();
                                string sql1 = "ERP_ILT_INSERT";
                                SqlCommand objcommand = new SqlCommand(sql1, dbConn);


                                objcommand.CommandType = CommandType.StoredProcedure;
                                objcommand.Parameters.AddWithValue("@function_id", FUNCTION_ID);
                                objcommand.Parameters.AddWithValue("@branch_id", branch_id);
                                objcommand.Parameters.AddWithValue("@branch_to", branch_to.ToString());

                                objcommand.Parameters.AddWithValue("@ILT_REF", isconfig);
                                objcommand.Parameters.AddWithValue("@ILT_DATE", itrf_date);
                                objcommand.Parameters.AddWithValue("@ILT_MODE", trf_mode);
                                objcommand.Parameters.AddWithValue("@remarks", remarks);
                                objcommand.Parameters.AddWithValue("@status", status);
                                objcommand.Parameters.AddWithValue("@created_by", CREATED_BY);
                                objcommand.Parameters.AddWithValue("@created_on", "");
                                objcommand.Parameters.AddWithValue("@lst_upd_by", LST_UPD_BY);
                                objcommand.Parameters.AddWithValue("@lst_upd_on", LST_UPD_ON);

                                objcommand.Parameters.AddWithValue("@ipaddress", IPADDRESS);
                                var reader1 = objcommand.ExecuteReader();
                                System.Data.DataTable results1 = new System.Data.DataTable();
                                results1.Load(reader1);

                                //for (int i = 0; i < results1.Rows.Count; i++)
                                //{
                                //    DataRow row1 = results1.Rows[i];
                                //    itrf_id = row1[0].ToString();


                                //}
                                if (Outputval != string.Empty)
                                {
                                    Outputval = "Inserted successfully";

                                }

                                //dbConn.Close();
                                //return Outputval;

                            }
                            else
                            {
                                DataSet dsuserdetails1 = new DataSet();
                                string sql1 = "ERP_ILT_UPDATE";
                                SqlCommand objcommand = new SqlCommand(sql1, dbConn);


                                objcommand.CommandType = CommandType.StoredProcedure;
                                objcommand.Parameters.AddWithValue("@function_id", FUNCTION_ID);
                                objcommand.Parameters.AddWithValue("@branch_to", branch_to);
                                objcommand.Parameters.AddWithValue("@ILT_REF ", itrf_ref);

                                objcommand.Parameters.AddWithValue("@branch_id", branch_id);
                                objcommand.Parameters.AddWithValue("@ILT_DATE", itrf_date);
                                objcommand.Parameters.AddWithValue("@remarks", remarks);
                                // objcommand.Parameters.AddWithValue("@remarks", remarks);
                                objcommand.Parameters.AddWithValue("@status", "N");
                                objcommand.Parameters.AddWithValue("@lst_upd_on", LST_UPD_ON);
                                objcommand.Parameters.AddWithValue("@ipaddress", "::1");

                                objcommand.Parameters.AddWithValue("@lst_upd_by", LST_UPD_BY);
                                objcommand.Parameters.AddWithValue("@ILT_ID", itrfid);


                                //var reader2 = objcommand.ExecuteReader();
                                //System.Data.DataTable results2 = new System.Data.DataTable();
                                //results2.Load(reader2);
                                //Outputval = results2.ToString();

                                var reader1 = objcommand.ExecuteReader();
                                System.Data.DataTable results1 = new System.Data.DataTable();
                                results1.Load(reader1);

                                for (int o = 0; o < results1.Rows.Count; o++)
                                {
                                    DataRow rowm = results1.Rows[o];
                                    itrf_id = rowm[0].ToString();


                                }
                                if (Outputval != string.Empty)
                                {
                                    Outputval = "Updated successfully";

                                }

                            }




                        }


                    }


                    if (itrf_id == "")
                    {
                        //getInterLocationMaster();
                        //btnSave.Text = "Save";
                    }




                    var serializedObjects = JsonConvert.SerializeObject(data).ToString();


                    var objs = JsonConvert.DeserializeObject<JObject>(data.ToString());


                    JObject obj1s = JsonConvert.DeserializeObject<JObject>(data.ToString());





                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent1s = obj_parent.GetValue("DetailsInterlocation")[0] as JObject;
                    string flag = "";

                    foreach (KeyValuePair<string, JToken> item in obj_parent1s)
                    {
                        JProperty p1 = obj_parent1.Property(item.Key);


                        if (item.Key == "itrf_id")
                        {
                            itrf_id = item.Value.ToString();
                        }

                        if (item.Key == "function_id")
                        {
                            function_id = item.Value.ToString();
                        }

                        if (item.Key == "ITEM_QTY")
                        {
                            ITEM_QTY = item.Value.ToString();
                        }

                        if (item.Key == "remarkss")
                        {
                            remarkss = item.Value.ToString();
                        }

                        if (item.Key == "statuss")
                        {
                            statuss = item.Value.ToString();
                        }

                        if (item.Key == "created_by")
                        {
                            created_by = item.Value.ToString();
                        }

                        if (item.Key == "created_on")
                        {
                            created_on = item.Value.ToString();
                        }
                        if (item.Key == "lst_upd_by")
                        {
                            created_by = item.Value.ToString();
                        }

                        if (item.Key == "lst_upd_on")
                        {
                            created_on = item.Value.ToString();
                        }

                        if (item.Key == "ipaddress")
                        {
                            ipaddress = item.Value.ToString();
                        }
                        if (item.Key == "branch_ids")
                        {
                            branch_ids = item.Value.ToString();
                        }
                        if (item.Key == "item_detailed_description")
                        {
                            item_detailed_description = item.Value.ToString();
                        }



                        if (item.Key == "flag")
                        {
                            flag = item.Value.ToString();
                        }

                        if (flag == "N")

                        {


                            DataSet dsuserdetails1 = new DataSet();
                            string sql1 = "ERP_ILT_DETAILS_INSERT";
                            SqlCommand objcommand = new SqlCommand(sql1, dbConn);


                            objcommand.CommandType = CommandType.StoredProcedure;
                            objcommand.Parameters.AddWithValue("@ILT_ID", itrfid);
                            objcommand.Parameters.AddWithValue("@function_id", function_id);
                            objcommand.Parameters.AddWithValue(",@ITEM_QTY", ITEM_QTY);

                            objcommand.Parameters.AddWithValue("@remarks", remarkss);
                            objcommand.Parameters.AddWithValue("@status", statuss);
                            objcommand.Parameters.AddWithValue("@created_by", created_by);
                            objcommand.Parameters.AddWithValue("@created_on", created_on);
                            objcommand.Parameters.AddWithValue("@lst_upd_by", lst_upd_by);
                            objcommand.Parameters.AddWithValue("@lst_upd_on", lst_upd_on);
                            objcommand.Parameters.AddWithValue("@ipaddress", ipaddress);
                            objcommand.Parameters.AddWithValue("@branch_id", branch_ids);
                            objcommand.Parameters.AddWithValue("@item_detailed_description", item_detailed_description);


                            var reader1 = objcommand.ExecuteReader();
                            System.Data.DataTable results1 = new System.Data.DataTable();
                            results1.Load(reader1);


                            if (Outputval != string.Empty)
                            {
                                Outputval = "Inserted successfully";

                            }

                            // dbConn.Close();
                        }
                        if (flag == "U")
                        {
                            //itrfid = row["itrf_id"].ToString();
                            if (function_id == "" && function_id == "0" && branch_ids == "" && branch_ids == "0" && itrf_id == "" && itrf_id == "0")
                            {
                                DataSet dsuserdetails1 = new DataSet();
                                string sql1 = "ERP_ILT_DETAILS_INSERT";
                                SqlCommand objcommand = new SqlCommand(sql1, dbConn);


                                objcommand.CommandType = CommandType.StoredProcedure;
                                objcommand.Parameters.AddWithValue("@ILT_ID", itrf_id);
                                objcommand.Parameters.AddWithValue("@function_id", function_id);
                                objcommand.Parameters.AddWithValue(",@ITEM_QTY", ITEM_QTY);

                                objcommand.Parameters.AddWithValue("@remarks", remarkss);
                                objcommand.Parameters.AddWithValue("@status", statuss);
                                objcommand.Parameters.AddWithValue("@created_by", created_by);
                                objcommand.Parameters.AddWithValue("@created_on", created_on);
                                objcommand.Parameters.AddWithValue("@lst_upd_by", lst_upd_by);
                                objcommand.Parameters.AddWithValue("@lst_upd_on", lst_upd_on);
                                objcommand.Parameters.AddWithValue("@ipaddress", ipaddress);
                                objcommand.Parameters.AddWithValue("@branch_id", branch_ids);
                                objcommand.Parameters.AddWithValue("@item_detailed_description", item_detailed_description);


                                var reader1 = objcommand.ExecuteReader();
                                System.Data.DataTable results1 = new System.Data.DataTable();
                                results1.Load(reader1);


                                if (Outputval != string.Empty)
                                {
                                    Outputval = "Updated successfully";

                                }

                                // dbConn.Close();
                            }


                        }
                    }
                    //if (strSql != "")
                    //{
                    //    string id = objSql.getString(strSql);
                    //}


                    dbConn.Close();



                }
                return Ok("ok");


            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }





        //shankari on 10Nov


        [HttpPost]
        [Route("InterLocationTransfer_Insert_Update")]
        public async Task<ActionResult<ERP>> InterLocationTransfer_Insert_Update(dynamic data)
        {
            string status = ""; string ChkRelease = ""; string branch_to = ""; string branch_id = ""; string FUNCTION_ID = ""; string itrfid = "";
            string btnSave = ""; string itrf_id = ""; string itrf_ref = ""; string isconfig = ""; string itrf_date = ""; string trf_mode = ""; string remarks = ""; string IPADDRESS = ""; string CREATED_BY = ""; string LST_UPD_BY = ""; string itrfid1 = ""; string LST_UPD_ON = ""; string Outputval = "";


            string function_id = ""; string ITEM_QTY = ""; string remarkss = ""; string statuss = ""; string created_by = "";
            string created_on = ""; string lst_upd_by = ""; string lst_upd_on = ""; string ipaddress = ""; string branch_ids = "";
            string item_detailed_description = "";
            int id = 0;
            int dt = 2;
            int dt1 = 1;
            int i = 0;
            int inew = 0;
            string itemFromFlag = "";
            string strMessage = "";
            string ItrfTransferNo = "";
            DataRow row = null;
            int id1 = 1;
            int dtb = 1;
            DataSet dsInterLocationTransfer = new DataSet();
            DataSet dstILTMaster = new DataSet();
            DataSet dsStockUpdation = new DataSet();
            CultureInfo objCulture;
            string culture = "";
            ArrayList err = new ArrayList();
            DataSet ds1 = new DataSet();
            DataSet dsBranch = new DataSet();
            string strMode = "";
            string CREATED_ON = "";
            string type = "InterLocationTransfer";
            string itrfno = "";
            //string LST_UPD_BY = "";
            string BRANCH_ID = "";
            string ILT_ID = "";


            string pk_column_name1 = string.Empty;

            string pk_column_name2 = string.Empty;

            string pk_column_name3 = string.Empty;

            string pk_column_name4 = string.Empty;

            string pk_column_name5 = string.Empty;


            string STATUS_COLUMN = string.Empty;

            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    dbConn.Open();
                    var serializedObject = JsonConvert.SerializeObject(data).ToString();
                    var obj = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj1 = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent = JsonConvert.DeserializeObject<JObject>(data.ToString());
                    JObject obj_parent1 = obj_parent.GetValue("MasterInterlocation")[0] as JObject;
                    foreach (KeyValuePair<string, JToken> item in obj_parent1)


                    {
                        JProperty p1 = obj_parent1.Property(item.Key);



                        if (item.Key == "FUNCTION_ID")
                        {
                            FUNCTION_ID = item.Value.ToString();
                        }




                        if (item.Key == "branch_id")
                        {
                            branch_id = item.Value.ToString();
                        }
                        if (item.Key == "branch_to")
                        {
                            branch_to = item.Value.ToString();

                        }

                        //if (ChkRelease == "true")
                        //{
                        //    if (item.Key == "status")
                        //    {
                        //        status = "N";
                        //    }

                        //}



                        //if (status != "" && status != null)
                        //{
                        //    if (status == "D")
                        //    {
                        //        if (item.Key == "status")
                        //        {
                        //            status = item.Value.ToString();

                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (item.Key == "status")
                        //        {
                        //            status = item.Value.ToString();

                        //        }
                        //    }
                        //}


                        if (FUNCTION_ID != "")
                        {
                            if (itrf_ref == "")
                            {

                                if (btnSave != "Initiate")
                                {
                                    DataSet dsmrscode = new DataSet();
                                    string sqlmrscode = "ERP_PRS_ISCONFIG";
                                    SqlCommand cmdmrscode = new SqlCommand(sqlmrscode, dbConn);


                                    cmdmrscode.CommandType = CommandType.StoredProcedure;

                                    cmdmrscode.Parameters.AddWithValue("@FUNCTIONID", FUNCTION_ID);
                                    cmdmrscode.Parameters.AddWithValue("@TYPE", "InterLocationTransfer");

                                    cmdmrscode.ExecuteNonQuery();
                                    var mrscodereader = cmdmrscode.ExecuteReader();
                                    System.Data.DataTable resultsmrscode = new System.Data.DataTable();
                                    resultsmrscode.Load(mrscodereader);
                                    //string outputval = cmd1.Parameters["@outputparam"].Value.ToString();
                                    for (int l = 0; l < resultsmrscode.Rows.Count; l++)
                                    {
                                        DataRow rowmrscode = resultsmrscode.Rows[l];
                                        isconfig = rowmrscode[0].ToString();


                                    }

                                    itrf_ref = isconfig;
                                }
                            }

                        }
                        else
                        {
                            itrf_ref = isconfig;
                        }
                        //if (item.Key == "itrf_ref")
                        //{
                        //    itrf_ref = item.Value.ToString();

                        //}



                        if (item.Key == "itrf_date")
                        {
                            itrf_date = item.Value.ToString();
                        }
                        if (item.Key == "trf_mode")
                        {
                            trf_mode = item.Value.ToString();
                        }
                        trf_mode = "Intra";

                        if (item.Key == "remarks")
                        {
                            remarks = item.Value.ToString();
                        }
                        if (item.Key == "status")
                        {
                            status = item.Value.ToString();
                        }

                        if (item.Key == "CREATED_BY")
                        {
                            CREATED_BY = item.Value.ToString();
                        }
                        if (item.Key == "CREATED_ON")
                        {
                            CREATED_ON = item.Value.ToString();
                        }
                        if (item.Key == "LST_UPD_BY")
                        {
                            LST_UPD_BY = item.Value.ToString();
                        }
                        if (item.Key == "LST_UPD_ON")
                        {
                            LST_UPD_ON = item.Value.ToString();
                        }

                        if (item.Key == "IPADDRESS")
                        {
                            IPADDRESS = item.Value.ToString();
                        }
                        //if (item.Key == "ILT_ID")
                        //{
                        //    ILT_ID = item.Value.ToString();
                        //}



                        if (item.Key == "itrf_id")
                        {
                            itrf_id = item.Value.ToString();
                        }

                        if (btnSave != "Initiate")
                        {

                            //dbConn.Open();

                            string query = "";
                            query = "select max(ILT_ID) as maxid from ERP_ILT_MASTER";

                            SqlCommand cmd = new SqlCommand(query, dbConn);
                            var reader = cmd.ExecuteReader();
                            System.Data.DataTable results = new System.Data.DataTable();
                            results.Load(reader);

                            for (int k = 0; k < results.Rows.Count; k++)
                            {
                                DataRow row1 = results.Rows[k];
                                itrfid = row1[0].ToString() + 1;
                            }

                            //dbConn.Close();
                        }






                        if (btnSave != "Initiate")
                        {


                            //if (FUNCTION_ID == "" && FUNCTION_ID == "0" && BRANCH_ID == "" && BRANCH_ID == "0" && ILT_ID == "" && ILT_ID == "0")
                            //{

                            DataSet dsuserdetails1 = new DataSet();
                            string sql1 = "ERP_ILT_INSERT";
                            SqlCommand objcommand = new SqlCommand(sql1, dbConn);


                            objcommand.CommandType = CommandType.StoredProcedure;
                            objcommand.Parameters.AddWithValue("@function_id", FUNCTION_ID);
                            objcommand.Parameters.AddWithValue("@branch_id", branch_id);
                            objcommand.Parameters.AddWithValue("@branch_to", branch_to.ToString());

                            objcommand.Parameters.AddWithValue("@ILT_REF", isconfig);
                            objcommand.Parameters.AddWithValue("@ILT_DATE", itrf_date);
                            objcommand.Parameters.AddWithValue("@ILT_MODE", trf_mode);
                            objcommand.Parameters.AddWithValue("@remarks", remarks);
                            objcommand.Parameters.AddWithValue("@status", status);
                            objcommand.Parameters.AddWithValue("@created_by", CREATED_BY);
                            objcommand.Parameters.AddWithValue("@created_on", "");
                            objcommand.Parameters.AddWithValue("@lst_upd_by", LST_UPD_BY);
                            objcommand.Parameters.AddWithValue("@lst_upd_on", LST_UPD_ON);

                            objcommand.Parameters.AddWithValue("@ipaddress", IPADDRESS);
                            var reader1 = objcommand.ExecuteReader();
                            System.Data.DataTable results1 = new System.Data.DataTable();
                            results1.Load(reader1);

                            //for (int i = 0; i < results1.Rows.Count; i++)
                            //{
                            //    DataRow row1 = results1.Rows[i];
                            //    itrf_id = row1[0].ToString();


                            //}
                            if (Outputval != string.Empty)
                            {
                                Outputval = "Inserted successfully";

                            }

                        }


                    }

                    dbConn.Close();



                }
                return Ok("ok");


            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }

        [HttpPost]
        [Route("Request_for_quotation")]
        public async Task<ActionResult<ERP>> Request_for_quotation(dynamic data)
        {
            string rfqid = "", RfqId = "", vendor_id = "", branchid = "", quotationdate = "", requiredqty = "", NetPrice = "", strUserId = "", strIpAddress = "", Email = "", VENDORITEMID = "", PRSCurrency = "", amt = "", PRSDetailsID = "", prs_Id = "";
            string strFunction = "1", itemids = "", rfno = "", unit_price = "";
            string type = "PurchaseEnquiryMaster";
            string logdata = "";
            ArrayList saveitems = new ArrayList();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //DataTable dtAssign = new DataTable();
            DataRow rowUserSummary = null;
            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    //If(string.IsNullOrEmpty(strFunction))

                    dbConn.Open();
                    string sql5 = "select serial_no+1 from BO_SLNO_PARAMETER where type='" + type + "' and  function_id='" + strFunction + "'";

                    SqlCommand cmd = new SqlCommand(sql5, dbConn);
                    var reader = cmd.ExecuteReader();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row1 = results.Rows[i];
                        itemids = row1[0].ToString();


                    }

                    string sql = "Update BO_SLNO_PARAMETER set serial_no='" + itemids + "' where function_id='" + strFunction + "'and type='" + type + "'";

                    //dbConn.Open();
                    SqlCommand cmd1 = new SqlCommand(sql, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    saveitems.Add(incrementAutoNo(strFunction, type));




                    string ItemID = string.Empty;

                    string query = "";


                    query = "select distinct BO_BRANCH_MASTER.BRANCH_ID,BO_BRANCH_MASTER.BRANCH_DESC from BO_BRANCH_MASTER INNER JOIN BO_FUNCTION_ACCESS WITH(NOLOCK) ON BO_FUNCTION_ACCESS.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = BO_FUNCTION_ACCESS.FUNCTION_ID where 1 = 1AND BO_BRANCH_MASTER.FUNCTION_ID = '" + strFunction + "'";


                    SqlCommand cmd3 = new SqlCommand(query, dbConn);
                    var reader3 = cmd3.ExecuteReader();
                    System.Data.DataTable results3 = new System.Data.DataTable();
                    results3.Load(reader3);

                    //ERP_GETREFERENCE

                    string sql1 = "EXEC ERP_GETREFERENCE '" + strFunction + "','" + type + "'";
                    SqlCommand cmd4 = new SqlCommand(sql1, dbConn);
                    var reader4 = cmd4.ExecuteReader();
                    System.Data.DataTable results4 = new System.Data.DataTable();
                    results4.Load(reader4);
                    for (int i = 0; i < results4.Rows.Count; i++)
                    {
                        DataRow row1 = results4.Rows[i];
                        rfno = row1[0].ToString();


                    }


                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent2 = obj_parents.GetValue("Raise_quotation")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent2)
                    {
                        JProperty p2 = obj_parent2.Property(item.Key);



                        if (item.Key == "Raise_details")
                        {
                            var Raise_details = item.Value.ToString();

                            JArray array = JArray.Parse(Raise_details);
                            JArray jsonArray = JArray.Parse(Raise_details);



                            foreach (JObject content in array.Children<JObject>())
                            {
                                DataTable dtAssign = new DataTable();
                                foreach (JProperty prop in content.Properties())
                                {
                                    string Name = prop.Name.ToString().Trim();
                                    string Value = prop.Value.ToString().Trim();




                                    if (Name == "ItemID")
                                    {
                                        ItemID = Value.ToString();
                                    }
                                    if (Name == "RFQID")
                                    {
                                        RfqId = Value.ToString();
                                        prs_Id = Value.ToString();
                                    }
                                    if (Name == "vendor_id")
                                    {
                                        vendor_id = Value.ToString();
                                    }
                                    if (Name == "branchid")
                                    {
                                        branchid = Value.ToString();
                                    }
                                    if (Name == "quotationdate")
                                    {
                                        quotationdate = Value.ToString();
                                    }
                                    if (Name == "requiredqty")
                                    {
                                        requiredqty = Value.ToString();
                                    }
                                    if (Name == "NetPrice")
                                    {
                                        NetPrice = Value.ToString();
                                    }
                                    if (Name == "strUserId")
                                    {
                                        strUserId = Value.ToString();
                                    }
                                    if (Name == "strIpAddress")
                                    {
                                        strIpAddress = Value.ToString();
                                    }
                                    if (Name == "Email")
                                    {
                                        Email = Value.ToString();
                                    }
                                    if (Name == "VENDORITEMID")
                                    {
                                        VENDORITEMID = Value.ToString();
                                    }
                                    if (Name == "PRSDetailsID")
                                    {
                                        PRSDetailsID = Value.ToString();
                                    }
                                    if (Name == "NETP")
                                    {
                                        amt = Value.ToString();
                                    }
                                }

                                if (ItemID != null && ItemID != "")
                                {

                                    string strPRSCurrency = "select  distinct  ERP_PRS_MASTER.CURRENCY CURRENCY FROM ERP_PRS_MASTER INNER JOIN USERACCESS WITH (NOLOCK) ON USERACCESS.FUNCTION_ID=ERP_PRS_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID = ERP_PRS_MASTER.CREATED_BY INNER JOIN erp_prs_details ON erp_prs_details.prs_id = ERP_PRS_MASTER.prs_id INNER JOIN ERP_RFQ_DETAILS ON erp_prs_details.RFQID = ERP_RFQ_DETAILS.RFQID INNER JOIN ERP_RFQ_MASTER RM1 ON RM1.RFQID = ERP_RFQ_DETAILS.RFQID  where ERP_PRS_MASTER.CURRENCY != '' and RM1.RFQID = '" + RfqId + "'";


                                    //string strPRSCurrency = "select   ERP_PRS_MASTER.CURRENCY FROM ERP_PRS_MASTER INNER JOIN USERACCESS WITH (NOLOCK) ON USERACCESS.FUNCTION_ID=ERP_PRS_MASTER.FUNCTION_ID and USERACCESS.TUM_USER_ID=ERP_PRS_MASTER.CREATED_BY INNER JOIN erp_prs_details ON erp_prs_details.prs_id=ERP_PRS_MASTER.prs_id INNER JOIN ERP_RFQ_DETAILS ON erp_prs_details.RFQID = ERP_RFQ_DETAILS.RFQID INNER JOIN ERP_RFQ_MASTER RM1 ON  RM1.RFQID = ERP_RFQ_DETAILS.RFQID WHERE RM1.RFQID='" + RfqId + "'";
                                    SqlCommand cmdcurr = new SqlCommand(strPRSCurrency, dbConn);
                                    var readercurr = cmdcurr.ExecuteReader();
                                    System.Data.DataTable resultscurr = new System.Data.DataTable();
                                    resultscurr.Load(readercurr);
                                    for (int i = 0; i < resultscurr.Rows.Count; i++)
                                    {
                                        DataRow row1 = resultscurr.Rows[i];
                                        PRSCurrency = row1[0].ToString();


                                    }
                                    //vel


                                    dtAssign.Columns.Add("function_id", typeof(String));
                                    dtAssign.Columns.Add("vendor_id", typeof(String));
                                    dtAssign.Columns.Add("prsId", typeof(String));
                                    dtAssign.Columns.Add("branch_id", typeof(String));
                                    dtAssign.Columns.Add("Quote_Ref", typeof(String));
                                    dtAssign.Columns.Add("Quote_Date", typeof(String));
                                    dtAssign.Columns.Add("required_qty", typeof(String));
                                    dtAssign.Columns.Add("status", typeof(String));
                                    dtAssign.Columns.Add("netprice", typeof(String));
                                    dtAssign.Columns.Add("itemId", typeof(String));
                                    dtAssign.Columns.Add("created_by", typeof(String));
                                    dtAssign.Columns.Add("lst_upd_by", typeof(String));
                                    dtAssign.Columns.Add("ipaddress", typeof(String));
                                    dtAssign.Columns.Add("email", typeof(String));
                                    dtAssign.Columns.Add("VENDORITEMID", typeof(String));
                                    dtAssign.Columns.Add("PRSDETAILSID", typeof(String));
                                    dtAssign.Columns.Add("PRSCurrency", typeof(String));
                                    DataRow dr = null;
                                    //dr = dtAssign.NewRow();



                                    //ds.Tables[0].Rows.Add(dr);
                                    for (int i = 0; i < 1; i++)
                                    {

                                        dr = dtAssign.NewRow();
                                        dr["function_id"] = strFunction.ToString();
                                        dr["vendor_id"] = vendor_id.ToString();
                                        dr["prsId"] = prs_Id;
                                        dr["branch_id"] = branchid;
                                        dr["Quote_Ref"] = rfno;
                                        dr["Quote_Date"] = quotationdate;
                                        dr["required_qty"] = requiredqty;
                                        dr["status"] = "N";
                                        dr["netprice"] = NetPrice;
                                        dr["itemId"] = ItemID;
                                        dr["created_by"] = strUserId;
                                        dr["lst_upd_by"] = strUserId;
                                        dr["ipaddress"] = strIpAddress;
                                        dr["email"] = Email;
                                        dr["VENDORITEMID"] = VENDORITEMID;
                                        dr["PRSDETAILSID"] = PRSDetailsID;
                                        dr["PRSCurrency"] = PRSCurrency;
                                        dtAssign.Rows.Add(dr);

                                    }
                                    //vel


                                    
                                }




                                unit_price = "0";
                                if (dtAssign?.Rows?.Count > 0)
                                {
                                    //foreach (DataRow item1 in dtAssign.Rows)
                                    //{
                                    //cmd.CommandText = "ERP_VENDOR_EVALUATION_QUOTATION_RAISE";
                                   // string sql2 = "ERP_VENDOR_EVALUATION_QUOTATION_RAISE";
                                    string sql2 = "ERP_VENDOR_EVALUATION_QUOTATION_RAISE";
                                    SqlCommand cmd2 = new SqlCommand(sql2, dbConn);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@FUNCTION_ID", dtAssign.Rows[0]["function_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@BRANCH_ID", dtAssign.Rows[0]["branch_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@PRSID", dtAssign.Rows[0]["prsId"].ToString());
                                    cmd2.Parameters.AddWithValue("@VENDOR_ID", dtAssign.Rows[0]["vendor_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@QUOTE_REF", dtAssign.Rows[0]["Quote_Ref"].ToString());
                                    cmd2.Parameters.AddWithValue("@QUOTE_DATE", dtAssign.Rows[0]["Quote_Date"].ToString());
                                    cmd2.Parameters.AddWithValue("@STATUS", dtAssign.Rows[0]["status"].ToString());
                                    cmd2.Parameters.AddWithValue("@CREATED_BY", dtAssign.Rows[0]["created_by"].ToString());
                                    cmd2.Parameters.AddWithValue("@IPADDRESS", dtAssign.Rows[0]["ipaddress"].ToString());
                                    cmd2.Parameters.AddWithValue("@QUOT_ID", null);
                                    cmd2.Parameters.AddWithValue("@REQUIRED_QTY", dtAssign.Rows[0]["required_qty"].ToString());
                                    cmd2.Parameters.AddWithValue("@UNIT_PRICE", unit_price);
                                    cmd2.Parameters.AddWithValue("@NETPRICE", dtAssign.Rows[0]["netprice"].ToString());
                                    cmd2.Parameters.AddWithValue("@ITEMID", dtAssign.Rows[0]["itemId"].ToString());
                                    cmd2.Parameters.AddWithValue("@VENDORITEMID", dtAssign.Rows[0]["VENDORITEMID"].ToString());
                                    cmd2.Parameters.AddWithValue("@PRSDETAILSID", dtAssign.Rows[0]["PRSDETAILSID"].ToString());
                                    cmd2.Parameters.AddWithValue("@PRSCurrency", dtAssign.Rows[0]["PRSCurrency"].ToString());
                                    SqlDataAdapter objadapter = new SqlDataAdapter(cmd2);
                                    objadapter.Fill(dtAssign);
                                    //dbConn.Close();

                                    // }

                                }
                            }

                        }
                    }

                    logdata = "Vendor Quotation Raised Successfully";
                }

                var result = (new { logdata });
                return Ok(logdata);


                // return Ok(json);
            }

            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
        }



        public string incrementAutoNo(string function_id, string type)
        {
            string itemids = "";
            try
            {

                using (SqlConnection dbConn = new SqlConnection(strconn))
                {


                    dbConn.Open();
                    string sql5 = "select serial_no+1 from BO_SLNO_PARAMETER where type='" + type + "' and  function_id='" + function_id + "'";
                    SqlCommand cmd4 = new SqlCommand(sql5, dbConn);
                    var reader4 = cmd4.ExecuteReader();
                    System.Data.DataTable results4 = new System.Data.DataTable();
                    results4.Load(reader4);
                    for (int i = 0; i < results4.Rows.Count; i++)
                    {
                        DataRow row1 = results4.Rows[i];
                        itemids = row1[0].ToString();


                    }
                    return " Update BO_SLNO_PARAMETER set serial_no='" + itemids + "' where function_id='" + function_id + "'and type='" + type + "'";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        [HttpGet]
        [Route("getponnumber/{fuctionid}/{branchid}/{ponumber}")]
        public dynamic getponnumber(string fuctionid, string branchid, string ponumber)
        {
            List<ERP> Logdata = new List<ERP>();

            string Logdata1 = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";

                query = "select a.po_id,a.po_number from ERP_PO_MASTER a inner join ERP_PO_PAYMENT_APPROVALS b on a.po_id=b.PO_ID where a.po_number like'%" + ponumber + "%'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                Logdata1 = DataTableToJSONWithStringBuilder(results);

                dbConn.Close();
            }
            return Logdata1;
        }


        //display invoice amount 

        [HttpGet]
        [Route("getpo_invoiceamount/{fuctionid}/{branchid}/{poid}")]
        public dynamic getpo_invoiceamount(string fuctionid, string branchid, string poid)
        {
            List<ERP> Logdata = new List<ERP>();

            string Logdata1 = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";

                query = "select po_id,po_number,po_amount from ERP_PO_MASTER where po_id='" + poid + "' and function_id='" + fuctionid + "' and branch_id='" + branchid + "'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                Logdata1 = DataTableToJSONWithStringBuilder(results);

                dbConn.Close();
            }
            return Logdata1;
        }



        [HttpGet]
        [Route("getinvoicedetails/{fuctionid}/{branchid}/{poid}")]
        public dynamic getinvoicedetails(string fuctionid, string branchid, string poid)
        {
            List<ERP> Logdata = new List<ERP>();

            string Logdata1 = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";

                query = "SELECT A.FUNCTION_ID,A.BRANCH_ID,A.INVOICE_ID,A.PO_ID,C.po_number,A.PAYMENT_ID,A.VENDOR_ID,CONVERT(VARCHAR(10), A.IVOICE_DATE, 103) AS 'IVOICE_DATE',A.INVOICE_REF,A.INVOICE_MODE,A.PO_AMOUNT,A.DUE_AMOUNT,A.INVOICE_AMOUNT,A.INVOICE_FILE_PATH,A.REMARKSS,case  A.status when 'U' then 'UnderProcess' when 'P' then 'Pending' when 'A' then 'Approval' when 'R' then 'Rejected' when 'X' then 'Payment Under Process' when 'Z' then 'Paid' end as status,A.CREATED_BY,A.UPDATED_BY,A.CREATED_ON,A.UPDATED_ON,A.IPADDRESS,CONVERT(VARCHAR(10), B.Payment_confirm_on, 103) AS Payment_Date, B.Payment_confirm_remarks as Payment_ref FROM ERP_PURCHASE_INVOICE_MASTER A LEFT JOIN ERP_PO_PAYMENT_TERMS B ON B.FUNCTION_ID = A.FUNCTION_ID and B.BRANCH_ID = A.BRANCH_ID and B.PO_ID = A.PO_ID and B.PAYMENT_ID = A.PAYMENT_ID LEFT JOIN ERP_PO_MASTER C ON C.po_id = A.PO_ID WHERE 1 = 1 ";

                if (fuctionid.ToString() != "" && fuctionid.ToString() != string.Empty && fuctionid.ToString() != null && fuctionid.ToString() != "0")
                {
                    query = query + " and A.FUNCTION_ID='" + fuctionid + "'";
                }
                if (branchid.ToString() != "" && branchid.ToString() != string.Empty && branchid.ToString() != null && branchid.ToString() != "0")
                {
                    query = query + " and A.BRANCH_ID='" + branchid + "'";
                }
                if (poid.ToString() != "" && poid.ToString() != string.Empty && poid.ToString() != null && poid.ToString() != "0")
                {
                    query = query + " and A.PO_ID='" + poid + "'";
                }

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                Logdata1 = DataTableToJSONWithStringBuilder(results);

                dbConn.Close();
            }
            return Logdata1;
        }

        //shankari


        [HttpPost]
        [Route("MaterialIssueDetailsLinkSearch")]
        public async Task<ActionResult<ERP>> MaterialIssueDetailsLinkSearch(ERP data)
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


                    if (data.FUNCTIONIDMIS.ToString() == "0" || data.FUNCTIONIDMIS.ToString() == "" || data.FUNCTIONIDMIS.ToString() == string.Empty || data.FUNCTIONIDMIS.ToString() == null)
                    {
                        data.FUNCTIONIDMIS = "0";
                    }


                    if (data.SORTEXPRESSIONMIS.ToString() == "0" || data.SORTEXPRESSIONMIS.ToString() == "" || data.SORTEXPRESSIONMIS.ToString() == string.Empty || data.SORTEXPRESSIONMIS.ToString() == null)
                    {
                        data.SORTEXPRESSIONMIS = "0";
                    }



                    DataSet dsuserdetails = new DataSet();
                    dbConn.Open();
                    string sql = "ERP_MI_GETSTOCKDETAILS_MBLE";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);

                    //EXEC ERP_MI_GETSTOCKDETAILS @FUNCTIONID='1',@BRANCHID='1',@LOCATION_ID='12',@BIN='36',@ITEM_ID='1272',@PAGEINDEX='0',@PAGESIZE='10',@SORTEXPRESSION='batch_serial_no desc',@ALPHANAME='' ,@STOREE='165',@RACKE='5953'

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FUNCTIONID", data.FUNCTIONIDMIS);
                    cmd.Parameters.AddWithValue("@BRANCHID", data.BRANCHIDMIS);
                    cmd.Parameters.AddWithValue("@LOCATION_ID", data.LOCATION_IDMIS);
                    cmd.Parameters.AddWithValue("@BIN", data.BINMIS);
                    cmd.Parameters.AddWithValue("@ITEM_ID", data.ITEM_IDMIS);
                    cmd.Parameters.AddWithValue("@ALPHANAME", data.ALPHANAMEMIS);

                    cmd.Parameters.AddWithValue("@SORTEXPRESSION", data.SORTEXPRESSIONMIS);
                    cmd.Parameters.AddWithValue("@PAGEINDEX", data.PAGEINDEXMIS);
                    cmd.Parameters.AddWithValue("@PAGESIZE", data.PAGESIZEMIS);
                    cmd.Parameters.AddWithValue("@STOREE", data.STOREEMIS);
                    cmd.Parameters.AddWithValue("@RACKE", data.RACKEMIS);



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




        //EXEC ERP_PO_ORDER_SUMMARY '1','1','','','','','A','','1','286','0','20','po_id desc','',''
        //Purchase Payment search sankari

        [HttpGet]
        [Route("purchase_payment_summary/{BRANCHID}/{FUNCTIONID}/{PONUMBER}/{VENDORCODE}/{FROMDATE}/{TODATE}/{STATUS}")]
        //        {STATUS  }/{ITEMCODE}/{ usertype}/{ userid}/{ PAGEINDEX}/{ PAGESIZE}/{ SORTEXPRESSION}/{ ALPHANAME{ PRSCODE}")]

        public string purchase_payment_summary(string BRANCHID = null, string FUNCTIONID = null, string PONUMBER = null, string VENDORCODE = null, string FROMDATE = null, string TODATE = null, string STATUS = null, string ITEMCODE = null, string usertype = null, string userid = null, string PAGEINDEX = null, string PAGESIZE = null, string SORTEXPRESSION = null, string ALPHANAME = null, string PRSCODE = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";

            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
               // string sql = "MBLE_ERP_PO_ORDER_SUMMARY";

                string sql = "ERP_PO_ORDER_SUMMARY";
                // string sql = "MOB_FM_RSUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (BRANCHID.ToString() != "" || BRANCHID.ToString() != string.Empty || BRANCHID.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@BRANCHID", BRANCHID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BRANCHID", "0");
                }
                if (FUNCTIONID.ToString() != "" || FUNCTIONID.ToString() != string.Empty || FUNCTIONID.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@FUNCTIONID", FUNCTIONID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FUNCTIONID", "0");
                }

                if (PONUMBER.ToString() != "" || PONUMBER.ToString() != string.Empty || PONUMBER.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@PONUMBER", PONUMBER);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PONUMBER", "0");
                }

                if (VENDORCODE.ToString() != "" || VENDORCODE.ToString() != string.Empty || VENDORCODE.ToString() != null)
                {
                    cmd.Parameters.AddWithValue("@VENDORCODE", VENDORCODE);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@VENDORCODE", "0");
                }
                if (FROMDATE.ToString() != "" || FROMDATE.ToString() != string.Empty || FROMDATE.ToString() != null)
                {
                    // FROMDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FROMDATE", "0");
                }
                if (TODATE.ToString() != "" || TODATE.ToString() != string.Empty || TODATE.ToString() != null)
                {
                    //TODATE = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.AddWithValue("@TODATE", TODATE);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TODATE", "0");
                }


                cmd.Parameters.AddWithValue("@STATUS", "A");

                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);





                cmd.Parameters.AddWithValue("@usertype", "10");


                cmd.Parameters.AddWithValue("@userid", "0");

                cmd.Parameters.AddWithValue("@PAGEINDEX", "0");

                cmd.Parameters.AddWithValue("@PAGESIZE", "20");


                cmd.Parameters.AddWithValue("@SORTEXPRESSION", "po_id desc");


                cmd.Parameters.AddWithValue("@ALPHANAME", "0");

                cmd.Parameters.AddWithValue("@PRSCODE", "0");


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




        [HttpPost]
        [Route("Confirm_poDetails")]
        public async Task<ActionResult<ERP>> Confirm_poDetails(ERP data)
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
                string sql = "ERP_ConfirmPoDetails";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTIONID", data.functionid);
                cmd.Parameters.AddWithValue("@POID", data.poid);
               var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }



        [HttpPost]
        [Route("vendor_payment_confirm")]
        public async Task<ActionResult<ERP>> vendor_payment_confirm(ERP data)
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

               // string sql = "ERP_PURCHASEORDER_UPDATE_PAYMENTDETAILS_NEW";
                string sql = "ERP_PURCHASEORDER_UPDATE_PAYMENTDETAILS_NEW";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IPADDRESS  ", data.ipaddress);
                cmd.Parameters.AddWithValue("@PCREMARKS ", data.PCREMARKS);
                cmd.Parameters.AddWithValue("@PCDATE ", data.PCDATE);
                cmd.Parameters.AddWithValue("@FUNCTION_ID ", data.FUNCTION_ID);
                cmd.Parameters.AddWithValue("@BRANCH_ID ", data.BRANCH_ID);
                cmd.Parameters.AddWithValue("@PO_ID ", data.PO_ID);
                cmd.Parameters.AddWithValue("@REV_NO ", data.REV_NO);
                cmd.Parameters.AddWithValue("@PMT_ID ", data.PMT_ID);
                cmd.Parameters.AddWithValue("@PO_NO ", data.PO_NO);
                cmd.Parameters.AddWithValue("@APPROVALID ", data.APPROVALID);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    dbConn.Close();
                }
                if (Logdata1 == "")
                {
                    Logdata1 = "update success";
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }


        //Autocompletion vendor code sang purchase payment
        [HttpGet]
        [Route("getvendorcode/{code}")]
        public string getvendorcode(string code)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "SELECT  vendor_code as VENDOR_CODE FROM erp_vendor_master WHERE STATUS='A' AND VENDOR_CODE LIKE '%" + code + "%' ";

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


        //Material issuedetail popup search
        [HttpGet]
        [Route("getmaterialpopup/{ItemID}")]
        public string getmaterialpopup(string ItemID)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "SELECT * FROM ERP_ITEM_MASTER  WHERE ITEM_ID = '" + ItemID + "' AND Status = 'A'  ";

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



        //Material issuedetail Location Description popup search
        [HttpGet]
        [Route("getmaterialpopupLocationdesc/{functionid}/{branchid}")]
        public string getmaterialpopupLocationdesc(string functionid, string branchid)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select BranchID,LocationID,LocationCode,LocationDescription from ERP_LOCATION_MASTER where function_id='" + functionid + "' and BranchId='" + branchid + "' ";

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


        //Material issuedetail Bin Description popup search
        [HttpGet]
        [Route("getmaterialpopupbindesc/{functionid}/{branchid}")]
        public string getmaterialpopupbindesc(string functionid, string branchid)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select location_id,bin_id,bin_code,bin_desc from ERP_BIN_LOCATION_MASTER where function_id='" + functionid + "' and Branch_Id='" + branchid + "' ";

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

        //deepak nov17


        [HttpPost]
        [Route("uploadinvoice")]
        public async Task<ActionResult<ERPupload>> uploadinvoice(ERPupload data)
        {


            List<ERPupload> Logdata = new List<ERPupload>();
            string Logdata1 = string.Empty;
            string Logdata2 = string.Empty;
            string Logdata3 = string.Empty;
            string Logdata4 = string.Empty;
            string Logdata5 = string.Empty;
            string Logdata6 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();

                DataSet dsinvoice = new DataSet();
                dsinvoice.Tables.Add("INVOICE");
                dsinvoice.Tables[0].Columns.Add("FUNCTION_ID");
                dsinvoice.Tables[0].Columns.Add("BRANCH_ID");
                dsinvoice.Tables[0].Columns.Add("INVOICE_ID");
                dsinvoice.Tables[0].Columns.Add("PO_ID");
                dsinvoice.Tables[0].Columns.Add("PAYMENT_ID");
                dsinvoice.Tables[0].Columns.Add("VENDOR_ID");
                dsinvoice.Tables[0].Columns.Add("IVOICE_DATE");
                dsinvoice.Tables[0].Columns.Add("INVOICE_REF");
                dsinvoice.Tables[0].Columns.Add("INVOICE_MODE");
                dsinvoice.Tables[0].Columns.Add("PO_AMOUNT");
                dsinvoice.Tables[0].Columns.Add("DUE_AMOUNT");
                dsinvoice.Tables[0].Columns.Add("INVOICE_AMOUNT");
                dsinvoice.Tables[0].Columns.Add("INVOICE_FILE_PATH");
                dsinvoice.Tables[0].Columns.Add("REMARKSS");
                dsinvoice.Tables[0].Columns.Add("STATUS");
                dsinvoice.Tables[0].Columns.Add("CREATED_BY");
                dsinvoice.Tables[0].Columns.Add("UPDATED_BY");
                dsinvoice.Tables[0].Columns.Add("IPADDRESS");
                dsinvoice.Tables[0].Columns.Add("ApprovalID");

                string filename = data.filename;
                char[] sep1 = { '\\' };
                string[] fName = filename.Split(sep1);
                int lngth = fName.Length;
                string docName = fName[lngth - 1];
                char[] sep = { '.' };
                string[] arrfile = filename.Split(sep);
                string docExtname = arrfile[0];
                string docExt = arrfile[1];
                string FileLocation = "SmartERP/UI/Forms/Userfiles/";
                
                //   docName = docName + "_" + HF_PAYMENT_ID.Value;
                DataRow row1 = dsinvoice.Tables[0].NewRow();
                row1.BeginEdit();
                string id = row1["Po_id"].ToString();
                row1["FUNCTION_ID"] = data.functionid;
                row1["BRANCH_ID"] = data.branchid;

                row1["PO_ID"] = data.poid;

                string paymentid = "select Payment_ID from ERP_PO_PAYMENT_APPROVALS where po_id = '" + data.poid + "'";

                SqlCommand cmd5 = new SqlCommand(paymentid, dbConn);
                var reader5 = cmd5.ExecuteReader();
                System.Data.DataTable results5 = new System.Data.DataTable();
                results5.Load(reader5);
                // Logdata2 = DataTableToJSONWithStringBuilder(results5);
                for (int i = 0; i < results5.Rows.Count; i++)
                {

                    DataRow row2 = results5.Rows[i];

                    Logdata2 = row2[0].ToString();
                }


                string vendor_id = "select vendor_id from ERP_PO_MASTER where po_id = '" + data.poid + "'";

                SqlCommand cmd6 = new SqlCommand(vendor_id, dbConn);
                var reader6 = cmd6.ExecuteReader();
                System.Data.DataTable results6 = new System.Data.DataTable();
                results6.Load(reader6);
                //   Logdata3 = DataTableToJSONWithStringBuilder(results6);
                for (int i = 0; i < results6.Rows.Count; i++)
                {


                    DataRow row3 = results6.Rows[i];

                    Logdata3 = row3[0].ToString();
                }



                row1["PAYMENT_ID"] = Logdata2;
                row1["VENDOR_ID"] = Logdata3;
                row1["IVOICE_DATE"] = data.invoicedate;

                row1["INVOICE_REF"] = data.invoiceref;
                row1["INVOICE_MODE"] = "U";

                row1["PO_AMOUNT"] = "0";
                row1["DUE_AMOUNT"] = "0";
                row1["INVOICE_AMOUNT"] = data.invoiceamount;

                row1["INVOICE_FILE_PATH"] = docName;

                row1["REMARKSS"] = data.remarks;
                row1["STATUS"] = "P";
                row1["CREATED_BY"] = data.userid;
                row1["UPDATED_BY"] = data.userid;
                row1["IPADDRESS"] = "::1";
                string approveid = "select Approval_id from ERP_PO_PAYMENT_APPROVALS where po_id = '" + data.poid + "'";

                SqlCommand cmd4 = new SqlCommand(approveid, dbConn);
                var reader4 = cmd4.ExecuteReader();
                System.Data.DataTable results4 = new System.Data.DataTable();
                results4.Load(reader4);
                //Logdata1 = DataTableToJSONWithStringBuilder(results4);
                for (int i = 0; i < results4.Rows.Count; i++)
                {


                    DataRow row4 = results4.Rows[i];

                    Logdata1 = row4[0].ToString();
                }

                row1["ApprovalID"] = Logdata1;
                row1.EndEdit();
                dsinvoice.Tables[0].Rows.Add(row1);

                string INVOICE_ID = string.Empty;
                string INVOICE_FILE_PATH = string.Empty;
                DataRow row = null;
                string sql = "";

                if (dsinvoice.Tables[0].Rows.Count > 0)
                {
                    row = dsinvoice.Tables[0].Rows[0];
                    string branchID = "select branch_id from ERP_PO_MASTER where po_id = '" + row["PO_ID"] + "'";

                    SqlCommand cmd1 = new SqlCommand(branchID, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    // Logdata1 = DataTableToJSONWithStringBuilder(results1);
                    for (int i = 0; i < results1.Rows.Count; i++)
                    {


                        DataRow row5 = results1.Rows[i];

                        Logdata4 = row5[0].ToString();
                    }


                    sql = "EXEC ERP_VENDORMASTER_SAVEINVOICEFILES_NEW '" + row["FUNCTION_ID"] + "','" + Logdata4 + "','" + row["PO_ID"] + "','" + row["PAYMENT_ID"] + "','" + row["VENDOR_ID"] + "','" + row["IVOICE_DATE"] + "','" + row["INVOICE_REF"] + "','" + row["INVOICE_MODE"] + "','" + row["PO_AMOUNT"] + "','" + row["DUE_AMOUNT"] + "','" + row["INVOICE_AMOUNT"] + "','" + row["REMARKSS"] + "','" + row["STATUS"] + "','" + row["CREATED_BY"] + "','" + row["UPDATED_BY"] + "','" + row["IPADDRESS"] + "','" + row["ApprovalID"] + "'";

                    SqlCommand cmd2 = new SqlCommand(sql, dbConn);
                    var reader2 = cmd2.ExecuteReader();
                    System.Data.DataTable results2 = new System.Data.DataTable();
                    results2.Load(reader2);
                    // Logdata1 = DataTableToJSONWithStringBuilder(results2);
                    for (int i = 0; i < results2.Rows.Count; i++)
                    {


                        DataRow row6 = results2.Rows[i];

                        Logdata5 = row6[0].ToString();
                    }


                    INVOICE_ID = Logdata5;
                    INVOICE_FILE_PATH = INVOICE_ID + "_" + row["INVOICE_FILE_PATH"].ToString();

                    sql = "UPDATE ERP_PURCHASE_INVOICE_MASTER SET INVOICE_FILE_PATH='" + INVOICE_FILE_PATH + "' WHERE INVOICE_ID='" + INVOICE_ID + "'";

                    SqlCommand cmd3 = new SqlCommand(sql, dbConn);
                    var reader3 = cmd3.ExecuteReader();
                    System.Data.DataTable results3 = new System.Data.DataTable();
                    results3.Load(reader3);
                    Logdata1 = DataTableToJSONWithStringBuilder(results3);


                    string strVideofile = data.filedata;


                    //... string URLprifix = @"F:\Production\Application\nTireERP\nTireoffice\SmartERP\UI\Forms\Userfiles\";
                    string strpath = ConfigurationManager.AppSettings["Uploadpath"];
                    string URLprifix = strpath;

                    //string URLprifix = @"F:\deepak\";
                    string filepath = @"F:\deepak\";
                    filepath = URLprifix + docExtname + "." + docExt;

                    byte[] imageBytes33 = Convert.FromBase64String(strVideofile);
                    MemoryStream mss12 = new MemoryStream(imageBytes33, 0, imageBytes33.Length);
                    mss12.Write(imageBytes33, 0, imageBytes33.Length);
                    System.IO.File.WriteAllBytes(filepath, imageBytes33);

                }

                dbConn.Close();


                return Ok("Uploaded successfully");


            }
        }


        //dowload invoice nov22

        [HttpGet]
        [Route("downloadinvoice/{PO_ID}/{INVOICE_ID}")]
        public string downloadinvoice(string PO_ID, string INVOICE_ID)
        {
            string Logdata1 = string.Empty;
            string invoice_file_path = string.Empty;
            //...string URLprifix = @"D:\Production\Application\nTireERP\nTireoffice\SmartERP\UI\Forms\Userfiles\";
            

            string URLprifix1 = "https://demo.herbie.ai/nTireERP/SmartERP/UI/Forms/Userfiles/";
            string filepath = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select invoice_file_path from ERP_PURCHASE_INVOICE_MASTER where PO_ID='" + PO_ID + "' and INVOICE_ID='" + INVOICE_ID + "' ";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                     DataRow row5 = results.Rows[i];
                    invoice_file_path = row5[0].ToString();



                }
                var items = invoice_file_path.Split('_');
                var pieces = invoice_file_path.Split('_', 2);
                string invoice_file_name = items[1];
               

                filepath = URLprifix1 + invoice_file_name;

                Logdata1 = filepath;


              
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                

                dbConn.Close();

                var result = (new { recordsets = Logdata1 });

            }
            return (filepath);
        }








        [HttpPost]
        [Route("saveVendorItemDetails_update")]
        public async Task<ActionResult<ERP>> saveVendorItemDetails_update(dynamic data)
        {
            string REV_NO = "", sqlVQBranch = "";
            string logdata = "";

            string function_id = "";
            string branch_id = "";
            string quote_id = "";
            string enquiry_id = "";
            string item_id = "";
            string qty = "";
            string status = "";
            string unit_price = "";
            string tax2 = "";
            string margin = "";
            string other_charges = "";
            string TC = "";
            string delivery_before = "";
            string net_price = "";
            string remarks = "";
            string currency = "";
            string VENDORITEMID = "";
            string tax3 = "";
            string tax4 = "";
            string Multi_Currency = "";
            string delivery_terms = "";
            string delivery_terms_desc = "";
            string rfqid = "";
            try
            {


                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    //If(string.IsNullOrEmpty(strFunction))

                    dbConn.Open();

                    JObject obj_parents = JsonConvert.DeserializeObject<JObject>(data.ToString());

                    JObject obj_parent2 = obj_parents.GetValue("Save_details")[0] as JObject;


                    foreach (KeyValuePair<string, JToken> item in obj_parent2)
                    {
                        JProperty p2 = obj_parent2.Property(item.Key);


                        if (item.Key == "rfqid")
                        {
                            rfqid = item.Value.ToString();
                        }
                        if (item.Key == "delivery_terms")
                        {
                            delivery_terms = item.Value.ToString();
                        }
                        if (item.Key == "delivery_terms_desc")
                        {
                            delivery_terms_desc = item.Value.ToString();
                        }


                        if (item.Key == "ItemDetails")
                        {
                            var ItemDetails = item.Value.ToString();

                            JArray array = JArray.Parse(ItemDetails);
                            JArray jsonArray = JArray.Parse(ItemDetails);



                            foreach (JObject content in array.Children<JObject>())
                            {
                                DataTable dtAssign = new DataTable();
                                DataTable ds1 = new DataTable();
                                foreach (JProperty prop in content.Properties())
                                {
                                    string Name = prop.Name.ToString().Trim();
                                    string Value = prop.Value.ToString().Trim();




                                    if (Name == "function_id")
                                    {
                                        function_id = Value.ToString();
                                    }
                                    if (Name == "branch_id")
                                    {
                                        branch_id = Value.ToString();

                                    }
                                    if (Name == "quote_id")
                                    {
                                        quote_id = Value.ToString();
                                    }
                                    if (quote_id == "")
                                    {
                                        quote_id = null;
                                    }
                                    if (Name == "enquiry_id")
                                    {
                                        enquiry_id = Value.ToString();
                                    }
                                    if (Name == "item_id")
                                    {
                                        item_id = Value.ToString();
                                    }
                                    if (item_id == "")
                                    {
                                        item_id = null;
                                    }
                                    if (Name == "qty")
                                    {
                                        qty = Value.ToString();
                                    }
                                    if (Name == "status")
                                    {
                                        status = Value.ToString();
                                    }
                                    if (Name == "unit_price")
                                    {
                                        unit_price = Value.ToString();
                                    }
                                    if (Name == "tax2")
                                    {
                                        tax2 = Value.ToString();
                                    }
                                    if (Name == "margin")
                                    {
                                        margin = Value.ToString();
                                    }
                                    if (Name == "other_charges")
                                    {
                                        other_charges = Value.ToString();
                                    }
                                    if (Name == "TC")
                                    {
                                        TC = Value.ToString();
                                    }
                                    if (Name == "delivery_before")
                                    {
                                        delivery_before = Value.ToString();
                                    }
                                    if (Name == "net_price")
                                    {
                                        net_price = Value.ToString();

                                    }
                                    if (Name == "remarks")
                                    {
                                        remarks = Value.ToString();
                                    }
                                    if (Name == "currency")
                                    {
                                        currency = Value.ToString();
                                    }
                                    if (Name == "VENDORITEMID")
                                    {
                                        VENDORITEMID = Value.ToString();
                                    }
                                    if (Name == "tax3")
                                    {
                                        tax3 = Value.ToString();
                                    }
                                    if (Name == "tax4")
                                    {
                                        tax4 = Value.ToString();
                                    }
                                    if (Name == "Multi_Currency")
                                    {
                                        Multi_Currency = Value.ToString();
                                    }

                                }
                                //Vel

                                dtAssign.Columns.Add("function_id", typeof(String));
                                dtAssign.Columns.Add("branch_id", typeof(String));
                                dtAssign.Columns.Add("quote_id", typeof(String));
                                dtAssign.Columns.Add("enquiry_id", typeof(String));
                                dtAssign.Columns.Add("item_id", typeof(String));
                                dtAssign.Columns.Add("qty", typeof(String));
                                dtAssign.Columns.Add("status", typeof(String));
                                dtAssign.Columns.Add("unit_price", typeof(String));
                                dtAssign.Columns.Add("tax2", typeof(String));
                                dtAssign.Columns.Add("margin", typeof(String));
                                dtAssign.Columns.Add("other_charges", typeof(String));
                                dtAssign.Columns.Add("TC", typeof(String));
                                dtAssign.Columns.Add("delivery_before", typeof(String));
                                dtAssign.Columns.Add("net_price", typeof(String));
                                dtAssign.Columns.Add("remarks", typeof(String));
                                dtAssign.Columns.Add("currency", typeof(String));
                                dtAssign.Columns.Add("VENDORITEMID", typeof(String));
                                dtAssign.Columns.Add("tax3", typeof(String));
                                dtAssign.Columns.Add("tax4", typeof(String));
                                DataRow dr = null;
                                //dr = dtAssign.NewRow();
                                //ds.Rows.Add(dr);
                                try
                                {
                                    for (int i = 0; i < 1; i++)
                                    {

                                        dr = dtAssign.NewRow();
                                        dr["function_id"] = function_id.ToString();
                                        dr["branch_id"] = branch_id.ToString();
                                        dr["quote_id"] = quote_id.ToString();
                                        dr["enquiry_id"] = enquiry_id.ToString();
                                        dr["item_id"] = item_id.ToString();
                                        dr["qty"] = qty.ToString();
                                        dr["status"] = status.ToString();
                                        dr["unit_price"] = unit_price.ToString();
                                        dr["tax2"] = tax2.ToString();
                                        dr["margin"] = margin.ToString();
                                        dr["other_charges"] = other_charges.ToString();
                                        dr["TC"] = TC.ToString();
                                        dr["delivery_before"] = delivery_before.ToString();
                                        dr["net_price"] = net_price.ToString();
                                        dr["remarks"] = remarks.ToString();
                                        dr["currency"] = currency.ToString();
                                        dr["VENDORITEMID"] = VENDORITEMID.ToString();
                                        dr["tax3"] = tax3.ToString();
                                        dr["tax4"] = tax4.ToString();
                                        dtAssign.Rows.Add(dr);

                                    }
                                }
                                catch (NullReferenceException e)
                                {
                                    var json = new JavaScriptSerializer().Serialize(e.Message+" (Note: quote_id , item_id should not be Empty)");
                                    return Ok(json);
                                }

                                //vel

                                if (dtAssign?.Rows?.Count > 0)
                                {
                                   


                                    string strPRSCurrency = "EXEC GETREVISIONNO '" + dtAssign.Rows[0]["function_id"].ToString() + "','" + dtAssign.Rows[0]["branch_id"].ToString() + "','" + quote_id + "','" + dtAssign.Rows[0]["item_id"].ToString() + "'";

                                    SqlCommand cmdcurr = new SqlCommand(strPRSCurrency, dbConn);
                                    var readercurr = cmdcurr.ExecuteReader();
                                    System.Data.DataTable resultscurr = new System.Data.DataTable();
                                    resultscurr.Load(readercurr);
                                    for (int i = 0; i < resultscurr.Rows.Count; i++)
                                    {
                                        DataRow row1 = resultscurr.Rows[i];
                                        REV_NO = row1[0].ToString();


                                    }

                                     string sql2 = "ERP_PURCHASE_VENDOR_QUOTATIONS_DETAILS_UPDATE";
                                    //string sql2 = "ERP_PURCHASE_VENDOR_QUOTATIONS_DETAILS_UPDATE";
                                    SqlCommand cmd2 = new SqlCommand(sql2, dbConn);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@STATUS", dtAssign.Rows[0]["status"].ToString());
                                    cmd2.Parameters.AddWithValue("@DELIVERYBEFORE", dtAssign.Rows[0]["delivery_before"].ToString());
                                    cmd2.Parameters.AddWithValue("@QUANTITY", dtAssign.Rows[0]["qty"].ToString());
                                    cmd2.Parameters.AddWithValue("@UNITPRICE", dtAssign.Rows[0]["unit_price"].ToString());
                                    cmd2.Parameters.AddWithValue("@MARGIN", dtAssign.Rows[0]["margin"].ToString());
                                    cmd2.Parameters.AddWithValue("@NETPRICE", dtAssign.Rows[0]["net_price"].ToString());
                                    cmd2.Parameters.AddWithValue("@TRANSPORTCHARGES", dtAssign.Rows[0]["TC"].ToString());
                                    cmd2.Parameters.AddWithValue("@TAXLEVIES", dtAssign.Rows[0]["other_charges"].ToString());
                                    cmd2.Parameters.AddWithValue("@REMARKS", dtAssign.Rows[0]["remarks"].ToString());
                                    cmd2.Parameters.AddWithValue("@CURRENCY", dtAssign.Rows[0]["currency"].ToString());
                                    cmd2.Parameters.AddWithValue("@REVNO", REV_NO);
                                    cmd2.Parameters.AddWithValue("@FUNCTIONID", dtAssign.Rows[0]["function_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@BRANCH_ID", dtAssign.Rows[0]["branch_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@QUOTEID", dtAssign.Rows[0]["quote_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@ITEMID", dtAssign.Rows[0]["item_id"].ToString());
                                    cmd2.Parameters.AddWithValue("@TAX2", dtAssign.Rows[0]["tax2"].ToString());
                                    cmd2.Parameters.AddWithValue("@vendoritemid", dtAssign.Rows[0]["VENDORITEMID"].ToString());


                                    //SqlDataAdapter objadapter = new SqlDataAdapter(cmd2);
                                    //objadapter.Fill(dtAssign);



                                    string sql = "select branch_id from ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER where QUOTE_ID='" + quote_id + "'";
                                    SqlCommand cmdsql = new SqlCommand(sql, dbConn);
                                    var readersql = cmdsql.ExecuteReader();
                                    System.Data.DataTable resultssql = new System.Data.DataTable();
                                    resultssql.Load(readersql);
                                    for (int i = 0; i < resultssql.Rows.Count; i++)
                                    {
                                        DataRow row1 = resultssql.Rows[i];
                                        sqlVQBranch = row1[0].ToString();


                                    }
                                    dbConn.Close();
                                  
                                    dbConn.Open();


                                    string strupdate = "UPDATE ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER SET BID_CURRENCY='" + Multi_Currency + "' WHERE function_id='" + function_id + "' AND branch_id='" + sqlVQBranch + "' AND QUOTE_ID='" + quote_id + "'";

                                    SqlCommand cmdupdate = new SqlCommand(strupdate, dbConn);
                                    var readerupdate = cmdupdate.ExecuteReader();
                                    System.Data.DataTable resultsupdate = new System.Data.DataTable();
                                    resultsupdate.Load(readerupdate);
                                    for (int i = 0; i < resultsupdate.Rows.Count; i++)
                                    {
                                        DataRow row1 = resultsupdate.Rows[i];



                                    }
                                    dbConn.Close();
                                }

                               // dbConn.Close();

                            }
                        }

                    }

                        if (TC != "" && delivery_before != "")
                        {
                        dbConn.Open();
                            string strsql1 = "UPDATE ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER SET delivery_terms='" + delivery_terms.ToString() + "',delivery_date='" + delivery_before.ToString() + "',delivery_terms_des='" + delivery_terms_desc.ToString() + "' WHERE rfq_id='" + rfqid + "' AND QUOTE_ID='" + quote_id + "'";
                            SqlCommand cmdupdate = new SqlCommand(strsql1, dbConn);
                            var readerupdate = cmdupdate.ExecuteReader();
                            System.Data.DataTable resultsupdate = new System.Data.DataTable();
                            resultsupdate.Load(readerupdate);


                        dbConn.Close();
                        }
                        else if (TC != "" && delivery_before == "")
                        {
                        dbConn.Open();
                            string strsql1 = "UPDATE ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER SET delivery_terms='" + delivery_terms.ToString() + "',delivery_date=convert(varchar, '" + DateTime.Now.ToString("dd/MMM/yyyy") + "', 121),delivery_terms_des='" + TC.ToString() + "' WHERE rfq_id='" + rfqid + "' AND QUOTE_ID='" + quote_id + "'";
                            SqlCommand cmdupdate = new SqlCommand(strsql1, dbConn);
                            var readerupdate = cmdupdate.ExecuteReader();
                            System.Data.DataTable resultsupdate = new System.Data.DataTable();
                            resultsupdate.Load(readerupdate);
                        dbConn.Close();

                        }
                        else if (TC == "0" && delivery_before != "")
                        {
                        dbConn.Open();
                            string strsql1 = "UPDATE ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER SET delivery_terms='13',delivery_date='" + delivery_before.ToString() + "', 121),delivery_terms_des='Free Delivery at ICRISAT Stores' WHERE rfq_id='" + rfqid + "' AND QUOTE_ID='" + quote_id + "'";
                            SqlCommand cmdupdate = new SqlCommand(strsql1, dbConn);
                            var readerupdate = cmdupdate.ExecuteReader();
                            System.Data.DataTable resultsupdate = new System.Data.DataTable();
                            resultsupdate.Load(readerupdate);
                        dbConn.Close();
                        }
                        else
                        {
                        dbConn.Open();
                            string strsql1 = "UPDATE ERP_PURCHASE_VENDOR_QUOTATIONS_MASTER SET delivery_terms='13',delivery_date=convert(varchar, '" + DateTime.Now.ToString("dd/MMM/yyyy") + "', 121),delivery_terms_des='Free Delivery at ICRISAT Stores' WHERE rfq_id='" + rfqid + "' AND QUOTE_ID='" + quote_id + "'";
                            SqlCommand cmdupdate = new SqlCommand(strsql1, dbConn);
                            var readerupdate = cmdupdate.ExecuteReader();
                            System.Data.DataTable resultsupdate = new System.Data.DataTable();
                            resultsupdate.Load(readerupdate);

                        //objSql.ExecuteNonQuery(strsql1);
                        dbConn.Close();
                        }




                   // }

                    logdata = "updated Successfully";
                }
                var result = (new { logdata });
                return Ok(logdata);


            }


            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return Ok(json);
            }
            
        }





        //sowmi23/11
        [HttpPost]
        [Route("vendor_paymentdata")]
        public async Task<ActionResult<ERP>> vendor_paymentdata(ERP data)
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
               // string sql = "ERP_getSalesPaymentDeta2_New";

                string sql = "ERP_getSalesPaymentDeta2_New";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@POID", data.poid);
                cmd.Parameters.AddWithValue("@REVISIONNO", "0");
                cmd.Parameters.AddWithValue("@PAGEINDEX", "0");
                cmd.Parameters.AddWithValue("@PAGESIZE", "500");
                cmd.Parameters.AddWithValue("@SORTEXPRESSION", "po_id");
                cmd.Parameters.AddWithValue("@ALPHANAME", "");
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    dbConn.Close();
                }
                var result = (new { logdata });
                return Ok(Logdata1);
            }
        }

        //--------------Saravanan-------

        [HttpGet]
        [Route("GetCurrency")]
        public string GetCurrency()
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select TEXT as TEXT,VAL as VALUE from bo_parameter where type='currency' and function_id='1'";

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




        //Auto completion MRS code

        [HttpGet]
        [Route("MRScode/{code}")]
        public string MRScode(string code)
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select mrs_code from ERP_MRS_MASTER where mrs_code like'%" + code + "%'";

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
        [Route("Listservices")]
        public string Listservices()
        {
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select* from BO_ITEM_SUB_CATEGORY_MASTER sc left join BO_PARAMETER p on sc.function_id = p.FUNCTION_ID and sc.category_id = p.VAL where p.type = 'ERPItemCategory' and p.code = 'Services'";

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



        //isvendor


        //public string isVendor(string functionid, string strUserType)
        //{
        //    string sql = string.Empty;
        //    sql = "EXEC ERP_isVendor '" + functionid + "','" + strUserType + "'";
        //    string sqll = objsql.getString(sql);
        //    return sqll;

        //}



        //edit pending quotation
        [HttpGet]
        [Route("getVendorquotationdetails/{QUOTE_ID}")]
        public string getVendorquotationdetails(string QUOTE_ID)
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "select * from ERP_PURCHASE_VENDOR_QUOTATIONS_DETAILS where QUOTE_ID='" + QUOTE_ID + "'";

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









        //subdelivery terms dropdown


                
        [HttpGet]
        [Route("getVendortermsdetails")]
        public string getVendortermsdetails()
        {



            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string query = "";
                query = "SELECT TEXT AS TEXT, VAL AS VALUE FROM BO_PARAMETER WHERE TYPE = 'Delivery Terms' AND FUNCTION_ID = '1' order by TEXT asc";

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




        //    protected void cascadedropdown()
        //    {


        //        DataSet strassuser = objSql.getDataSet("SELECT TEXT AS TEXT,VAL AS VALUE FROM BO_PARAMETER WHERE TYPE='Delivery Terms' AND FUNCTION_ID='1' order by TEXT asc");

        //        DropDowndeliveryterms.DataSource = strassuser;
        //        DropDowndeliveryterms.DataTextField = "TEXT";
        //        DropDowndeliveryterms.DataValueField = "VALUE";
        //        DropDowndeliveryterms.DataBind();
        //        DropDowndeliveryterms.Items.Insert(0, new ListItem("<<Select>>", "0"));fet


        //    }
        //}


        [HttpGet]
        [Route("Tax1Tax2Tax3Tax4Details/{strfunction}/{catid}/{subcatid}")]
        public string Tax1Tax2Tax3Tax4Details(string strfunction, string catid,string subcatid)

        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    try
                    {
                        string Logdata1 = string.Empty;
                        string sql = "";
                        //[ERP_PO_GET_ERP_FA_TAX_MASTER_ByCategory_ALLTAX]
                        sql = "Exec ERP_PO_GET_ERP_FA_TAX_MASTER_ByCategory_ALLTAX '" + strfunction + "','" + catid + "','" + subcatid + "'";

                        DataSet dtPending = objSQLAccesLayer.getDataSet(sql);



                        for (int i = 0; i < dtPending.Tables[0].Rows.Count; i++)
                        {
                            DataRow row = dtPending.Tables[0].Rows[i];
                            Logdata1 = DataTableToJSONWithStringBuilder(dtPending.Tables[0]);
                        }
                        //}
                        return Logdata1;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }




                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return json;
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
