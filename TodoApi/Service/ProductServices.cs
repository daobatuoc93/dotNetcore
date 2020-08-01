using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Service
{

    public class ProductServices : IProductServices
    {
        public IList<ProducMode> Getall()
        {
            return new List<ProducMode>
            {
                new ProducMode {ID = 1, Name = "Pen Drive" },
                new ProducMode {ID = 2, Name = "Memory Card" },
                new ProducMode {ID = 3, Name = "Mobile Phone" },
                new ProducMode {ID = 4, Name = "Tablet" },
                new ProducMode {ID = 5, Name = "Desktop PC" } ,
            };
        }
    }
    public class BetterProductService : IProductService
    {
        public List<ProducMode> getAll()
        {
            return new List<ProducMode>
        {
            new ProducMode {ID = 1, Name = "Television" },
            new ProducMode {ID = 2, Name = "Refrigerator" },
            new ProducMode {ID = 3, Name = "IPhone" },
            new ProducMode {ID = 4, Name = "Laptop" },
        };
        }
    }
}
