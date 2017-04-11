using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Tracker_History.MediaTypeFormatters {
   public class PlainTextMediaFormatter : BufferedMediaTypeFormatter {
      public PlainTextMediaFormatter() {
         SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
      }

      public override bool CanReadType(Type type) {
         return false;//type == typeof(string);
      }

      public override bool CanWriteType(Type type) {
         return type == typeof(string);
      }

      public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content) {
         using(var writer = new StreamWriter(writeStream)) {
            writer.Write(value.ToString());
            writer.Flush();
         }
      }

      public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken) {
         using(var writer = new StreamWriter(writeStream)) {
            writer.Write(value.ToString());
            writer.Flush();
         }
      }

      public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger) {
         return base.ReadFromStream(type, readStream, content, formatterLogger);
      }

      public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken) {
         return base.ReadFromStream(type, readStream, content, formatterLogger, cancellationToken);
      }
   }
}