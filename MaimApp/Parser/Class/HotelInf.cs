using DataModels;
using MaimApp.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Parser.Class
{
    public class HotelInf
    {
        int IdHotelInClass { get; set; }
        private static int CountHotel { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string DistanceToCenter { get; set; }
        public string ImagePath { get; set; }
        public string Price { get; set; }
        public string IsFavorite { get; set; }
        public string Reviews { get; set; }
        public Image1[] Images { get; set; }
        public int CountStars { get; set; }
        public string Breakefast { get; set; }
        public string City { get; set; }


        public HotelInf(int iD, string name, string adress, string distanceToCenter, string imagePath,
                        string price, string isFavorite, string reviews, Image1[] images, int countStars,
                        string breakefast,string city)
        {
            ID = iD;
            Name = name;
            Adress = adress;
            DistanceToCenter = distanceToCenter;
            ImagePath = imagePath;
            Price = price;
            IsFavorite = isFavorite;
            Reviews = reviews;
            Images = images;
            CountHotel++;
            IdHotelInClass = CountHotel;
            CountStars = countStars;
            Breakefast = breakefast;
            City = city;
        }
    }
}
