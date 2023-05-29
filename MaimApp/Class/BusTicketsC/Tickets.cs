using Catharsis.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaimApp.Class.BusTickets
{
    public class Tickets
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StartCity { get; set; }
        public string EndCity { get; set; }
        public decimal Price { get; set; }
        public string TravelTime { get; set; }
        public int NumberSeats { get; set; }
        public string BusImage { get; set; }
        public string DateStart { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }

        public Tickets(int id, string name, string startCity, string endCity, decimal price, int travelTime, int numberSeats, string busImage, DateTime dateStart) 
        { 
            ID = id;
            Name = name;
            StartCity = startCity;
            EndCity = endCity;
            Price = price;
            NumberSeats = numberSeats;
            BusImage = busImage;
            DateStart = dateStart.ToString();
            TimeStart = dateStart.ToString("HH:mm");
            TimeEnd = dateStart.AddMinutes(travelTime).ToString("HH:mm");
            var diff = dateStart.AddMinutes(travelTime).Subtract(dateStart);
            TravelTime = String.Format("{0}:{1}", diff.Hours, diff.Minutes);
        }
    }
}
