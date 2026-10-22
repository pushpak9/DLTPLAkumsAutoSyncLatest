using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.Modals
{
    public class ReportModel
    {
        public string SerialNo { get; set; } = string.Empty;
        public string AssetNo { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string SiteCode { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string ItemDesc { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string UserID { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string PLANT { get; set; } = string.Empty;
        public string CompleteReport { get; set; } = "0";
    }
    public class InventoryReceivingReportModel
    {
        public string? SiteCode { get; set; } = string.Empty;
        public string? ItemCode { get; set; } = string.Empty;
        public string? SONo { get; set; } = string.Empty;
        public string? SalesPerson { get; set; } = string.Empty;
        public string? CourierDetails { get; set; } = string.Empty;
        public string? PONO { get; set; } = string.Empty;
        public string? InvoiceNo { get; set; } = string.Empty;
        public string? ItemBarcode { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
        public string? ItemStatus { get; set; } = string.Empty;
        public string? ReceivedBy { get; set; } = string.Empty;
        public DateTime? ReceivedOn { get; set; }
        public string? DispatchedBy { get; set; } = string.Empty;
        public DateTime? DispatchedOn { get; set; }
        public string? Result { get; set; } = string.Empty;
        public string? CustName { get; set; } = string.Empty;
    }

    public class MasterModelData
    {
        public string UserID { get; set; } = string.Empty;
        public string SiteCode { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public int COUNTRECORD { get; set; } = 0;
        public int CUST_COUNTRECORD { get; set; } = 0;
        public int ITEM_COUNTRECORD { get; set; } = 0;
        public string MatType { get; set; } = string.Empty;
        public int ITEM_REC_COUNTRECORD { get; set; } = 0;
        public string Result { get; set; } = string.Empty;
    }

}
