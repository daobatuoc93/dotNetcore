using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Service
{
    public interface IProductServices
    {
       public IList<ProducMode> Getall();
    }
}
