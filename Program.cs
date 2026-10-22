using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using AkumsAutoSync.Modals;
using AkumsAutoSync.BusinessLayer;

namespace ApiDataSync
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static Timer timer;
        private static IConfigurationRoot configuration;
        private static string logFilePath;

        static async Task Main(string[] args)
        {
            LogMessage("Service Task Started At " + DateTime.Now);

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            configuration = builder.Build();

            SessoinClass.sBaseURL = configuration["sBaseURL"];
            SessoinClass.sMethodName = configuration["sMethodName"];
            SessoinClass.PublishFolder = configuration["PublishFolder"];
            SessoinClass.ConnectionString = configuration.GetConnectionString("DefaultConnection");

            LogMessage("Sync Start at " + DateTime.Now + "With Task Schedular");
            await DownloadShipperMaterData();
            await DownloadCustomerData();
            await DownloadDispatchData();
            LogMessage("Sync Completed at " + DateTime.Now + "With Task Schedular");

            ScheduleHourlyTask(async () =>
            {
                LogMessage("Hourly Sync Start at " + DateTime.Now + "With Time");
                await DownloadShipperMaterData();
                await DownloadCustomerData();
                await DownloadDispatchData();
                LogMessage("Hourly Sync Completed at " + DateTime.Now + "With Time");
            });

            Console.WriteLine("Shipper Management Auto Sync is Running: Press [Enter] to exit the program.");
            Console.ReadLine();
            LogMessage("Service Task Completed At " + DateTime.Now);
        }

        private static void LogMessage(string message)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                logFilePath = $@"C:\Shp.Location\LogReportText\LogForAutoSyncTaskUpdatebyKunal_{date}_WithSchedular.txt";

                if (!File.Exists(logFilePath))
                {
                    using (FileStream fs = File.Create(logFilePath))
                    {
                        // Just to create the file
                    }
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while logging: " + ex.Message);
            }
        }

        private static void ScheduleHourlyTask(Func<Task> task)
        {
            timer = new Timer(async _ =>
            {
                await task();
            }, null, TimeSpan.Zero, TimeSpan.FromHours(1)); // 2 hour
            //}, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        }

        #region Logic Layer
        private async static Task DownloadShipperMaterData()
        {
            try
            {
                var data = await ProcessOrderSyncMaterData.DownloadShipperMaterData();
                if (data != null && data.Count > 0)
                {
                    LogMessage("Data synchronized shipper successfully at " + DateTime.Now);
                }
                else
                {
                    LogMessage("No data to synchronize for shipper at " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                LogMessage("An error occurred while synchronizing shipper data: " + ex.Message);
            }
        }

        private async static Task DownloadCustomerData()
        {
            try
            {
                var data = await SyncCustomerMasterData.DownloadCustomerData();
                if (data != null && data.Count > 0)
                {
                    LogMessage("Data synchronized successfully for Customer Data at " + DateTime.Now);
                }
                else
                {
                    LogMessage("No data to synchronize for Customer Data at " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                LogMessage("An error occurred while synchronizing Customer Data: " + ex.Message);
            }
        }

        private async static Task DownloadDispatchData()
        {
            try
            {
                var data = await SyncDeliveryMasterData.DownloadDispatchData();
                if (data != null && data.Count > 0)
                {
                    LogMessage("Data synchronized successfully for Dispatch Data at " + DateTime.Now);
                }
                else
                {
                    LogMessage("No data to synchronize for Dispatch Data at " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                LogMessage("An error occurred while synchronizing Dispatch Data: " + ex.Message);
            }
        }
        #endregion 
    }
}

