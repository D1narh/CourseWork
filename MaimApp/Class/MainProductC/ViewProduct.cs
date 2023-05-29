using DataModels;
using LinqToDB;
using MaimApp.BLL;
using MaimApp.Class.Favorite;
using MaimApp.Class.User;
using MaimApp.Parser.Class;
using MaimApp.Parser.Models;
using MaimApp.Views.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Media;

namespace MaimApp.Class.MainProductC
{
    public class ViewProduct
    {
        static ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();
        private readonly Formatter _formatter = new Formatter();
        public int NowPage { get; set; } = 1;
        public int CountLine { get; set; } = 0;
        public static List<HotelInf> result;
        public static List<UserFavoriteProductC> favorite;

        public async Task<ObservableCollection<HotelInf>> Load40Product(int sort, string searchText = "")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Products.Clear();
            });
            if (result == null)
            {
                await GetAllSite(sort);
            }

            var filteredResults = result.Where(x => x.Name.Contains(searchText)).ToList();
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Products.Add(new HotelInf(item.ID, item.Name, item.Adress, item.DistanceToCenter, item.ImagePath, item.Price, isFavorite, item.Reviews, item.Images, item.CountStars, item.Breakefast));
                });
                count++;
            }
            return Products;
        }

        public async Task<List<HotelInf>> GetAllSite(int sort)
        {
            result = await _formatter.GetAddressesFromUrl(
                 "https://101hotels.com/api/hotel/search?r=0.1345066605085845&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D", sort);
            //"https://101hotels.com/api/hotel/search?r=0.5406233108723135&params=%7B%22city_ids%22%3A%5B75%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Астрахань id=75
            //"https://101hotels.com/api/hotel/search?r=0.8865264677573255&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Москва
            CountLine = result.Count / 21;
            return result;
        }

        public async Task<List<UserFavoriteProductC>> GetAllFavorite()
        {
            FindFavorite fav = new FindFavorite();
            favorite = await fav.LoadFavorite();

            return favorite;
        }
        public ObservableCollection<HotelInf> GetProducts()
        {
            return Products;
        }

        public IpInfo GetUserCountryByIp()
        {
            IpInfo ipInfo = new IpInfo();
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

        public async Task<ObservableCollection<HotelInf>> SortButtonClick(int sort, string SearchText = "")
        {
            await GetAllSite(sort);
            return await Load40Product(sort, SearchText);
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
