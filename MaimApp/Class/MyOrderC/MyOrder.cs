using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.MyOrderC
{
    public class MyOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int? Count { get; set; }
        public double TotalPrice { get; set; }

        public MyOrder(int basketId,string name,string type,int? count,double totalPrice)
        { 
            Id = basketId;
            Name = name;
            Category = type;
            Count = count;
            TotalPrice = totalPrice;
        }
    }
}
