using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Data_Access.Entities;

namespace WebApplication3.Models.Data_Access.Mapping
{
    public class MemberMapping : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
         
            builder.HasKey(a => a.MemberID);
            builder.Property(a => a.MemberID).ValueGeneratedOnAdd().UseIdentityColumn(1, 1);


           
        }
    }
}
