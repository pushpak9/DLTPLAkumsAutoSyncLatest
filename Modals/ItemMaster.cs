using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.Modals
{
    public class ItemMaster
    {
        public int Id { get; set; } = 0;
        public string? SiteCode { get; set; } = string.Empty;
        public string? ItemCode { get; set; } = string.Empty;
        public string? ItemName { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? Result { get; set; } = string.Empty;

    }
}
