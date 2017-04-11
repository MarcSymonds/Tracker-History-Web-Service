using System;
using System.Configuration;
using System.Net;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

using MySql.Data.MySqlClient;

using Tracker_History.Attributes;
using Tracker_History.Classes;
using Tracker_History.Database;
using Tracker_History.MediaTypeFormatters;

namespace Tracker_History.Controllers {
   public class TrackerHistoryController : ApiController {

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public IHttpActionResult Post([XmlDocumentFromBody] XmlDocument value) {
         try {
            XmlSerializer serializer = new XmlSerializer(typeof(TrackerHistory));

            using (XmlNodeReader reader = new XmlNodeReader(value.DocumentElement)) {
               TrackerHistory trackerHistory = (TrackerHistory)serializer.Deserialize(reader);

               if (trackerHistory == null || trackerHistory.Application == null) {
                  return Content(HttpStatusCode.BadRequest, "Invalid message format - missing root and/or application.");
               }

               UpdateDatabaseWithHistory(trackerHistory);
            };

            return StatusCode(HttpStatusCode.Accepted);
         }
         catch (Exception ex) {
            return Content(HttpStatusCode.InternalServerError, ex.ToString(), new PlainTextMediaFormatter(), "text/plain");
         }
      }

      private void UpdateDatabaseWithHistory(TrackerHistory trackerHistory) {
         using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["TrackerDB"].ConnectionString)) {
            conn.Open();
            try {
               using (MySqlTransaction trans = conn.BeginTransaction()) {
                  try {
                     TrackerHistoryUpdate updater = new TrackerHistoryUpdate(conn, trans);

                     int appid = updater.UpdateApplication(trackerHistory.Application);
                     if (trackerHistory.TrackerDevice != null) {
                        foreach (TrackerHistoryTrackerDevice trackerDevice in trackerHistory.TrackerDevice) {
                           int devid = updater.UpdateTrackerDevice(appid, trackerDevice);

                           if (trackerDevice.History != null) {
                              foreach (TrackerHistoryTrackerDeviceHistory deviceHistory in trackerDevice.History) {
                                 updater.AddTrackerHistory(devid, deviceHistory);
                              }
                           }

                        }
                     }

                     trans.Commit();
                  }
                  catch (Exception ex) {
                     trans.Rollback();
                     throw;
                  }
               }
            }
            finally {
               conn.Close();
            }
         }
      }
   }
}
