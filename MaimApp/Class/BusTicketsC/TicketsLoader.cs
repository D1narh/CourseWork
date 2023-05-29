using DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MaimApp.Class.BusTickets
{
    public class TicketsLoader
    {
        public static ObservableCollection<Tickets> TicketsList = new ObservableCollection<Tickets>();
        public async Task<ObservableCollection<Tickets>> Load()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                TicketsList.Clear();
                using (var db = new DbA99dc4MaimfDB())
                {
                    var data = db.BusTickets.Where(x => x.DateStart >= DateTime.Now && x.NumberSeats > 0).ToList();

                    foreach (var i in data)
                    {
                        var startCity = db.Cities.FirstOrDefault(x => x.Id == i.Id);
                        var endCity = db.Cities.FirstOrDefault(x => x.Id == i.EndCity);

                        TicketsList.Add(new Tickets(i.Id, i.Name, startCity.Name, endCity.Name, i.Price, i.TravelTime, i.NumberSeats, i.BusImage, i.DateStart)
                        {
                            ID = i.Id,
                            Name = i.Name,
                            StartCity = startCity.Name,
                            EndCity = endCity.Name,
                            Price = i.Price,

                            TravelTime = String.Format("{0} ч {1} мин", (i.DateStart.AddMinutes(i.TravelTime) - i.DateStart).Hours,
                            (i.DateStart.AddMinutes(i.TravelTime) - i.DateStart).Minutes),

                            NumberSeats = i.NumberSeats,
                            BusImage = i.BusImage,
                            DateStart = i.DateStart.ToString("dd/MM/yyyy"),
                        });
                    }
                }
            });
            return TicketsList;
        }

        public Tickets GetTicket(int id)
        {
            return TicketsList.FirstOrDefault(x => x.ID == id);
        }
    }
}
