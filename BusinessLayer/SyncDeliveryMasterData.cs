using AkumsAutoSync.DataLayer;
using AkumsAutoSync.Modals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.BusinessLayer
{
    public class SyncDeliveryMasterData
    {

        private static string logFilePath;
        //public static async Task<List<DispatchMasterDownload>> DownloadDispatchData()
        //{
        //    List<SyncDispatchMasterDownload> selectedCollection1 = new List<SyncDispatchMasterDownload>();
        //    List<DispatchMasterDownload> selectedCollection = new List<DispatchMasterDownload>();
        //    string MmgData = string.Empty;
        //    DL_SyncProcessData objdl_ItemMaster = new DL_SyncProcessData();

        //    try
        //    {
        //        DataTable dtsite = objdl_ItemMaster.dtGetSite();
        //        List<UserSiteMaster> selectedSites = (from DataRow dr in dtsite.Rows
        //                                              select new UserSiteMaster()
        //                                              {
        //                                                  ID = Convert.ToInt32(dr["ID"]),
        //                                                  SiteCode = Convert.ToString(dr["SiteCode"]),
        //                                                  UserCode = Convert.ToString(dr["UserCode"]),
        //                                                  AssignBy = Convert.ToString(dr["AssignBy"]),
        //                                                  CreatedBy = Convert.ToString(dr["AssignBy"])
        //                                              }).ToList();

        //        HttpClientHandler clientHandler = new HttpClientHandler();
        //        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        //        // Pass the handler to httpclient(from you are calling api)
        //        //HttpClient client = new HttpClient(clientHandler);
        //        HttpClient client = new HttpClient(clientHandler)
        //        {
        //            Timeout = TimeSpan.FromMinutes(30) // Set longer timeout for API call
        //        };

        //        foreach (var site in selectedSites)
        //        {
        //            LogMessage($"Processing site: {site.SiteCode}");
        //            ReportModel reportModel = new ReportModel
        //            {
        //                // Set the site code for each request
        //                PLANT = site.SiteCode
        //            };
        //            var json = JsonConvert.SerializeObject(reportModel);
        //            string sSendData = SessoinClass.sBaseURL + SessoinClass.sMethodName + "deliverydata";
        //            var request = new HttpRequestMessage
        //            {
        //                Method = HttpMethod.Get,
        //                RequestUri = new Uri(SessoinClass.sBaseURL + SessoinClass.sMethodName + "deliverydata"),
        //                Content = new StringContent(json, Encoding.UTF8, "application/json")
        //            };
        //            request.Headers.Add("Authorization", "Basic c2ItZmNjNDQyODktNzU5MS00NDI3LWJkMzQtYzIzYTFiOGFjMjgwIWI0NDE5MXxpdC1ydC1jcGlwcm9kLXd6MXl4d3QyIWIxNDg6N2Q1Nzc5MzktNTU4My00YjViLTg3YTQtYmIxNDAyNWM2NDQ1JFhPLWNHbUVkb05nOVFKQl9lUS1tUWVrQy1JdnNFUkpKY3BzVXVzMnlIVVU9");
        //            LogMessage($"Sending request to {sSendData}");
        //            var response = client.SendAsync(request).ConfigureAwait(false);
        //            var responseInfo = response.GetAwaiter().GetResult();
        //            if (responseInfo.IsSuccessStatusCode)
        //            {
        //                var content1 = responseInfo.Content.ReadAsStringAsync().Result;
        //                var CustData = JsonConvert.DeserializeObject<List<SyncDispatchMasterDownload>>(content1);
        //                //LogMessage($"Received successful response: {content1}");
        //                selectedCollection1 = (List<SyncDispatchMasterDownload>)CustData;
        //                DataTable dtItemList = ListToDatatable.ToDataTable(selectedCollection1);
        //                if (dtItemList.Rows.Count > 0)
        //                {
        //                    selectedCollection = (from DataRow dr in dtItemList.Rows
        //                                          select new DispatchMasterDownload()
        //                                          {
        //                                              Delivery = Convert.ToString(dr["VBELN"]),
        //                                              Delivery_Date = Convert.ToString(dr["ERDAT"]),
        //                                              Material_Code = Convert.ToString(dr["MATNR"]),
        //                                              Material_Name = Convert.ToString(dr["MAKTX"]),
        //                                              Customer_Code = Convert.ToString(dr["KUNNR"]),
        //                                              Sales_quantity = Convert.ToString(dr["KWMENG"]),
        //                                              Sales_Unit = Convert.ToString(dr["VRKME"]),
        //                                              BatchNo = Convert.ToString(dr["CHARG"]),
        //                                              //PlantCode = Convert.ToString(dr["WERKS"]),
        //                                              No_of_Cases_Shipper = Convert.ToString(dr["ZNO_CASES"])
        //                                              //No_of_Cases_Shipper = Convert.ToString(dr["ZNO_CASE"])
        //                                          }).ToList();
        //                }
        //                ItemMaster objItemMaster = new ItemMaster();
        //                dtItemList = selectedCollection.ToDataTable();
        //                objItemMaster.SiteCode = site.SiteCode;
        //                if (site.SiteCode=="9700") {
        //                    LogMessage($"upload data for site 9700 is {dtItemList}.");
        //                }
        //                DataTable dt = objdl_ItemMaster.UploadDisptachData(objItemMaster, dtItemList);
        //                if (dt.Rows.Count > 0)
        //                {
        //                    MmgData = Convert.ToString(dt.Rows[0][0].ToString());
        //                    LogMessage($"Data uploaded successfully for site {site.SiteCode}.");
        //                }
        //                else
        //                {
        //                    LogMessage($"Failed to upload data for site {site.SiteCode}.");
        //                    MmgData = "ERROR~Unable to save data, Please try again";
        //                }
        //            }
        //        }
        //        LogMessage("Dispatch data download process completed.");
        //        return (selectedCollection);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMessage($"An error occurred: {ex.Message}");
        //        return selectedCollection;  
        //    }
        //}

        public static async Task<List<DispatchMasterDownload>> DownloadDispatchData()
        {
            List<SyncDispatchMasterDownload> selectedCollection1 =
                new List<SyncDispatchMasterDownload>();

            List<DispatchMasterDownload> selectedCollection =
                new List<DispatchMasterDownload>();

            string MmgData = string.Empty;

            DL_SyncProcessData objdl_ItemMaster =
                new DL_SyncProcessData();

            try
            {
                // ============================================================
                // GET SITE DATA
                // ============================================================

                DataTable dtsite = objdl_ItemMaster.dtGetSite();

                List<UserSiteMaster> selectedSites =
                    (from DataRow dr in dtsite.Rows
                     select new UserSiteMaster()
                     {
                         ID = Convert.ToInt32(dr["ID"]),
                         SiteCode = Convert.ToString(dr["SiteCode"]),
                         UserCode = Convert.ToString(dr["UserCode"]),
                         AssignBy = Convert.ToString(dr["AssignBy"]),
                         CreatedBy = Convert.ToString(dr["AssignBy"])
                     }).ToList();


                // ============================================================
                // HTTP CLIENT
                // ============================================================

                HttpClientHandler clientHandler =
                    new HttpClientHandler();

                clientHandler.ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) =>
                    {
                        return true;
                    };

                HttpClient client =
                    new HttpClient(clientHandler)
                    {
                        Timeout = TimeSpan.FromMinutes(30)
                    };


                // ============================================================
                // PROCESS EACH SITE
                // ============================================================

                foreach (var site in selectedSites)
                {
                    try
                    {
                        LogMessage(
                            $"Processing site: {site.SiteCode}"
                        );


                        // ====================================================
                        // 9000 SITE START
                        // ====================================================

                        if (site.SiteCode == "9000")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("==============================================");
                            Console.WriteLine("          9000 SITE PROCESS START");
                            Console.WriteLine("==============================================");

                            Console.WriteLine(
                                "Site Code : " + site.SiteCode
                            );
                        }


                        // ====================================================
                        // REQUEST MODEL
                        // ====================================================

                        ReportModel reportModel =
                            new ReportModel
                            {
                                PLANT = site.SiteCode
                            };


                        string json =
                            JsonConvert.SerializeObject(reportModel);


                        string sSendData =
                            SessoinClass.sBaseURL +
                            SessoinClass.sMethodName +
                            "deliverydata";


                        // ====================================================
                        // REQUEST
                        // ====================================================

                        var request =
                            new HttpRequestMessage
                            {
                                Method = HttpMethod.Get,

                                RequestUri =
                                    new Uri(
                                        SessoinClass.sBaseURL +
                                        SessoinClass.sMethodName +
                                        "deliverydata"
                                    ),

                                Content =
                                    new StringContent(
                                        json,
                                        Encoding.UTF8,
                                        "application/json"
                                    )
                            };


                        request.Headers.Add(
                            "Authorization",
                            "Basic c2ItZmNjNDQyODktNzU5MS00NDI3LWJkMzQtYzIzYTFiOGFjMjgwIWI0NDE5MXxpdC1ydC1jcGlwcm9kLXd6MXl4d3QyIWIxNDg6N2Q1Nzc5MzktNTU4My00YjViLTg3YTQtYmIxNDAyNWM2NDQ1JFhPLWNHbUVkb05nOVFKQl9lUS1tUWVrQy1JdnNFUkpKY3BzVXVzMnlIVVU9"
                        );


                        LogMessage(
                            $"Sending request to {sSendData}"
                        );


                        // ====================================================
                        // 9000 REQUEST DETAILS
                        // ====================================================

                        if (site.SiteCode == "9000")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("9000 API REQUEST");
                            Console.WriteLine(
                                "URL : " + sSendData
                            );
                            Console.WriteLine(
                                "PLANT : " + site.SiteCode
                            );
                            Console.WriteLine(
                                "JSON : " + json
                            );
                            Console.WriteLine(
                                "Calling API..."
                            );
                        }


                        // ====================================================
                        // CALL API
                        // ====================================================

                        var response =
                            await client.SendAsync(request);

                        var responseInfo = response;


                        // ====================================================
                        // RESPONSE STATUS
                        // ====================================================

                        if (site.SiteCode == "9000")
                        {
                            Console.WriteLine("");
                            Console.WriteLine(
                                "9000 API RESPONSE"
                            );

                            Console.WriteLine(
                                "Status Code : " +
                                (int)responseInfo.StatusCode
                            );

                            Console.WriteLine(
                                "Status : " +
                                responseInfo.StatusCode
                            );

                            Console.WriteLine(
                                "Reason : " +
                                responseInfo.ReasonPhrase
                            );
                        }


                        // ====================================================
                        // SUCCESS RESPONSE
                        // ====================================================

                        if (responseInfo.IsSuccessStatusCode)
                        {
                            string content1 =
                                await responseInfo.Content
                                    .ReadAsStringAsync();


                            // =================================================
                            // 9000 RAW API RESPONSE
                            // =================================================

                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "9000 API RESPONSE LENGTH : " +
                                    (content1?.Length ?? 0)
                                );

                                Console.WriteLine(
                                    "9000 API RESPONSE RECEIVED."
                                );
                            }


                            // =================================================
                            // DESERIALIZE
                            // =================================================

                            var CustData =
                                JsonConvert.DeserializeObject<
                                    List<SyncDispatchMasterDownload>
                                >(content1);


                            // =================================================
                            // 9000 RECORD COUNT
                            // =================================================

                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "=============================================="
                                );

                                Console.WriteLine(
                                    "9000 API RECORD COUNT : " +
                                    (CustData?.Count ?? 0)
                                );

                                Console.WriteLine(
                                    "=============================================="
                                );
                            }


                            // =================================================
                            // NO DATA CHECK
                            // =================================================

                            if (CustData == null ||
                                CustData.Count == 0)
                            {
                                if (site.SiteCode == "9000")
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine(
                                        "******** 9000 NO DATA ********"
                                    );

                                    Console.WriteLine(
                                        "API se 9000 ka koi data nahi mila."
                                    );

                                    Console.WriteLine(
                                        "********************************"
                                    );
                                }

                                LogMessage(
                                    $"No data received for site {site.SiteCode}."
                                );

                                continue;
                            }


                            selectedCollection1 =
                                CustData;


                            // =================================================
                            // CONVERT TO DATATABLE
                            // =================================================

                            DataTable dtItemList =
                                ListToDatatable.ToDataTable(
                                    selectedCollection1
                                );


                            // =================================================
                            // 9000 DATATABLE COUNT
                            // =================================================

                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "9000 DATATABLE ROW COUNT : " +
                                    dtItemList.Rows.Count
                                );
                            }


                            // =================================================
                            // CONVERT DATA
                            // =================================================

                            if (dtItemList.Rows.Count > 0)
                            {
                                selectedCollection =
                                    (from DataRow dr in dtItemList.Rows
                                     select new DispatchMasterDownload()
                                     {
                                         Delivery =
                                             Convert.ToString(
                                                 dr["VBELN"]
                                             ),

                                         Delivery_Date =
                                             Convert.ToString(
                                                 dr["ERDAT"]
                                             ),

                                         Material_Code =
                                             Convert.ToString(
                                                 dr["MATNR"]
                                             ),

                                         Material_Name =
                                             Convert.ToString(
                                                 dr["MAKTX"]
                                             ),

                                         Customer_Code =
                                             Convert.ToString(
                                                 dr["KUNNR"]
                                             ),

                                         Sales_quantity =
                                             Convert.ToString(
                                                 dr["KWMENG"]
                                             ),

                                         Sales_Unit =
                                             Convert.ToString(
                                                 dr["VRKME"]
                                             ),

                                         BatchNo =
                                             Convert.ToString(
                                                 dr["CHARG"]
                                             ),

                                         No_of_Cases_Shipper =
                                             Convert.ToString(
                                                 dr["ZNO_CASES"]
                                             )

                                     }).ToList();
                            }


                            // =================================================
                            // ITEM MASTER
                            // =================================================

                            ItemMaster objItemMaster =
                                new ItemMaster();


                            dtItemList =
                                selectedCollection.ToDataTable();


                            objItemMaster.SiteCode =
                                site.SiteCode;


                            // =================================================
                            // 9000 BEFORE UPLOAD
                            // =================================================

                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "=============================================="
                                );

                                Console.WriteLine(
                                    "9000 UPLOAD INFORMATION"
                                );

                                Console.WriteLine(
                                    "Site Code : " +
                                    objItemMaster.SiteCode
                                );

                                Console.WriteLine(
                                    "Upload Rows : " +
                                    dtItemList.Rows.Count
                                );

                                Console.WriteLine(
                                    "Starting DB Upload..."
                                );

                                Console.WriteLine(
                                    "=============================================="
                                );
                            }


                            // =================================================
                            // DATABASE UPLOAD
                            // =================================================

                            DataTable dt =
                                objdl_ItemMaster.UploadDisptachData(
                                    objItemMaster,
                                    dtItemList
                                );


                            // =================================================
                            // 9000 DB RESPONSE
                            // =================================================

                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "=============================================="
                                );

                                Console.WriteLine(
                                    "9000 DB UPLOAD RESPONSE"
                                );

                                if (dt != null &&
                                    dt.Rows.Count > 0)
                                {
                                    Console.WriteLine(
                                        "DB Response : " +
                                        Convert.ToString(
                                            dt.Rows[0][0]
                                        )
                                    );
                                }
                                else
                                {
                                    Console.WriteLine(
                                        "DB Response : NO RESPONSE"
                                    );
                                }

                                Console.WriteLine(
                                    "=============================================="
                                );
                            }


                            // =================================================
                            // FINAL RESULT
                            // =================================================

                            if (dt != null &&
                                dt.Rows.Count > 0)
                            {
                                MmgData =
                                    Convert.ToString(
                                        dt.Rows[0][0]
                                    );

                                LogMessage(
                                    $"Data uploaded successfully for site {site.SiteCode}."
                                );


                                if (site.SiteCode == "9000")
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine(
                                        "******** 9000 UPLOAD SUCCESS ********"
                                    );

                                    Console.WriteLine(
                                        "Response : " +
                                        MmgData
                                    );

                                    Console.WriteLine(
                                        "**************************************"
                                    );
                                }
                            }
                            else
                            {
                                LogMessage(
                                    $"Failed to upload data for site {site.SiteCode}."
                                );


                                if (site.SiteCode == "9000")
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine(
                                        "******** 9000 UPLOAD FAILED ********"
                                    );

                                    Console.WriteLine(
                                        "UploadDisptachData returned no data."
                                    );

                                    Console.WriteLine(
                                        "************************************"
                                    );
                                }
                            }
                        }
                        else
                        {
                            // =================================================
                            // API FAILED
                            // =================================================

                            string errorResponse =
                                await responseInfo.Content
                                    .ReadAsStringAsync();


                            LogMessage(
                                $"API failed for site {site.SiteCode}. " +
                                $"Status: {responseInfo.StatusCode}"
                            );


                            if (site.SiteCode == "9000")
                            {
                                Console.WriteLine("");
                                Console.WriteLine(
                                    "=============================================="
                                );

                                Console.WriteLine(
                                    "           9000 API FAILED"
                                );

                                Console.WriteLine(
                                    "Status Code : " +
                                    (int)responseInfo.StatusCode
                                );

                                Console.WriteLine(
                                    "Status : " +
                                    responseInfo.StatusCode
                                );

                                Console.WriteLine(
                                    "Reason : " +
                                    responseInfo.ReasonPhrase
                                );

                                Console.WriteLine(
                                    "Response : " +
                                    errorResponse
                                );

                                Console.WriteLine(
                                    "=============================================="
                                );
                            }
                        }
                    }
                    catch (Exception siteEx)
                    {
                        // ====================================================
                        // SITE LEVEL ERROR
                        // ====================================================

                        LogMessage(
                            $"Error processing site {site.SiteCode}: " +
                            siteEx.ToString()
                        );


                        if (site.SiteCode == "9000")
                        {
                            Console.WriteLine("");
                            Console.WriteLine(
                                "=============================================="
                            );

                            Console.WriteLine(
                                "          9000 SITE EXCEPTION"
                            );

                            Console.WriteLine(
                                siteEx.ToString()
                            );

                            Console.WriteLine(
                                "=============================================="
                            );
                        }


                        // Continue with next site
                        continue;
                    }
                }


                // ============================================================
                // COMPLETE
                // ============================================================

                LogMessage(
                    "Dispatch data download process completed."
                );


                Console.WriteLine("");
                Console.WriteLine(
                    "Dispatch data download process completed."
                );


                return selectedCollection;
            }
            catch (Exception ex)
            {
                // ============================================================
                // MAIN ERROR
                // ============================================================

                LogMessage(
                    $"An error occurred: {ex}"
                );


                Console.WriteLine("");
                Console.WriteLine(
                    "=============================================="
                );

                Console.WriteLine(
                    "MAIN ERROR"
                );

                Console.WriteLine(
                    ex.ToString()
                );

                Console.WriteLine(
                    "=============================================="
                );


                return selectedCollection;
            }
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
    }
}

