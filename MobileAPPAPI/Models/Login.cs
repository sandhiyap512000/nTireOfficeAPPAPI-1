using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSIGamingAPI.Models
{
    public class Login
    {
        //BO_USER_MASTER TABLE

        public int functionid { get; set; }
        public int TUM_BRANCH_ID { get; set; }
        public int TUM_USER_ID { get; set; }
        public string TUM_USER_CODE { get; set; }
        public string TUM_USER_NAME { get; set; }
        public int TUM_USER_TEAM { get; set; }
        public byte TUM_USER_PWD { get; set; }
        public string TUM_USER_TYPE { get; set; }
        public DateTime TUM_VALIDITY_FROM { get; set; }
        public DateTime TUM_VALIDITY_TO { get; set; }
        public string TUM_FORCE_LOGON { get; set; }
        public int TUM_USER_FORMAT { get; set; }
        public int TUM_USER_LANGUAGE { get; set; }
        public int TUM_AUTOLOGOUT_PERIOD { get; set; }
        public string TUM_USER_STATUS { get; set; }
        public string TUM_USER_ADDRESS1 { get; set; }
        public string TUM_USER_ADDRESS2 { get; set; }
        public string TUM_USER_CITY { get; set; }
        public string TUM_USER_STATE { get; set; }
        public string TUM_USER_COUNTRY { get; set; }
        public string TUM_USER_PINCODE { get; set; }
        public string TUM_RES_PHONE { get; set; }
        public string TUM_USER_MOBILE { get; set; }
        public string TUM_USER_EMAILID { get; set; }
        public DateTime TUM_USER_DOB { get; set; }
        public int TUM_USER_SKILLRATING { get; set; }
        public string TUM_USER_EDUCATION { get; set; }
        public int TUM_USER_LOCATION { get; set; }
        public int TUM_USER_REPORTINGROLE { get; set; }
        public int TUM_USER_ROLE { get; set; }
        public byte SHOW_DATE_CALL { get; set; }
        public int CREATED_BY { get; set; }
        public int UPDATED_BY { get; set; }
        public DateTime UPDATED_ON { get; set; }
        public string IPADDRESS { get; set; }
        public int TOTAL_CLOSED { get; set; }
        public int TOTAL_MADE { get; set; }
        public int LOGIN_STATUS { get; set; }
        public string IS_INBOUND { get; set; }
        public string SESSION_ID { get; set; }
        public int TUM_CUSTOMER_ID { get; set; }
        public string IS_CUSTOMER { get; set; }
        public string TUM_VIEW_CUSTOMER { get; set; }
        public string THEME { get; set; }
        public string PASSWORD_STATUS { get; set; }
        public int TUM_USER_SETTING { get; set; }
        public int TUM_REPORTING_TO { get; set; }
        public int TXN_ID { get; set; }
        public string BIZ_SEGMENT { get; set; }
        public string escalation_in { get; set; }
        public string escalation_in_type { get; set; }
        public string IsSuperUser { get; set; }
        public string TUM_USER_PHOTO { get; set; }
        public DateTime CREATED_ON { get; set; }
        public int TUM_DF_PROJECT_ID { get; set; }
        public string TRANSACTION_PASSWORD { get; set; }
        public string AutomaticPWDUpdate { get; set; }
        public string TUM_USER_SIGN { get; set; }
        public int TUM_ADD { get; set; }
        public int TUM_READY { get; set; }
        public string MAIL_SIGN { get; set; }
        public string TUM_USER_SEX { get; set; }
        public string TUM_PFX_SIGN { get; set; }
        public string TUM_PFX_PWD { get; set; }
        public string TUM_SIGN_MODE { get; set; }
        public string OLDSESSION_ID { get; set; }
        public string Email_password { get; set; }
        public string CurrentDeviceID { get; set; }
        public string current_location { get; set; }
        public DateTime LOCATION_UPDATED_ON { get; set; }
        public string Device_Token { get; set; }
        public int IN_OUT_TRACK { get; set; }
        public int TUM_USER_OTP { get; set; }
        public int Workflow_avilable { get; set; }
        public int workflow_Alt_approver { get; set; }
        //payload parameters
        public string user_lower { get; set; }

        public string password { get; set; }

        public string sessionid { get; set; }

        public string companyname { get; set; }

        public int ip { get; set; }
    }
}
