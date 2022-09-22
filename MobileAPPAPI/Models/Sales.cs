using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class Sales
    {
        public int userid { get; set; }

        public int branchid { get; set; }

        public int Location { get; set; }

        public string usercode { get; set; }

        public int Personal { get; set; }
        public int Web { get; set; }
        public int Branch { get; set; }
        public int Corporateweb { get; set; }
        public int Mobile { get; set; }
        public int locationID { get; set; }
        public string Locationcode { get; set; }
        public string Locationname { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public int ProductCatID { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }

    }
}
