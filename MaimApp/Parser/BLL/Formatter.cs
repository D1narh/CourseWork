using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaimApp.Parser.Models;
using MaimApp.Interfaces;
using MaimApp.DAL;
using System;
using MaimApp.Parser.Class;
using Catharsis.Commons;
using MaimApp.Class.Translator;
using DataModels;
using System.ComponentModel;
using MaimApp.Class;
using System.Windows;

namespace MaimApp.BLL
{
    public class Formatter
    {
        private readonly IParser _parser = new MainParser();

        IpInfo ipInfo = new IpInfo();
        private static Rootobject _cache = null;
        string url;

        public async Task<Rootobject> WarmUpCache(string city)
        {
            if (_cache != null && _cache.response.hotels.Count() > 0)
            {
                int i = 0;
                int maxAttempts;
                if (_cache.response.hotels.Length < 10)
                {
                    maxAttempts = _cache.response.hotels.Length;
                }
                else
                {
                    maxAttempts = 10;
                }

                while (i < maxAttempts)
                {
                    if (_cache.response.hotels[i].city_name == ipInfo.GetCity())
                    {
                        return _cache;
                    }
                    i++;
                }
            }

            url = URL(city);
            if (url == "")
            {
                MessageBox.Show("У нас нет информации по отелям в вашем городе", "Извините");
            }
            else
            {
                return _cache = await _parser.Parse(url);
            }
            return _cache;
        }

        public async Task<List<HotelInf>> GetAddressesFromUrl(string city, int sort)
        {
            var standartImagePath = "\\Image\\heart-shape.png";

            Rootobject result = await WarmUpCache(city);

            var hotels = result.response.hotels.Where(x => x.image != null).Select(x => CreateHotelInf(x, standartImagePath)).ToList();

            switch (sort)
            {
                case 0:
                    return hotels.OrderBy(x => x.DistanceToCenter).ToList();
                case 1:
                    return hotels.OrderByDescending(x => x.Reviews.Remove(x.Reviews.Length - 3)).ToList();
                case 2:
                    return hotels.OrderBy(x => x.Price).ToList();
                case 3:
                    return hotels.OrderByDescending(x => x.Price.ToDouble()).ToList();
                default:
                    return hotels;
            }
        }

        private HotelInf CreateHotelInf(Hotel hotel, string standartImagePath)
        {
            var distanceToCenter = $"До центра {hotel.center_distance} км";
            var rating = $"{hotel.rating}/10";
            var price = hotel.min_price.ToString();

            return new HotelInf(hotel.id, hotel.name, hotel.address, hotel.center_distance.ToString(), hotel.image.path,
                hotel.min_price.ToString(), standartImagePath, hotel.rating.ToString(), hotel.images, hotel.stars, hotel.breakfast == 1 ? "В отеле можно позавтракать" : "", hotel.city_name)
            {
                ID = hotel.id,
                Name = hotel.name,
                Adress = hotel.address,
                DistanceToCenter = distanceToCenter,
                ImagePath = hotel.image.path,
                Price = price,
                Reviews = rating,
                IsFavorite = standartImagePath,
                Images = hotel.images,
                CountStars = hotel.stars,
                Breakefast = hotel.breakfast == 1 ? "В отеле можно позавтракать" : "",
                City = hotel.city_name,
            };
        }


        public string URL(string city)
        {
            Translator translator = new Translator();
            var transCity = translator.Translate(city);
            ipInfo.ChangeCity(transCity);
            using (var db = new DbA99dc4MaimfDB())
            {
                var link = db.Cities?.FirstOrDefault(x => x.Name.Contains(transCity))?.Link;
                if (link != null)
                {
                    return link;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}