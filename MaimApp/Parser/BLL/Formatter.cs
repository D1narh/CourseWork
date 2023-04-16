using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaimApp.Parser.Models;
using MaimApp.Interfaces;
using MaimApp.DAL;
using System;
using MaimApp.Parser.Class;

namespace MaimApp.BLL
{
    public class Formatter
    {
        private readonly IParser _parser = new MainParser();

        private Rootobject _cache = null;

        public async Task WarmUpCache(string url)
        {
            var result = await _parser.Parse(url);
            _cache = result;
        }

        public async Task<List<string>> GetImagesFromUrl(string url)
        {
            Rootobject result;

            if (_cache == null)
            {
                result = await _parser.Parse(url);
            }
            else
                result = _cache;

            return result.response.hotels.Select(x => x.image.preview_path).ToList();
        }

        public async Task<List<HotelInf>> GetAddressesFromUrl(string url)
        {
            var result = await _parser.Parse(url);

            return result.response.hotels.Where(x => x.image != null).Select(x => 
            new HotelInf(x.id,x.name,x.address,x.center_distance.ToString(),x.image.path,x.min_price.ToString(),false,x.rating.ToString(),x.images)
            {
                ID= x.id,
                Name= x.name,
                Adress = x.address,
                DistanceToCenter = $"До центра { x.center_distance } км",
                ImagePath= x.image.path,
                Price = x.min_price.ToString(),
                Reviews= $"{x.rating}/10",
                IsFavorite = false,
                Images = x.images
            }).ToList();

        }
    }
}