using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using Tracker_History.Classes;

namespace Tracker_History.Database {
   /// <summary>
   /// Class for updating tracker history in the DB.
   /// </summary>
   public class TrackerHistoryUpdate {
      private MySqlConnection dbConn;
      private MySqlTransaction dbTrans;

      private MySqlCommand cmdUpdateApplication = null;
      private MySqlCommand cmdUpdateTrackerDevice = null;
      private MySqlCommand cmdAddTrackerHistory = null;

      public TrackerHistoryUpdate(MySqlConnection conn, MySqlTransaction trans) {
         dbConn = conn;
         dbTrans = trans;
      }

      /// <summary>
      /// Builds a MySqlCommand object that calls the "update_application"
      /// stored procedure.
      /// 
      /// This stored procedure inserts or updates a record in the
      /// "tracker_application" table.
      /// </summary>
      /// <returns>MySqlCommand object that can be used to execute the stored
      /// procedure</returns>
      private MySqlCommand buildUpdateApplicationCmd() {
         if (cmdUpdateApplication == null) {
            cmdUpdateApplication = new MySqlCommand("update_application", dbConn, dbTrans);
            cmdUpdateApplication.CommandType = CommandType.StoredProcedure;

            cmdUpdateApplication.Parameters.Add("$guid", MySqlDbType.VarChar);
            cmdUpdateApplication.Parameters.Add("$name", MySqlDbType.VarChar);
            cmdUpdateApplication.Parameters.Add("$imei", MySqlDbType.VarChar);

            cmdUpdateApplication.Parameters.Add("$id", MySqlDbType.Int32).Direction = ParameterDirection.Output;
         }

         return cmdUpdateApplication;
      }

      private MySqlCommand buildUpdateTrackerDeviceCmd() {
         if (cmdUpdateTrackerDevice == null) {
            cmdUpdateTrackerDevice = new MySqlCommand("update_tracker_device", dbConn, dbTrans);
            cmdUpdateTrackerDevice.CommandType = CommandType.StoredProcedure;

            cmdUpdateTrackerDevice.Parameters.Add("$application_id", MySqlDbType.Int32);
            cmdUpdateTrackerDevice.Parameters.Add("$is_my_location", MySqlDbType.Bit);
            cmdUpdateTrackerDevice.Parameters.Add("$guid", MySqlDbType.VarChar);
            cmdUpdateTrackerDevice.Parameters.Add("$name", MySqlDbType.VarChar);
            cmdUpdateTrackerDevice.Parameters.Add("$imei", MySqlDbType.VarChar);
            cmdUpdateTrackerDevice.Parameters.Add("$telephone_number", MySqlDbType.VarChar);

            cmdUpdateTrackerDevice.Parameters.Add("$id", MySqlDbType.Int32).Direction = ParameterDirection.Output;
         }

         return cmdUpdateTrackerDevice;
      }

      private MySqlCommand buildAddTrackerHistoryCmd() {
         if (cmdAddTrackerHistory == null) {
            cmdAddTrackerHistory = new MySqlCommand("add_tracker_history", dbConn, dbTrans);
            cmdAddTrackerHistory.CommandType = CommandType.StoredProcedure;

            cmdAddTrackerHistory.Parameters.Add("$device_id", MySqlDbType.Int32);
            cmdAddTrackerHistory.Parameters.Add("$time_recorded", MySqlDbType.DateTime);
            cmdAddTrackerHistory.Parameters.Add("$longitude", MySqlDbType.Double);
            cmdAddTrackerHistory.Parameters.Add("$latitude", MySqlDbType.Double);
            cmdAddTrackerHistory.Parameters.Add("$gps", MySqlDbType.Bit);

            cmdAddTrackerHistory.Parameters.Add("$id", MySqlDbType.Int32).Direction = ParameterDirection.Output;
         }
         return cmdAddTrackerHistory;
      }

      public int UpdateApplication(TrackerHistoryApplication application) {
         MySqlCommand cmd = cmdUpdateApplication ?? buildUpdateApplicationCmd();

         cmd.Parameters["$guid"].Value = application.guid;
         cmd.Parameters["$name"].Value = application.name;
         cmd.Parameters["$imei"].Value = application.imei;

         cmd.ExecuteNonQuery();
         
         return (int)cmd.Parameters["$id"].Value;
      }

      public int UpdateTrackerDevice(int applicationID, TrackerHistoryTrackerDevice device) {
         MySqlCommand cmd = cmdUpdateTrackerDevice ?? buildUpdateTrackerDeviceCmd();

         cmd.Parameters["$application_id"].Value = applicationID;
         cmd.Parameters["$is_my_location"].Value = device.isMyLocation ? 1 : 0;
         cmd.Parameters["$guid"].Value = device.guid;
         cmd.Parameters["$name"].Value = device.name;
         cmd.Parameters["$imei"].Value = device.imei;
         cmd.Parameters["$telephone_number"].Value = device.telephoneNumber;

         cmd.ExecuteNonQuery();

         return (int)cmd.Parameters["$id"].Value;
      }

      public int AddTrackerHistory(int deviceID, TrackerHistoryTrackerDeviceHistory history) {
         MySqlCommand cmd = cmdAddTrackerHistory ?? buildAddTrackerHistoryCmd();

         cmd.Parameters["$device_id"].Value = deviceID;
         cmd.Parameters["$time_recorded"].Value = history.whenRecorded;
         cmd.Parameters["$longitude"].Value = history.longitude;
         cmd.Parameters["$latitude"].Value = history.latitude;
         cmd.Parameters["$gps"].Value = history.isGps ? 1 : 0;

         cmd.ExecuteNonQuery();

         return (int)cmd.Parameters["$id"].Value;
      }
   }
}