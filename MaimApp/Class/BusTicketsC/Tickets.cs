using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaimApp.Class.BusTickets
{
    public class Tickets
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public Tickets(int id, string name, string description, string shortDescription, decimal price,string imagePath)
        {
            ID = id;
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
