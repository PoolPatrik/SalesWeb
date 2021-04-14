using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWeb31.Models;

namespace SalesWeb31.Data
{
    public class SalesWeb31Context : DbContext
    {
        public SalesWeb31Context (DbContextOptions<SalesWeb31Context> options)
            : base(options)
        {
        }

        public DbSet<Departments> Departments { get; set; }
        public  DbSet<Seller> Seller{ get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }


    }
}
