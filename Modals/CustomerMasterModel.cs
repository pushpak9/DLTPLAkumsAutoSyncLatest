using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.Modals
{
    public class CustomerMasterModel
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerName2 { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Address3 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }

    public class SyncCustomerMasterModel
    {
        public string KUNNR { get; set; } = string.Empty;
        public string NAME1 { get; set; } = string.Empty;
        public string NAME2 { get; set; } = string.Empty;
        public string STREET { get; set; } = string.Empty;
        public string STR_SUPPL2 { get; set; } = string.Empty;
        public string STR_SUPPL3 { get; set; } = string.Empty;
        public string CITY1 { get; set; } = string.Empty;
        public string POST_CODE1 { get; set; } = string.Empty;
    }
}
