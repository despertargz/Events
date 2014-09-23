using AttributeRouting.Web.Http;
using Mev.Events.Lib;
using MongoDB.Bson;
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
        private MongoCollection<Event> GetCollection()
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("testdb");
            var collection = database.GetCollection<Event>("stuff");
            return collection;
        }

        [GET("events")]
        public object GetEvents()
        {
            MongoCollection<Event> collection = this.GetCollection();

            var result = new {
                data = collection.FindAll().ToArray()
            };

            return result;
            
        }

        [POST("events")]
        public void CreateEvent(NewEvent newEvent)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("testdb");
            var collection = database.GetCollection("stuff");

            collection.Insert<Event>(new Event()
            {
                Created = DateTime.Now,
                Description = newEvent.Description,
                Due = newEvent.Due,
                Priority = newEvent.Priority,
                Subject = newEvent.Subject,
                Status = Status.Pending,
                Updated = DateTime.Now,
            });
        }

        [POST("events/{id}/status")]
        public void SetStatus(string id, int status)
        {
            var col = this.GetCollection();
            var myEvent = col.FindOneById(new ObjectId(id));
            myEvent.Status = (Status)status;
            col.Save(myEvent);
        }

        [POST("events/{id}/priority")]
        public void SetStatus(string id, Priority priority)
        {
            var col = this.GetCollection();
            var myEvent = col.FindOneById(new ObjectId(id));
            myEvent.Priority = priority;
            col.Save(myEvent);
        }

        [POST("events/{id}/comments")]
        public void AddComment(string id, string comment)
        {
            var col = this.GetCollection();
            var myEvent = col.FindOneById(id);
            myEvent.Comments.Add(new Comment() { Time = DateTime.Now, Message = comment });
            col.Save(myEvent);
        }

        [POST("events/{id}/tags")]
        public void AddTag(string id, string tag)
        {
            var col = this.GetCollection();
            var myEvent = col.FindOneById(id);
            myEvent.Tags.Add(new Tag() { Name = tag });
            col.Save(myEvent);
        }

        [POST("events/{id}/data")]
        public void AddData(string id, string key, string value)
        {
            var col = this.GetCollection();
            var myEvent = col.FindOneById(id);
            myEvent.Data.key = value;
            col.Save(myEvent);
            
        }
    }

    public class NewEvent
    {
        public string Description { get; set; }
        public string Subject { get; set; }
        public DateTime? Due { get; set; }
        public Priority Priority { get; set; }
    }
    
}