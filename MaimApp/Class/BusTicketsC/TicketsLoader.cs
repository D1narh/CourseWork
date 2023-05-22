using Catharsis.Commons;
using DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaimApp.Class.BusTickets
{
    public class TicketsLoader
    {
        ObservableCollection<Tickets> TicketsList = new ObservableCollection<Tickets>();
        public async Task<ObservableCollection<Tickets>> Load()
        {
            using (var db = new DbA96b40MaimfDB())
            {
                var data = db.CompanyProducts.Where(x => x.CategoriId == 2).ToList();

                foreach (var i in data)
                {
                    TicketsList.Add(new Tickets(i.Id, i.Name, i.Description, i.ShorDescription,i.Price, i.Image)
                    {
                        ID = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        ShortDescription = i.ShorDescription,
                        Price = i.Price ,
                        ImagePath = i.Image
                    });
                }
            }
            return TicketsList;
        }
    }
}
