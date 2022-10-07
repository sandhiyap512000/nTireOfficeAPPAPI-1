using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class Sales
    {
        public int userid { get; set; }
        public int CustId { get; set; }

        public int functionid { get; set; }
        public int usertype { get; set; }

        public int branchid { get; set; }

        public int Location { get; set; }

        public string usercode { get; set; }

        public string ContactName { get; set; }

        public int Personal { get; set; }
        public int Web { get; set; }
        public int Branch { get; set; }
        public int Corporateweb { get; set; }
        public string Mobile { get; set; }
        public int locationID { get; set; }
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Locationcode { get; set; }
        public string Locationname { get; set; }

        public string Email { get; set; }

        public string ResNo { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int CUSTLEADID { get; set; }
        public int ProductCatID { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string fdate { get; set; }
        public string tdate { get; set; }
        public string status { get; set; }
        public string RESPONSE { get; set; }
       
        public string CAMPAIGNNAME { get; set; }
        public string PRIORITY { get; set; }
        public string RATING { get; set; }
        public string LEADBY { get; set; }
        public string TCM_CAMPAIGN_SHORTDESC { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }

        public int LatLong { get; set; }
        public int LeadID { get; set; }

        //public int custid { get; set; }

    }
}
