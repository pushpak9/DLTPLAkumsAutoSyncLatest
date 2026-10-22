using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.Modals
{
    public class DispatchMasterDownload
    {
        public string Delivery { get; set; } = string.Empty;
        public string Delivery_Date { get; set; } = string.Empty;
        public string Material_Code { get; set; } = string.Empty;
        public string Material_Name { get; set; } = string.Empty;
        public string Customer_Code { get; set; } = string.Empty;
        public string Sales_quantity { get; set; } = string.Empty;
        public string Sales_Unit { get; set; } = string.Empty;
        public string No_of_Cases_Shipper { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        //public string PlantCode { get; set; } = string.Empty;
    }

    public class SyncDispatchMasterDownload
    {
        public string VBELN { get; set; } = string.Empty;
        public string ERDAT { get; set; } = string.Empty;
        public string MATNR { get; set; } = string.Empty;
        public string MAKTX { get; set; } = string.Empty;
        public string KUNNR { get; set; } = string.Empty;
        public string KWMENG { get; set; } = string.Empty;
        public string VRKME { get; set; } = string.Empty;
        public string ZNO_CASES { get; set; } = string.Empty;
        public string CHARG { get; set; } = string.Empty;
        public string WERKS { get; set; } = string.Empty;
    }

    public class DeliveryOrder
    {
        public string ProductCode { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int ShipperCount { get; set; }
        public int ScanCount { get; set; }
        public int RemCount { get; set; }
    }
    public class UserSiteMaster
    {
        public int ID { get; set; }
        public string SiteCode { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public string AssignBy { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
