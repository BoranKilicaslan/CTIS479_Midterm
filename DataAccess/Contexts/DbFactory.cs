﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=stu_db;trusted_connection=true;");
            // First, create an object containing the connection string of your database
            // (it's more suitable to use the development database).

            return new Db(optionsBuilder.Options);
            // Then, return an object of type Db using the optionsBuilder object we created above.
        }
    }
}
