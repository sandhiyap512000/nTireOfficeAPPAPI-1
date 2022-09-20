using JSIGamingAPI.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MobileAppAPI.Models;
using Newtonsoft.Json;
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
           // var result = "";
            using (SqlConnection dbConn = new SqlConnection(strconn))
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                Byte[] hashedBytes = null;
                SHA256Managed hashString = new SHA256Managed();
                hashedBytes = hashString.ComputeHash(UE.GetBytes(data.password));
                DataSet dsuserdetails = new DataSet();
                dbConn.Open();
                string sql = "BO_MOB_USER_AUTHENCATION";
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
                //string outputval = cmd.Parameters["@outputparam"].Value.ToString();
                for (int i = 0; i < results.Rows.Count; i++)
                {
                    DataRow row = results.Rows[i];
                    Logdata1 = DataTableToJSONWithStringBuilder(results);
                    logdata= DataTableToJSONWithStringBuilder(results);

                    dbConn.Close();
                }
                //Token token = new Token();
               string Username = data.user_lower;
               string Password = data.password;

               // token = GetAccessToken(Username, Password);

                var tokenString= GetAccessToken(Username, Password);
               // strtoken = token.ToString();
                strtoken = tokenString;
                
                
            }
            var result = (new { data=logdata, token= strtoken });
            return Ok(result);

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




                //var tokenResponse = client.PostAsync("token", new FormUrlEncodedContent(RequestBody)).Result;

                //if (tokenResponse.IsSuccessStatusCode)
                //{

                //    var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                //    token = JsonConvert.DeserializeObject<Token>(JsonContent);
                //}




               
            }
            catch (Exception ex)
            {

                //return BadRequest(ex);
                string s = ex.ToString();
                return s;
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

