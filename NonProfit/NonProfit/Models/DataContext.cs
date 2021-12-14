using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Models
{
    public class DataContext : DbContext
    {
        public DbSet<NonProfitEmployee> NonProfitEmployee { get; set; }

        public DbSet<NonProfitOwner> NonProfitOwner { get; set; }

        public DbSet<NonProfitLocation> NonProfitLocation { get; set; }

        public DbSet<NonProfitDonator> NonProfitDonator { get; set; }

        public DbSet<NonProfitDonation> NonProfitDonation { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
    }
}
