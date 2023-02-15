using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.Models
{
    public class Helper
    {

        public string Connectionstring()
        {
            //production server

        // string Connectionstring = "Server=172.107.203.228;User ID=Developer;Password=Sqldb*2021;Database=SSG";

         //   string Connectionstring = "Server=172.107.203.228;User ID=Developer;Password=Sqldb*2021;Database=mydesk20";




        //Demo server

        //Property
     // string Connectionstring = "Server=108.60.219.44;User ID=Developer;Password=D$e6ve8lo8per;Database=nTireproperty";

         
            //CAMS,SALES,ERP
     // string Connectionstring = "Server=108.60.219.44;User ID=Developer;Password=D$e6ve8lo8per;Database=mydesk20";

            string Connectionstring = "Server=172.107.203.171;User ID=Techteam;Password=Password@123;Database=DevMydesk20";

            //HRMS
            //  string Connectionstring = "Server=108.60.219.44;User ID=Developer;Password=D$e6ve8lo8per;Database=ssg";


            //testing ICRISAT
            //string Connectionstring = "Server=108.60.219.44;User ID=Developer;Password=D$e6ve8lo8per;Database=ICRISAT";

            return Connectionstring;

        }
    }
}
