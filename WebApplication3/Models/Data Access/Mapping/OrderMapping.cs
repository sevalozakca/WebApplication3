using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Data_Access.Entities;

namespace WebApplication3.Models.Data_Access.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            
                builder.HasKey(a => a.OrderID);
                builder.HasOne(a => a.Member).WithMany(b => b.Orders).HasForeignKey(c => c.MemberID);
                builder.Property(a => a.OrderID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);

          

        }
    }
}
