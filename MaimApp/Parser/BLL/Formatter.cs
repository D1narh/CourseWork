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
            var standartImagePath = "\\Image\\heart-shape.png";
            Rootobject result;
            if (_cache == null)
            {
                result = await _parser.Parse(url);
                _cache = result;
            }
            else
                result = _cache;

            switch (sort)
            {
                case 0:
                    return result.response.hotels.Where(x => x.image != null).Select(x =>
                    new HotelInf(x.id, x.name, x.address, x.center_distance.ToString(), x.image.path, x.min_price.ToString(), standartImagePath, x.rating.ToString(), x.images)
                    {
                        ID = x.id,
                        Name = x.name,
                        Adress = x.address,
                        DistanceToCenter = $"До центра {x.center_distance} км",
                        ImagePath = x.image.path,
                        Price = x.min_price.ToString(),
                        Reviews = $"{x.rating}/10",
                        IsFavorite = standartImagePath,
                        Images = x.images
                    }).OrderBy(x => x.DistanceToCenter).ToList();

                case 1:
                    return result.response.hotels.Where(x => x.image != null).Select(x =>
                    new HotelInf(x.id, x.name, x.address, x.center_distance.ToString(), x.image.path, x.min_price.ToString(), standartImagePath, x.rating.ToString(), x.images)
                    {
                        ID = x.id,
                        Name = x.name,
                        Adress = x.address,
                        DistanceToCenter = $"До центра {x.center_distance} км",
                        ImagePath = x.image.path,
                        Price = x.min_price.ToString(),
                        Reviews = $"{x.rating}/10",
                        IsFavorite = standartImagePath,
                        Images = x.images
                    }).OrderByDescending(x => x.Reviews.Remove(x.Reviews.Length - 3)).ToList();
                case 2:
                    return result.response.hotels.Where(x => x.image != null).Select(x =>
                    new HotelInf(x.id, x.name, x.address, x.center_distance.ToString(), x.image.path, x.min_price.ToString(), standartImagePath, x.rating.ToString(), x.images)
                    {
                        ID = x.id,
                        Name = x.name,
                        Adress = x.address,
                        DistanceToCenter = $"До центра {x.center_distance} км",
                        ImagePath = x.image.path,
                        Price = x.min_price.ToString(),
                        Reviews = $"{x.rating}/10",
                        IsFavorite = standartImagePath,
                        Images = x.images
                    }).OrderBy(x => x.Price.ToDouble()).ToList();
                case 3:
                    return result.response.hotels.Where(x => x.image != null).Select(x =>
                    new HotelInf(x.id, x.name, x.address, x.center_distance.ToString(), x.image.path, x.min_price.ToString(), standartImagePath, x.rating.ToString(), x.images)
                    {
                        ID = x.id,
                        Name = x.name,
                        Adress = x.address,
                        DistanceToCenter = $"До центра {x.center_distance} км",
                        ImagePath = x.image.path,
                        Price = x.min_price.ToString(),
                        Reviews = $"{x.rating}/10",
                        IsFavorite = standartImagePath,
                        Images = x.images
                    }).OrderByDescending(x => x.Price.ToDouble()).ToList();
            }

            return result.response.hotels.Where(x => x.image != null).Select(x =>
                new HotelInf(x.id, x.name, x.address, x.center_distance.ToString(), x.image.path, x.min_price.ToString(), standartImagePath, x.rating.ToString(), x.images)
                {
                    ID = x.id,
                    Name = x.name,
                    Adress = x.address,
                    DistanceToCenter = $"До центра {x.center_distance} км",
                    ImagePath = x.image.path,
                    Price = x.min_price.ToString(),
                    Reviews = $"{x.rating}/10",
                    IsFavorite = standartImagePath,
                    Images = x.images
                }).ToList();
        }
    }
}