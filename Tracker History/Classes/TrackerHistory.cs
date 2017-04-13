using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
   <TrackerHistory>
      <Application guid="1234-5678" name="MyPhone" imei="123456" />
      <TrackerDevice guid="1234-9876" name="Tracker Device" imei="123987" telephoneNumber="1234" isMyLocation="false">
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" >
            <Additional lac="1234" cid="1234" lastKnownLongitude="1.012" lastKnownLatitude="2.12" message="Hello World" />
            </History>

         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
      </TrackerDevice>

         <TrackerDevice guid="1234-9876" name="Tracker Device" imei="123987" telephoneNumber="1234" isMyLocation="false">
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
         <History whenRecorded="2017-03-01 12:30:30" latitude="52.1233445" longitude="-9.3729272" isGps="true" />
      </TrackerDevice>
   </TrackerHistory>
 */

namespace Tracker_History.Classes {
   // Created with "Paste Special/Paste XML as Classes".


   /// <remarks/>
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
   public partial class TrackerHistory {

      private TrackerHistoryApplication applicationField;
      private TrackerHistoryTrackerDevice[] trackerDeviceField;

      /// <remarks/>
      public TrackerHistoryApplication Application {
         get {
            return this.applicationField;
         }
         set {
            this.applicationField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("TrackerDevice")]
      public TrackerHistoryTrackerDevice[] TrackerDevice {
         get {
            return this.trackerDeviceField;
         }
         set {
            this.trackerDeviceField = value;
         }
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   public partial class TrackerHistoryApplication {
      private string guidField;
      private string nameField;
      private string imeiField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string guid {
         get {
            return this.guidField;
         }
         set {
            this.guidField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string name {
         get {
            return this.nameField;
         }
         set {
            this.nameField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string imei {
         get {
            return this.imeiField;
         }
         set {
            this.imeiField = value;
         }
      }

      public override string ToString() {
         return string.Format("Application - GUID: {0}, Name: {1}, IMEI: {2}",
            guidField ?? "NULL",
            nameField ?? "NULL",
            imeiField ?? "NULL");
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   public class TrackerHistoryTrackerDevice {

      private TrackerHistoryTrackerDeviceHistory[] historyField;
      private string guidField;
      private string nameField;
      private string imeiField;
      private string telephoneNumberField;
      private bool isMyLocationField;

      public override string ToString() {
         return string.Format("Tracker Device - GUID: {0}, Name: {1}, IMEI: {2}, Telephone number: {3}, Is my location: {4}",
            guidField ?? "NULL",
            nameField ?? "NULL",
            imeiField ?? "NULL",
            telephoneNumberField ?? "NULL",
            isMyLocationField);
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlElementAttribute("History")]
      public TrackerHistoryTrackerDeviceHistory[] History {
         get {
            return this.historyField;
         }
         set {
            this.historyField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string guid {
         get {
            return this.guidField;
         }
         set {
            this.guidField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string name {
         get {
            return this.nameField;
         }
         set {
            this.nameField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string imei {
         get {
            return this.imeiField;
         }
         set {
            this.imeiField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string telephoneNumber {
         get {
            return this.telephoneNumberField;
         }
         set {
            this.telephoneNumberField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public bool isMyLocation {
         get {
            return this.isMyLocationField;
         }
         set {
            this.isMyLocationField = value;
         }
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   public class TrackerHistoryTrackerDeviceHistory {
      private TrackerHistoryTrackerDeviceHistoryAdditional additionalField;
      private DateTime whenRecordedField;
      private double latitudeField;
      private double longitudeField;
      private bool isGpsField;

      public override string ToString() {
         return string.Format("Tracker History - When recorded: {0:dd/MM/yyyy HH:mm:ss}, Lat: {1}, Long: {2}, From GPS: {3}",
            whenRecordedField,
            latitudeField,
            longitudeField,
            isGpsField
            );
      }

      /// <remarks/>
      public TrackerHistoryTrackerDeviceHistoryAdditional Additional {
         get {
            return this.additionalField;
         }
         set {
            this.additionalField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public DateTime whenRecorded {
         get {
            return this.whenRecordedField;
         }
         set {
            this.whenRecordedField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public double latitude {
         get {
            return this.latitudeField;
         }
         set {
            this.latitudeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public double longitude {
         get {
            return this.longitudeField;
         }
         set {
            this.longitudeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public bool isGps {
         get {
            return this.isGpsField;
         }
         set {
            this.isGpsField = value;
         }
      }
   }

   /// <remarks/>
   [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
   public partial class TrackerHistoryTrackerDeviceHistoryAdditional {
      private string lacField;
      private string cidField;
      private double lastKnownLongitudeField;
      private double lastKnownLatitudeField;
      private string messageField;

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string lac {
         get {
            return this.lacField;
         }
         set {
            this.lacField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string cid {
         get {
            return this.cidField;
         }
         set {
            this.cidField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public double lastKnownLongitude {
         get {
            return this.lastKnownLongitudeField;
         }
         set {
            this.lastKnownLongitudeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public double lastKnownLatitude {
         get {
            return this.lastKnownLatitudeField;
         }
         set {
            this.lastKnownLatitudeField = value;
         }
      }

      /// <remarks/>
      [System.Xml.Serialization.XmlAttributeAttribute()]
      public string message {
         get {
            return this.messageField;
         }
         set {
            this.messageField = value;
         }
      }
   }
}
