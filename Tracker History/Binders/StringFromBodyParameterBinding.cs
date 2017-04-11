using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Tracker_History.Binders {
   /// <summary>
   /// Reads the Request body into a string and assigns it to the parameter bound.
   /// 
   /// Should only be used with a single parameter on a Web API method using 
   /// the [StringFromBody] attribute
   /// </summary>
   public class StringFromBodyParameterBinding : HttpParameterBinding {
         public StringFromBodyParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor) {

         }

         public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken) {
            var binding = actionContext.ActionDescriptor.ActionBinding;

            if (binding.ParameterBindings.Length > 1 || actionContext.Request.Method == HttpMethod.Get)
               return Helpers.EmptyTask.Start();

            var type = binding.ParameterBindings[0].Descriptor.ParameterType;

            if (type == typeof(string)) {
               return actionContext.Request.Content
                       .ReadAsStringAsync()
                       .ContinueWith((task) => {
                          var stringResult = task.Result;
                          SetValue(actionContext, stringResult);
                       });
            }
            /*else if (type == typeof(byte[])) {
               return actionContext.Request.Content
                   .ReadAsByteArrayAsync()
                   .ContinueWith((task) => {
                      byte[] result = task.Result;
                      SetValue(actionContext, result);
                   });
            }*/

            throw new InvalidOperationException("Only string is supported for [StringFromBody] parameters");
         }

         public override bool WillReadBody {
            get {
               return true;
            }
         }
   }
}