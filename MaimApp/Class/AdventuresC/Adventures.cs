using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.AdventuresC
{
    public class Adventures
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Descroption { get; set; }
        public string ShortDescroption { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string IsFavorite { get; set; }

        public Adventures(int id, string name, string descroption, string shortDescroption, string price, string image,string isFavorite)
        {
            ID = id;
            Name = name;
            Descroption = descroption;
            ShortDescroption = shortDescroption;
            Price = price;
            Image = image;
            IsFavorite = isFavorite;
        }
    }
}
