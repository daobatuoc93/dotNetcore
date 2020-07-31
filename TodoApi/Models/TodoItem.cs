using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Service;
namespace TodoApi.Models
{
    public class TodoItem
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public long Diem { get; set; }

        public bool IsComplete { get; set; }
        public string Secret { get; set; }
        public string date { get; set; }
        public string From { get; set; }
        
    }
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
