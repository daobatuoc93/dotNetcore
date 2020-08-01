using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class ProducMode
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class MyDependency
    {
        public MyDependency()
        {
        }

        public Task WriteMessage(string message)
        {
            Console.WriteLine(
                $"MyDependency.WriteMessage called. Message: {message}");

            return Task.FromResult(0);
        }
    }
}
