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

        public int LETTER_TYPE { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int DESIGNATION_ID { get; set; }
        
        public int functionid { get; set; }

        public string assetcode { get; set; }

        public string STATUS { get; set; }

        public string REASON { get; set; }

        public string TxnReference { get; set; }

        public string emp_code { get; set; }

        public string DESIGNATION { get; set; }
        //new
        public string req_type { get; set; }

        public string scheme_id { get; set; }

        public string MonthLy_installment { get; set; }

        public string Amount { get; set; }

      

        public string Monthly_deduct { get; set; }

        public string Rev_loan { get; set; }

        public string CreatedBy { get; set; }

        public string Createdon { get; set; }

        public string Updatedon { get; set; }

        public string ipaddress { get; set; }

        public string isdeferral { get; set; }

        public string deferralmode { get; set; }

        public int usertype { get; set; }

        public string ODRequestRef { get; set; }
        public string typerequest { get; set; }
        public string from { get; set; }

        public string to { get; set; }

        public string perdate { get; set; }

        public string Expenseid { get; set; }

        public string Exp_id { get; set; }

        public string ExpensesType { get; set; }

        public string ExpensesAmount { get; set; }

        public string Remarks { get; set; }
        public string RequestRef { get; set; }
        public string currentstatus { get; set; }
        public string Fromdate { get; set; }
        public string todate { get; set; }
        public string nodays { get; set; }

        public string fromhours { get; set; }
        public string tohours { get; set; }
        public string EXPENSE_REF { get; set; }
        public string EXPENSE_ID { get; set; }
        public string EXPENSE_DATE { get; set; }
        public string EXPENSE_CATEGORY { get; set; }
        public string DESCRIPTION { get; set; }
        public string IS_BILLABLE { get; set; }
        public string COMMENTS { get; set; }
        public string request_ref { get; set; }
        


        //end
    }
}
