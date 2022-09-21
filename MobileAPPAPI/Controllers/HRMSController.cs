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
    public class HRMSController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();


        [HttpGet]
        [Route("CommonDropdown/{Type}/{FilterType}/{Parameter1}/{Parameter2}")]
        public string CommonDropdown( string Type = null, string FilterType = null, string Parameter1 = null, string Parameter2 = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();
            
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "dbo.HRMS_COMMON_DROPDOWN";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@FilterType", FilterType);
                cmd.Parameters.AddWithValue("@Parameter1", Parameter1);
                cmd.Parameters.AddWithValue("@Parameter2", Parameter2);
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
        [Route("EmployeeSearch/{EmployeeId}/{Name}/{Code}/{Designation}/{Branch}/{Department}/{Top}/{Increament}/{appURL}")]
        public string EmployeeSearch(string EmployeeId = null, string Name = null, string Code = null, string Designation = null, string Branch = null, string Department = null, string Top = null, string Increament = null,string appURL = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "DBO.EMPLOYEE_MASTER_SELECT";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeCode", Code);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@Branch", Branch);
                cmd.Parameters.AddWithValue("@Top", Top);
                cmd.Parameters.AddWithValue("@Increament", Increament);
                cmd.Parameters.AddWithValue("@appURL", appURL);
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
        [Route("Boworkflowusersummary/{UserID}/{Functionid}")]
        public string Boworkflowusersummary(string UserID, string Functionid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "BO_WORKFLOW_USERWISE_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", UserID);
                cmd.Parameters.AddWithValue("@FUNCTIONID", Functionid);
                
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
        [Route("EmployeePayslip/{EmployeeId}/{appURL}")]
        public string EmployeePayslip(string EmployeeId, string appURL)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "HRMS_My_Payslip";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@appURL", appURL);

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
        [Route("EmployeeApproval/{EmployeeId}")]
        public string EmployeeApproval(string EmployeeId)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "HRMS_My_Approval";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                

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
        [Route("get_training_details")]
        public async Task<ActionResult<HRMS>> get_training_details(HRMS data)
        {
            // string struser = data.user_lower;

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select Distinct 'Trainer' as FeedBack, (CASE AB.Training_Type When 'O' then 'Online' else 'Classroom' end) as 'Training_Type',KM.training_desc as 'Training_Name',Convert(varchar,KM.training_from,103) as 'From_Date',Convert(varchar,KM.training_to,103) 'To_Date',case KM.training_status when 'C' then 'Completed' end as 'Status' From HRMS_KTA_TRAINING_MASTER KM Inner Join HRMS_KTA_AB_TRAINING_REQUEST AB on AB.TRAINING_NAME=CONVERT(VARCHAR,KM.TRAINING_ID) where KM.training_status='C' And KM.user_id='" + data.userid + "' Union select Distinct 'Trainee', (CASE AB.Training_Type When 'O' then 'Online' else 'Classroom' end) as 'Training_Type',KM.training_desc as 'Training_Name',Convert(varchar,KM.training_from,103) as 'From_Date',Convert(varchar,KM.training_to,103) 'To_Date',case KM.training_status when 'C' then 'Completed' end as 'Status' From HRMS_KTA_TRAINING_MASTER KM Inner Join HRMS_KTA_AB_TRAINING_REQUEST AB on AB.TRAINING_NAME=CONVERT(VARCHAR,KM.TRAINING_ID) where KM.user_id='" + data.userid + "'";

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
        [Route("getbirthdaywishesMydesk")]
        public async Task<ActionResult<HRMS>> getbirthdaywishesMydesk(HRMS data)
        {
            // string struser = data.user_lower;

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select em_emp_name,em_emp_dob,em_emp_photo,(select distinct Description from BO_USER_TYPE_MASTER where TYPE_ID=HRMS_EMPLOYEE_MASTER.em_emp_designation) as Designation from HRMS_EMPLOYEE_MASTER where  MONTH(HRMS_EMPLOYEE_MASTER.em_emp_dob)='" + data.month + "' and em_emp_status='A' order by day(em_emp_dob)";

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
