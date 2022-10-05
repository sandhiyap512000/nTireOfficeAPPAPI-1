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
    public class SalesController : ControllerBase
    {

        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();

        [HttpPost]
        [Route("getBranchAccess")]
        public async Task<ActionResult<Sales>> getBranchAccess(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select BO_BRANCH_MASTER.BRANCH_ID,BRANCH_DESC from BO_BRANCH_ACCESS inner join BO_BRANCH_MASTER on BO_BRANCH_ACCESS.BRANCH_ID=BO_BRANCH_MASTER.BRANCH_ID where USER_ID= '"+data.userid+"'";

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
        [Route("get_Allappointments")]
        public async Task<ActionResult<Sales>> get_Allappointments(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "set dateformat dmy;select distinct BO_USER_MASTER.TUM_USER_TYPE,BO_USER_MASTER.current_location as SalespersonLocation,LMS_CURRENT_CAMPAIGN.TCC_CALL_ID as CALL_ID,LMS_CURRENT_CAMPAIGN.Meeting_Address as MeetingAddress,LMS_CURRENT_CAMPAIGN.TCC_LOCATION_TO_MEET as TCC_LOCATION_TO_MEET,LMS_CURRENT_CAMPAIGN.Actual_Meeting_Location,LMS_CURRENT_CAMPAIGN.Meeting_address,LMS_CURRENT_CAMPAIGN.START_TIME as START_TIME,LMS_CURRENT_CAMPAIGN.END_TIME as END_TIME,LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID as customer_lead_id,LMS_CURRENT_CAMPAIGN.TCC_RESPONSE,LMS_CURRENT_CAMPAIGN.FUNCTION_ID,LMS_CUSTOMER_MASTER.SKYPENAME,LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID,LMS_CURRENT_CAMPAIGN.TCC_CUST_ID as TCC_CUSTOMER_ID,LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID as TCC_CAMPAIGN_ID,  CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_LAST_CALL_DATE) as TCC_LAST_CALLED, case when cast(CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE)as datetime) = cast(CONVERT(VARCHAR(10),'01/01/1900') as datetime) then null else LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE end AS TCC_NEXT_CALL_DATE,  CAST(CONVERT(varchar(10), LMS_CURRENT_CAMPAIGN.TCC_CUST_ID) as varchar ) +'-'+isnull(LMS_CUSTOMER_MASTER.CUST_FNAME,'') + ' ' +   LMS_CUSTOMER_MASTER.CUST_LNAME +'-'+LMS_CUSTOMER_CONTACT_DETAILS.contact_name as CUSTOMER_NAME,  LMS_CUSTOMER_MASTER.CUST_FNAME as 'CUSTOMER_FNAME', LMS_CUSTOMER_MASTER.CUST_LNAME  as 'CUSTOMER_LNAME',concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) CustFullName,   concat(isnull(LMS_CUSTOMER_MASTER.ResSTDCODE,''), isnull(LMS_CUSTOMER_MASTER.RESPHONE,'')) RESPHONE , concat(isnull(LMS_CUSTOMER_MASTER.OffSTDCODE,'') ,  isnull(LMS_CUSTOMER_MASTER.OFFPHONE,'')) OFFPHONE,isnull(LMS_CUSTOMER_MASTER.MOBILE,'') AS MOBILE, ISNULL(LMS_CUSTOMER_MASTER.DNC_Continue,'U') as DNC_Continue, LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE AS NEXT_CALL_DATE, LMS_CAMPAIGN_MASTER.PRODUCTTYPE,  LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC+'-'+LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as ProdAndCamp, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as Campaign,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_CODE,convert(varchar(20),LMS_CUSTOMER_MASTER.CUST_CREATION_DATE) as CreatedOn,LMS_CUSTOMER_MASTER.CUST_CREATION_DATE as CreatedOn1,LMS_CURRENT_CAMPAIGN.TCC_LEAD_ID as LEAD_id,LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY as LeadBy,BO_BRANCH_MASTER.BRANCH_CODE as BRANCH_CODE,BO_BRANCH_MASTER.BRANCH_ID,BLM.LOCATION_DESC as LOCATION_DESC,BLM.LOCATION_ID,CASE WHEN  LMS_CAMPAIGN_MASTER.PRODUCT_MODE =N'M' then 'MultiProduct' else 'SingleProduct' end as PRODUCT_MODE,convert (varbinary(255),Rating.IMAGE)as IMAGE,Rating.text as Ratingtext,Rating.Val as RatingVal,CUST_REF+'-'+CUST_ACC_NO as CUSTACCNO,LMS_CURRENT_CAMPAIGN.TCC_PRIORITY as priority,LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING,LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE,LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE,LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID,LMS_CURRENT_CAMPAIGN.TCC_REMARKS as Remarks,LMS_CURRENT_CAMPAIGN.Action_Req as Action_req  , CASE WHEN ISNULL(LMS_CURRENT_CAMPAIGN.TCC_SHARED_ID,0)=0 THEN '' ELSE 'SHARED' END AS LEADSTATUS ,BO_USER_MASTER.TUM_USER_CODE as Current_Caller,BO_USER_MASTER.TUM_USER_CODE +'-'+BO_USER_MASTER.TUM_USER_NAME as Current_Caller1,ISNULL(LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID,'0') as CUST_CONT_PER, case when ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')=N'01/01/1900' then '' when  ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')<> '01/01/1900'  then Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate)  end as ClosedDate,LMS_CURRENT_CAMPAIGN.ExpectedClosedDate as ClosedDate1,LMS_CURRENT_CAMPAIGN.ExpctedAmount,Nature.TEXT as Nature,Priority.TEXT as PriorityText,LMS_CURRENT_CAMPAIGN.TCC_LEAD_SOURCE,LMS_PRODUCT_GROUP.ProductType_Id GroupId,LMS_CAMPAIGN_MASTER.PRODUCTTYPE,producttype.TEXT producttypeTEXT FROM LMS_CURRENT_CAMPAIGN INNER JOIN LMS_CUSTOMER_MASTER ON  LMS_CURRENT_CAMPAIGN.TCC_CUST_ID=LMS_CUSTOMER_MASTER.CUST_ID and  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=LMS_CUSTOMER_MASTER.FUNCTION_ID inner join LMS_CUSTOMER_CAMPAIGN on LMS_CUSTOMER_MASTER.CUST_ID=LMS_CUSTOMER_CAMPAIGN.CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID and  LMS_CUSTOMER_CAMPAIGN.FUNCTION_ID=LMS_CURRENT_CAMPAIGN.FUNCTION_ID  INNER JOIN LMS_CAMPAIGN_MASTER ON  LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID=LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID and LMS_CAMPAIGN_MASTER.FUNCTION_ID= LMS_CURRENT_CAMPAIGN.FUNCTION_ID join LMS_PRODUCT_GROUP on LMS_CAMPAIGN_MASTER.TCM_PRODUCT_GROUP=LMS_PRODUCT_GROUP.ProductType_Id left JOIN LMS_CUSTOMER_CONTACT_DETAILS ON LMS_CUSTOMER_CONTACT_DETAILS.Customer_id=LMS_CUSTOMER_MASTER.CUST_ID AND Convert(varchar,LMS_CUSTOMER_CONTACT_DETAILS.ROW_ID)=LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID left join BO_PARAMETER as producttype on producttype.VAL=LMS_CAMPAIGN_MASTER.PRODUCTTYPE  and producttype.TYPE=N'LMS_PRODUCTTYPE' inner join BO_BRANCH_MASTER on BO_BRANCH_MASTER.BRANCH_ID=LMS_CURRENT_CAMPAIGN.BRANCH_ID and LMS_CURRENT_CAMPAIGN.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID inner join BO_USER_MASTER on BO_USER_MASTER.TUM_USER_ID=LMS_CURRENT_CAMPAIGN .TCC_CALLER_ID  AND BO_USER_MASTER.TUM_USER_STATUS=N'A' left outer join BO_BRANCH_LOCATION_MASTER BLM on BLM.LOCATION_ID=LMS_CURRENT_CAMPAIGN.LOCATION_ID left outer  JOIN BO_PARAMETER  Rating ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Rating.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING=Rating.VAL AND Rating.TYPE=N'LMS_CALLRATING'  and Rating.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Stage ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Stage.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE=Stage.VAL AND Stage.TYPE=N'LMS_CALLSTAGE'  and Stage.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Nature ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Nature.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE=Nature.VAL AND Nature.TYPE=N'LMS_CALLNATURE'  and Nature.status=N'A' INNER JOIN BO_PARAMETER  Priority ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Priority.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_PRIORITY=Priority.VAL AND Priority.TYPE=N'LMS_CALLPRIORITY'  and Priority.status=N'A'  where 1=1  and LMS_CUSTOMER_MASTER.CUST_STATUS=N'A'  and LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE >= dateadd(day,datediff(day,1,GETDATE()),0) and LMS_CURRENT_CAMPAIGN.BRANCH_ID= '" + data.branchid + "'";

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
        [Route("salesdashboard_productwiseforcast")]
        public async Task<ActionResult<Sales>> salesdashboard_productwiseforcast(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "SET DATEFORMAT DMY;SELECT FUNCTION_DESC AS Company,branch_desc AS Branch,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC AS 'Product',isnull(COUNT(CUST_LEAD_ID),0) AS 'Total Leads', isnull(CLOSEDCALLS.CLOSEDLEADS,0) AS 'Closed Leads', CAST(CAST(ISNULL(CLOSEDCALLS.CLOSEDLEADS,0) AS DECIMAL(10,2))/CAST(ISNULL(COUNT(CUST_LEAD_ID),0) AS DECIMAL(10,2))*100 AS DECIMAL(5,1)) AS '% Closed Leads' FROM LMS_CAMPAIGN_MASTER WITH(NOLOCK) INNER JOIN BO_FUNCTION_MASTER WITH(NOLOCK) ON BO_FUNCTION_MASTER.FUNCTION_ID = LMS_CAMPAIGN_MASTER.FUNCTION_ID INNER JOIN LMS_CUSTOMER_CAMPAIGN WITH(NOLOCK) ON LMS_CUSTOMER_CAMPAIGN.CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID inner join BO_BRANCH_MASTER on LMS_CUSTOMER_CAMPAIGN.BRANCH_ID=BO_BRANCH_MASTER.BRANCH_ID and dbo.BO_FUNCTION_MASTER.FUNCTION_ID=BO_BRANCH_MASTER.FUNCTION_ID LEFT OUTER JOIN (SELECT TCC_CAMP_ID,COUNT(TCC_CUST_LEAD_ID) AS CLOSEDLEADS,  LMS_CLOSED_CALLS.FUNCTION_ID, LMS_CLOSED_CALLS.BRANCH_ID FROM LMS_CLOSED_CALLS WITH(NOLOCK) INNER JOIN LMS_CAMPAIGN_MASTER WITH(NOLOCK) ON LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID = LMS_CLOSED_CALLS.TCC_CAMP_ID GROUP BY TCC_CAMP_ID, LMS_CLOSED_CALLS.FUNCTION_ID, LMS_CLOSED_CALLS.BRANCH_ID) CLOSEDCALLS ON CLOSEDCALLS.TCC_CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID and CLOSEDCALLS.BRANCH_ID =  LMS_CUSTOMER_CAMPAIGN.BRANCH_ID and CLOSEDCALLS.FUNCTION_ID = LMS_CAMPAIGN_MASTER.FUNCTION_ID WHERE LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_TYPE='C' AND dbo.lms_campaign_master.FUNCTION_ID='1' and LMS_CUSTOMER_CAMPAIGN.BRANCH_ID='"+ data.branchid + "' group by bo_function_master.function_desc,lms_campaign_master.tcm_campaign_shortdesc,closedcalls.closedleads,branch_desc";

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
        [Route("get_AllappointmentsbyDate")]
        public async Task<ActionResult<Sales>> get_AllappointmentsbyDate(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "set dateformat dmy;select  BO_USER_MASTER.TUM_USER_TYPE,BO_USER_MASTER.current_location as SalespersonLocation,LMS_CURRENT_CAMPAIGN.TCC_CALL_ID as CALL_ID,LMS_CURRENT_CAMPAIGN.Meeting_Address as MeetingAddress,LMS_CURRENT_CAMPAIGN.TCC_LOCATION_TO_MEET as TCC_LOCATION_TO_MEET,LMS_CURRENT_CAMPAIGN.Actual_Meeting_Location,LMS_CURRENT_CAMPAIGN.Meeting_address,LMS_CURRENT_CAMPAIGN.START_TIME as START_TIME,LMS_CURRENT_CAMPAIGN.END_TIME as END_TIME,LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID as customer_lead_id,LMS_CURRENT_CAMPAIGN.TCC_RESPONSE,LMS_CURRENT_CAMPAIGN.FUNCTION_ID,LMS_CUSTOMER_MASTER.SKYPENAME,LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID,LMS_CURRENT_CAMPAIGN.TCC_CUST_ID as TCC_CUSTOMER_ID,LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID as TCC_CAMPAIGN_ID,  CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_LAST_CALL_DATE) as TCC_LAST_CALLED, case when cast(CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE)as datetime) = cast(CONVERT(VARCHAR(10),'01/01/1900') as datetime) then null else LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE end AS TCC_NEXT_CALL_DATE,  CAST(CONVERT(varchar(10), LMS_CURRENT_CAMPAIGN.TCC_CUST_ID) as varchar ) +'-'+isnull(LMS_CUSTOMER_MASTER.CUST_FNAME,'') + ' ' +   LMS_CUSTOMER_MASTER.CUST_LNAME +'-'+LMS_CUSTOMER_CONTACT_DETAILS.contact_name as CUSTOMER_NAME,  LMS_CUSTOMER_MASTER.CUST_FNAME as 'CUSTOMER_FNAME', LMS_CUSTOMER_MASTER.CUST_LNAME  as 'CUSTOMER_LNAME',concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) CustFullName,   concat(isnull(LMS_CUSTOMER_MASTER.ResSTDCODE,''), isnull(LMS_CUSTOMER_MASTER.RESPHONE,'')) RESPHONE , concat(isnull(LMS_CUSTOMER_MASTER.OffSTDCODE,'') ,  isnull(LMS_CUSTOMER_MASTER.OFFPHONE,'')) OFFPHONE,isnull(LMS_CUSTOMER_MASTER.MOBILE,'') AS MOBILE, ISNULL(LMS_CUSTOMER_MASTER.DNC_Continue,'U') as DNC_Continue, LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE AS NEXT_CALL_DATE, LMS_CAMPAIGN_MASTER.PRODUCTTYPE,  LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC+'-'+LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as ProdAndCamp, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as Campaign,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_CODE,convert(varchar(20),LMS_CUSTOMER_MASTER.CUST_CREATION_DATE) as CreatedOn,LMS_CUSTOMER_MASTER.CUST_CREATION_DATE as CreatedOn1,LMS_CURRENT_CAMPAIGN.TCC_LEAD_ID as LEAD_id,LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY as LeadBy,BO_BRANCH_MASTER.BRANCH_CODE as BRANCH_CODE,BO_BRANCH_MASTER.BRANCH_ID,BLM.LOCATION_DESC as LOCATION_DESC,BLM.LOCATION_ID,CASE WHEN  LMS_CAMPAIGN_MASTER.PRODUCT_MODE =N'M' then 'MultiProduct' else 'SingleProduct' end as PRODUCT_MODE,convert (varbinary(255),Rating.IMAGE)as IMAGE,Rating.text as Ratingtext,Rating.Val as RatingVal,CUST_REF+'-'+CUST_ACC_NO as CUSTACCNO,LMS_CURRENT_CAMPAIGN.TCC_PRIORITY as priority,LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING,LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE,LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE,LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID,LMS_CURRENT_CAMPAIGN.TCC_REMARKS as Remarks,LMS_CURRENT_CAMPAIGN.Action_Req as Action_req  , CASE WHEN ISNULL(LMS_CURRENT_CAMPAIGN.TCC_SHARED_ID,0)=0 THEN '' ELSE 'SHARED' END AS LEADSTATUS ,BO_USER_MASTER.TUM_USER_CODE as Current_Caller,BO_USER_MASTER.TUM_USER_CODE +'-'+BO_USER_MASTER.TUM_USER_NAME as Current_Caller1,ISNULL(LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID,'0') as CUST_CONT_PER, case when ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')=N'01/01/1900' then '' when  ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')<> '01/01/1900'  then Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate)  end as ClosedDate,LMS_CURRENT_CAMPAIGN.ExpectedClosedDate as ClosedDate1,LMS_CURRENT_CAMPAIGN.ExpctedAmount,Nature.TEXT as Nature,Priority.TEXT as PriorityText,LMS_CURRENT_CAMPAIGN.TCC_LEAD_SOURCE,LMS_PRODUCT_GROUP.ProductType_Id GroupId,LMS_CAMPAIGN_MASTER.PRODUCTTYPE,producttype.TEXT producttypeTEXT FROM LMS_CURRENT_CAMPAIGN INNER JOIN LMS_CUSTOMER_MASTER ON  LMS_CURRENT_CAMPAIGN.TCC_CUST_ID=LMS_CUSTOMER_MASTER.CUST_ID and  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=LMS_CUSTOMER_MASTER.FUNCTION_ID inner join LMS_CUSTOMER_CAMPAIGN on LMS_CUSTOMER_MASTER.CUST_ID=LMS_CUSTOMER_CAMPAIGN.CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID and  LMS_CUSTOMER_CAMPAIGN.FUNCTION_ID=LMS_CURRENT_CAMPAIGN.FUNCTION_ID  INNER JOIN LMS_CAMPAIGN_MASTER ON  LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID=LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID and LMS_CAMPAIGN_MASTER.FUNCTION_ID= LMS_CURRENT_CAMPAIGN.FUNCTION_ID join LMS_PRODUCT_GROUP on LMS_CAMPAIGN_MASTER.TCM_PRODUCT_GROUP=LMS_PRODUCT_GROUP.ProductType_Id left JOIN LMS_CUSTOMER_CONTACT_DETAILS ON LMS_CUSTOMER_CONTACT_DETAILS.Customer_id=LMS_CUSTOMER_MASTER.CUST_ID AND Convert(varchar,LMS_CUSTOMER_CONTACT_DETAILS.ROW_ID)=LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID left join BO_PARAMETER as producttype on producttype.VAL=LMS_CAMPAIGN_MASTER.PRODUCTTYPE  and producttype.TYPE=N'LMS_PRODUCTTYPE' inner join BO_BRANCH_MASTER on BO_BRANCH_MASTER.BRANCH_ID=LMS_CURRENT_CAMPAIGN.BRANCH_ID and LMS_CURRENT_CAMPAIGN.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID inner join BO_USER_MASTER on BO_USER_MASTER.TUM_USER_ID=LMS_CURRENT_CAMPAIGN .TCC_CALLER_ID  AND BO_USER_MASTER.TUM_USER_STATUS=N'A' left outer join BO_BRANCH_LOCATION_MASTER BLM on BLM.LOCATION_ID=LMS_CURRENT_CAMPAIGN.LOCATION_ID left outer  JOIN BO_PARAMETER  Rating ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Rating.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING=Rating.VAL AND Rating.TYPE=N'LMS_CALLRATING'  and Rating.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Stage ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Stage.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE=Stage.VAL AND Stage.TYPE=N'LMS_CALLSTAGE'  and Stage.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Nature ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Nature.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE=Nature.VAL AND Nature.TYPE=N'LMS_CALLNATURE'  and Nature.status=N'A' INNER JOIN BO_PARAMETER  Priority ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Priority.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_PRIORITY=Priority.VAL AND Priority.TYPE=N'LMS_CALLPRIORITY'  and Priority.status=N'A'  where 1=1  and LMS_CUSTOMER_MASTER.CUST_STATUS=N'A' and LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE BETWEEN '"+data.fdate+"' and '"+data.tdate+"' and LMS_CURRENT_CAMPAIGN.BRANCH_ID='"+data.branchid+"' ORDER BY cast(convert(varchar(10),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE,103) as datetime) ASC";
                

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
        [Route("allpendleadsdatabybranch")]
        public async Task<ActionResult<Sales>> allpendleadsdatabybranch(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select distinct LMS_CURRENT_CAMPAIGN.TCC_CALL_ID as CALL_ID,LMS_CURRENT_CAMPAIGN.TCC_LOCATION_TO_MEET as TCC_LOCATION_TO_MEET,LMS_CURRENT_CAMPAIGN.Meeting_address,LMS_CURRENT_CAMPAIGN.START_TIME as START_TIME,LMS_CURRENT_CAMPAIGN.END_TIME as END_TIME,LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID as customer_lead_id,LMS_CURRENT_CAMPAIGN.TCC_RESPONSE,LMS_CURRENT_CAMPAIGN.FUNCTION_ID,LMS_CUSTOMER_MASTER.SKYPENAME,LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID,LMS_CURRENT_CAMPAIGN.TCC_CUST_ID as TCC_CUSTOMER_ID,LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID as TCC_CAMPAIGN_ID,  CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_LAST_CALL_DATE) as TCC_LAST_CALLED, case when cast(CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE)as datetime) = cast(CONVERT(VARCHAR(10),'01/01/1900') as datetime) then null else LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE end AS TCC_NEXT_CALL_DATE,  CAST(CONVERT(varchar(10), LMS_CURRENT_CAMPAIGN.TCC_CUST_ID) as varchar ) +'-'+isnull(LMS_CUSTOMER_MASTER.CUST_FNAME,'') + ' ' +   LMS_CUSTOMER_MASTER.CUST_LNAME +'-'+LMS_CUSTOMER_CONTACT_DETAILS.contact_name as CUSTOMER_NAME,  LMS_CUSTOMER_MASTER.CUST_FNAME as 'CUSTOMER_FNAME', LMS_CUSTOMER_MASTER.CUST_LNAME  as 'CUSTOMER_LNAME',concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) CustFullName,   concat(isnull(LMS_CUSTOMER_MASTER.ResSTDCODE,''), isnull(LMS_CUSTOMER_MASTER.RESPHONE,'')) RESPHONE , concat(isnull(LMS_CUSTOMER_MASTER.OffSTDCODE,'') ,  isnull(LMS_CUSTOMER_MASTER.OFFPHONE,'')) OFFPHONE,isnull(LMS_CUSTOMER_MASTER.MOBILE,'') AS MOBILE, ISNULL(LMS_CUSTOMER_MASTER.DNC_Continue,'U') as DNC_Continue, LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE AS NEXT_CALL_DATE, LMS_CAMPAIGN_MASTER.PRODUCTTYPE,  LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC+'-'+LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as ProdAndCamp, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as Campaign,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_CODE,convert(varchar(20),LMS_CUSTOMER_MASTER.CUST_CREATION_DATE) as CreatedOn,LMS_CUSTOMER_MASTER.CUST_CREATION_DATE as CreatedOn1,LMS_CURRENT_CAMPAIGN.TCC_LEAD_ID as LEAD_id,LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY as LeadBy,BO_BRANCH_MASTER.BRANCH_CODE+'-'+BO_BRANCH_MASTER.BRANCH_DESC as BRANCH_DESC,BO_BRANCH_MASTER.BRANCH_ID,BLM.LOCATION_DESC as LOCATION_DESC,BLM.LOCATION_ID,CASE WHEN  LMS_CAMPAIGN_MASTER.PRODUCT_MODE =N'M' then 'MultiProduct' else 'SingleProduct' end as PRODUCT_MODE,convert (varbinary(255),Rating.IMAGE)as IMAGE,Rating.text as Ratingtext,Rating.Val as RatingVal,CUST_REF+'-'+CUST_ACC_NO as CUSTACCNO,LMS_CURRENT_CAMPAIGN.TCC_PRIORITY as priority,LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING,LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE,LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE,LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID,LMS_CURRENT_CAMPAIGN.TCC_REMARKS as Remarks,LMS_CURRENT_CAMPAIGN.Action_Req as Action_req  , CASE WHEN ISNULL(LMS_CURRENT_CAMPAIGN.TCC_SHARED_ID,0)=0 THEN '' ELSE 'SHARED' END AS LEADSTATUS ,BO_USER_MASTER.TUM_USER_CODE +'-'+BO_USER_MASTER.TUM_USER_NAME as Current_Caller,ISNULL(LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID,'0') as CUST_CONT_PER, case when ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')=N'01/01/1900' then '' when  ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')<> '01/01/1900'  then Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate)  end as ClosedDate,LMS_CURRENT_CAMPAIGN.ExpectedClosedDate as ClosedDate1,LMS_CURRENT_CAMPAIGN.ExpctedAmount,Stage.TEXT as Stage,Nature.TEXT as Nature,Priority.TEXT as PriorityText,LMS_CURRENT_CAMPAIGN.TCC_LEAD_SOURCE,LMS_PRODUCT_GROUP.ProductType_Id GroupId,LMS_CAMPAIGN_MASTER.PRODUCTTYPE,producttype.TEXT producttypeTEXT FROM LMS_CURRENT_CAMPAIGN INNER JOIN LMS_CUSTOMER_MASTER ON  LMS_CURRENT_CAMPAIGN.TCC_CUST_ID=LMS_CUSTOMER_MASTER.CUST_ID and  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=LMS_CUSTOMER_MASTER.FUNCTION_ID inner join LMS_CUSTOMER_CAMPAIGN on LMS_CUSTOMER_MASTER.CUST_ID=LMS_CUSTOMER_CAMPAIGN.CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID and  LMS_CUSTOMER_CAMPAIGN.FUNCTION_ID=LMS_CURRENT_CAMPAIGN.FUNCTION_ID  INNER JOIN LMS_CAMPAIGN_MASTER ON  LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID=LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID and LMS_CAMPAIGN_MASTER.FUNCTION_ID= LMS_CURRENT_CAMPAIGN.FUNCTION_ID join LMS_PRODUCT_GROUP on LMS_CAMPAIGN_MASTER.TCM_PRODUCT_GROUP=LMS_PRODUCT_GROUP.ProductType_Id left JOIN LMS_CUSTOMER_CONTACT_DETAILS ON LMS_CUSTOMER_CONTACT_DETAILS.Customer_id=LMS_CUSTOMER_MASTER.CUST_ID AND Convert(varchar,LMS_CUSTOMER_CONTACT_DETAILS.ROW_ID)=LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID inner join BO_PARAMETER as producttype on producttype.VAL=LMS_CAMPAIGN_MASTER.PRODUCTTYPE  and producttype.TYPE=N'LMS_PRODUCTTYPE'  left outer join BO_BRANCH_LOCATION_MASTER BLM on BLM.LOCATION_ID=LMS_CURRENT_CAMPAIGN.LOCATION_ID left outer  JOIN BO_PARAMETER  Rating ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Rating.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING=Rating.VAL AND Rating.TYPE=N'LMS_CALLRATING'  and Rating.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Stage ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Stage.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE=Stage.VAL AND Stage.TYPE=N'LMS_CALLSTAGE'  and Stage.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Nature ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Nature.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE=Nature.VAL AND Nature.TYPE=N'LMS_CALLNATURE'  and Nature.status=N'A' INNER JOIN BO_PARAMETER  Priority ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Priority.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_PRIORITY=Priority.VAL AND Priority.TYPE=N'LMS_CALLPRIORITY'  and Priority.status=N'A'   join BO_BRANCH_MASTER on BO_BRANCH_MASTER.BRANCH_ID=LMS_CURRENT_CAMPAIGN.BRANCH_ID and LMS_CURRENT_CAMPAIGN.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID inner join BO_USER_MASTER on BO_USER_MASTER.TUM_USER_ID=LMS_CURRENT_CAMPAIGN .TCC_CALLER_ID and BO_USER_MASTER.FUNCTION_ID =LMS_CURRENT_CAMPAIGN.FUNCTION_ID AND BO_USER_MASTER.TUM_USER_STATUS=N'A'  where 1=1  and LMS_CUSTOMER_MASTER.CUST_STATUS=N'A'  and LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID='"+ data.userid + "' and LMS_CURRENT_CAMPAIGN.BRANCH_ID='"+data.branchid+"'";


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
        [Route("getallappointments_post")]
        public async Task<ActionResult<Sales>> getallappointments_post(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "set dateformat dmy; select distinct  BO_USER_MASTER.current_location as SalespersonLocation,LMS_CURRENT_CAMPAIGN.TCC_CALL_ID as CALL_ID,LMS_CURRENT_CAMPAIGN.TCC_LOCATION_TO_MEET as TCC_LOCATION_TO_MEET,LMS_CURRENT_CAMPAIGN.Actual_Meeting_Location,LMS_CURRENT_CAMPAIGN.START_TIME as START_TIME,LMS_CURRENT_CAMPAIGN.END_TIME as END_TIME,LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID as customer_lead_id,LMS_CURRENT_CAMPAIGN.TCC_RESPONSE,LMS_CURRENT_CAMPAIGN.FUNCTION_ID,LMS_CUSTOMER_MASTER.SKYPENAME,LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID,LMS_CURRENT_CAMPAIGN.TCC_CUST_ID as TCC_CUSTOMER_ID,LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID as TCC_CAMPAIGN_ID,  CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_LAST_CALL_DATE) as TCC_LAST_CALLED, case when cast(CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE)as datetime) = cast(CONVERT(VARCHAR(10),'01/01/1900') as datetime) then null else LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE end AS TCC_NEXT_CALL_DATE,  CAST(CONVERT(varchar(10), LMS_CURRENT_CAMPAIGN.TCC_CUST_ID) as varchar ) +'-'+isnull(LMS_CUSTOMER_MASTER.CUST_FNAME,'') + ' ' +   LMS_CUSTOMER_MASTER.CUST_LNAME +'-'+LMS_CUSTOMER_CONTACT_DETAILS.contact_name as CUSTOMER_NAME,  LMS_CUSTOMER_MASTER.CUST_FNAME as 'CUSTOMER_FNAME', LMS_CUSTOMER_MASTER.CUST_LNAME  as 'CUSTOMER_LNAME',concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) CustFullName,   concat(isnull(LMS_CUSTOMER_MASTER.ResSTDCODE,''), isnull(LMS_CUSTOMER_MASTER.RESPHONE,'')) RESPHONE , concat(isnull(LMS_CUSTOMER_MASTER.OffSTDCODE,'') ,  isnull(LMS_CUSTOMER_MASTER.OFFPHONE,'')) OFFPHONE,isnull(LMS_CUSTOMER_MASTER.MOBILE,'') AS MOBILE, ISNULL(LMS_CUSTOMER_MASTER.DNC_Continue,'U') as DNC_Continue, LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE AS NEXT_CALL_DATE, LMS_CAMPAIGN_MASTER.PRODUCTTYPE,  LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC+'-'+LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as ProdAndCamp, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as Campaign,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_CODE,convert(varchar(20),LMS_CUSTOMER_MASTER.CUST_CREATION_DATE) as CreatedOn,LMS_CUSTOMER_MASTER.CUST_CREATION_DATE as CreatedOn1,LMS_CURRENT_CAMPAIGN.TCC_LEAD_ID as LEAD_id,LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY as LeadBy,BO_BRANCH_MASTER.BRANCH_CODE+'-'+BO_BRANCH_MASTER.BRANCH_DESC as BRANCH_DESC,BO_BRANCH_MASTER.BRANCH_ID,BLM.LOCATION_DESC as LOCATION_DESC,BLM.LOCATION_ID,CASE WHEN  LMS_CAMPAIGN_MASTER.PRODUCT_MODE =N'M' then 'MultiProduct' else 'SingleProduct' end as PRODUCT_MODE,convert (varbinary(255),Rating.IMAGE)as IMAGE,Rating.text as Ratingtext,Rating.Val as RatingVal,CUST_REF+'-'+CUST_ACC_NO as CUSTACCNO,LMS_CURRENT_CAMPAIGN.TCC_PRIORITY as priority,LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING,LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE,LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE,LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID,LMS_CURRENT_CAMPAIGN.TCC_REMARKS as Remarks,LMS_CURRENT_CAMPAIGN.Action_Req as Action_req  , CASE WHEN ISNULL(LMS_CURRENT_CAMPAIGN.TCC_SHARED_ID,0)=0 THEN '' ELSE 'SHARED' END AS LEADSTATUS ,BO_USER_MASTER.TUM_USER_CODE +'-'+BO_USER_MASTER.TUM_USER_NAME as Current_Caller,ISNULL(LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID,'0') as CUST_CONT_PER, case when ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')=N'01/01/1900' then '' when  ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')<> '01/01/1900'  then Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate)  end as ClosedDate,LMS_CURRENT_CAMPAIGN.ExpectedClosedDate as ClosedDate1,LMS_CURRENT_CAMPAIGN.ExpctedAmount,Nature.TEXT as Nature,Priority.TEXT as PriorityText,LMS_CURRENT_CAMPAIGN.TCC_LEAD_SOURCE,LMS_PRODUCT_GROUP.ProductType_Id GroupId,LMS_CAMPAIGN_MASTER.PRODUCTTYPE,producttype.TEXT producttypeTEXT FROM LMS_CURRENT_CAMPAIGN INNER JOIN LMS_CUSTOMER_MASTER ON  LMS_CURRENT_CAMPAIGN.TCC_CUST_ID=LMS_CUSTOMER_MASTER.CUST_ID and  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=LMS_CUSTOMER_MASTER.FUNCTION_ID inner join LMS_CUSTOMER_CAMPAIGN on LMS_CUSTOMER_MASTER.CUST_ID=LMS_CUSTOMER_CAMPAIGN.CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID and  LMS_CUSTOMER_CAMPAIGN.FUNCTION_ID=LMS_CURRENT_CAMPAIGN.FUNCTION_ID  INNER JOIN LMS_CAMPAIGN_MASTER ON  LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID=LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID and LMS_CAMPAIGN_MASTER.FUNCTION_ID= LMS_CURRENT_CAMPAIGN.FUNCTION_ID join LMS_PRODUCT_GROUP on LMS_CAMPAIGN_MASTER.TCM_PRODUCT_GROUP=LMS_PRODUCT_GROUP.ProductType_Id left JOIN LMS_CUSTOMER_CONTACT_DETAILS ON LMS_CUSTOMER_CONTACT_DETAILS.Customer_id=LMS_CUSTOMER_MASTER.CUST_ID AND Convert(varchar,LMS_CUSTOMER_CONTACT_DETAILS.ROW_ID)=LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID inner join BO_PARAMETER as producttype on producttype.VAL=LMS_CAMPAIGN_MASTER.PRODUCTTYPE  and producttype.TYPE=N'LMS_PRODUCTTYPE'  left outer join BO_BRANCH_LOCATION_MASTER BLM on BLM.LOCATION_ID=LMS_CURRENT_CAMPAIGN.LOCATION_ID left outer  JOIN BO_PARAMETER  Rating ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Rating.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING=Rating.VAL AND Rating.TYPE=N'LMS_CALLRATING'  and Rating.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Stage ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Stage.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE=Stage.VAL AND Stage.TYPE=N'LMS_CALLSTAGE'  and Stage.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Nature ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Nature.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE=Nature.VAL AND Nature.TYPE=N'LMS_CALLNATURE'  and Nature.status=N'A' INNER JOIN BO_PARAMETER  Priority ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Priority.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_PRIORITY=Priority.VAL AND Priority.TYPE=N'LMS_CALLPRIORITY'  and Priority.status=N'A'   join BO_BRANCH_MASTER on BO_BRANCH_MASTER.BRANCH_ID=LMS_CURRENT_CAMPAIGN.BRANCH_ID and LMS_CURRENT_CAMPAIGN.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID inner join BO_USER_MASTER on BO_USER_MASTER.TUM_USER_ID=LMS_CURRENT_CAMPAIGN .TCC_CALLER_ID and BO_USER_MASTER.FUNCTION_ID =LMS_CURRENT_CAMPAIGN.FUNCTION_ID AND BO_USER_MASTER.TUM_USER_STATUS=N'A'  where 1=1  and LMS_CUSTOMER_MASTER.CUST_STATUS=N'A' and LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID = '" + data.userid + "'  and ((LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE between '" + data.fdate + "' and '" + data.tdate + "') or (CONVERT(NVARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE,103) = '" + data.fdate + "'))";
                


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
        [Route("getHeatMapRecordbranchwise")]
        public async Task<ActionResult<Sales>> getHeatMapRecordbranchwise(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "SELECT  LMS_CUSTOMER_CAMPAIGN.BRANCH_ID,LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC, Count(LMS_CUSTOMER_CAMPAIGN.CAMP_ID) as ToltalCustomers,Count(LMS_CUSTOMER_CAMPAIGN.CALLER_ID) as ToltalCalls,count(CASE WHEN year(LMS_CUSTOMER_CAMPAIGN.CREATED_ON)=year(GETDATE()) THEN 1 END) as YTD_Cust,count(CASE WHEN month(LMS_CUSTOMER_CAMPAIGN.CREATED_ON)=month(GETDATE()) THEN 1 END) as MTD_Cust FROM LMS_CUSTOMER_CAMPAIGN INNER JOIN LMS_CAMPAIGN_MASTER on LMS_CUSTOMER_CAMPAIGN.CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID WHERE LMS_CUSTOMER_CAMPAIGN.BRANCH_ID= '"+data.branchid+"'";
                if (data.status=="4")
                {
                    query = query + "AND LMS_CUSTOMER_CAMPAIGN.CUR_STATUS='4' GROUP BY LMS_CUSTOMER_CAMPAIGN.BRANCH_ID,LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC";

                }
                else
                {
                    query = query + "AND LMS_CUSTOMER_CAMPAIGN.CUR_STATUS<>'4' GROUP BY LMS_CUSTOMER_CAMPAIGN.BRANCH_ID,LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC";

                }



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
        [Route("getHeatMapRecord")]
        public async Task<ActionResult<Sales>> getHeatMapRecord(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "SELECT LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC, Count(LMS_CUSTOMER_CAMPAIGN.CAMP_ID) as ToltalCustomers,Count(LMS_CUSTOMER_CAMPAIGN.CALLER_ID) as ToltalCalls,count(CASE WHEN year(LMS_CUSTOMER_CAMPAIGN.CREATED_ON)=year(GETDATE()) THEN 1 END) as YTD_Cust,count(CASE WHEN month(LMS_CUSTOMER_CAMPAIGN.CREATED_ON)=month(GETDATE()) THEN 1 END) as MTD_Cust FROM LMS_CUSTOMER_CAMPAIGN INNER JOIN LMS_CAMPAIGN_MASTER on LMS_CUSTOMER_CAMPAIGN.CAMP_ID = LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID where CALLER_ID= '"+data.userid+"'";
               
                if (data.status == "4")
                {
                    query = query + " AND LMS_CUSTOMER_CAMPAIGN.CUR_STATUS=" + data.status + " GROUP BY LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC";
                    

                }
                else
                {
                    query = query + "AND LMS_CUSTOMER_CAMPAIGN.CUR_STATUS<>'4' GROUP BY LMS_CUSTOMER_CAMPAIGN.CAMP_ID,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_CODE, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC";


                }



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
        [Route("getbranchusers")]
        public async Task<ActionResult<Sales>> getbranchusers(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select tum_user_id,TUM_USER_CODE,TUM_USER_NAME from BO_USER_MASTER where  TUM_USER_STATUS='A' and TUM_BRANCH_ID= '"+data.branchid+"'";
                



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
        [Route("branch_list_get")]
        public async Task<ActionResult<Sales>> branch_list_get(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select * from BO_BRANCH_MASTER";




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
        [Route("getallsaleslocation")]
        public async Task<ActionResult<Sales>> getallsaleslocation(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select  * from BO_USER_MASTER where TUM_USER_TYPE = '"+data.usertype + "' and TUM_BRANCH_ID='"+data.branchid+ "' and TUM_USER_STATUS = 'A' and CAST(LOCATION_UPDATED_ON AS DATE)  = CAST(GETDATE() AS DATE) AND (DATEDIFF(millisecond, LOCATION_UPDATED_ON,getdate()) <= 9999999) AND TUM_USER_CODE!='" + data.usercode+"'";
             




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
        [Route("user_type_get")]
        public async Task<ActionResult<Sales>> user_type_get(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select TYPE_ID,DESCRIPTION from BO_USER_TYPE_MASTER where FUNCTION_ID = 1 and Status = 'A'";





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
        [Route("locationbranch")]
        public async Task<ActionResult<Sales>> locationbranch(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "SELECT DISTINCT BRANCH_ID,BRANCH_DESC,BRANCH_LATLONG from BO_BRANCH_MASTER WHERE FUNCTION_ID='"+data.functionid + "'";





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
        [Route("pendleadsdata")]
        public async Task<ActionResult<Sales>> pendleadsdata(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {

                dbConn.Open();
                string query = "";
                query = "select distinct LMS_CURRENT_CAMPAIGN.TCC_CALL_ID as CALL_ID,LMS_CURRENT_CAMPAIGN.TCC_LOCATION_TO_MEET as TCC_LOCATION_TO_MEET,LMS_CURRENT_CAMPAIGN.START_TIME as START_TIME,LMS_CURRENT_CAMPAIGN.END_TIME as END_TIME,LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID as customer_lead_id,LMS_CURRENT_CAMPAIGN.TCC_RESPONSE,LMS_CURRENT_CAMPAIGN.FUNCTION_ID,LMS_CUSTOMER_MASTER.SKYPENAME,LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID,LMS_CURRENT_CAMPAIGN.TCC_CUST_ID as TCC_CUSTOMER_ID,LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID as TCC_CAMPAIGN_ID,  CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_LAST_CALL_DATE) as TCC_LAST_CALLED, case when cast(CONVERT(VARCHAR(20),LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE)as datetime) = cast(CONVERT(VARCHAR(10),'01/01/1900') as datetime) then null else LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE end AS TCC_NEXT_CALL_DATE,  CAST(CONVERT(varchar(10), LMS_CURRENT_CAMPAIGN.TCC_CUST_ID) as varchar ) +'-'+isnull(LMS_CUSTOMER_MASTER.CUST_FNAME,'') + ' ' +   LMS_CUSTOMER_MASTER.CUST_LNAME +'-'+LMS_CUSTOMER_CONTACT_DETAILS.contact_name as CUSTOMER_NAME,  LMS_CUSTOMER_MASTER.CUST_FNAME as 'CUSTOMER_FNAME', LMS_CUSTOMER_MASTER.CUST_LNAME  as 'CUSTOMER_LNAME',concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) CustFullName,   concat(isnull(LMS_CUSTOMER_MASTER.ResSTDCODE,''), isnull(LMS_CUSTOMER_MASTER.RESPHONE,'')) RESPHONE , concat(isnull(LMS_CUSTOMER_MASTER.OffSTDCODE,'') ,  isnull(LMS_CUSTOMER_MASTER.OFFPHONE,'')) OFFPHONE,isnull(LMS_CUSTOMER_MASTER.EMAIL_ID,'') AS EMAIL_ID,isnull(LMS_CUSTOMER_MASTER.MOBILE,'') AS MOBILE, ISNULL(LMS_CUSTOMER_MASTER.DNC_Continue,'U') as DNC_Continue, LMS_CURRENT_CAMPAIGN.TCC_NEXT_CALL_DATE AS NEXT_CALL_DATE, LMS_CAMPAIGN_MASTER.PRODUCTTYPE,  LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC,LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_DESC+'-'+LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as ProdAndCamp, LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC as Campaign,LMS_CAMPAIGN_MASTER.TCM_PRODUCT_CODE,convert(varchar(20),LMS_CUSTOMER_MASTER.CUST_CREATION_DATE) as CreatedOn,LMS_CUSTOMER_MASTER.CUST_CREATION_DATE as CreatedOn1,LMS_CURRENT_CAMPAIGN.TCC_LEAD_ID as LEAD_id,LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY as LeadBy,BO_BRANCH_MASTER.BRANCH_CODE+'-'+BO_BRANCH_MASTER.BRANCH_DESC as BRANCH_DESC,BO_BRANCH_MASTER.BRANCH_ID,BLM.LOCATION_DESC as LOCATION_DESC,BLM.LOCATION_ID,CASE WHEN  LMS_CAMPAIGN_MASTER.PRODUCT_MODE =N'M' then 'MultiProduct' else 'SingleProduct' end as PRODUCT_MODE,convert (varbinary(255),Rating.IMAGE)as IMAGE,Rating.text as Ratingtext,Rating.Val as RatingVal,CUST_REF+'-'+CUST_ACC_NO as CUSTACCNO,LMS_CURRENT_CAMPAIGN.TCC_PRIORITY as priority,LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING,LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE,LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE,LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID,LMS_CURRENT_CAMPAIGN.TCC_REMARKS as Remarks,LMS_CURRENT_CAMPAIGN.Action_Req as Action_req  , CASE WHEN ISNULL(LMS_CURRENT_CAMPAIGN.TCC_SHARED_ID,0)=0 THEN '' ELSE 'SHARED' END AS LEADSTATUS ,BO_USER_MASTER.TUM_USER_CODE +'-'+BO_USER_MASTER.TUM_USER_NAME as Current_Caller,ISNULL(LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID,'0') as CUST_CONT_PER, case when ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')=N'01/01/1900' then '' when  ISNULL(Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate),'')<> '01/01/1900'  then Convert(varchar(20),LMS_CURRENT_CAMPAIGN.ExpectedClosedDate)  end as ClosedDate,LMS_CURRENT_CAMPAIGN.ExpectedClosedDate as ClosedDate1,LMS_CURRENT_CAMPAIGN.Currency,LMS_CURRENT_CAMPAIGN.ExpctedAmount,Stage.TEXT as Stage,Nature.TEXT as Nature,Priority.TEXT as PriorityText,LMS_CURRENT_CAMPAIGN.TCC_LEAD_SOURCE,LMS_PRODUCT_GROUP.ProductType_Id GroupId,LMS_CAMPAIGN_MASTER.PRODUCTTYPE,producttype.TEXT producttypeTEXT FROM LMS_CURRENT_CAMPAIGN INNER JOIN LMS_CUSTOMER_MASTER ON  LMS_CURRENT_CAMPAIGN.TCC_CUST_ID=LMS_CUSTOMER_MASTER.CUST_ID and  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=LMS_CUSTOMER_MASTER.FUNCTION_ID inner join LMS_CUSTOMER_CAMPAIGN on LMS_CUSTOMER_MASTER.CUST_ID=LMS_CUSTOMER_CAMPAIGN.CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_ID and LMS_CUSTOMER_CAMPAIGN.CUST_LEAD_ID=LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID and  LMS_CUSTOMER_CAMPAIGN.FUNCTION_ID=LMS_CURRENT_CAMPAIGN.FUNCTION_ID  INNER JOIN LMS_CAMPAIGN_MASTER ON  LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_ID=LMS_CURRENT_CAMPAIGN.TCC_CAMP_ID and LMS_CAMPAIGN_MASTER.FUNCTION_ID= LMS_CURRENT_CAMPAIGN.FUNCTION_ID join LMS_PRODUCT_GROUP on LMS_CAMPAIGN_MASTER.TCM_PRODUCT_GROUP=LMS_PRODUCT_GROUP.ProductType_Id left JOIN LMS_CUSTOMER_CONTACT_DETAILS ON LMS_CUSTOMER_CONTACT_DETAILS.Customer_id=LMS_CUSTOMER_MASTER.CUST_ID AND Convert(varchar,LMS_CUSTOMER_CONTACT_DETAILS.ROW_ID)=LMS_CUSTOMER_CAMPAIGN.CORP_CONT_PER_ID inner join BO_PARAMETER as producttype on producttype.VAL=LMS_CAMPAIGN_MASTER.PRODUCTTYPE  and producttype.TYPE=N'LMS_PRODUCTTYPE'  and producttype.FUNCTION_ID=LMS_CAMPAIGN_MASTER.FUNCTION_ID  left outer join BO_BRANCH_LOCATION_MASTER BLM on BLM.LOCATION_ID=LMS_CURRENT_CAMPAIGN.LOCATION_ID left outer  JOIN BO_PARAMETER  Rating ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Rating.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_RATING=Rating.VAL AND Rating.TYPE=N'LMS_CALLRATING'  and Rating.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Stage ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Stage.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_STAGE=Stage.VAL AND Stage.TYPE=N'LMS_CALLSTAGE'  and Stage.status=N'A' LEFT OUTER  JOIN BO_PARAMETER  Nature ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Nature.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_LEAD_NATURE=Nature.VAL AND Nature.TYPE=N'LMS_CALLNATURE'  and Nature.status=N'A' INNER JOIN BO_PARAMETER  Priority ON  LMS_CURRENT_CAMPAIGN.FUNCTION_ID=Priority.FUNCTION_ID AND  LMS_CURRENT_CAMPAIGN.TCC_PRIORITY=Priority.VAL AND Priority.TYPE=N'LMS_CALLPRIORITY'  and Priority.status=N'A'   join BO_BRANCH_MASTER on BO_BRANCH_MASTER.BRANCH_ID=LMS_CURRENT_CAMPAIGN.BRANCH_ID and LMS_CURRENT_CAMPAIGN.FUNCTION_ID = BO_BRANCH_MASTER.FUNCTION_ID inner join BO_USER_MASTER on BO_USER_MASTER.TUM_USER_ID=LMS_CURRENT_CAMPAIGN .TCC_CALLER_ID and BO_USER_MASTER.FUNCTION_ID =LMS_CURRENT_CAMPAIGN.FUNCTION_ID AND BO_USER_MASTER.TUM_USER_STATUS=N'A'  where 1=1  and LMS_CUSTOMER_MASTER.CUST_STATUS=N'A' and LMS_CURRENT_CAMPAIGN.TCC_CALLER_ID ='"+data.userid+"' and LMS_CURRENT_CAMPAIGN.TCC_RESPONSE='"+data.RESPONSE+"'  and BO_BRANCH_MASTER.BRANCH_ID='"+data.branchid+"' ";

                if (data.CUSTLEADID !=null )
                {

                    query = query + "and LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID='"+ data.CUSTLEADID + "' ";

                }
                if (data.Name != "" && data.Name != null)
                {

                     query = query + "and concat(LMS_CUSTOMER_MASTER.CUST_FNAME,' ',LMS_CUSTOMER_MASTER.CUST_LNAME) like '"+ data.Name + "' ";

                }

                if (data.Mobile != null)
                {

                    query = query + "and LMS_CUSTOMER_MASTER.MOBILE='"+ data.Mobile + "' ";

                }

                if (data.TCM_CAMPAIGN_SHORTDESC != "" && data.TCM_CAMPAIGN_SHORTDESC != null)
                {

                    query = query + "and LMS_CAMPAIGN_MASTER.TCM_CAMPAIGN_SHORTDESC='"+ data.TCM_CAMPAIGN_SHORTDESC + "' ";

                }

                if (data.PRIORITY != "" && data.PRIORITY != null)
                {

                    query = query + "and Priority.Value ='"+ data.PRIORITY + "' ";

                }

                if (data.RATING != "" && data.RATING != null)
                {

                    query = query + "and Rating.Value ='"+ data.RATING + "' ";

                }

                if (data.LEADBY != "" && data.LEADBY != null)
                {

                    query = query + "and LMS_CURRENT_CAMPAIGN.TCC_LEAD_BY= '"+ data.LEADBY + "'";

                }

                query = query + " ORDER BY LMS_CURRENT_CAMPAIGN.TCC_CUST_LEAD_ID desc OFFSET "+data.offset+" ROWS FETCH NEXT "+data.limit+" ROWS ONLY";






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
        [Route("pendleadsdatalength")]
        public async Task<ActionResult<Sales>> pendleadsdatalength(Sales data)
        {
            

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string sql = "MBL_MOB_PROD_PENDINGLEAD";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", data.Name);
                cmd.Parameters.AddWithValue("@CUSTLEADID", data.CUSTLEADID);
                cmd.Parameters.AddWithValue("@BRANCHID", data.branchid);
                cmd.Parameters.AddWithValue("@USERID", data.userid);
                cmd.Parameters.AddWithValue("@RESPONSE", data.RESPONSE);
                cmd.Parameters.AddWithValue("@MOBILE", data.Mobile);
                cmd.Parameters.AddWithValue("@CAMPAIGNNAME", data.CAMPAIGNNAME);
                cmd.Parameters.AddWithValue("@PRIORITY", data.PRIORITY);
                cmd.Parameters.AddWithValue("@RATING", data.RATING);
                cmd.Parameters.AddWithValue("@LEADBY", data.LEADBY);




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
        [Route("update_current_location")]
        public async Task<ActionResult<Sales>> update_current_location(Sales data)
        {
            // string struser = data.user_lower;

            List<Sales> Logdata = new List<Sales>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "update BO_USER_MASTER set current_location ='" + data.Location + "',LOCATION_UPDATED_ON = GETDATE() where TUM_USER_CODE='" + data.usercode + "'";

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
        [Route("getCompany/{name}")]
        public List<Sales> getCompany(string name)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select CUST_LNAME,CUST_ID from LMS_CUSTOMER_MASTER where FUNCTION_ID='1' and CUST_CAT='C' and CUST_LNAME like '"+name+"'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.CompanyName = Convert.ToString(row[0]);
                    log.ID = Convert.ToInt32(row[1]);
                   
                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("GetContactDetails/{CustId}")]
        public List<Sales> GetContactDetails(int CustId)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select ROW_ID,CONTACT_NAME,MOBILE,EMAIL,TELEPHONE from LMS_CUSTOMER_CONTACT_DETAILS where Customer_id='"+ CustId + "' and status='A'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.ID = Convert.ToInt32(row[0]);
                    log.ContactName = Convert.ToString(row[1]);
                   
                    log.Mobile = Convert.ToString(row[2]);
                    log.Email = Convert.ToString(row[3]);
                    log.ResNo = Convert.ToString(row[4]);

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("GetEmployee")]
        public dynamic GetEmployee()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select EM_EMP_CODE as Code,em_emp_name as Name,em_emp_id as id from HRMS_EMPLOYEE_MASTER where em_emp_status='A' ";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.usercode = Convert.ToString(row[0]);
                    log.Name = Convert.ToString(row[1]);
                    log.userid = Convert.ToInt32(row[2]);

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        //deepak
       
        [HttpGet]
        [Route("getdashboard_sourcewise/{id}")]
        public List<Sales> getdashboard_sourcewise(int id)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "SELECT COUNT(CASE WHEN VAL ='1' THEN (TCC_LEAD_SOURCE) END) AS  'Personal',COUNT(CASE WHEN VAL ='2' THEN (TCC_LEAD_SOURCE) END) AS  'Web Lead',COUNT(CASE WHEN VAL ='3' THEN (TCC_LEAD_SOURCE) END) AS  'Branch',COUNT(CASE WHEN VAL ='4' THEN (TCC_LEAD_SOURCE) END) AS  'Corporate Website',COUNT(CASE WHEN VAL ='5' THEN (TCC_LEAD_SOURCE) END) AS  'Mobile' FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and TCC_CALLER_ID = " + id + "";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Personal = Convert.ToInt32(row[0]);
                    log.Web = Convert.ToInt32(row[1]);
                    log.Branch = Convert.ToInt32(row[2]);
                    log.Corporateweb = Convert.ToInt32(row[3]);
                    log.Mobile = Convert.ToString(row[4]);
                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("GetEmployee/{emcode}")]
        public List<Sales> GetEmployee(string emcode)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select EM_EMP_CODE as Code,em_emp_name as Name,em_emp_id as id from HRMS_EMPLOYEE_MASTER where em_emp_status='A' and EM_EMP_CODE='"+ emcode + "' ";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.usercode = Convert.ToString(row[0]);
                    log.Name = Convert.ToString(row[1]);
                    log.userid = Convert.ToInt32(row[2]);
                   
                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        
        //shylaja

        [HttpPost]
        [Route("getdashboard_sourcewise")]
        public async Task<ActionResult<Sales>> getdashboard_sourcewise(Sales data)
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
                query = "SELECT COUNT(CASE WHEN VAL ='1' THEN (TCC_LEAD_SOURCE) END) AS  'Personal',COUNT(CASE WHEN VAL ='2' THEN (TCC_LEAD_SOURCE) END) AS  'Web Lead',COUNT(CASE WHEN VAL ='3' THEN (TCC_LEAD_SOURCE) END) AS  'Branch',COUNT(CASE WHEN VAL ='4' THEN (TCC_LEAD_SOURCE) END) AS  'Corporate Website',COUNT(CASE WHEN VAL ='5' THEN (TCC_LEAD_SOURCE) END) AS  'Mobile' FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and TCC_CALLER_ID = " + data.branchid + "";

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


        //shylaja dashboard stagewise

        [HttpPost]
        [Route("getdashboard_stagewise")]
        public async Task<ActionResult<Sales>> getdashboard_stagewise(Sales data)
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
                query = "select COUNT(CASE WHEN VAL ='2' THEN (TCC_CUST_LEAD_ID) END) AS  'Qualified',COUNT(CASE WHEN VAL ='5' THEN (TCC_CUST_LEAD_ID) END) AS    'Negotiation',COUNT(CASE WHEN VAL ='4' THEN (TCC_CUST_LEAD_ID) END) AS 'Proposal',COUNT(CASE WHEN VAL ='1' THEN (TCC_CUST_LEAD_ID) END) AS 'Enquiry',COUNT(CASE WHEN VAL ='6' THEN (TCC_CUST_LEAD_ID) END) AS 'Demo',COUNT(CASE WHEN VAL ='8' THEN (TCC_CUST_LEAD_ID) END) AS 'Lost',COUNT(CASE WHEN VAL ='7' THEN (TCC_CUST_LEAD_ID) END) AS 'Quotes Given',COUNT(CASE WHEN VAL ='3' THEN (TCC_CUST_LEAD_ID) END) AS 'Quality Testing done'FROM LMS_SALES_ANALYSIS WITH(NOLOCK) WHERE 1=1 and   BRANCH_ID = " + data.branchid + "";

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


        //deepak
        [HttpGet]
        [Route("BranchLocation/{id}")]
        public List<Sales> BranchLocation(int id)
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select a.LOCATION_ID,a.LOCATION_CODE,a.LOCATION_DESC from BO_BRANCH_LOCATION_MASTER a inner join BO_BRANCH_MASTER b on a.branch_id=b.branch_id where a.branch_id=" + id + "";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.locationID = Convert.ToInt32(row[0]);
                    log.Locationcode = row[1].ToString().Trim();
                    log.Locationname = row[2].ToString().Trim();

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }



        ////shylaja
        //[HttpGet]
        //[Route("BranchLocation/{id}")]
        //public string BranchLocation(int id)
        //{
        //    List<Sales> Logdata = new List<Sales>();
        //    string Logdata1 = string.Empty;
        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {
        //        string query = "";
        //        query = "select a.LOCATION_ID,a.LOCATION_CODE,a.LOCATION_DESC from BO_BRANCH_LOCATION_MASTER a inner join BO_BRANCH_MASTER b on a.branch_id=b.branch_id where a.branch_id=" + id + "";

        //        dbConn.Open();
        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);
        //        for (int i = 0; i < results.Rows.Count; i++)
        //        {
        //            DataRow row = results.Rows[i];
        //            Logdata1 = DataTableToJSONWithStringBuilder(results);
        //        }
        //        dbConn.Close();
        //        return Logdata1;
        //    }
        //}


        //deepak
        [HttpGet]
        [Route("GetProduct")]
        public dynamic GetProduct()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select TCM_CAMPAIGN_SHORTDESC,TCM_CAMPAIGN_ID,PRODUCTTYPE from LMS_CAMPAIGN_MASTER";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.ProductName = row[0].ToString().Trim();
                    log.ProductID = Convert.ToInt32(row[1]);
                    log.ProductCatID = Convert.ToInt32(row[1]);

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        ////shylaja
        //[HttpGet]
        //[Route("GetProduct")]
        //public string GetProduct()
        //{
        //    List<Sales> Logdata = new List<Sales>();

        //    using (SqlConnection dbConn = new SqlConnection(strconn))
        //    {
        //        string Logdata1 = string.Empty;
        //        string query = "";
        //        query = "select TCM_CAMPAIGN_SHORTDESC,TCM_CAMPAIGN_ID,PRODUCTTYPE from LMS_CAMPAIGN_MASTER";

        //        dbConn.Open();
        //        SqlCommand cmd = new SqlCommand(query, dbConn);
        //        var reader = cmd.ExecuteReader();
        //        System.Data.DataTable results = new System.Data.DataTable();
        //        results.Load(reader);
        //        for (int i = 0; i < results.Rows.Count; i++)
        //        {
        //            DataRow row = results.Rows[i];
        //            Logdata1 = DataTableToJSONWithStringBuilder(results);


        //        }
        //         dbConn.Close();
        //        return Logdata1;
        //    }
        //}




        [HttpGet]
        [Route("Nametitle")]
        public dynamic Nametitle()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE ='BO_TITLE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);
                  

                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callpriority")]
        public dynamic callpriority()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLPRIORITY'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callrating")]
        public dynamic callrating()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLRATING'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callnature")]
        public dynamic callnature()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLNATURE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }

        [HttpGet]
        [Route("callstage")]
        public dynamic callstage()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_CALLSTAGE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("Leadsource")]
        public dynamic Leadsource()
        {
            List<Sales> Logdata = new List<Sales>();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                string query = "";
                query = "select text,val from BO_PARAMETER where FUNCTION_ID = 1 and STATUS = 'A' and TYPE='LMS_LEADSOURCE'";

                dbConn.Open();
                SqlCommand cmd = new SqlCommand(query, dbConn);
                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                for (int i = 0; i < results.Rows.Count; i++)
                {

                    Sales log = new Sales();
                    DataRow row = results.Rows[i];
                    log.Text = row[0].ToString().Trim();
                    log.Value = Convert.ToInt32(row[1]);


                    Logdata.Add(log);
                }
                dbConn.Close();
            }
            return Logdata;
        }


        [HttpGet]
        [Route("customerresponse/{producttypeid}")]
        public string customerresponse(string producttypeid)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "MOB_CustResponse";
                string sql = "MBL_MOB_CustResponse";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@producttypeid", producttypeid);


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
        [Route("AddContact/{CompanyId}/{functionid}/{BranchId}/{ContactName}/{Designation}/{Mobile}/{Email}/{OfficeName}/{ResidancePhone}/{UserID}/{userType}")]
        public string AddContact(string CompanyId, string functionid, string BranchId, string ContactName, string Designation, string Mobile, string Email, string OfficeName, string ResidancePhone, string UserID, string userType)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                //string sql = "MOB_CustResponse";
                string sql = "MBL_Mob_AddContact";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@Functionid", functionid);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@Designation", Designation);
                cmd.Parameters.AddWithValue("@Mobile", Mobile);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@OfficeName", OfficeName);
                cmd.Parameters.AddWithValue("@ResidancePhone", ResidancePhone);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@userType", userType);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string st = cmd.Parameters["@Result"].Value.ToString();
                var json = new JavaScriptSerializer().Serialize(st);
                return json;

                //SqlDataAdapter da = new SqlDataAdapter(cmd);


                //var reader = cmd.ExecuteReader();
                //System.Data.DataTable results = new System.Data.DataTable();
                //results.Load(reader);
                ////string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                //for (int i = 0; i < results.Rows.Count; i++)
                //{
                //    DataRow row = results.Rows[i];
                //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                //}
                //return Logdata1;
            }
        }



        [HttpGet]
        [Route("corporatelead/{functionid}/{BRANCH_ID}/{productcategoryid}/{productid}/{campaignid}/{CompanyName}/{ContactName}/{mobile}/{OfficePhone}/{ResidencePhone}/{callpriorityid}/{callratingid}/{callnatureid}/{leadsourceid}/{callstageid}/{customerresponse}/{time}/{NextCallDate}/{remarks}/{ExpectedClose}/{ExpectedAmount}/{Leadby}/{UserID}/{userType}/{LocationId}")]
        public string corporatelead(string functionid, string BRANCH_ID, string productcategoryid, string productid, string campaignid, string CompanyName, string ContactName, string mobile, string OfficePhone, string ResidencePhone, string callpriorityid, string callratingid, string callnatureid, string leadsourceid, string callstageid, string customerresponse, string time, string NextCallDate, string remarks, string ExpectedClose, string ExpectedAmount, string Leadby, string UserID, string userType, string LocationId)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_MOB_NewLeadInsert_corporate";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FUNCTION_ID", functionid);
                cmd.Parameters.AddWithValue("@BRANCH_ID", BRANCH_ID);
                cmd.Parameters.AddWithValue("@productcategoryid", productcategoryid);
                cmd.Parameters.AddWithValue("@productid", productid);
                cmd.Parameters.AddWithValue("@campaignid", campaignid);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@mobile", mobile);
                cmd.Parameters.AddWithValue("@OfficePhone", OfficePhone);
                cmd.Parameters.AddWithValue("@ResidencePhone", ResidencePhone);
                cmd.Parameters.AddWithValue("@callpriorityid", callpriorityid);
                cmd.Parameters.AddWithValue("@callratingid", callratingid);
                cmd.Parameters.AddWithValue("@callnatureid", callnatureid);
                cmd.Parameters.AddWithValue("@leadsourceid", leadsourceid);
                cmd.Parameters.AddWithValue("@callstageid", callstageid);
                cmd.Parameters.AddWithValue("@customerresponse", customerresponse);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@NextCallDate", NextCallDate);
                cmd.Parameters.AddWithValue("@remarks", remarks);
                cmd.Parameters.AddWithValue("@ExpectedClose", ExpectedClose);
                cmd.Parameters.AddWithValue("@ExpectedAmount", ExpectedAmount);
                cmd.Parameters.AddWithValue("@Leadby", Leadby);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@userType", userType);
                cmd.Parameters.AddWithValue("@LocationId", LocationId);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string st = cmd.Parameters["@Result"].Value.ToString();
                var json = new JavaScriptSerializer().Serialize(st);
                return json;

                
            }
        }



        [HttpGet]
        [Route("NewCorporate/{functionid}/{BranchId}/{CompanyName}/{Address}/{Pin}/{UserID}/{userType}")]
        public string NewCorporate(string functionid, string BranchId, string CompanyName, string Address, string Pin, string UserID, string userType)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            DataSet dsbranchcount = new DataSet();

            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                dbConn.Open();
                string sql = "MBL_Mob_NewCorporate";
                SqlCommand cmd = new SqlCommand(sql, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Functionid", functionid);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Pin", Pin);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@userType", userType);
                cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string st = cmd.Parameters["@Result"].Value.ToString();

                var json = new JavaScriptSerializer().Serialize(st);
                return json;


            }
        }



        //insert lead shylaja
        [HttpGet]
        [Route("insertlead")]
        public string insertlead(string functionid, string BRANCH_ID, string productcategoryid, string productid, string campaignid, string customerfname, string customerlname, string mobile, string OfficePhone, string ResidencePhone, string callpriorityid, string callratingid, string callnatureid, string leadsourceid, string callstageid, string customerresponse, string NextCallDate, string time, string remarks, string ExpectedClose, string ExpectedAmount, string Leadby, string UserID, string userType, string LocationId, string EmailId, string Currency)
        {
            try
            {
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    if (functionid.ToString() == "0" || functionid.ToString() == "" || functionid.ToString() == string.Empty || functionid.ToString() == "null")
                    {
                        functionid =null;
                    }


                    if (BRANCH_ID.ToString() == "0" || BRANCH_ID.ToString() == "" || BRANCH_ID.ToString() == string.Empty || BRANCH_ID.ToString() == "null")
                    {
                        BRANCH_ID = null;
                    }
                    if (productcategoryid.ToString() == "0" || productcategoryid.ToString() == "" || productcategoryid.ToString() == string.Empty || productcategoryid.ToString() == "null")
                    {
                        productcategoryid = null;
                    }

                    if (productid.ToString() == "0" || productid.ToString() == "" || productid.ToString() == string.Empty || productid.ToString() == "null")
                    {
                        productid = null;
                    }
                    if (campaignid.ToString() == "0" || campaignid.ToString() == "" || campaignid.ToString() == string.Empty || campaignid.ToString() == "null")
                    {
                        campaignid = null;
                    }

                    if (customerfname.ToString() == "0" || customerfname.ToString() == "" || customerfname.ToString() == string.Empty || customerfname.ToString() == "null")
                    {
                        customerfname = null;
                    }

                    if (customerlname.ToString() == "0" || customerlname.ToString() == "" || customerlname.ToString() == string.Empty || customerlname.ToString() == "null")
                    {
                        customerlname = null;
                    }

                    if (mobile.ToString() == "0" || mobile.ToString() == "" || mobile.ToString() == string.Empty || mobile.ToString() == "null")
                    {
                        mobile = null;
                    }

                    if (OfficePhone.ToString() == "0" || OfficePhone.ToString() == "" || OfficePhone.ToString() == string.Empty || OfficePhone.ToString() == "null")
                    {
                        OfficePhone = null;
                    }

                    if (ResidencePhone.ToString() == "0" || ResidencePhone.ToString() == "" || ResidencePhone.ToString() == string.Empty || ResidencePhone.ToString() == "null")
                    {
                        ResidencePhone = null;
                    }

                    if (callpriorityid.ToString() == "0" || callpriorityid.ToString() == "" || callpriorityid.ToString() == string.Empty || callpriorityid.ToString() == "null")
                    {
                        callpriorityid = null;
                    }

                    if (callratingid.ToString() == "0" || callratingid.ToString() == "" || callratingid.ToString() == string.Empty || callratingid.ToString() == "null")
                    {
                        callratingid = null;
                    }

                    if (callnatureid.ToString() == "0" || callnatureid.ToString() == "" || callnatureid.ToString() == string.Empty || callnatureid.ToString() == "null")
                    {
                        callnatureid= null;
                    }

                    if (leadsourceid.ToString() == "0" || leadsourceid.ToString() == "" || leadsourceid.ToString() == string.Empty || leadsourceid.ToString() == "null")
                    {
                        leadsourceid = null;
                    }

                    if (callstageid.ToString() == "0" || callstageid.ToString() == "" || callstageid.ToString() == string.Empty || callstageid.ToString() == "null")
                    {
                        callstageid=null;
                    }

                    if (customerresponse.ToString() == "0" || customerresponse.ToString() == "" || customerresponse.ToString() == string.Empty || customerresponse.ToString() == "null")
                    {
                        customerresponse= null;
                    }

                    if (NextCallDate.ToString() == "0" || NextCallDate.ToString() == "" || NextCallDate.ToString() == string.Empty || NextCallDate.ToString() == "null")
                    {
                        NextCallDate = null;
                    }

                    if (time.ToString() == "0" || time.ToString() == "" || time.ToString() == string.Empty || time.ToString() == "null")
                    {
                        time = null;
                    }

                    if (remarks.ToString() == "0" || remarks.ToString() == "" || remarks.ToString() == string.Empty || remarks.ToString() == "null")
                    {
                        remarks = null;
                    }
                    if (ExpectedClose.ToString() == "0" || ExpectedClose.ToString() == "" || ExpectedClose.ToString() == string.Empty || ExpectedClose.ToString() == "null")
                    {
                        ExpectedClose = null;
                    }

                    if (ExpectedAmount.ToString() == "0" || ExpectedAmount.ToString() == "" || ExpectedAmount.ToString() == string.Empty || ExpectedAmount.ToString() == "null")
                    {
                        ExpectedAmount = "0";
                    }

                    if (Leadby.ToString() == "0" || Leadby.ToString() == "" || Leadby.ToString() == string.Empty || Leadby.ToString() == "null")
                    {
                        Leadby ="0";
                    }
                    if (UserID.ToString() == "0" || UserID.ToString() == "" || UserID.ToString() == string.Empty || UserID.ToString() == "null")
                    {
                        UserID = "0";
                    }
                    if (userType.ToString() == "0" || userType.ToString() == "" || userType.ToString() == string.Empty || userType.ToString() == "null")
                    {
                        userType="0";
                    }
                    if (LocationId.ToString() == "0" || LocationId.ToString() == "" || LocationId.ToString() == string.Empty || LocationId.ToString() == "null")
                    {
                        LocationId ="0";
                    }
                    if (EmailId.ToString() == "0" || EmailId.ToString() == "" || EmailId.ToString() == string.Empty || EmailId.ToString() == "null")
                    {
                        EmailId ="0";
                    }
                    if (Currency.ToString() == "0" || Currency.ToString() == "" || Currency.ToString() == string.Empty || Currency.ToString() == "null")
                    {
                        Currency="0";
                    }
                    string Logdata1 = string.Empty;









                    dbConn.Open();
                    string sql = "MBL_MOB_NewLeadInsert";
                    SqlCommand cmd = new SqlCommand(sql, dbConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FUNCTION_ID", functionid);
                    cmd.Parameters.AddWithValue("@BRANCH_ID", BRANCH_ID);
                    cmd.Parameters.AddWithValue("@productcategoryid", productcategoryid);
                    cmd.Parameters.AddWithValue("@productid", productid);
                    cmd.Parameters.AddWithValue("@campaignid", campaignid);
                    cmd.Parameters.AddWithValue("@customerfname", customerfname);
                    cmd.Parameters.AddWithValue("@customerlname", customerlname);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@OfficePhone", OfficePhone);
                    cmd.Parameters.AddWithValue("@ResidencePhone", ResidencePhone);
                    cmd.Parameters.AddWithValue("@callpriorityid", callpriorityid);
                    cmd.Parameters.AddWithValue("@callratingid", callratingid);
                    cmd.Parameters.AddWithValue("@callnatureid", callnatureid);
                    cmd.Parameters.AddWithValue("@leadsourceid", leadsourceid);
                    cmd.Parameters.AddWithValue("@callstageid", callstageid);
                    cmd.Parameters.AddWithValue("@customerresponse", customerresponse);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@NextCallDate", NextCallDate);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@ExpectedClose", ExpectedClose);
                    cmd.Parameters.AddWithValue("@ExpectedAmount", ExpectedAmount);
                    cmd.Parameters.AddWithValue("@Leadby", Leadby);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@userType", userType);
                    cmd.Parameters.AddWithValue("@LocationId", LocationId);
                    cmd.Parameters.AddWithValue("@Currency", Currency);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);

                    cmd.ExecuteNonQuery();

                    //var reader = cmd.ExecuteReader();
                    //System.Data.DataTable results = new System.Data.DataTable();
                    //results.Load(reader);
                    ////string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                    //for (int i = 0; i < results.Rows.Count; i++)
                    //{
                    //    DataRow row = results.Rows[i];
                    //    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    //}
                    //return Logdata1;

                    cmd.Parameters.Add("@Result", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string st = cmd.Parameters["@Result"].Value.ToString();

                    var json = new JavaScriptSerializer().Serialize(st);
                    return json;
                }
            }
            catch (Exception ex)
            {

                var json = new JavaScriptSerializer().Serialize(ex.Message);
                return json;
            }

        }

        //shylaja
        [HttpGet]
        [Route("Getcustomer/{EmpCode}")]
        public string Getcustomer(string EmpCode)
        {
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {


                dbConn.Open();
                string query = "";
                query = "select CUST_LNAME as Code,CUST_ID as ID from LMS_CUSTOMER_MASTER where CUST_LNAME ='" + EmpCode + "'";

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

//end


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
