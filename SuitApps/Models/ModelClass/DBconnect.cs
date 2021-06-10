using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class DBconnect
    { 
        public static SqlConnection getDataBaseConnection()
        {
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_Offline_TierraFoods;User Id=SuitApps_Offline_TierraFoods;Password=SuitApps_Offline_TierraFoods";
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_OfflineMV;User Id=SuitApps_OfflineMV;Password=SuitApps_OfflineMV";
           // string connectionString = " Data Source=43.255.152.25;Initial Catalog=SuitApps_SargaKFC2;User Id=SuitApps_SargaKFC2;Password=SuitApps_SargaKFC2@123";
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_PampadyMobileApp;User Id=SuitApps_PampadyMobileApp;Password=SuitApps_PampadyMobileApp";
          // string connectionString = " Data Source=43.255.152.25;Initial Catalog=thomasandsons1;User Id=thomasandsons1;Password=thomasandsons1@123";

            var connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            // var connectionString = ConfigurationManager.AppSettings["DbConnection"];
            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public static SqlConnection getDataBaseConnection2()
        {
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_Offline_TierraFoods;User Id=SuitApps_Offline_TierraFoods;Password=SuitApps_Offline_TierraFoods";
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_OfflineMV;User Id=SuitApps_OfflineMV;Password=SuitApps_OfflineMV";
            // string connectionString = " Data Source=43.255.152.25;Initial Catalog=SuitApps_SargaKFC2;User Id=SuitApps_SargaKFC2;Password=SuitApps_SargaKFC2@123";
            //string connectionString = " Data Source=43.255.152.26;Initial Catalog=SuitApps_PampadyMobileApp;User Id=SuitApps_PampadyMobileApp;Password=SuitApps_PampadyMobileApp";
            // string connectionString = " Data Source=43.255.152.25;Initial Catalog=thomasandsons1;User Id=thomasandsons1;Password=thomasandsons1@123";

            var connectionString = ConfigurationManager.ConnectionStrings["DbConnection2"].ConnectionString;
            // var connectionString = ConfigurationManager.AppSettings["DbConnection"];
            SqlConnection con = new SqlConnection(connectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
    }
}