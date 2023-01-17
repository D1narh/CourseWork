using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class
{
    public class HotelInformation
    {
        public int ID { get; set; }
        public string Adress { get; set; }
        public string DistanceToCenter { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public bool IsFavorite { get; set; }
        public string Reviews { get; set; }
    }
}

