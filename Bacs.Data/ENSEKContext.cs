using ENSEK.Models;
using ENSEK.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ENSEK.Data
{
    public class ENSEKContext : DbContext
    {
        public ENSEKContext(DbContextOptions<ENSEKContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasMany(x => x.MeterReadings);
            modelBuilder.Entity<MeterReading>().Ignore(x => x.ResponseMessage).HasOne(x=>x.Account);
            modelBuilder.Entity<MeterReading>().HasKey(x => x.Id);

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
    }
}
