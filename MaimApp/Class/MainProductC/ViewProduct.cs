using DataModels;
using LinqToDB;
using MaimApp.BLL;
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
using System.Web.UI.WebControls;
using System.Windows.Media;

namespace MaimApp.Class.MainProductC
{
    public class ViewProduct
    {
        ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();
        private readonly Formatter _formatter = new Formatter();
        public int NowPage { get; set; } = 1;
        public int CountLine { get; set; } = 0;
        public static List<HotelInf> result;
        public List<UserFavoriteProductC> userFavorite = new List<UserFavoriteProductC>();

        public async Task<ObservableCollection<HotelInf>> Load40Product(int sort)
        {
            ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();
            if (result == null)
            {
                await GetAllSite(sort);
            }
            var count = (NowPage - 1) * 20;

            while (count < NowPage * 20)
            {
                var i = result[count];

                var IsFavorite = "\\Image\\heart-shape.png";
                if (userFavorite != null)
                {
                    if (userFavorite.FirstOrDefault(x => x.ProductId == i.ID && x.ProductType == 1) != null)
                        IsFavorite = "\\Image\\heart.png";
                }

                Products.Add(new HotelInf(i.ID, i.Name, i.Adress, i.DistanceToCenter, i.ImagePath, i.Price, IsFavorite, i.Reviews, i.Images)
                {
                    ID = i.ID,
                    Name = i.Name,
                    Adress = i.Adress,
                    DistanceToCenter = i.DistanceToCenter,
                    ImagePath = i.ImagePath,
                    Price = i.Price + "₽",
                    Reviews = i.Reviews,
                    IsFavorite = IsFavorite,
                    Images = i.Images
                });
                count++;
            }
            return Products;
        }

        public async Task<ObservableCollection<HotelInf>> FillCatalog(int sort)
        {
            if (result == null)
            {
                await GetAllSite(sort);
                foreach (var i in result)
                {
                    Products.Add(new HotelInf(i.ID, i.Name, i.Adress, i.DistanceToCenter, i.ImagePath, i.Price, i.IsFavorite, i.Reviews, i.Images)
                    {
                        ID = i.ID,
                        Name = i.Name,
                        Adress = i.Adress,
                        DistanceToCenter = i.DistanceToCenter,
                        ImagePath = i.ImagePath,
                        Price = i.Price,
                        Reviews = i.Reviews,
                        IsFavorite = i.IsFavorite,
                        Images = i.Images
                    });
                }
                return Products;
            }
            return Products;
        }

        public async Task<List<HotelInf>> GetAllSite(int sort)
        {
            result = await _formatter.GetAddressesFromUrl(
                 "https://101hotels.com/api/hotel/search?r=0.1345066605085845&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D", sort);
            //"https://101hotels.com/api/hotel/search?r=0.5406233108723135&params=%7B%22city_ids%22%3A%5B75%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Астрахань id=75
            //"https://101hotels.com/api/hotel/search?r=0.8865264677573255&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Москва
            CountLine = result.Count / 20;
            return result;
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

        public async Task<ObservableCollection<HotelInf>> SortButtonClick(int sort)
        {
            await GetAllSite(sort);
            return await Load40Product(sort);
        }

        public HotelInf GetInfoHotel(int id)
        {
            return result.FirstOrDefault(x => x.ID == id);
        }

        public void DelOrIns(int IdProduct)
        {
            AuthUser user = new AuthUser();
            using (var db = new DbA96b40MaimfDB())
            {
                if (db.UserFavProducts.FirstOrDefault(x => x.ProductId == IdProduct && x.ProductType == 1) == null)
                {
                    userFavorite.Add(new UserFavoriteProductC(IdProduct, 1)
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
                }
                else
                {
                    var itemForDel = userFavorite.FirstOrDefault(x => x.ProductId == IdProduct && x.ProductType == 1);
                    userFavorite.Remove(itemForDel);
                    db.UserFavProducts.Where(x => x.ProductId == IdProduct && x.ProductType == 1).Delete();
                }
            }
        }
    }
}
