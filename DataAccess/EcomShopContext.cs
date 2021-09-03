using Microsoft.EntityFrameworkCore;
using System;

namespace EfDataAccess
{
    public class EcomShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
