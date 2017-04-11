using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tracker_History.Controllers {
   // Simple controller so that the Tracker application can check it can connect to this web.
   public class PingController : ApiController {
      public IHttpActionResult Get() {
         return Ok();
      }
   }
}
