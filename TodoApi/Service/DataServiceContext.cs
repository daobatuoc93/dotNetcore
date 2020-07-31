using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Service
{
    public class DataServiceContext : DbContext
    {
        public DataServiceContext(DbContextOptions<DataServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Data> Datas { get; set; }
        //        public DbSet<BankAccount> BankAccounts { get; set; }
    }

}