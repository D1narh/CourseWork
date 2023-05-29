using DataModels;
using MaimApp.Class.MainProductC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.Favorite
{
    public class FindFavorite // Класс предбальник (определяет нужно ли залазить в БД или нет) для поиска избранных 
    {
        static List<UserFavoriteProductC> userFavorite = null;
        private readonly Favorite favorite = new Favorite();

        public async Task<List<UserFavoriteProductC>> LoadFavorite()
        {
            if (userFavorite == null)
            {
                userFavorite = await favorite.LoadFavorite();
            }
            return userFavorite;
        }
    }
}
