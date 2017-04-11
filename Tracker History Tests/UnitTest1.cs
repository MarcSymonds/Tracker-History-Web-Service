using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Net.Http;
using System.Data;

using MySql.Data.MySqlClient;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Tracker_History.Classes;
using Tracker_History.Database;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;

namespace Tracker_History_Tests {
   [TestClass]
   public class UnitTest1 {
      private TestContext testContextInstance;
      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
      public TestContext TestContext {
         get {
            return testContextInstance;
         }
         set {
            testContextInstance = value;
         }
      }

      private string getTestData() {
         return @"<TrackerHistory>
      <Application guid=""c8eb0580-e362-410a-a69a-06fc0f520713"" xname=""MyPhone"" imei=""123456"" />
      <TrackerDevice guid = ""3f0566b4-caad-450c-bbe2-f1bd4763d802"" name=""Kimmy the dog"" imei=""838729109381938"" telephoneNumber=""1234"" isMyLocation=""true"">
         <History whenRecorded=""2017-03-02T12:30:30"" latitude=""52.1233450"" longitude=""-9.3729272"" isGps=""true"" />
         <History whenRecorded=""2017-03-02T12:31:30"" latitude=""52.1233451"" longitude=""-9.3729274"" isGps=""true"" />
         <History whenRecorded=""2017-03-02T12:32:30"" latitude=""52.1233452"" longitude=""-9.3729278"" isGps=""false"" />
         <History whenRecorded=""2017-03-02T12:33:30"" latitude=""52.1233453"" longitude=""-9.3729284"" isGps=""true"" />
      </TrackerDevice>

      <TrackerDevice guid=""5cfd81c2-198d-4940-afe0-2d65a5fd7980"" name=""Shadow the cat"" imei=""837291839100439"" telephoneNumber=""1234"" isMyLocation=""false"">
         <History whenRecorded=""2017-03-05T12:30:30"" latitude=""52.1233446"" longitude=""-9.3729272"" isGps=""true"" />
         <History whenRecorded=""2017-03-05T12:35:30"" latitude=""52.1233447"" longitude=""-9.3729271"" isGps=""true"" />
         <History whenRecorded=""2017-03-05T12:40:30"" latitude=""52.1233448"" longitude=""-9.3729270"" isGps=""true"" />
         <History whenRecorded=""2017-03-05T12:45:30"" latitude=""52.1233449"" longitude=""-9.3729269"" isGps=""true"" />
      </TrackerDevice>
   </TrackerHistory>";

      }

      [TestMethod]
      public void TestTrackerHistoryClass() {
         XmlSerializer serializer = new XmlSerializer(typeof(TrackerHistory));

         string testData = getTestData();

         using (TextReader reader = new StringReader(testData)) {
            TrackerHistory result = (TrackerHistory)serializer.Deserialize(reader);
            if (result == null) {
               TestContext.WriteLine("NULL result");
            }
            else {
               if (result.Application == null) {
                  TestContext.WriteLine("Application is NULL");
               }
               else {
                  TestContext.WriteLine(result.Application.ToString());

                  foreach (TrackerHistoryTrackerDevice td in result.TrackerDevice) {
                     TestContext.WriteLine(td.ToString());

                     foreach (TrackerHistoryTrackerDeviceHistory th in td.History) {
                        TestContext.WriteLine(th.ToString());
                     }
                  }
               }
            }
         }
      }

      [TestMethod]
      public void TestUpdateApplication() {
         XmlSerializer serializer = new XmlSerializer(typeof(TrackerHistory));

         string testData = getTestData();

         using (TextReader reader = new StringReader(testData)) {
            TrackerHistory result = (TrackerHistory)serializer.Deserialize(reader);

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["TrackerDB"].ConnectionString);
            MySqlTransaction tran = null;
            try {
               conn.Open();
               tran = conn.BeginTransaction();

               TrackerHistoryUpdate upd = new TrackerHistoryUpdate(conn, tran);
               int id = upd.UpdateApplication(result.Application);

               foreach (TrackerHistoryTrackerDevice td in result.TrackerDevice) {
                  int did = upd.UpdateTrackerDevice(id, td);

                  foreach(TrackerHistoryTrackerDeviceHistory dh in td.History) {
                     int hid = upd.AddTrackerHistory(did, dh);
                  }
               }


               tran.Commit();

               TestContext.WriteLine("ID = {0}", id);
               

            }
            catch(Exception ex) {
               TestContext.WriteLine(ex.ToString());

               if (tran != null) {
                  tran.Rollback();
                  tran = null;
               }
               conn.Close();
               conn.Dispose();
            }





         }
      }

      private HttpClient httpClient;

      [TestMethod]
      public void TestWebAPIPing() {
         httpClient = new HttpClient();

         httpClient.BaseAddress = new Uri("http://localhost:8080");
         httpClient.DefaultRequestHeaders.Accept.Clear();

         //MemoryStream m = null;
         //StreamContent s = new StreamContent(m);

         PingAsync().Wait();
         PingAsync().Wait();
         PingAsync().Wait();
      }

      private async Task PingAsync() {
         //HttpResponseMessage response = await PingServer();
         HttpResponseMessage response = await httpClient.GetAsync("TrackerHistory/api/Ping");

         Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Status not 200");
      }

      /*private async Task<HttpResponseMessage> PingServer() {
         HttpResponseMessage response = await httpClient.GetAsync("TrackerHistory/api/Ping");

         return response;
      }*/

      [TestMethod]
      public void TestWebAPISaveHistory() {
         httpClient = new HttpClient();

         httpClient.BaseAddress = new Uri("http://localhost:8080");
         httpClient.DefaultRequestHeaders.Accept.Clear();
         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

         string data = getTestData();

         SaveHistory(data).Wait();
      }

      private async Task SaveHistory(string data) {
         StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/xml");
         
         HttpResponseMessage response = await httpClient.PostAsync("TrackerHistory/api/TrackerHistory", content);

         Assert.AreEqual(response.StatusCode, HttpStatusCode.Accepted, "Not Accepted - " + response.ToString());
      }
   }
}
