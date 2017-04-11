using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Xml;

namespace Tracker_History.Binders {
   public class XmlDocumentFromBodyParameterBinding : HttpParameterBinding {
      public XmlDocumentFromBodyParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor) {

      }

      public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken) {
         var binding = actionContext.ActionDescriptor.ActionBinding;

         if (binding.ParameterBindings.Length > 1 || actionContext.Request.Method == HttpMethod.Get)
            return Helpers.EmptyTask.Start();

         var type = binding.ParameterBindings[0].Descriptor.ParameterType;

         if (type == typeof(XmlDocument)) {
            return actionContext.Request.Content
                 .ReadAsStreamAsync()
                 .ContinueWith(task => {
                    XmlDocument doc = new XmlDocument();
                    // Load the XML from the request stream. Assumes the whole of the body is an XML document.
                    doc.Load(task.Result);
                    SetValue(actionContext, doc);
                 });
         }

         throw new InvalidOperationException("Only XmlDocument is supported for [XmlDocumentFromBody] parameters");
      }

      public override bool WillReadBody {
         get {
            return true;
         }
      }
   }
}