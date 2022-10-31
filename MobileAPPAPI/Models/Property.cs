using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class Property
    {
        public int userid { get; set; }
        public int functionid { get; set; }

        public int branchid { get; set; }

        public string Priority { get; set; }

        public string pm_due_date { get; set; }

        public string drpPMType { get; set; }

        public string txtDetails { get; set; }

        public string assetownerid { get; set; }
        public string assetid { get; set; }
        public string assetcode { get; set; }

        public string status { get; set; }
        public string pmr_reference { get; set; }
    }
}
