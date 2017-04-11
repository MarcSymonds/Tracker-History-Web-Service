using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Tracker_History.Attributes {
   /// <summary>
   /// An attribute that captures the entire content body and stores it
   /// into the parameter of type XmlDocument.
   /// </summary>
   /// <remarks>
   /// The parameter marked up with this attribute should be the only parameter as it reads the
   /// entire request body and assigns it to that parameter.    
   /// </remarks>
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
   public sealed class XmlDocumentFromBodyAttribute : ParameterBindingAttribute {
      public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter) {
         if (parameter == null)
            throw new ArgumentException("Invalid parameter");

         return new Binders.XmlDocumentFromBodyParameterBinding(parameter);
      }
   }
}