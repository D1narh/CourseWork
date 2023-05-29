using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaimApp.Parser.Models;
using MaimApp.Interfaces;
using MaimApp.DAL;
using System;
using MaimApp.Parser.Class;
using Catharsis.Commons;

namespace MaimApp.BLL
{
    public class Formatter
    {
        private readonly IParser _parser = new MainParser();

        private static Rootobject _cache = null;

        public async Task WarmUpCache(string url)
        {
            var result = await _parser.Parse(url);
            _cache = result;
        }

        public async Task<List<HotelInf>> GetAddressesFromUrl(string url, int sort)
        {
            //var i = new List<string> { "Близко к центру", "Лучшие оценки", "Цена: Сначала дешевле", "Цена: Сначала дороже" };
            var standartImagePath = "\\Image\\heart-shape.png";
            Rootobject result = await GetParsedData(url);

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

        private async Task<Rootobject> GetParsedData(string url)
        {
            if (_cache == null)
            {
                _cache = await _parser.Parse(url);
            }

            return _cache;
        }

        private HotelInf CreateHotelInf(Hotel hotel, string standartImagePath)
        {
            var distanceToCenter = $"До центра {hotel.center_distance} км";
            var rating = $"{hotel.rating}/10";
            var price = hotel.min_price.ToString();

            return new HotelInf(hotel.id, hotel.name, hotel.address, hotel.center_distance.ToString(), hotel.image.path,
                hotel.min_price.ToString(), standartImagePath, hotel.rating.ToString(), hotel.images,hotel.stars, hotel.breakfast == 1 ? "В отеле можно позавтракать" : "")
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
            };
        }
    }
}