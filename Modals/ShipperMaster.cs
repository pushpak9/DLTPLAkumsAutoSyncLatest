using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AkumsAutoSync.Modals
{
    public class ShipperMaster
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        public DateTime? MfgDate { get; set; }
        public DateTime? ExpDate { get; set; }
        public decimal BatchSize { get; set; } = 0;
        public string BatchUnit { get; set; } = string.Empty;
        public decimal mrpValue { get; set; } = 0;
        public string mrpUnit { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Plant { get; set; } = string.Empty;
        public string ProcessOrder { get; set; } = string.Empty;
        public DateTime? PackingDate { get; set; }
        public string PackingType { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string SalesOrderNo { get; set; } = string.Empty;
        public decimal SalesOrderItem { get; set; } = 0;
        public string GenricField { get; set; } = string.Empty;
    }

    public class SyncShipperMasterModel
    {
        public string MATNR { get; set; } = string.Empty;
        public string MAKTX { get; set; } = string.Empty;
        public string CHARG { get; set; } = string.Empty;
        public string DWERK { get; set; } = string.Empty;
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? HSDAT { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? VFDAT { get; set; }

        public decimal GAMNG { get; set; } = 0;
        public string GMEIN { get; set; } = string.Empty;
        public decimal MRPRT { get; set; } = 0;
        public string MRPUT { get; set; } = string.Empty;
        public string GROES { get; set; } = string.Empty;
        public string WERKS { get; set; } = string.Empty;
        public string AUFNR { get; set; } = string.Empty;

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? ERDAT { get; set; }
        public string MAGRV { get; set; } = string.Empty;
        public string KUNNR { get; set; } = string.Empty;
        public string KDAUF { get; set; } = string.Empty;
        public decimal KDPOS { get; set; } = 0;

        public string STKTX { get; set; } = string.Empty;
    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.String)
            {
                var dateStr = reader.Value.ToString();
                if (DateTime.TryParseExact(dateStr, DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var date))
                {
                    return date;
                }

                // Handle other date formats or return null if parsing fails
                return null;
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
