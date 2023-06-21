using DataModels;
using MaimApp.Class.Favorite;
using MaimApp.Parser.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tavis.UriTemplates;

namespace MaimApp.Class.BusTickets
{
    public class TicketsLoader
    {
        public int NowPage { get; set; } = 1;
        public int CountLine { get; set; } = 0;

        public static ObservableCollection<Tickets> TicketsList = new ObservableCollection<Tickets>();
        ObservableCollection<Tickets> First21Tickets = new ObservableCollection<Tickets>();

        public async Task<ObservableCollection<Tickets>> Load21Product()
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                TicketsList.Clear();
                First21Tickets.Clear();
            });

            await Task.Run(() => Load());

            double countLineDouble = (double)TicketsList.Count / 21;
            CountLine = (int)Math.Ceiling(countLineDouble);

            if (NowPage > CountLine)
            {
                NowPage = CountLine + 1;
            }

            var count = (NowPage - 1) * 21;

            var lastQuantity = NowPage * 21;
            if (TicketsList.Count < lastQuantity)
            {
                lastQuantity = TicketsList.Count;
            }

            while (count < lastQuantity)
            {
                var i = TicketsList[count];

                Application.Current.Dispatcher.Invoke(() =>
                {
                    First21Tickets.Add(i);
                });

                count++;
            }

            return First21Tickets;
        }

        public void Load()
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                var data = db.BusTickets.Where(x => x.DateStart >= DateTime.Now && x.NumberSeats > 0).ToList();

                foreach (var i in data)
                {
                    var startCity = db.Cities.FirstOrDefault(x => x.Id == i.Id);
                    var endCity = db.Cities.FirstOrDefault(x => x.Id == i.EndCity);

                    TicketsList.Add(new Tickets(i.Id, i.Name, startCity.Name, endCity.Name, i.Price.ToString(), i.TravelTime, i.NumberSeats, i.BusImage, i.DateStart)
                    {
                        ID = i.Id,
                        Name = i.Name,
                        StartCity = startCity.Name,
                        EndCity = endCity.Name,
                        Price = i.Price + "₽",

                        TravelTime = String.Format("{0} ч {1} мин", (i.DateStart.AddMinutes(i.TravelTime) - i.DateStart).Hours,
                        (i.DateStart.AddMinutes(i.TravelTime) - i.DateStart).Minutes),

                        NumberSeats = i.NumberSeats,
                        BusImage = i.BusImage,
                        DateStart = i.DateStart.ToString("dd/MM/yyyy"),
                    });
                }
            }
        }

        public Tickets GetTicket(int id)
        {
            return TicketsList.FirstOrDefault(x => x.ID == id);
        }
    }
}
