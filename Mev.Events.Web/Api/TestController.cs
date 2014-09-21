using AttributeRouting.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Mev.Events.Web.Api
{
    public class DataController : ApiController
    {
        [GET("data")]
        public object GetData()
        {
            return new {
                data = new[] {
                    new { first = "benjamin", last = "mevissen" },
                    new { first = "christopher", last = "mevissen" }
                }
                
            };
        }
    }
}