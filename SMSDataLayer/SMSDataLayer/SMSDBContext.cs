using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SMSDataLayer
{
    public class SMSDBContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Salesman> Salesmans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Record> Records { get; set; }
        
    }
}