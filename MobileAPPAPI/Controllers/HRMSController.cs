using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.Models;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
               // string sql = "dbo.HRMS_COMMON_DROPDOWN";
                string sql = "MBL_HRMS_COMMON_DROPDOWN";
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
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("GetEmployees/{EmpCode}")]
        public string GetEmployees(string EmpCode)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                // string sql = "dbo.HRMS_COMMON_DROPDOWN";
                string sql = "MBL_HRMS_GETEMPLOYEES";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPCODE", EmpCode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }

        [HttpGet]
        [Route("EmployeeDetail/{usercode}")]
        public string EmployeeDetail(string usercode)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select a.em_emp_id,a.em_emp_name,a.em_branch_id,a.em_emp_department,a.em_emp_designation from HRMS_EMPLOYEE_MASTER a left join BO_USER_MASTER b on a.tum_user_id = b.TUM_USER_ID where b.TUM_USER_CODE = '" + usercode + "'";

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

                //var result = (new { recordsets = Logdata1 });
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("EmployeeSearch/{EmployeeId}/{Name}/{Code}/{Designation}/{Branch}/{Department}/{Top}/{Increament}/{appURL}")]
        public string EmployeeSearch(string EmployeeId = null, string Name = null, string Code = null, string Designation = null, string Branch = null, string Department = null, string Top = null, string Increament = null, string appURL = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_EMPLOYEE_MASTER_SELECT";
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
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("WorkFlowAuth/{PK1}/{PK2}/{PK3}/{PK4}/{User}/{Function}/{WorkFlowTable}")]
        public string WorkFlowAuth(string PK1, string PK2, string PK3, string PK4, string User, string Function, string WorkFlowTable)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
              
                SqlCommand cmd = new SqlCommand("", dbConn);
                cmd.CommandType = CommandType.Text;
                
                int wfId = 0;

                if (PK2.ToLower() == "null")
                {
                    PK2 = "";
                }
                if (PK3.ToLower() == "null")
                {
                    PK3 = "";
                }
                if (PK4.ToLower() == "null")
                {
                    PK4 = "";
                }

                try
                {
                    cmd.CommandText = "select count(*) from BO_WORKFLOW_CONFIGURATIONS where table_name like '%" + WorkFlowTable + "%' and FUNCTION_ID=" + Function + " and status='A'";

                    int _count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (_count > 0)
                    {
                        cmd.CommandText = "select WF_CONFIG_ID from BO_WORKFLOW_CONFIGURATIONS where table_name like '%" + WorkFlowTable + "%' and status='A' and Function_ID='" + Function + "'";


                        wfId = Convert.ToInt32(cmd.ExecuteScalar());

                        cmd.CommandText = "select pk_column_name1,pk_column_name2,pk_column_name3,pk_column_name4,pk_column_name5,STATUS_COLUMN from BO_WORKFLOW_CONFIGURATIONS with (nolock) where WF_CONFIG_ID='" + wfId + "'";
                        DataSet ds = new DataSet();

                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["pk_column_name1"].ToString() != "" && ds.Tables[0].Rows[0]["STATUS_COLUMN"].ToString() != "")
                            {
                                string strsql = "exec usp_WF_ApprovalUsers '" + WorkFlowTable + "','" + ds.Tables[0].Rows[0]["pk_column_name1"] + "','" + ds.Tables[0].Rows[0]["pk_column_name2"] + "','" + ds.Tables[0].Rows[0]["pk_column_name3"] + "','" + ds.Tables[0].Rows[0]["pk_column_name4"] + "','" + ds.Tables[0].Rows[0]["pk_column_name5"] + "','" + PK1 + "','" + PK2 + "','" + PK3 + "' ,'" + PK4 + "',' ' ,'" + Function + "' ,'" + User + "' ,'" + ds.Tables[0].Rows[0]["STATUS_COLUMN"] + "','P' ,'" + wfId + "'";
                                cmd.CommandText = strsql;

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    return "1";
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }

            }
        }





        [HttpGet]
        [Route("EmployeeLeaveDetails/{EmployeeId}/{Year}/{Month}")]
        public string EmployeeLeaveDetails(string EmployeeId = null, string Year = null, string Month = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_HRMS_Leave_Details";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Month", Month);
             
                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("EmployeeSalaryRegularDeduction/{EmployeeId}/{Year}/{Month}")]
        public string EmployeeSalaryRegularDeduction(string EmployeeId = null, string Year = null, string Month = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_HRMS_PAYROLL_SALARY_REGULAR_DEDUCTIONS";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Month", Month);

                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("EmployeeSalaryRegularEarnings/{EmployeeId}/{Year}/{Month}")]
        public string EmployeeSalaryRegularEarnings(string EmployeeId = null, string Year = null, string Month = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_HRMS_EMPLOYEE_PAROLL_REGULAR_EARNINGS";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Month", Month);

                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
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
                //string sql = "BO_WORKFLOW_USERWISE_SUMMARY";
                string sql = "MBL_BO_WORKFLOW_USERWISE_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERID", UserID);
                cmd.Parameters.AddWithValue("@FUNCTIONID", Functionid);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("SaveLeave/{Function}/{UserID}/{EmpID}/{RequestID}/{LeaveType}/{FromDate}/{ToDate}/{NoDays}/{ContactPhone}/{LeaveReason}/{Status}/{Reaslese}")]
        public string SaveLeave(string Function, string UserID, string EmpID, string RequestID, string LeaveType, string FromDate, string ToDate, string NoDays, string ContactPhone, string LeaveReason, string Status,string Reaslese)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();
            int WF_CONFIG_ID;

            string result = "", result1 = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "BO_WORKFLOW_USERWISE_SUMMARY";
                //string sql = "MBL_HRMS_INSERTUPDATELEAVE";
                string sql = "MBL_HRMS_INSERTUPDATELEAVE1";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION", Function);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                cmd.Parameters.AddWithValue("@RequestID", RequestID);
                cmd.Parameters.AddWithValue("@LeaveType", LeaveType);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@NoDays", NoDays);
                cmd.Parameters.AddWithValue("@ContactPhone", ContactPhone);
                cmd.Parameters.AddWithValue("@LeaveReason", LeaveReason);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@Result1", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;//6dec

                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@Result"].Value.ToString();
                result1 = cmd.Parameters["@Result1"].Value.ToString();//dec
                var json = new JavaScriptSerializer().Serialize(result);

                if (result == "Your request is on holiday, please choose different date" )
                {
                    Reaslese = "False";
                }
                if (result== "Leave Request already raised for this date. Cannot Process your Request")
                {
                    Reaslese = "False";
                }
                if (result == "Coff already available for this date")
                {
                    Reaslese = "False";
                }
                if (result == "OT already available for this date")
                {
                    Reaslese = "False";
                }
                if (result == "Permission already available for this date")
                {
                    Reaslese = "False";
                }
                if (result == "OD already available for this date")
                {
                    Reaslese = "False";
                }

                if (Reaslese == "True")//6 dec
                {



                    string query2 = "";
                    query2 = "select WF_CONFIG_ID from BO_WORKFLOW_CONFIGURATIONS where table_name like '%Leave%' and status='A' and Function_ID='" + Function + "'";

                    SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                    var reader2 = cmd2.ExecuteReader();
                    System.Data.DataTable results2 = new System.Data.DataTable();
                    results2.Load(reader2);
                    for (int i = 0; i < results2.Rows.Count; i++)
                    {


                        DataRow row = results2.Rows[i];
                        int wkno = Convert.ToInt32(row[0]);

                        WF_CONFIG_ID = Convert.ToInt32(row[0]);




                        string query3 = "";
                        query3 = "exec usp_WF_ApprovalUsers 'HRMS_ATTLEAVEREQUEST','LeaveRequestRef','','','','','" + result1 + "','','' ,'','' ,'1' ,'" + UserID + "' ,'CurrentStatus','' ,'" + WF_CONFIG_ID + "'";
                        SqlCommand cmd3 = new SqlCommand(query3, dbConn);
                        var reader3 = cmd3.ExecuteReader();

                        System.Data.DataTable results3 = new System.Data.DataTable();
                        results3.Load(reader3);
                        Logdata1 = "Successfully Saved";
                        for (int i2 = 0; i2 < results3.Rows.Count; i2++)
                        {
                            Logdata1 = DataTableToJSONWithStringBuilder(results3);
                            dbConn.Close();
                        }
                    }

                }
                return json;


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
               // string sql = "HRMS_My_Payslip";
                string sql = "MBL_HRMS_My_Payslip";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@appURL", appURL);

                SqlDataAdapter da = new SqlDataAdapter(cmd);


                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    Logdata1 = "No data found";
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
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
               // string sql = "HRMS_My_Approval";
                string sql = "MBL_HRMS_My_Approval";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);


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
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("getlastinsertedexpense")]
        public async Task<ActionResult<HRMS>> getlastinsertedexpense(HRMS data)
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
                query = "select MAX(EXPSEP_ID) as expenseid,MAX(EXP_ID) AS EXP_id,MAX(EXPREF_ID) AS EXPREF_ID from HRMS_EXPENSE_DETAILS";

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
                return Ok(Logdata1);


            }
        }



        [HttpPost]
        [Route("getdesignation")]
        public async Task<ActionResult<HRMS>> getdesignation(HRMS data)
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
                query = "select distinct HRMS_EMPLOYEE_MASTER.*,BO_PARAMETER.*,BO_USER_TYPE_MASTER.* from ((HRMS_EMPLOYEE_MASTER inner join BO_PARAMETER on HRMS_EMPLOYEE_MASTER.em_emp_department = BO_PARAMETER.VAL) inner join BO_USER_TYPE_MASTER on HRMS_EMPLOYEE_MASTER.em_emp_designation = BO_USER_TYPE_MASTER.TYPE_ID) where HRMS_EMPLOYEE_MASTER.EM_EMP_CODE ='" + data.emp_code + "' and  BO_PARAMETER.TYPE='BO_TEAM' and BO_PARAMETER.STATUS = 'A' and BO_USER_TYPE_MASTER.Status='A' ";

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
                return Ok(Logdata1);


            }
        }



        [HttpPost]
        [Route("getCurrencyType")]
        public async Task<ActionResult<HRMS>> getCurrencyType(HRMS data)
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
                query = "SELECT TEXT,VAL  FROM BO_PARAMETER  WHERE BO_PARAMETER.FUNCTION_ID='"+data.functionid+"' AND  TYPE ='BASE_CURRENCY' ";

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
                return Ok(Logdata1);


            }
        }

        [HttpPost]
        [Route("getrequestrefid")]
        public async Task<ActionResult<HRMS>> getrequestrefid(HRMS data)
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
                query = "select ODRequestRef from hrms_attodrequest where TxnReference ='" + data.TxnReference + "' ";

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
                return Ok(Logdata1);


            }
        }




        [HttpPost]
        [Route("getreportingto")]
        public async Task<ActionResult<HRMS>> getreportingto(HRMS data)
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
                query = "select *,TUM_USER_NAME as requestor from BO_USER_MASTER where TUM_USER_ID = '"+data.userid+"'";

                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Property log = new Property();
                    DataRow row = results.Rows[i];

                   string  query1 = "select Device_Token from BO_USER_MASTER where TUM_USER_ID = '" + row["TUM_REPORTING_TO"] + "'";

                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader);

                    if (data.typerequest== "Permission Request")
                    {
                        string st =row["requestor"].ToString()+ " has raised a request from"+data.from+" to "+data.to+" on "+data.perdate;
                        Logdata1 = new JavaScriptSerializer().Serialize(st);
                    }
                   else if (data.typerequest == "Coff Request")
                    {
                        string st = row["requestor"].ToString() + " has raised a request for coff date" + data.to + " on working date " + data.from ;
                        Logdata1 = new JavaScriptSerializer().Serialize(st);
                    }
                    else if (data.typerequest == "Loan Request")
                    {
                        string st = row["requestor"].ToString() + " has raised a"  + data.perdate + "request";
                        Logdata1 = new JavaScriptSerializer().Serialize(st);
                    }
                    else
                    {
                        string st = row["requestor"].ToString() + " has raised a request from" + data.from + " to " + data.to;
                        Logdata1 = new JavaScriptSerializer().Serialize(st);
                    }
                    

                }


                //    if (results.Rows.Count == 0)
                //{
                //    string st = "No data found";

                //    Logdata1 = new JavaScriptSerializer().Serialize(st);
                //}
                //else
                //{
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //}
                dbConn.Close();

                var result = (new { recordsets = Logdata1 });
                return Ok(Logdata1);


            }
        }





        [HttpPost]
        [Route("getTravelMode")]
        public async Task<ActionResult<HRMS>> getTravelMode(HRMS data)
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
                query = "SELECT TEXT,VAL FROM BO_PARAMETER WHERE TYPE='TRAVEL MODE' and FUNCTION_ID=" + data.functionid + "";

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
                return Ok(Logdata1);


            }
        }




        [HttpPost]
        [Route("saveExpenseClaimsDetail")]
        public async Task<ActionResult<HRMS>> saveExpenseClaimsDetail(HRMS data)
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
                query = "EXEC MBL_HRMS_CLAIMS_EXPENSE_DETAILS_SAVE @Functionid ='" + data.functionid + "',@Branchid ='" + data.branchid + "',@Expenseid  ='" + data.Expenseid + "',@Exp_id ='" + data.Exp_id + "',@ExpensesType ='" + data.ExpensesType + "',@ExpensesAmount   ='" + data.ExpensesAmount + "',@Remarks ='" + data.Remarks + "',@UserId   ='" + data.userid + "',@RequestRef='" + data.RequestRef + "'";

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
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
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
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("EmployeeDailyAttendance/{EmployeeId}/{Year}/{Month}/{Type}")]
        public string EmployeeDailyAttendance(string EmployeeId = null, string Year = null, string Month = null, string Type = null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_DAILY_ATTENDANCE";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (EmployeeId != null && EmployeeId.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPLOYEEID", EmployeeId);
                else
                    sqlCommand.Parameters.AddWithValue("@EMPLOYEEID", 0);

                if (Year != null && Year.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@YEAR", Year.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@YEAR", "0");

                if (Month != null && Month.Trim() != "0" && Month.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@MONTH", Month.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@MONTH", "0");

                if (Type != null && Type.Trim() != "0" && Type.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@TYPE", Type.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@TYPE", "0");


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("SearchCOFF/{Function}/{EmpID}/{FromDate}/{ToDate}")]
        public string SearchCOFF(string Function, string EmpID, string FromDate, string ToDate)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_SEARCHCOFFREQUESTS";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (Function != null && Function.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", Function);
                else
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", 0);

                if (EmpID != null && EmpID.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPID", EmpID.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@EMPID", "0");
                    sqlCommand.Parameters.AddWithValue("@FROMDATE", FromDate.Trim());
                    sqlCommand.Parameters.AddWithValue("@TODATE", ToDate.Trim());


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }




        [HttpGet]
        [Route("SearchOD/{Function}/{EmpID}/{FromDate}/{ToDate}")]
        public string SearchOD(string Function, string EmpID, string FromDate, string ToDate)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_SEARCHODREQUESTS";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (Function != null && Function.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", Function);
                else
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", 0);

                if (EmpID != null && EmpID.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPID", EmpID.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@EMPID", "0");
                sqlCommand.Parameters.AddWithValue("@FROMDATE", FromDate.Trim());
                sqlCommand.Parameters.AddWithValue("@TODATE", ToDate.Trim());


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }



      




        [HttpGet]
        [Route("LoadLeaveType/{Function}/{EmpID}")]
        public string LoadLeaveType(string Function, string EmpID)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_LoadLeaveType";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (Function != null && Function.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", Function);
                else
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", 0);

                if (EmpID != null && EmpID.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPID", EmpID.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@EMPID", "0");
                


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("SearchLeave/{Function}/{EmpID}/{FromDate}/{ToDate}/{LEAVETYPE}")]
        public string SearchLeave(string Function, string EmpID, string FromDate, string ToDate, string LEAVETYPE=null)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_SEARCHLEAVEREQUESTS";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (Function != null && Function.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", Function);
                else
                    sqlCommand.Parameters.AddWithValue("@FUNCTION", 0);

                if (EmpID != null && EmpID.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPID", EmpID.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@EMPID", "0");
                sqlCommand.Parameters.AddWithValue("@FROMDATE", FromDate.Trim());
                sqlCommand.Parameters.AddWithValue("@TODATE", ToDate.Trim());
                sqlCommand.Parameters.AddWithValue("@LEAVETYPE", LEAVETYPE);


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpGet]
        [Route("CancelRequest/{RequestID}/{RequestType}")]
        public string CancelRequest(string RequestID, string RequestType)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_HRMS_CancelRequest";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", RequestID);
                cmd.Parameters.AddWithValue("@RequestType", RequestType);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string st = cmd.Parameters["@Result"].Value.ToString();
                var json = new JavaScriptSerializer().Serialize(st);
                return json;


                
            }
        }


        [HttpGet]
        [Route("EmployeeLeaveConfig/{LeaveType}/{EmpID}")]
        public string EmployeeLeaveConfig(string LeaveType, string EmpID)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_EMPLOYEELEAVETYPECONFIG";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@LeaveType", LeaveType);
                sqlCommand.Parameters.AddWithValue("@EMPID", EmpID);



                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpPost]
        [Route("getsummaryClaims")]
        public async Task<ActionResult<HRMS>> getsummaryClaims(HRMS data)
        {
            // string struser = data.user_lower;

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            var JsonString = new StringBuilder();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "MBL_HRMS_CLAIMS_SUMMARY";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", data.functionid);
                cmd.Parameters.AddWithValue("@Branch", data.branchid);
                cmd.Parameters.AddWithValue("@EmpId", data.EmpId);
                cmd.Parameters.AddWithValue("@ExpCategory", "0");
                cmd.Parameters.AddWithValue("@Description", "");
                cmd.Parameters.AddWithValue("@Status", data.assetcode);
              


                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                else
                {

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Ok(Logdata1);



            }
        }

        [HttpPost]
        [Route("getExpenseIDClaims")]
        public async Task<ActionResult<HRMS>> getExpenseIDClaims(HRMS data)
        {
            // string struser = data.user_lower;

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            var JsonString = new StringBuilder();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "select EXPENSE_ID as pk1 from HRMS_EXPENSE_SPECIFICATION_DETAILS where request_ref= '" + data.RequestRef + "'";

                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                else
                {

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Ok(Logdata1);



            }
        }





        [HttpPost]
        [Route("saveMasterClaimsDetail")]
        public async Task<ActionResult<HRMS>> saveMasterClaimsDetail(HRMS data)
        {
            // string struser = data.user_lower;

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            var JsonString = new StringBuilder();
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "EXEC MBL_HRMS_CLAIMS_EXPENSE_MASTER_SAVE @FUNCTION_ID ='" + data.functionid + "',@BRANCH_ID ='" + data.branchid + "',@EMP_ID ='" + data.EmpId + "',@EXPENSE_REF ='" + data.EXPENSE_REF + "',@EXPENSE_ID ='" + data.EXPENSE_ID + "',@EXPENSE_DATE   ='" + data.EXPENSE_DATE + "',@EXPENSE_CATEGORY ='" + data.EXPENSE_CATEGORY + "',@DESCRIPTION   ='" + data.DESCRIPTION + "',@AMOUNT='" + data.Amount + "',@IS_BILLABLE='" + data.IS_BILLABLE + "',@COMMENTS='" + data.COMMENTS + "',@STATUS='" + data.STATUS + "',@CREATED_BY='" + data.userid + "',@UPDATED_BY='" + data.userid + "',@IPADDRESS='::1',@request_ref='" + data.request_ref + "'";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
             
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                else
                {

                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Ok(Logdata1);



            }
        }




        [HttpPost]
        [Route("gettravelDetailsClaims")]
        public async Task<ActionResult<HRMS>> gettravelDetailsClaims(HRMS data)
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
                string sql = "MBL_HRMS_CLAIMS_TRAVEL_DETAILS";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TxnReference", data.TxnReference);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Ok(Logdata1);



            }
        }


        [HttpPost]
        [Route("getRequestCategoryClaims")]
        public async Task<ActionResult<HRMS>> getRequestCategoryClaims(HRMS data)
        {
            

            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT TEXT,VAL FROM BO_PARAMETER WHERE TYPE ='RequestCategory' and FUNCTION_ID=" + data.functionid + "";

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
                return Ok(Logdata1);


            }
        }




        [HttpPost]
        [Route("getTravelDetails")]
        public async Task<ActionResult<HRMS>> getTravelDetails(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select HRMS_ATTTRAVELDETAILS.*,(SELECT TEXT FROM BO_PARAMETER WHERE TYPE='TRAVEL MODE' and BO_PARAMETER.VAL = HRMS_ATTTRAVELDETAILS.TravelMode and BO_PARAMETER.FUNCTION_ID=HRMS_ATTTRAVELDETAILS.CompanyID ) as TravelMode,HRMS_EMP_TRAVEL_PROCESS_DETAILS.*,(select doc_name from BO_DOCS_UPLOAD with (nolock) where MODULE_ID='2000003' AND PK1=HRMS_EMP_TRAVEL_PROCESS_DETAILS.PROCESS_ID AND PK2='508' AND PK3='' AND PK4='') as Doc_Name from HRMS_ATTTRAVELDETAILS  left outer JOIN HRMS_EMP_TRAVEL_PROCESS_DETAILS On HRMS_ATTTRAVELDETAILS.RequestRef = HRMS_EMP_TRAVEL_PROCESS_DETAILS.leaveref where HRMS_ATTTRAVELDETAILS.UserID=" + data.userid + " and HRMS_ATTTRAVELDETAILS.RequestRef='" + data.RequestRef + "'";

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
                return Ok(Logdata1);


            }
        }





        [HttpPost]
        [Route("getrequestReferencedata")]
        public async Task<ActionResult<HRMS>> getrequestReferencedata(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select TxnReference  from HRMS_ATTODREQUEST OD INNER JOIN HRMS_EMPLOYEE_MASTER EM WITH (NOLOCK) ON EM.em_emp_domain = convert(varchar,OD.CompanyID) AND convert(varchar,EM.em_emp_id) = OD.UserID where CurrentStatus='A' AND EM_EMP_ID ='" + data.EmpId + "'";

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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("getODUpdateDate")]
        public async Task<ActionResult<HRMS>> getODUpdateDate(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "update HRMS_ATTODREQUEST set CurrentStatus='" + data.currentstatus + "',FromDate='" + data.Fromdate + "',ToDate='" + data.todate + "',NoDays='" + data.nodays + "',FromHours='" + data.fromhours + "',ToHours='" + data.tohours + "',ODReason='" + data.REASON + "' where ODRequestRef='" + data.RequestRef + "'";

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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("getExpensetypeClaims")]
        public async Task<ActionResult<HRMS>> getExpensetypeClaims(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "SELECT TEXT,VAL FROM BO_PARAMETER WHERE TYPE ='Expense Type'and FUNCTION_ID=" + data.functionid + "";

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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("hrmeducationcategory")]
        public async Task<ActionResult<HRMS>> hrmeducationcategory(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from BO_PARAMETER where FUNCTION_ID=" + data.functionid + " AND type LIKE '%HRMS Education%'";

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
                return Ok(Logdata1);


            }
        }



        [HttpPost]
        [Route("insertletter_request")]
        public async Task<ActionResult<HRMS>> insertletter_request(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "insert into HRMS_LETTER_REQUEST_DETAILS (FUNCTION_ID,BRANCH_ID,DEPARTMENT_ID,DESIGNATION_ID,EMP_CODE,LETTER_TYPE,REQ_DATE,REASON,STATUS,URL,UPDATED_ON,CREATED_BY,CREATED_ON,UPDATED_BY,IP_ADDRESS) VALUES('" + data.functionid + "', '" + data.branchid + "', '" + data.DEPARTMENT_ID + "', '" + data.DESIGNATION_ID + "', '" + data.emp_code + "', '" + data.LETTER_TYPE + "', GETDATE(), '" + data.REASON + "', '" + data.STATUS + "', NULL, NULL, '1', GETDATE(), NULL, '1');SELECT SCOPE_IDENTITY() AS inserted_id";

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
                return Ok(Logdata1);


            }
        }


        [HttpPost]
        [Route("letter_dropdown")]
        public async Task<ActionResult<HRMS>> letter_dropdown(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select BO_PARAMETER.TEXT,BO_PARAMETER.VAL FROM  BO_PARAMETER WHERE status ='A' and type like '%hrms letters%'";

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
                return Ok(Logdata1);


            }
        }

        [HttpPost]
        [Route("summaryletter_request")]
        public async Task<ActionResult<HRMS>> summaryletter_request(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from HRMS_LETTER_REQUEST_DETAILS order by REQ_ID desc";

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
                return Ok(Logdata1);


            }
        }


        //oct7 SHYLAJA

        [HttpPost]
        [Route("getloanscheme")]
        public async Task<ActionResult<HRMS>> getloanscheme(HRMS data)
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
                string sql = "MBL_LOAN_GETSCHEME";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DESIGNATION", data.DESIGNATION);
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Ok(Logdata1);



            }
        }



        [HttpGet]
        [Route("LoadAssetCategory")]
        public string LoadAssetCategory()
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select VAL as id,TEXT as Text from BO_PARAMETER where type='INFCATEGORY' and status='A'";

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

                //var result = (new { recordsets = Logdata1 });
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("LoadAssetSubCategory/{Function}/{Category}")]
        public string LoadAssetSubCategory(string Function, string Category)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select bp.SUB_CATEGORY_ID as id,bp.SUB_CATEGORY_DESC as TEXT from CAMS_ASSET_SUBCATEGORY_MASTER bp  where bp.CATEGORY_ID = "+ Category+" and bp.STATUS = 'A'";

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

                //var result = (new { recordsets = Logdata1 });
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("SearchAssets/{EmpID}/{FromDate}/{ToDate}/{Category}/{SubCategory}")]
        public string SearchAssets(string EmpID, string FromDate, string ToDate, string Category, string SubCategory)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "HRMS_EMPLOYEE_UPDATE_BY_DETAILS";
                string sql = "MBL_HRMS_SEARCHASSETREQUESTS";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (EmpID != null && EmpID.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EMPID", EmpID.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@EMPID", "0");
                sqlCommand.Parameters.AddWithValue("@FROMDATE", FromDate.Trim());
                sqlCommand.Parameters.AddWithValue("@TODATE", ToDate.Trim());
                sqlCommand.Parameters.AddWithValue("@Category", Category.Trim());
                sqlCommand.Parameters.AddWithValue("@SubCategory", SubCategory.Trim());


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }


        [HttpPost]
        [Route("insertloanadvancedetails")]
        public async Task<ActionResult<HRMS>> insertloanadvancedetails(HRMS data)
        {
            // string struser = data.user_lower;

       
            System.Data.DataTable results2 = new System.Data.DataTable();
            string Logdata1 = string.Empty;
            
            int req_id;
            string strreq_id = "";
            string pk_column_name1="";
            string pk_column_name2 = "";
            string pk_column_name3 = "";
            string pk_column_name4 = "";
            string pk_column_name5 = "";
            string STATUS_COLUMN = "";

            string output = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                string sql = "MBL_HRMS_EMPLOYEE_LOAN_DETAILS_INSERT";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@EMPID", data.EmpId);
                sqlCommand.Parameters.AddWithValue("@REQUESTTYPE",data.req_type);
                sqlCommand.Parameters.AddWithValue("@SCHEMEID", data.scheme_id);
                sqlCommand.Parameters.AddWithValue("@MAXINSTALL",data.MonthLy_installment);
                sqlCommand.Parameters.AddWithValue("@AMOUNT", data.Amount);
                sqlCommand.Parameters.AddWithValue("@STATUS", data.STATUS);
                sqlCommand.Parameters.AddWithValue("@MONTHLYDEDUCTION", data.Monthly_deduct);
                sqlCommand.Parameters.AddWithValue("@REVLOAN ", data.Rev_loan);
                sqlCommand.Parameters.AddWithValue("@CREATEDBY ", data.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@USERID ", data.userid);
                sqlCommand.Parameters.AddWithValue("@CREATEDON ", data.Createdon);
                sqlCommand.Parameters.AddWithValue("@UPDATEDON ", data.Updatedon);
                sqlCommand.Parameters.AddWithValue("@IPADDRESS", data.ipaddress);
                sqlCommand.Parameters.AddWithValue("@ISDEFFERRAL ", data.isdeferral);
                sqlCommand.Parameters.AddWithValue("@DEFMODE ", data.deferralmode);
                sqlCommand.Parameters.AddWithValue("@FUNCTIONID ", data.functionid);


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);

             

                    string query1 = "";
                    query1 = "select max(req_id) as reqid from HRMS_EMPLOYEE_LOAN_DETAILS";
                    SqlCommand cmd1 = new SqlCommand(query1, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                for (int i1 = 0; i1 < results1.Rows.Count; i1++)
                {
                    DataRow row1 = results1.Rows[i1];
                    req_id = Convert.ToInt32(row1[0]);
                    strreq_id = row1[0].ToString();

                }


                        string query2 = "";
                        query2 = "select pk_column_name1,pk_column_name2,pk_column_name3,pk_column_name4,pk_column_name5,STATUS_COLUMN from BO_WORKFLOW_CONFIGURATIONS with (nolock) where WF_CONFIG_ID='13' ";

                        SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                        var reader2 = cmd2.ExecuteReader();

                        results2.Load(reader2);

                for (int i2 = 0; i2 < results2.Rows.Count; i2++)
                {
                    DataRow row2 = results1.Rows[i2];
                    pk_column_name1 = row2[0].ToString();

                    pk_column_name2 = row2[0].ToString();
                    pk_column_name3 = row2[0].ToString();
                    pk_column_name4 = row2[0].ToString();
                    pk_column_name5 = row2[0].ToString();
                    STATUS_COLUMN = row2[0].ToString();
                }


                string sql2 = "MBL_usp_WF_ApprovalUsers";
                SqlCommand sqlCommand2 = new SqlCommand(sql2, dbConn);
                sqlCommand2.Parameters.AddWithValue("@TableName", "HRMS_EMPLOYEE_LOAN_DETAILS");
                sqlCommand2.Parameters.AddWithValue("@PKColumnName1" ,pk_column_name1);
                sqlCommand2.Parameters.AddWithValue("@PKColumnName2", pk_column_name2);
                sqlCommand2.Parameters.AddWithValue("@PKColumnName3", pk_column_name3);
                sqlCommand2.Parameters.AddWithValue("@PKColumnName4", pk_column_name4);
                sqlCommand2.Parameters.AddWithValue("@PKColumnName5", pk_column_name5);
                sqlCommand2.Parameters.AddWithValue("@PKColumnValue1", strreq_id);
                sqlCommand2.Parameters.AddWithValue("@PKColumnValue2", "0");
                sqlCommand2.Parameters.AddWithValue("@PKColumnValue3", "0");
                sqlCommand2.Parameters.AddWithValue("@PKColumnValue4", "0");
                sqlCommand2.Parameters.AddWithValue("@PKColumnValue5", "0");
                sqlCommand2.Parameters.AddWithValue("@PKDomain", data.functionid);
                sqlCommand2.Parameters.AddWithValue("@RequestedBy", data.userid);
                sqlCommand2.Parameters.AddWithValue("@PKStatusColumn", STATUS_COLUMN);
                sqlCommand2.Parameters.AddWithValue("@PKStatus", "P");
                sqlCommand2.Parameters.AddWithValue("@ModuleId", "167");


                SqlDataAdapter da1 = new SqlDataAdapter(sqlCommand);


                var reader3 = sqlCommand.ExecuteReader();
                System.Data.DataTable results3 = new System.Data.DataTable();
                results3.Load(reader3);


                for (int i3 = 0; i3 < results3.Rows.Count; i3++)
                {
                    DataRow row3 = results3.Rows[i3];
                    Logdata1 = DataTableToJSONWithStringBuilder(results3);
                }
               // return Logdata1;
                return Ok(Logdata1);
            }


        }


        [HttpPost]
        [Route("loanrequestsummary")]
        public async Task<ActionResult<HRMS>> loanrequestsummary(HRMS data)
        {
            // string struser = data.user_lower;

            string output = "";
            string em_emp_id;
            string Logdata1 = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();

                if (data.usertype == 1)
                {

                    string query = "";
                    query = "select distinct HRMS_EMPLOYEE_LOAN_DETAILS.*,HRMS_LOAN_MASTER.scheme_code,case when (HRMS_LOAN_MASTER.scheme_desc is null and HRMS_EMPLOYEE_LOAN_DETAILS.scheme_id=0) then 'Advance' else HRMS_LOAN_MASTER.scheme_desc end as scheme_desc  from HRMS_EMPLOYEE_LOAN_DETAILS left join HRMS_LOAN_MASTER on HRMS_EMPLOYEE_LOAN_DETAILS.scheme_id = HRMS_LOAN_MASTER.scheme_id order by req_id desc";
                    SqlCommand cmd1 = new SqlCommand(query, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    for (int i = 0; i < results1.Rows.Count; i++)
                    {
                        DataRow row3 = results1.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results1);
                    }

                }
                else
                {
                    string query = "";
                    query = "select em_emp_id from hrms_employee_master where EM_EMP_CODE='" + data.emp_code + "'";
                    SqlCommand cmd1 = new SqlCommand(query, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);
                    for (int i1 = 0; i1 < results1.Rows.Count; i1++)
                    {
                        DataRow row1 = results1.Rows[i1];
                        em_emp_id = row1[0].ToString();



                        string query1 = "";
                        query1 = "select distinct HRMS_EMPLOYEE_LOAN_DETAILS.*,HRMS_LOAN_MASTER.scheme_code,case when(HRMS_LOAN_MASTER.scheme_desc is null and HRMS_EMPLOYEE_LOAN_DETAILS.scheme_id = 0) then 'Advance' else HRMS_LOAN_MASTER.scheme_desc end as scheme_desc  from HRMS_EMPLOYEE_LOAN_DETAILS left join HRMS_LOAN_MASTER on HRMS_EMPLOYEE_LOAN_DETAILS.scheme_id = HRMS_LOAN_MASTER.scheme_id where HRMS_EMPLOYEE_LOAN_DETAILS.empid = '" + em_emp_id + "' order by req_id desc";
                        SqlCommand cmd2 = new SqlCommand(query1, dbConn);
                        var reader2 = cmd2.ExecuteReader();
                        System.Data.DataTable results2 = new System.Data.DataTable();
                        results2.Load(reader2);

                        for (int i3 = 0; i3 < results2.Rows.Count; i3++)
                        {
                            DataRow row3 = results2.Rows[i3];
                            Logdata1 = DataTableToJSONWithStringBuilder(results2);
                        }
                    }

                }
                return Ok(Logdata1);
            }
        }

        [HttpGet]
        [Route("SaveOnDuty/{Function}/{EmpID}/{REQUESTID}/{FromDate}/{FromHours}/{ToDate}/{ToHours}/{NoDays}/{ContactPhone}/{ODReason}/{Status}/{istravelreq}/{isadvancereq}")]

        public string SaveOnDuty(string Function, string EmpID, string REQUESTID, string FromDate, string FromHours, string ToDate, string ToHours, string NoDays, string ContactPhone, string ODReason, string Status, string istravelreq, string isadvancereq)
        {
            FromHours = FromHours.Replace("@", ":");
            ToHours = ToHours.Replace("@", ":");
            string result = "";
             string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string sql = "[dbo].[MBL_HRMS_INSERTUPDATEOD]";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION", Function);
                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                cmd.Parameters.AddWithValue("@REQUESTID", REQUESTID);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@FromHours", FromHours);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@ToHours", ToHours);
                cmd.Parameters.AddWithValue("@NoDays", NoDays);
                cmd.Parameters.AddWithValue("@ContactPhone", ContactPhone);
                cmd.Parameters.AddWithValue("@ODReason", ODReason);
                cmd.Parameters.AddWithValue("@STATUS", Status);
                cmd.Parameters.AddWithValue("@IsTravelRequired", istravelreq);
                cmd.Parameters.AddWithValue("@IsAdvanceRequired", isadvancereq);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                SqlDataAdapter da = new SqlDataAdapter(cmd);


                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();

                string st = cmd.Parameters["@Result"].Value.ToString();
                var json = new JavaScriptSerializer().Serialize(st);
                return json;
                //if (results.Rows.Count == 0)
                //{
                //    string st = "No data found";

                //    Logdata1 = new JavaScriptSerializer().Serialize(st);
                //}
                //else
                //{
                //    for (int i = 0; i < results.Rows.Count; i++)
                //    {
                //        DataRow row = results.Rows[i];
                //        Logdata1 = DataTableToJSONWithStringBuilder(results);
                //    }
                //}
                //return Logdata1;
            }
        }


        [HttpGet]
        [Route("SavePermission/{Function}/{UserID}/{REQUESTID}/{ReqDate}/{FromDate}/{FromHours}/{ToHours}/{ContactPhone}/{Reason}/{CurrentStatus}")]

        public string SavePermission(string Function, string UserID, string REQUESTID, string ReqDate, string FromDate, string FromHours, string ToHours, string ContactPhone, string Reason, string CurrentStatus)
        {
            FromHours = FromHours.Replace("@", ":");
            ToHours = ToHours.Replace("@", ":");
            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string sql = "[dbo].[MBL_HRMS_INSERTUPDATEPERM]";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION", Function);
                cmd.Parameters.AddWithValue("@USERID", UserID);
                cmd.Parameters.AddWithValue("@REQUESTID", REQUESTID);
                cmd.Parameters.AddWithValue("@ReqDate", ReqDate);
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@FromHours", FromHours);
                cmd.Parameters.AddWithValue("@ToHours", ToHours);
                cmd.Parameters.AddWithValue("@ContactPhone", ContactPhone);
                cmd.Parameters.AddWithValue("@Reason", Reason);
                cmd.Parameters.AddWithValue("@STATUS", CurrentStatus);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                

                var reader = cmd.ExecuteReader();
                string st = cmd.Parameters["@Result"].Value.ToString();
                var json = new JavaScriptSerializer().Serialize(st);
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (json != "")
                {
                   Logdata1 = "REQUESTID : " + json + " Permission Updated Successfully"; 

                                     
                }
                else
                {
                    Logdata1 = "No data found";
                }
                return Logdata1;
            }
            }



        [HttpGet]
        [Route("SearchPermission/{Function}/{EmpID}/{FromDate}/{ToDate}/{Reason}")]
        public string SearchPermission(string Function, string EmpID, string FromDate, string ToDate,string Reason)
        {

            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();

                //string sql = "MBL_HRMS_SEARCHPERMREQUESTS";//6 DEC
                string sql = "MBL_HRMS_SEARCHPERMREQUESTS1";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION", Function);
                cmd.Parameters.AddWithValue("@EMPID", EmpID);
                cmd.Parameters.AddWithValue("@FROMDATE", FromDate);
                cmd.Parameters.AddWithValue("@TODATE", ToDate);
                cmd.Parameters.AddWithValue("@REASON", Reason);//6 Dec


                SqlDataAdapter da = new SqlDataAdapter(cmd);


                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }



        [HttpGet]
        [Route("EmployeeDetailsDelete/{EmployeeId}/{Type}/{Parameter1}/{Parameter2}")]
        public string EmployeeDetailsDelete(string EmployeeId = null, string Type = null, string Parameter1 = null, string Parameter2 = null)
        {

            string Logdata1 = string.Empty;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();

                string sql = "MBL_HRMS_EMPLOYEE_DETAILS_DELETE";
                SqlCommand sqlCommand = new SqlCommand(sql, dbConn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                if (EmployeeId != null && EmployeeId.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                else
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", 0);

                if (Type != null && Type.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@Type", Type.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@Type", null);

                if (Parameter1 != null && Parameter1.Trim() != "0" && Parameter1.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@Parameter1", Parameter1.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@Parameter1", null);

                if (Parameter2 != null && Parameter2.Trim() != "0" && Parameter2.Trim() != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@Parameter2", Parameter2.Trim());
                else
                    sqlCommand.Parameters.AddWithValue("@Parameter2", null);


                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);


                var reader = sqlCommand.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                if (results.Rows.Count == 0)
                {
                    string st = "No data found";

                    Logdata1 = new JavaScriptSerializer().Serialize(st);
                }
                else
                {
                    for (int i = 0; i < results.Rows.Count; i++)
                    {
                        DataRow row = results.Rows[i];
                        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    }
                }
                return Logdata1;
            }
        }




        [HttpPost]
        [Route("getODRequestRef")]
        public async Task<ActionResult<HRMS>> getODRequestRef(HRMS data)
        {


            List<HRMS> Logdata = new List<HRMS>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select * from HRMS_ATTODREQUEST where ODRequestRef='" + data.ODRequestRef + "'";

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
                return Ok(Logdata1);


            }
        }




        [HttpGet]
        [Route("SaveAssets/{Function}/{Branch}/{RequestID}/{EmpID}/{ReqDate}/{AssetCategory}/{AssetSubCategory}/{ReturnDate}/{Reason}/{Status}/{AssetID}/{userid}/{Reaslese}")]
        public string SaveAssets(string Function, string Branch, string RequestID, string EmpID, string ReqDate, string AssetCategory, string AssetSubCategory,  string ReturnDate, string Reason, string Status,string AssetID,string userid,string Reaslese)
        {

            string Logdata1 = string.Empty;
            string st = string.Empty;
            string st1 = string.Empty;
            int WF_CONFIG_ID;

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                if (RequestID == "" || RequestID == null || RequestID =="NULL" || RequestID=="@")
                {
                    if (RequestID == "NULL")
                    {
                        RequestID = "";
                    }

                    string sql = "[dbo].[MBL_HRMS_INSERTUPDATEASSET]";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Function", Function);
                    cmd.Parameters.AddWithValue("@Branch", Branch);
                    cmd.Parameters.AddWithValue("@RequestID", RequestID);
                    cmd.Parameters.AddWithValue("@EmpID", EmpID);
                    cmd.Parameters.AddWithValue("@ReqDate", ReqDate);
                    cmd.Parameters.AddWithValue("@AssetCategory", AssetCategory);
                    cmd.Parameters.AddWithValue("@AssetSubCategory", AssetSubCategory);
                    // cmd.Parameters.AddWithValue("@RequiredBefore", Requiredbefore);
                    cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;



                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    var reader = cmd.ExecuteReader();
                     st = cmd.Parameters["@Result"].Value.ToString();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    //if (results.Rows.Count == 0)
                    //{
                    //    string st1 = "No data found";

                    //    Logdata1 = new JavaScriptSerializer().Serialize(st1);
                    //}
                    //else
                    //{
                    //    for (int i = 0; i < results.Rows.Count; i++)
                    //    {
                    //        DataRow row = results.Rows[i];
                    //        Logdata1 = DataTableToJSONWithStringBuilder(results);
                    //    }
                    //}

                    if (AssetID==null || AssetID=="NULL"|| AssetID=="Null" || AssetID=="null")
                    {
                        AssetID = "0";
                    }


                    string query = "";
                    query = "INSERT INTO [dbo].[HRMS_ASSET_REQ](FUNCTION_ID,[BRANCH_ID],[EMP_ID],[ASSET_ID],[REQUEST_DATE],[RETURN_DATE],[EXTENSION_TILL],[REASON],[STATUS],[CREATED_BY],[CREATED_ON],[UPDATED_BY],[UPDATED_ON],[IP_ADDRESS])VALUES('" + Function + "','" + Branch + "','" + EmpID + "','" + AssetID + "',convert(datetime,'" + ReqDate + "',103),convert(datetime,'" + ReturnDate+ "',103),convert(datetime,null,103),'" + Reason + "','" + Status + "','" + userid + "',getdate(),'" + userid + "',getdate(),'::0');select Scope_Identity()";

                    SqlCommand cmd1 = new SqlCommand(query, dbConn);
                    var reader1 = cmd1.ExecuteReader();
                    System.Data.DataTable results1 = new System.Data.DataTable();
                    results1.Load(reader1);





                    st1 = "Asset Request ID is : " + st + "  Asset Request Saved Successfully";

                   


                }
                else
                {
                    string sql = "[dbo].[MBL_HRMS_INSERTUPDATEASSET]";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Function", Function);
                    cmd.Parameters.AddWithValue("@Branch", Branch);
                    cmd.Parameters.AddWithValue("@RequestID", RequestID);
                    cmd.Parameters.AddWithValue("@EmpID", EmpID);
                    cmd.Parameters.AddWithValue("@ReqDate", ReqDate);
                    cmd.Parameters.AddWithValue("@AssetCategory", AssetCategory);
                    cmd.Parameters.AddWithValue("@AssetSubCategory", AssetSubCategory);
                    // cmd.Parameters.AddWithValue("@RequiredBefore", Requiredbefore);
                    cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;



                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    var reader = cmd.ExecuteReader();
                    st = cmd.Parameters["@Result"].Value.ToString();
                    System.Data.DataTable results = new System.Data.DataTable();
                    results.Load(reader);
                    st1 = "Asset Request ID is : "+ st + "  Asset Request Updated Successfully";
                }


                if (Reaslese == "True")
                {



                    string query2 = "";
                    query2 = "select WF_CONFIG_ID from BO_WORKFLOW_CONFIGURATIONS where table_name like '%Asset%' and status='A' and Function_ID='" + Function + "'";

                    SqlCommand cmd2 = new SqlCommand(query2, dbConn);
                    var reader2 = cmd2.ExecuteReader();
                    System.Data.DataTable results2 = new System.Data.DataTable();
                    results2.Load(reader2);
                    for (int i = 0; i < results2.Rows.Count; i++)
                    {


                        DataRow row = results2.Rows[i];
                        int wkno = Convert.ToInt32(row[0]);

                        WF_CONFIG_ID = Convert.ToInt32(row[0]);




                        string query3 = "";
                        query3 = "exec usp_WF_ApprovalUsers 'ssg.DBO.hrms_asset_req','REQUEST_ID','','','','','" + st + "','','' ,'','' ,'1' ,'"+ userid+"' ,'STATUS','P' ,'" + WF_CONFIG_ID + "'";
                        SqlCommand cmd3 = new SqlCommand(query3, dbConn);
                        var reader3 = cmd3.ExecuteReader();

                        System.Data.DataTable results3 = new System.Data.DataTable();
                        results3.Load(reader3);
                        Logdata1 = "Successfully Saved";
                        for (int i2 = 0; i2 < results3.Rows.Count; i2++)
                        {
                            Logdata1 = DataTableToJSONWithStringBuilder(results3);
                            dbConn.Close();
                        }
                    }

                }




                return st1;
            }
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
                    if (strToDate.ToString() == "0" || strToDate.ToString() == "" || strToDate.ToString() == string.Empty || strToDate.ToString() == "null")
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







                }
                catch (Exception ex)
                {
                    Logdata1 = ex.ToString();
                }
            }

            return Logdata1;
        }





        public string DataTableToJSONWithStringBuilder1(DataTable table)
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
                            JSONString.Append( table.Columns[j].ColumnName.ToString() + ":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append( table.Columns[j].ColumnName.ToString() + ":" + "\"" + table.Rows[i][j].ToString() + "\"");
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
