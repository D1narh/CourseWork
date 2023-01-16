using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Parser.Class
{
    public class ProductInf
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortDiscription { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public bool IsFavorite { get; set; }

        public ProductInf(int iD, string name, string shortDiscription, string imagePath, decimal price, bool isFavorite)
        {
            ID = iD;
            Name = name;
            ShortDiscription = shortDiscription;
            ImagePath = imagePath;
            Price = price;
            IsFavorite = isFavorite;
        }
    }
}
