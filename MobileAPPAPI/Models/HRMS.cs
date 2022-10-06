using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class HRMS
    {

        public int userid { get; set; }
        public int month { get; set; }
        public int branchid { get; set; }

        public int EmpId { get; set; }
        public int functionid { get; set; }

        public string assetcode { get; set; }

        public string TxnReference { get; set; }

    }
}
