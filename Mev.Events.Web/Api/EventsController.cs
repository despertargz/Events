using AttributeRouting.Web.Http;
using Mev.Events.Lib;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Mev.Events.Web.Api
{
    public class EventsController : ApiController
    {
        [GET("events")]
        public object GetEvents()
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("testdb");
            var collection = database.GetCollection("stuff");

            var events = collection.FindAllAs<Event>();

            return new {
                data = events.ToArray()
            };
        }
    }
}