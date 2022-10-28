using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class ERPItems
    {
        public string prsid { get; set; }

        public string itemid { get; set; }

        public string function_id { get; set; }

        public string required_qty { get; set; }

        public string UOM { get; set; }

        public string expected_cost { get; set; }

        public string exp_date { get; set; }

        public string status { get; set; }

        public string created_by { get; set; }

        public string ipaddress { get; set; }

        public string unit_price { get; set; }

      public string Limit { get; set; }

        public string Availlimit { get; set; }

        public string BalanceLimit { get; set; }

        public string CATEGORY { get; set; }

        public string TAX1 { get; set; }

        public string TAX2 { get; set; }

        public string TAX1DESC { get; set; }

        public string TAX2DESC { get; set; }

        public string OTHERCHARGES { get; set; }

        public string item_short_desc { get; set; }

        public string REMARKS { get; set; }

        public string CategoryID { get; set; }

        public string SubCategoryID { get; set; }

        public string prsDetailID { get; set; }

        public string FreightVALUE { get; set; }

        public string FreightID { get; set; }

        public string RecoveryVALUE { get; set; }

        public string RecoveryID { get; set; }

        public string BDC { get; set; }

        public string PTM { get; set; }

        public string ACC { get; set; }

        public string CPC { get; set; }

        public string flag { get; set; }
    }
}
