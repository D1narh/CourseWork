using DataModels;
using LinqToDB;
using MaimApp.BLL;
using MaimApp.Class.Favorite;
using MaimApp.Class.User;
using MaimApp.Parser.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace MaimApp.Class.MainProductC
{
    public class ViewProduct
    {
        public int NowPage { get; set; } = 1;
        public int CountLine { get; set; } = 0;

        static ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();
        private readonly Formatter _formatter = new Formatter();
        public static List<HotelInf> result;
        public static List<UserFavoriteProductC> favorite;
        static IpInfo ipInfo = new IpInfo();

        public async Task<ObservableCollection<HotelInf>> Load40Product(int sort, string searchText = "")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Products.Clear();
            });

            if (result == null || result[1].City != ipInfo.GetCity())
            {
                await GetAllSite(sort);
            }
            var filteredResults = result;

            if(searchText != "")
            {
                filteredResults = result.Where(x => x.Name.Contains(searchText)).ToList();
            }
            CountLine = filteredResults.Count / 21;

            if (NowPage > CountLine)
            {
                NowPage = CountLine + 1;
            }

            var count = (NowPage - 1) * 21;

            var lastQuantity = NowPage * 21;
            if (filteredResults.Count < lastQuantity)
            {
                lastQuantity = filteredResults.Count;
            }

            while (count < lastQuantity)
            {
                var item = filteredResults[count];
                var isFavorite = "\\Image\\heart-shape.png";

                if (favorite != null && favorite.FirstOrDefault(x => x.ProductId == item.ID && x.ProductType == 1) != null)
                {
                    isFavorite = "\\Image\\heart.png";
                }

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Products.Add(new HotelInf(item.ID, item.Name, item.Adress, item.DistanceToCenter, item.ImagePath, item.Price, isFavorite, item.Reviews, item.Images, item.CountStars, item.Breakefast, item.City));
                });

                count++;
            }

            return Products;
        }

        public async Task GetAllSite(int sort)
        {
            result = await _formatter.GetAddressesFromUrl(ipInfo.GetCity(), sort);
            CountLine = result.Count / 21;
        }

        public ObservableCollection<HotelInf> GetProducts()
        {
            return Products;
        }

        public async Task<List<UserFavoriteProductC>> GetAllFavorite()
        {
            FindFavorite fav = new FindFavorite();
            favorite = await fav.LoadFavorite();

            return favorite;
        }

        public IpInfo GetUserCountryByIp()
        {
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io");
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }
            return ipInfo;
        }
        public async Task<ObservableCollection<HotelInf>> SortButtonClick(int sort, string searchText = "")
        {
            await GetAllSite(sort);
            return await Load40Product(sort, searchText);
        }

        public HotelInf GetInfoHotel(int id)
        {

            return result.FirstOrDefault(x => x.ID == id);
        }

        public bool DelOrIns(int IdProduct)
        {
            AuthUser user = new AuthUser();
            using (var db = new DbA99dc4MaimfDB())
            {
                if (db.UserFavProducts.FirstOrDefault(x => x.ProductId == IdProduct && x.ProductType == 1) == null)
                {
                    favorite.Add(new UserFavoriteProductC(IdProduct, 1)
                    {
                        ProductId = IdProduct,
                        ProductType = 1
                    });
                    db.Insert(new UserFavProduct
                    {
                        UserId = (int)(user.GetUserId()),
                        ProductId = IdProduct,
                        ProductType = 1,
                        DateIns = DateTime.Now
                    });
                    return true;
                }
                else
                {
                    var itemForDel = favorite.FirstOrDefault(x => x.ProductId == IdProduct && x.ProductType == 1);
                    favorite.Remove(itemForDel);
                    db.UserFavProducts.Where(x => x.ProductId == IdProduct && x.ProductType == 1).Delete();
                    return false;
                }
            }
        }
    }
}
