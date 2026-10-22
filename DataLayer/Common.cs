using AkumsAutoSync.Modals;
using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AkumsAutoSync.DataLayer
{
    public class Common
    {
        private readonly string _connectionString;
        /// <summary>
        /// Constructor 
        /// </summary>
        public Common()
        {
            _connectionString = SessoinClass.ConnectionString;
                //GetConnectionString();
        }
        /// <summary>
        /// Connection section
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {            

            var builder = new ConfigurationBuilder();
            //ConfigurationManager configuration = builder.Configuration;
            //SessoinClass.sBaseURL = configuration.GetValue<string>("Settings:api_base_url");
            //SessoinClass.sMethodName = configuration.GetValue<string>("Settings:MethodName");
            ////.SetBasePath(Directory.GetCurrentDirectory())
            ////.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            return configuration.GetConnectionString(SessoinClass.ConnectionString);
        }


        //private string GetConnectionStringSetting()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //    IConfigurationRoot configuration = builder.Build();

        //    string connectionString = configuration.GetConnectionString("DbConStr");

        //    // Example of accessing other settings
        //    SessoinClass.sBaseURL = configuration.GetValue<string>("Settings:api_base_url");
        //    SessoinClass.sMethodName = configuration.GetValue<string>("Settings:MethodName");

        //    return connectionString;
        //}

        /// <summary>
        /// 
        /// </summary>1
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(SqlCommand cmd)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable ExecuteDatatable(SqlCommand cmd)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //public static void LogMessage(string message)
        //{
        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(SessoinClass.ConnectionString, true))
        //        {
        //            writer.WriteLine($"{DateTime.Now}: {message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred while logging: " + ex.Message);
        //    }
        //}
    }
}
