using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class ERP
    {
		public string branchid { get; set; }
		public string functionid { get; set; }
		public string ponumber { get; set; }
		public string vendorcode { get; set; }
		public string fromdate { get; set; }
		public string todate { get; set; }
		public string status { get; set; }
		public string itemcode { get; set; }
		public string usertype { get; set; }
		public string userid { get; set; }
		public string pageindex { get; set; }
		public string pagesize { get; set; }
		public string sortexpression { get; set; }
		public string alphaname { get; set; }
		public string prscode { get; set; }

		public string code { get; set; }

		public string rfqcode { get; set; }
		public string vendorid { get; set; }

		public string quoteref { get; set; }

		public string mode { get; set; }

		public string usercode { get; set; }

		public string FUNCTION_ID { get; set; }
	}
}
