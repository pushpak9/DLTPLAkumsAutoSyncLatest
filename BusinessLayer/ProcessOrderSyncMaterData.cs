using AkumsAutoSync.DataLayer;
using AkumsAutoSync.Modals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.BusinessLayer
{
    public static class ProcessOrderSyncMaterData
    {
        public static async Task<List<ShipperMaster>> DownloadShipperMaterData()
        {
            List<SyncShipperMasterModel> selectedCollection1 = new List<SyncShipperMasterModel>();
            List<ShipperMaster> selectedCollection = new List<ShipperMaster>();
            string MmgData = string.Empty;
            try
            {
                ReportModel ReportModel = new ReportModel();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);
                var json = JsonConvert.SerializeObject(ReportModel);
                string sSendData = SessoinClass.sBaseURL + SessoinClass.sMethodName + "getPO";
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(SessoinClass.sBaseURL + SessoinClass.sMethodName + "getPO"),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                //request.Headers.Add("Authorization", "Basic YWFiYXBlcjpqYWlob21haGFkZXY=");
                request.Headers.Add("Authorization", "Basic c2ItZmNjNDQyODktNzU5MS00NDI3LWJkMzQtYzIzYTFiOGFjMjgwIWI0NDE5MXxpdC1ydC1jcGlwcm9kLXd6MXl4d3QyIWIxNDg6N2Q1Nzc5MzktNTU4My00YjViLTg3YTQtYmIxNDAyNWM2NDQ1JFhPLWNHbUVkb05nOVFKQl9lUS1tUWVrQy1JdnNFUkpKY3BzVXVzMnlIVVU9");
                //SessoinClass.objLogs.WriteLog("Downloaded SAP Data Send Data:" + sSendData, DTPLLogs.DTPLLogsWrite.LogType.Information, MethodBase.GetCurrentMethod());
                var response = client.SendAsync(request).ConfigureAwait(false);
                var responseInfo = response.GetAwaiter().GetResult();
                if (responseInfo.IsSuccessStatusCode)
                {
                    var content1 = responseInfo.Content.ReadAsStringAsync().Result;
                    //SessoinClass.objLogs.WriteLog("Downloaded SAP Data :" + content1, DTPLLogs.DTPLLogsWrite.LogType.Information, MethodBase.GetCurrentMethod());

                    var CustData = JsonConvert.DeserializeObject<List<SyncShipperMasterModel>>(content1);
                    selectedCollection1 = (List<SyncShipperMasterModel>)CustData;
                    //DataTable dtItemList = ListToDatatable.ToDataTable(selectedCollection1);                   
                    DataTable dtItemList = ListToDatatable.ToDataTable(selectedCollection1);
                    if (dtItemList.Rows.Count > 0)
                    {
                        selectedCollection = (from DataRow dr in dtItemList.Rows
                                              select new ShipperMaster()
                                              {
                                                  ProductCode = Convert.ToString(dr["MATNR"]),
                                                  ProductDescription = Convert.ToString(dr["MAKTX"]),
                                                  BatchNo = Convert.ToString(dr["CHARG"]),
                                                  MfgDate = dr["HSDAT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["HSDAT"]),
                                                  ExpDate = dr["VFDAT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["VFDAT"]),
                                                  BatchSize = dr["GAMNG"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["GAMNG"]),
                                                  BatchUnit = Convert.ToString(dr["GMEIN"]),
                                                  mrpValue = dr["MRPRT"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["MRPRT"]),
                                                  mrpUnit = Convert.ToString(dr["MRPUT"]),
                                                  Size = Convert.ToString(dr["GROES"]),
                                                  Plant = Convert.ToString(dr["DWERK"]),
                                                  ProcessOrder = Convert.ToString(dr["AUFNR"]),
                                                  PackingDate = dr["ERDAT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ERDAT"]),
                                                  PackingType = Convert.ToString(dr["MAGRV"]),
                                                  CustomerCode = Convert.ToString(dr["KUNNR"]),
                                                  SalesOrderNo = Convert.ToString(dr["KDAUF"]),
                                                  SalesOrderItem = dr["KDPOS"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["KDPOS"]),
                                                  GenricField = Convert.ToString(dr["STKTX"])
                                              }).ToList();
                    }
                    DL_SyncProcessData objdl_ItemMaster = new DL_SyncProcessData();
                    ItemMaster objItemMaster = new ItemMaster();
                    dtItemList = selectedCollection.ToDataTable();
                    //objItemMaster.SiteCode = HttpContext.Session.GetString("SiteCode");
                    //objItemMaster.CreatedBy = HttpContext.Session.GetString("userId");
                    DataTable dt = objdl_ItemMaster.UploadProcessData(objItemMaster, dtItemList);
                    if (dt.Rows.Count > 0)
                    {
                        MmgData = Convert.ToString(dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        MmgData = "ERROR~Unable to save data, Please try again";
                    }
                    return (selectedCollection);
                }
                else
                {
                    MmgData = "ERROR~Unable to save data, Please try again";
                }
            }
            catch (Exception ex)
            {

                //SessoinClass.objLogs.WriteLog(ex.Message, DTPLLogs.DTPLLogsWrite.LogType.Error, MethodBase.GetCurrentMethod());
            }
             return (selectedCollection);
        }

        
    }

    /// <summary>
    /// Kunal verma
    /// Created to convert DataTable to List
    /// </summary>
    //List To Data Table Class
    public static class ListToDatatable
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable ToDataTable1<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            for (int i = 0; i < pia.Length; i++)
            {
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[i].AllowDBNull = true;
            }

            //Populate the table
            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
