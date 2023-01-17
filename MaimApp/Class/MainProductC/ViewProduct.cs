using MaimApp.BLL;
using MaimApp.Parser.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.MainProductC
{
    public class ViewProduct
    {
        private readonly Formatter _formatter = new Formatter();

        public ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();

        public async Task<ObservableCollection<HotelInf>> FillCatalog()
        {
            var result = await GetAllSite();
            foreach (var i in result)
            {
                Products.Add(new HotelInf(i.ID, i.Name, i.Adress, i.DistanceToCenter, i.ImagePath, i.Price, false, i.Reviews)
                {
                    ID = i.ID,
                    Name = i.Name,
                    Adress = i.Adress,
                    DistanceToCenter = i.DistanceToCenter,
                    ImagePath = i.ImagePath,
                    Price = i.Price,
                    Reviews = i.Reviews,
                    IsFavorite = false
                });
            }
            return Products;
        }

        public async Task<List<HotelInf>> GetAllSite()
        {
            var result = await _formatter.GetAddressesFromUrl(
                 "https://101hotels.com/api/hotel/search?r=0.1345066605085845&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D");
            //"https://101hotels.com/api/hotel/search?r=0.5406233108723135&params=%7B%22city_ids%22%3A%5B75%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Астрахань id=75
            //"https://101hotels.com/api/hotel/search?r=0.8865264677573255&params=%7B%22city_ids%22%3A%5B2%5D%2C%22hotel_ids%22%3A%5B%5D%2C%22destination%22%3A%7B%7D%7D" Москва
            return result;
        }

        public static string GetUserCountryByIp()
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
            return ipInfo.Country;
        }
    }
}
