using DataModels;
using MaimApp.Class.BusTickets;
using MaimApp.Class.Favorite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaimApp.Class.AdventuresC
{
    public class AdLoader
    {
        public static List<UserFavoriteProductC> favorite;

        public int NowPage { get; set; } = 1;
        public int CountLine { get; set; } = 0;

        public static ObservableCollection<Adventures> AdventuresList = new ObservableCollection<Adventures>();
        ObservableCollection<Adventures> First21Adventures = new ObservableCollection<Adventures>();

        public async Task<ObservableCollection<Adventures>> Load21Product()
        {
            if (AdventuresList.Count == 0)
            {
                await Task.Run(() => Load());
            }

            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                First21Adventures.Clear();
            });

            double countLineDouble = (double)AdventuresList.Count / 21;
            CountLine = (int)Math.Ceiling(countLineDouble);

            if (NowPage > CountLine)
            {
                NowPage = CountLine + 1;
            }

            var count = (NowPage - 1) * 21;

            var lastQuantity = NowPage * 21;
            if (AdventuresList.Count < lastQuantity)
            {
                lastQuantity = AdventuresList.Count;
            }

            while (count < lastQuantity)
            {
                var i = AdventuresList[count];

                Application.Current.Dispatcher.Invoke(() =>
                {
                    First21Adventures.Add(i);
                });

                count++;
            }

            return First21Adventures;
        }

        public void Load()
        {
            if (AdventuresList.Count == 0)
            {
                using (var db = new DbA99dc4MaimfDB())
                {
                    var data = db.CompanyProducts.ToList();

                    foreach (var i in data)
                    {
                        var isFavorite = "\\Image\\heart-shape.png";

                        if (favorite != null && favorite.FirstOrDefault(x => x.ProductId == i.Id && x.ProductType == 3) != null)
                        {
                            isFavorite = "\\Image\\heart.png";
                        }

                        AdventuresList.Add(new Adventures(i.Id, i.Name, i.Description, i.ShorDescription, i.Price.ToString(), i.Image, isFavorite)
                        {
                            ID = i.Id,
                            Name = i.Name,
                            Descroption = i.Description,
                            ShortDescroption = i.ShorDescription,
                            Price = i.Price + "₽",
                            Image = i.Image,
                            IsFavorite = isFavorite,
                        });
                    }
                }
            }
        }

        public async Task<List<UserFavoriteProductC>> GetAllFavorite()
        {
            FindFavorite fav = new FindFavorite();
            favorite = await fav.LoadFavorite();

            return favorite;
        }
    }
}
