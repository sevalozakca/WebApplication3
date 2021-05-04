using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Data_Access.Entities;
using WebApplication3.Models.Data_Access.Mapping;

namespace WebApplication3.Models
{
    public class ECommercialWebSiteDbContext :DbContext
    {

        public ECommercialWebSiteDbContext(DbContextOptions<ECommercialWebSiteDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());
            base.OnModelCreating(modelBuilder);
        }



    }
}
