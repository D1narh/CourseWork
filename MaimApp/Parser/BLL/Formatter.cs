using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaimApp.Models;
using MaimApp.Interfaces;
using MaimApp.DAL;
using System;
using MaimApp.Parser.Class;

namespace MaimApp.BLL
{
    public class Formatter
    {
        private readonly IParser _parser = new MainParser();

        private Result _cache = null;

        public async Task WarmUpCache(string url)
        {
            var result = await _parser.Parse(url);
            _cache = result;
        }

        public async Task<List<string>> GetImagesFromUrl(string url)
        {
            Result result;

            if (_cache == null)
            {
                result = await _parser.Parse(url);
            }
            else
                result = _cache;

            return result.response.hotels.Select(x => x.image.preview_path).ToList();
        }

        public async Task<List<ProductInf>> GetAddressesFromUrl(string url)
        {
            var result = await _parser.Parse(url);

            return result.response.hotels.Where(x => x.image != null).Select(x => 
            new ProductInf(x.id,x.name,"dwa",x.image.path,Convert.ToDecimal(x.min_price),false)
            {
                ID= x.id,
                Name= x.name,
                Price = Convert.ToDecimal(x.min_price),
                ImagePath= x.image.path,
                ShortDiscription = x.image.path,
                IsFavorite = false
            }).ToList();

        }
    }
}