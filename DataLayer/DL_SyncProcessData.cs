using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AkumsAutoSync.Modals;
using Microsoft.Data.SqlClient;

namespace AkumsAutoSync.DataLayer
{
    public class DL_SyncProcessData
    {
        Common obj = new Common();
        SqlCommand cmd = null;

        /// <summary>
        /// UploadProcessData
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public DataTable UploadProcessData(ItemMaster request, DataTable dtData)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "usp_Download_ProcessData";
                cmd.Parameters.AddWithValue("@type", "SAVEMATERIAL");
                cmd.Parameters.AddWithValue("@SITECODE", request.SiteCode);
                cmd.Parameters.AddWithValue("@DOWNLAODEDBY", request.CreatedBy);
                cmd.Parameters.AddWithValue("@DETAILS", dtData);
                DataSet ds = obj.ExecuteDataset(cmd);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                //Common.LogMessage(ex.Message + DateTime.Now);
                 throw ex;
            }
            return dt;
        }
        /// <summary>
        /// UploadCustomerData
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public DataTable UploadCustomerData(ItemMaster request, DataTable dtData)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "usp_Download_CustomerData";
                cmd.Parameters.AddWithValue("@type", "SAVECUSTOMER");
                cmd.Parameters.AddWithValue("@SITECODE", request.SiteCode);
                cmd.Parameters.AddWithValue("@DOWNLAODEDBY", request.CreatedBy);
                cmd.Parameters.AddWithValue("@DETAILS", dtData);
                DataSet ds = obj.ExecuteDataset(cmd);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                //Common.LogMessage(ex.Message + DateTime.Now);
                //Common.objLogs.WriteLog(ex.Message, DTPLLogs.DTPLLogsWrite.LogType.Error, MethodBase.GetCurrentMethod());
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// UploadDisptachData
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public DataTable UploadDisptachData(ItemMaster request, DataTable dtData)
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "usp_Download_DeliveryOrderData";
                cmd.Parameters.AddWithValue("@type", "SAVEMATERIAL");
                cmd.Parameters.AddWithValue("@SITECODE", request.SiteCode);
                cmd.Parameters.AddWithValue("@DOWNLAODEDBY", request.CreatedBy);
                cmd.Parameters.AddWithValue("@DETAILS", dtData);
                DataSet ds = obj.ExecuteDataset(cmd);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                //Common.LogMessage(ex.Message + DateTime.Now);
                //Common.objLogs.WriteLog(ex.Message, DTPLLogs.DTPLLogsWrite.LogType.Error, MethodBase.GetCurrentMethod());
                throw ex;
            }
            return dt;
        }


        public DataTable dtGetSite()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "USP_USERSITEMASTER";
                cmd.Parameters.AddWithValue("@type", "GETSITE");
                DataSet ds = obj.ExecuteDataset(cmd);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                //Common.LogMessage(ex.Message + DateTime.Now);
                throw ex;
            }
            return dt;
        }
    }
}
