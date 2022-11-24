using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
	public class ERP
	{

		public string FUNCTIONIDP { get; set; }
		public string BRANCHIDP { get; set; }
		public string RFQCODEP { get; set; }

		public string FROMDATEP { get; set; }
		public string TODATEP { get; set; }
		public string ITEMCODEP { get; set; }
		public string VENDORIDP { get; set; }

		public string QUOTEREFP { get; set; }
		public string STATUSP { get; set; }
		public int PAGEINDEXP { get; set; }
		public int PAGESIZEP { get; set; }
		public string SORTEXPRESSIONP { get; set; }

		public string ALPHANAMEP { get; set; }
		public string modep { get; set; }

		public string VENDORCODEP { get; set; }
		public string branchid { get; set; }
		public string functionid { get; set; }

		public int functionid1 { get; set; }
		public string ponumber { get; set; }
		public string vendorcode { get; set; }
		public string fromdate { get; set; }
		public string todate { get; set; }
		public string status { get; set; }
		public string itemcode { get; set; }
		public string usertype { get; set; }
		public string userid { get; set; }
		public string pageindex { get; set; }

		public int pageindex1 { get; set; }
		public string pagesize { get; set; }

		public int pagesize1 { get; set; }
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
		public string reuestdate { get; set; }

		public string currentstatus { get; set; }

		public string reqtype { get; set; }

		public string menuid { get; set; }

		public string requser { get; set; }

		public string qutype { get; set; }

		public string prsref { get; set; }

		public string ipaddress { get; set; }

		public string reasonpurchase { get; set; }

		public string netamount { get; set; }

		public string currency { get; set; }

		public string requestcomments { get; set; }

		public string isbid { get; set; }

		public string prstype { get; set; }

		public string prscategory { get; set; }

		public string requestby { get; set; }

		public string requestdate { get; set; }

		public string requettype { get; set; }

		public string issinglevendor { get; set; }

		public string orderpriority { get; set; }

		public string createdby { get; set; }

		public string prsid { get; set; }

		public string items { get; set; }

		public string itemid { get; set; }

		public string PRS_Mode { get; set; }

		public string release { get; set; }

		//RFQ

		public string strfun { get; set; }
		public string rfqfromdate { get; set; }
		public string rfqtodate { get; set; }

		//[Column(TypeName = "jsonb")]
		//public string customfield { get; set; }
		public string item_deatils { get; set; }


		//Material Issue
		public string RBYMI { get; set; }
		public string STOREMI { get; set; }

		public string FUNCTIONIDMI { get; set; }
		public string BRANCHIDMI { get; set; }
		public string ITEM_CODEMI { get; set; }
		public string ITEM_REFMI { get; set; }
		public string ILT_REFMI { get; set; }
		public string SR_REFMI { get; set; }
		public string FROMDATEMI { get; set; }
		public string TODATEMI { get; set; }
		public string STATUSMI { get; set; }
		public string ALPHANAMEMI { get; set; }

		public string SORTEXPRESSIONMI { get; set; }

		public int PAGEINDEXMI { get; set; }

		public int PAGESIZEMI { get; set; }

		public string SEARCH_TYPEMI { get; set; }

		//Material issuedetails link search

		public string FUNCTIONIDMIS { get; set; }
		public string BRANCHIDMIS { get; set; }
		public string LOCATION_IDMIS { get; set; }
		public string BINMIS { get; set; }
		public string ITEM_IDMIS { get; set; }
		public string ALPHANAMEMIS { get; set; }
		public string SORTEXPRESSIONMIS { get; set; }
		public int PAGEINDEXMIS { get; set; }
		public int PAGESIZEMIS { get; set; }

		public string STOREEMIS { get; set; }

		public string RACKEMIS { get; set; }

		public List<ERPItems> Itemsdetail { get; set; }

		//[Column(TypeName = "jsonb")]
		//public ERPItems1 Itemsdetail { get; set; }

		public List<ERPItems1> ERPItems { get; set; }

	}
		public class RootObject
		{
			public DataTable Itemstable;

			public List<ERPItems> ERPItems { get; set; }
		}


	public class ERPItems1
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
