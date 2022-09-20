using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
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
