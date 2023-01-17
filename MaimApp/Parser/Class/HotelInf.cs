using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Parser.Class
{
    public class HotelInf
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string DistanceToCenter { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public bool IsFavorite { get; set; }
        public string Reviews { get; set; }

        public HotelInf(int iD, string name, string adress, string distanceToCenter, string imagePath, decimal price, bool isFavorite, string reviews)
        {
            ID = iD;
            Name = name;
            Adress = adress;
            DistanceToCenter = distanceToCenter;
            ImagePath = imagePath;
            Price = price;
            IsFavorite = isFavorite;
            Reviews = reviews;
        }
    }
}
