//using AkumsAutoSync.DataLayer;
//using AkumsAutoSync.Modals;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net.Http;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace AkumsAutoSync.BusinessLayer
//{
//    public static class SyncCustomerMasterData
//    {
//        public static async Task<List<CustomerMasterModel>> DownloadCustomerData()
//        {
//            List<SyncCustomerMasterModel> selectedCollection1 = new List<SyncCustomerMasterModel>();
//            List<CustomerMasterModel> selectedCollection = new List<CustomerMasterModel>();
//            string MmgData = string.Empty;
//            try
//            {
//                ReportModel ReportModel = new ReportModel();
//                //Api Section
//                HttpClientHandler clientHandler = new HttpClientHandler();
//                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

//                // Pass the handler to httpclient(from you are calling api)
//                HttpClient client = new HttpClient(clientHandler);
//                client.Timeout = TimeSpan.FromMinutes(10); // Set timeout to 10 minutes

//                var json = JsonConvert.SerializeObject(ReportModel);
//                string sSendData = SessoinClass.sBaseURL + SessoinClass.sMethodName + "zshipercust/shipercust?sap-client=900&saml2=disabled";
//                var request = new HttpRequestMessage
//                {
//                    Method = HttpMethod.Get,
//                    RequestUri = new Uri(SessoinClass.sBaseURL + SessoinClass.sMethodName + "zshipercust/shipercust?sap-client=900&saml2=disabled"),
//                    Content = new StringContent(json, Encoding.UTF8, "application/json")
//                };
//                //request.Headers.Add("Authorization", "Basic YWFiYXBlcjpqYWlob21haGFkZXY=");
//                request.Headers.Add("Authorization", "Basic c2RtYXN0ZXI6YWt1bXNAMzIx=");
//                //SessoinClass.objLogs.WriteLog("Downloaded SAP Data Send Data:" + sSendData, DTPLLogs.DTPLLogsWrite.LogType.Information, MethodBase.GetCurrentMethod());
//                var response = client.SendAsync(request).ConfigureAwait(false);
//                var responseInfo = response.GetAwaiter().GetResult();
//                if (responseInfo.IsSuccessStatusCode)
//                {
//                    var content1 = responseInfo.Content.ReadAsStringAsync().Result;

//                    var CustData = JsonConvert.DeserializeObject<List<SyncCustomerMasterModel>>(content1);
//                    //SessoinClass.objLogs.WriteLog("Downloaded SAP Data :" + content1, DTPLLogs.DTPLLogsWrite.LogType.Information, MethodBase.GetCurrentMethod());
//                    selectedCollection1 = (List<SyncCustomerMasterModel>)CustData;
//                    DataTable dtItemList = ListToDatatable.ToDataTable(selectedCollection1);
//                    if (dtItemList.Rows.Count > 0)
//                    {
//                        selectedCollection = (from DataRow dr in dtItemList.Rows
//                                              select new CustomerMasterModel()
//                                              {
//                                                  CustomerCode = Convert.ToString(dr["KUNNR"]),
//                                                  CustomerName = Convert.ToString(dr["NAME1"]),
//                                                  CustomerName2 = Convert.ToString(dr["NAME2"]),
//                                                  Address1 = Convert.ToString(dr["STREET"]),
//                                                  Address2 = Convert.ToString(dr["STR_SUPPL2"]),
//                                                  Address3 = Convert.ToString(dr["STR_SUPPL3"]),
//                                                  City = Convert.ToString(dr["CITY1"]),
//                                                  PostalCode = Convert.ToString(dr["POST_CODE1"])
//                                              }).ToList();
//                    }
//                    DL_SyncProcessData objdl_ItemMaster = new DL_SyncProcessData();
//                    ItemMaster objItemMaster = new ItemMaster();
//                    dtItemList = selectedCollection.ToDataTable();
//                    //objItemMaster.SiteCode = HttpContext.Session.GetString("SiteCode");
//                    //objItemMaster.CreatedBy = HttpContext.Session.GetString("userId");
//                    DataTable dt = objdl_ItemMaster.UploadCustomerData(objItemMaster, dtItemList);
//                    if (dt.Rows.Count > 0)
//                    {
//                        MmgData = Convert.ToString(dt.Rows[0][0].ToString());
//                    }
//                    else
//                    {
//                        MmgData = "ERROR~Unable to save data, Please try again";
//                    }
//                    return (selectedCollection);

//                }

//            }
//            catch (Exception ex)
//            {
//               // SessoinClass.objLogs.WriteLog(ex.Message, DTPLLogs.DTPLLogsWrite.LogType.Error, MethodBase.GetCurrentMethod());
//            }
//            return (selectedCollection);
//        }
//    }
//}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AkumsAutoSync.Modals;
using AkumsAutoSync.BusinessLayer;
using AkumsAutoSync.DataLayer;

public static class SyncCustomerMasterData
{
    public static async Task<List<CustomerMasterModel>> DownloadCustomerData()
    {
        List<SyncCustomerMasterModel> selectedCollection1 = new List<SyncCustomerMasterModel>();
        List<CustomerMasterModel> selectedCollection = new List<CustomerMasterModel>();
        string MmgData = string.Empty;

        try
        {
            ReportModel ReportModel = new ReportModel();

            // HttpClient setup with handler for certificate validation
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using HttpClient client = new HttpClient(clientHandler)
            {
                Timeout = TimeSpan.FromMinutes(10) // Set timeout to 10 minutes
            };

            string sSendData = SessoinClass.sBaseURL + SessoinClass.sMethodName + "customerdata";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(sSendData),
            };

            request.Headers.Add("Authorization", "Basic c2ItZmNjNDQyODktNzU5MS00NDI3LWJkMzQtYzIzYTFiOGFjMjgwIWI0NDE5MXxpdC1ydC1jcGlwcm9kLXd6MXl4d3QyIWIxNDg6N2Q1Nzc5MzktNTU4My00YjViLTg3YTQtYmIxNDAyNWM2NDQ1JFhPLWNHbUVkb05nOVFKQl9lUS1tUWVrQy1JdnNFUkpKY3BzVXVzMnlIVVU9");

            // Send the request and get response
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string content1 = await response.Content.ReadAsStringAsync();
                var CustData = JsonConvert.DeserializeObject<List<SyncCustomerMasterModel>>(content1);
                selectedCollection1 = CustData ?? new List<SyncCustomerMasterModel>();

                DataTable dtItemList = ListToDatatable.ToDataTable(selectedCollection1);
                if (dtItemList.Rows.Count > 0)
                {
                    selectedCollection = (from DataRow dr in dtItemList.Rows
                                          select new CustomerMasterModel()
                                          {
                                              CustomerCode = Convert.ToString(dr["KUNNR"]),
                                              CustomerName = Convert.ToString(dr["NAME1"]),
                                              CustomerName2 = Convert.ToString(dr["NAME2"]),
                                              Address1 = Convert.ToString(dr["STREET"]),
                                              Address2 = Convert.ToString(dr["STR_SUPPL2"]),
                                              Address3 = Convert.ToString(dr["STR_SUPPL3"]),
                                              City = Convert.ToString(dr["CITY1"]),
                                              PostalCode = Convert.ToString(dr["POST_CODE1"])
                                          }).ToList();
                }

                DL_SyncProcessData objdl_ItemMaster = new DL_SyncProcessData();
                ItemMaster objItemMaster = new ItemMaster();
                DataTable dt = objdl_ItemMaster.UploadCustomerData(objItemMaster, selectedCollection.ToDataTable());
                if (dt.Rows.Count > 0)
                {
                    MmgData = Convert.ToString(dt.Rows[0][0].ToString());
                }
                else
                {
                    MmgData = "ERROR~Unable to save data, Please try again";
                }
                Console.WriteLine("Data synchronized successfully for Customer Data at " + DateTime.Now);
            }
            else
            {
                Console.WriteLine("Failed to download customer data: " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);

        }

        return selectedCollection;
    }
}

