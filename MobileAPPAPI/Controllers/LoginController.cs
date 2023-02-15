using JSIGamingAPI.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MobileAppAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JSIGamingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static Helper objhelper = new Helper();
        public static string strconn = objhelper.Connectionstring();
        private object utf8Encoder;//new
        System.Security.Cryptography.SHA256Managed sha256 = new System.Security.Cryptography.SHA256Managed();//new
   


        //login post

        [HttpPost]
        [Route("loginMobileLos")]
        public async Task<ActionResult<Login>> loginMobileLos(Login data)
        {
            string struser = data.user_lower;

            List<Login> Logdata = new List<Login>();
            string Logdata1 = string.Empty;
            var logdata = "";
            var strtoken = "";
            dynamic strdata;
            // var result = "";
            string outputjson = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
              //  UTF8Encoding utf8Encoder = new UTF8Encoding();//new
                UnicodeEncoding UE = new UnicodeEncoding();
                Byte[] hashedBytes = null;
                SHA256Managed hashString = new SHA256Managed();
                hashedBytes = hashString.ComputeHash(UE.GetBytes(data.password));
                //hashedBytes= sha256.ComputeHash(utf8Encoder.GetBytes(data.password));//new added
                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "MBL_BO_MOB_USER_AUTHENCATION";
                SqlCommand cmd = new SqlCommand(sql, dbConn);


                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usercode", data.user_lower);
                cmd.Parameters.AddWithValue("@pwd", hashedBytes);
                cmd.Parameters.AddWithValue("@SessionID", data.sessionid);
                cmd.Parameters.AddWithValue("@function", data.functionid);
                cmd.Parameters.AddWithValue("@IP_ADDRESS", data.ip);
                // cmd.Parameters.AddWithValue("@OldSessionID", "");

                var reader = cmd.ExecuteReader();
                System.Data.DataTable results = new System.Data.DataTable();
                results.Load(reader);
                strdata = results;
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);//need to uncomment

                    Logdata1 = DataTableToJSONWithStringBuilder1(results);
                    // logdata= DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                //Token token = new Token();
                string Username = data.user_lower;
                string Password = data.password;

                // token = GetAccessToken(Username, Password);

                var tokenString = GetAccessToken(Username, Password);
                // strtoken = token.ToString();
                strtoken = tokenString;

                //  var resultJObj = JObject.Parse(Logdata1);


            }
            var result = (new { data = Logdata1, token = strtoken });
            return Ok(Logdata1);

        }
        //public static Token GetAccessToken(string username, string password)
        //{
        //    Token token = new Token();
        //    HttpClientHandler handler = new HttpClientHandler();
        //    HttpClient client = new HttpClient(handler);
        //    try {
        //        var RequestBody = new Dictionary<string, string>
        //        {
        //        {"grant_type", "password"},
        //        {"username", username},
        //        {"password", password},
        //        };


        //        var claims = new[] {
        //        new Claim ("username", username == null? "": username.ToString ()),
        //        new Claim ("password", password == null? "": password.ToString())
        //        };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey")); //_config["Jwt:Key"]
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token1 = new JwtSecurityToken("http://108.60.219.44:63939/",
        //   "http://108.60.219.44:63939/",
        //   claims,
        //   expires: DateTime.Now.AddMinutes(3000),
        //   signingCredentials: creds);



        //         new JwtSecurityTokenHandler().WriteToken(token1);





        //        //var tokenResponse = client.PostAsync("token", new FormUrlEncodedContent(RequestBody)).Result;

        //        //if (tokenResponse.IsSuccessStatusCode)
        //        //{

        //        //    var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
        //        //    token = JsonConvert.DeserializeObject<Token>(JsonContent);
        //        //}




        //        return token;
        //    }
        //    catch (Exception ex)
        //    {

        //        //return BadRequest(ex);
        //        string s = ex.ToString();
        //        return s;
        //    }
        //}
        [HttpGet]
        [Route("GetUserToken/{username}/{password}")]
        public string GetUserToken(string username, string password)
        {
            try

            {
                var result = "";
                var strtoken = "";
                var outputtoken = "";
                string s = "token";
                var JSONString = new StringBuilder();
                using (SqlConnection dbConn = new SqlConnection(strconn))
                {
                    dbConn.Open();
                    var tokenString = GetAccessToken(username, password);
                    // strtoken = token.ToString();
                    strtoken = tokenString;
                    outputtoken = s + strtoken;
                    JSONString.Append("{");
                    JSONString.Append("\"" + s + "\":" + "\"" + strtoken + "\"");
                    JSONString.Append("}");


                }
                return JSONString.ToString();
            }
            catch (Exception ex)
            {

                //return BadRequest(ex);
                string s = ex.ToString();
                return s;
            }

        }


                    public string GetAccessToken(string username, string password)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            try
            {
                var RequestBody = new Dictionary<string, string>
                {
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                };


                var claims = new[] {
                new Claim ("username", username == null? "": username.ToString ()),
                new Claim ("password", password == null? "": password.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey")); //_config["Jwt:Key"]
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token1 = new JwtSecurityToken("http://108.60.219.44:63939/",
           "http://108.60.219.44:63939/",
           claims,
           expires: DateTime.Now.AddMinutes(3000),
           signingCredentials: creds);



                return new JwtSecurityTokenHandler().WriteToken(token1);








            }
            catch (Exception ex)
            {

                //return BadRequest(ex);
                string s = ex.ToString();
                return s;
            }
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



        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                string strdata = "data";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                           // JSONString.Append("\"" + strdata + "\":" );
                            JSONString.Append("\""+table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
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
              
            }
            return JSONString.ToString();
        }




        [HttpGet]

        [Route("config/{sptoken}")]
        public IActionResult config(string sptoken)
        {
            return Ok();
        }

        [HttpOptions]
        public IActionResult CreateToken1()
        {
            return Ok(new { token = "" });
        }


        //dec6





        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //public static bool Email(string data, string toemail, string toname, string fromname)
        //{
          

        //    //return true;
        //    try
        //    {

        //        using (SqlConnection dbConn = new SqlConnection(strconn))
        //        {
        //            dbConn.Open();

        //            string EmailId = "";
        //            string port = "";
        //            //string serverName = "";
        //            string Password = "";
        //            string smtpserver = "";
        //            //string companyname = "";

        //            //sendermailid = /*bbp.getwewbconfigkeybase("sendermailid"); //*/    ConfigurationManager.AppSettings["SenderMailIdNew"].ToString();
        //            //port = /*bbp.getwewbconfigkeybase("port");  //*/     ConfigurationManager.AppSettings["port"].ToString();
        //            //serverName =/* bbp.getwewbconfigkeybase("smtpserver");//*/     ConfigurationManager.AppSettings["smtpserver"].ToString();
        //            //serverpwd = /*bbp.getwewbconfigkeybase("SenderPassword");//*/ ConfigurationManager.AppSettings["SenderPasswordNEW"].ToString();
        //            //smtpstender = ConfigurationManager.AppSettings["smtpstender"].ToString();
        //            //companyname= ConfigurationManager.AppSettings["companyname"].ToString();
        //            string commandText = string.Empty;
        //            commandText = "select MGE_SMTP_SERVER  ,MGE_EMAIL_ID ,MGE_PASSWORD ,MGE_PORT_NO  from BO_mail_Config where MGE_ID='1'";
        //            SqlCommand cmd = new SqlCommand(query, dbConn);
        //            var reader = cmd.ExecuteReader();
        //            System.Data.DataTable results = new System.Data.DataTable();
        //            results.Load(reader);
        //            if (ds1 != null)
        //            {
        //                if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //                {
        //                    smtpserver = ds1.Tables[0].Rows[0]["MGE_SMTP_SERVER"].ToString();
        //                    EmailId = ds1.Tables[0].Rows[0]["MGE_EMAIL_ID"].ToString();
        //                    Password = ds1.Tables[0].Rows[0]["MGE_PASSWORD"].ToString();
        //                    port = ds1.Tables[0].Rows[0]["MGE_PORT_NO"].ToString();


        //                }

        //            }


        //            var message = new MimeMessage();

        //            message.To.Add(new MailboxAddress(toname, toemail));
        //            /*
        //                            message.From.Add(new MailboxAddress("E-mail From Name", "srimathi@sunsmartglobal.com"));

        //                            message.Subject = "Subject";
        //                            //We will say we are sending HTML. But there are options for plaintext etc.
        //                            message.Body = new TextPart(TextFormat.Html)
        //                            {
        //                            Text = data
        //                            };

        //                            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
        //                            using (var emailClient = new SmtpClient())
        //                            {
        //                            //The last parameter here is to use SSL (Which you should!)
        //                            emailClient.Connect("mail.sunsmartglobal.com", 587, false);

        //                            //Remove any OAuth functionality as we won't be using it.
        //                            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

        //                            emailClient.Authenticate("srimathi@sunsmartglobal.com", "sender@123$");
        //                            */
        //            //message.From.Add(new MailboxAddress("SunSmart", "support@sunsmartglobal.com"));
        //            message.From.Add(new MailboxAddress("SunSmart", EmailId));



        //            message.Subject = "Notify from " + fromname;
        //            //We will say we are sending HTML. But there are options for plaintext etc.
        //            data = data.Replace("&lt;", "<");
        //            data = data.Replace("&gt;", ">");
        //            message.Body = new TextPart("html")
        //            {
        //                Text = data
        //            };

        //        }

        //        //Be careful that the SmtpClient class is the one from Mailkit not the framework!
        //        using (var emailClient = new SmtpClient())
        //        {




        //            //The last parameter here is to use SSL (Which you should!)
        //            //emailClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //            emailClient.Connect(smtpserver, 587, SecureSocketOptions.StartTls);


        //            //emailClient.Connect(smtpstender, 587, SecureSocketOptions.StartTls);



        //            //Remove any OAuth functionality as we won't be using it.
        //            // emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

        //            //emailClient.Authenticate("support@sunsmartglobal.com", "Ecqsufegzoucluji");
        //            emailClient.Authenticate(EmailId, Password);



        //            emailClient.Send(message);

        //            emailClient.Disconnect(true);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //[Route("LeadCreationAPI/ForgotPassword"), HttpPost]
        //public List<ForgotPassword> ForgotPassword(ForgotPassword ForgotPassword)
        //{
        //    List<ForgotPassword> lstForgotPassword = new List<ForgotPassword>();
        //    ForgotPassword objForgotPassword = new ForgotPassword();
        //    string password = "";
        //    string data = string.Empty;
        //    //string   companyname = ConfigurationManager.AppSettings["companyname"].ToString();
        //    try
        //    {
        //        string commandText = string.Empty;
        //        commandText = "select TUM_USER_CODE AS USER_CODE ,TUM_USER_NAME,TUM_USER_STATUS,TUM_USER_EMAILID,TUM_FORCE_LOGON ,PASSWORD_STATUS from BO_USER_MASTER where TUM_USER_CODE='" + ForgotPassword.User_Code + "'";

        //        DataSet ds1 = sa.getDataSet(commandText);
        //        if (ds1 != null)
        //        {
        //            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //            {
        //                objForgotPassword = new ForgotPassword();
        //                objForgotPassword.User_Code = ds1.Tables[0].Rows[0]["USER_CODE"].ToString();
        //                objForgotPassword.UserStatus = ds1.Tables[0].Rows[0]["TUM_USER_STATUS"].ToString();
        //                objForgotPassword.Emailid = ds1.Tables[0].Rows[0]["TUM_USER_EMAILID"].ToString();
        //                objForgotPassword.Logon = ds1.Tables[0].Rows[0]["TUM_FORCE_LOGON"].ToString();
        //                objForgotPassword.PwdStatus = ds1.Tables[0].Rows[0]["PASSWORD_STATUS"].ToString();
        //                objForgotPassword.UserName = ds1.Tables[0].Rows[0]["TUM_USER_NAME"].ToString();

        //            }


        //            if (objForgotPassword.UserStatus == "A" && objForgotPassword.PwdStatus == "I")
        //            {
        //                string strquery = "Update BO_USER_MASTER set PASSWORD_STATUS='A' WHERE TUM_USER_CODE='" + ForgotPassword.User_Code + "'";
        //                sa.ExecuteNonQuery(strquery);
        //            }

        //        }



        //        password = RandomString(6);
        //        string query = "Update BO_USER_MASTER set TUM_USER_PWD=convert(varbinary(max),'" + password + "') where TUM_USER_CODE='" + objForgotPassword.User_Code + "'";

        //        sa.ExecuteNonQuery(query);
        //        //S sa.getString(query);
        //        data = "The temporary password id : " + password;
        //        Email(data, objForgotPassword.Emailid, objForgotPassword.UserName, "SunSmart");





        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //    return lstForgotPassword;
        //}










    }

    public class UserCredential
    {
        [Key]
        [Column(TypeName = "int")]
        public int? companyid { get; set; }

        [Column(TypeName = "string")]
        public string pkcol { get; set; }

        [Column(TypeName = "int")]
        public int? userid { get; set; }

        [Column(TypeName = "int")]
        public int? employeeid { get; set; }

        [Column(TypeName = "string")]
        public string code { get; set; }

        [Column(TypeName = "string")]
        public string currency { get; set; }

        [Column(TypeName = "string")]
        public string username { get; set; }

        [Column(TypeName = "int")]
        public int? branchid { get; set; }

        [Column(TypeName = "string")]
        public string branchiddesc { get; set; }

        [Column(TypeName = "string")]
        public string language { get; set; }

        [Column(TypeName = "string")]
        public string emailid { get; set; }
        [Column(TypeName = "string")]
        public string usersource { get; set; }

        [Column(TypeName = "string")]
        public string countrycode { get; set; }

        [Column(TypeName = "string")]
        public string defaultpage { get; set; }

        [Column(TypeName = "string")]
        public string theme { get; set; }

        [Column(TypeName = "string")]
        public string layoutpage { get; set; }

        [Column(TypeName = "int")]
        public int? userroleid { get; set; }
        [Column(TypeName = "int")]
        public int? role { get; set; }

        [Column(TypeName = "int")]
        public int? finyearid { get; set; }

        [Column(TypeName = "string")]
        public string finyeardesc { get; set; }
    }

    public class LoginModel
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
    public class ForgotPassword
    {
        public string email { get; set; }
    }

    public class ResetPassword
    {
        public string password { get; set; }
        public string sptoken { get; set; }
    }




}

