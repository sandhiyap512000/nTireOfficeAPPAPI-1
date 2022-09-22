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


        [HttpGet]
        [Route("EmployeeUpdate/{EmployeeId}/{Type}/{Parameter1}/{Parameter2}/{Parameter3}/{Parameter4}/{Parameter5}/{Parameter6}/{Parameter7}/{Parameter8}")]
        public string EmployeeUpdate(string EmployeeId = null, string Type = null, string Parameter1 = null, string Parameter2 = null, string Parameter3 = null, string Parameter4 = null, string Parameter5 = null, string Parameter6 = null, string Parameter7 = null, string Parameter8 = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (EmployeeId != null && EmployeeId.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                else
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", 0);

                if (Type != null && Type.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@Type", Type);
                else
                    sqlCommand.Parameters.AddWithValue("@Type", 0);


                if (Type != null && Type.Trim() != string.Empty)
                {
                    if (Type.Trim() == "PersonalDetails")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@FirstName", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@FirstName", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@SecondName", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@SecondName", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@DOB", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@DOB", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@Qalification", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Qalification", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@SubQualification", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@SubQualification", null);

                    }
                    else if (Type.Trim() == "CurrentAddress")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@CurrentAddress", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CurrentAddress", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@CurrentCity", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CurrentCity", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@CurrentState", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CurrentState", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@CurrentCountry", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CurrentCountry", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@CurrentPincode", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CurrentPincode", null);

                    }
                    else if (Type.Trim() == "PermanentAddress")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@PermanentAddress", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@PermanentAddress", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@PermanentCity", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@PermanentCity", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@PermanentState", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@PermanentState", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@PermanentCountry", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@PermanentCountry", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@PermanentPincode", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@PermanentPincode", null);

                    }
                    else if (Type.Trim() == "ContactDetails")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@Email", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Email", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@Mobile", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Mobile", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@OfficeNumber", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@OfficeNumber", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@EmergencyName", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@EmergencyName", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@EmergencyNumber", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@EmergencyNumber", null);



                    }
                    else if (Type.Trim() == "EducationDetails")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@Category", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Category", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@Specilization", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Specilization", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@From", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@From", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@To", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@To", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@Institution", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Institution", null);

                        if (Parameter6 != null && Parameter6.Trim() != string.Empty && Parameter6 != "0")
                            sqlCommand.Parameters.AddWithValue("@Percentage", Parameter6.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Percentage", null);

                        if (Parameter7 != null && Parameter7.Trim() != string.Empty && Parameter7 != "0")
                            sqlCommand.Parameters.AddWithValue("@EducationId", Parameter7.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@EducationId", null);
                    }
                    else if (Type.Trim() == "CareerDetails")
                    {
                        if (Parameter1 != null && Parameter1.Trim() != string.Empty && Parameter1 != "0")
                            sqlCommand.Parameters.AddWithValue("@Employer", Parameter1.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Employer", null);

                        if (Parameter2 != null && Parameter2.Trim() != string.Empty && Parameter2 != "0")
                            sqlCommand.Parameters.AddWithValue("@FromDate", Parameter2.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@FromDate", null);

                        if (Parameter3 != null && Parameter3.Trim() != string.Empty && Parameter3 != "0")
                            sqlCommand.Parameters.AddWithValue("@ToDate", Parameter3.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@ToDate", null);

                        if (Parameter4 != null && Parameter4.Trim() != string.Empty && Parameter4 != "0")
                            sqlCommand.Parameters.AddWithValue("@ToDesignation", Parameter4.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@ToDesignation", null);

                        if (Parameter5 != null && Parameter5.Trim() != string.Empty && Parameter5 != "0")
                            sqlCommand.Parameters.AddWithValue("@Salary", Parameter5.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@Salary", null);

                        if (Parameter6 != null && Parameter6.Trim() != string.Empty && Parameter6 != "0")
                            sqlCommand.Parameters.AddWithValue("@CareerId", Parameter6.Trim());
                        else
                            sqlCommand.Parameters.AddWithValue("@CareerId", null);
                    }
                }



                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
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
