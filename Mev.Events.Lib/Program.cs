using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mev.Events.Lib
{
    class Program
    {
        static void Main()
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("testdb");
            var collection = database.GetCollection("stuff");




            var events = collection.FindAll();
            

            //Event myEvent = new Event()
            //{
            //    Created = DateTime.Now,
            //    Description = "this is a description",
            //    Priority = Priority.VeryHigh,
            //    Status = Status.Working,
            //    Subject = "this is a subject",
            //    Updated = DateTime.Now.AddSeconds(5),
            //    Id = 3
            //};

            //myEvent.Comments = new List<Comment>();
            //myEvent.Comments.Add(new Comment() { Message = "hey!", Time = DateTime.Now });

            //myEvent.Data = new ExpandoObject();
            //myEvent.Data.place = "steak n shake";
            //myEvent.Data.cost = "$100";

            //var one = collection.FindOneByIdAs<Event>(1);
            //one.Subject = "One";
            

            //collection.FindOneByIdAs<Event>(2).Subject = "Two";
            //collection.FindOneByIdAs<Event>(3).Subject = "Three";

            Console.WriteLine("!");
            Console.ReadLine();

        }
    }
}
