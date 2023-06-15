using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.Favorite
{
    public class Favorite
    {
        public Task<List<UserFavoriteProductC>> LoadFavorite()
        {
            List<UserFavoriteProductC> userFavorite = new List<UserFavoriteProductC>();
            using (var db = new DbA99dc4MaimfDB())
            {
                foreach (var i in db.UserFavProducts)
                {
                    userFavorite.Add(new UserFavoriteProductC(i.ProductId, i.ProductType)
                    {
                        ProductId = i.ProductId,
                        ProductType = i.ProductType
                    });
                }
            }
            return Task.FromResult(userFavorite);
        }
    }
}
