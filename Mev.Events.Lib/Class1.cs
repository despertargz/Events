using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mev.Events.Lib
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Due { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Alarm> Alarms { get; set; }
        public List<string> Tags { get; set; }

        public dynamic Data { get; set; }

        public Event()
	    {
            this.Comments = new List<Comment>();
            this.Alarms = new List<Alarm>();
            this.Tags = new List<string>();
            this.Data = new ExpandoObject();
	    }

        // children
        // order
        // group
    }

    public class Tag
    {
        public string Name { get; set; }
    }

    public class Alarm
    {
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime FireAt { get; set; }
    }

    public class Comment
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }

    public enum Status
    {
        Pending = 0,
        Working = 1,
        Completed = 2,
        Waiting = 3,
        Blocked = 4,
        Cancelled = 5
    }

    public enum Priority
    {
        VeryLow = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        VeryHigh = 5
    }
}
