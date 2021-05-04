using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Data_Access.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

        [DataType(DataType.Url)]
        public string Image { get; set; }
        public int Price { get; set; }
        
        public int MemberID { get; set; }
        public Member Member { get; set; }
      




    }
}
