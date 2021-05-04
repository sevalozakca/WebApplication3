using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Data_Access.Entities
{
    public class Member
    {
        
        public int MemberID { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }

        public ICollection<Order> Orders { get; set; }
       


    }
}
