using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class
{
    public class UserFavoriteProductC
    {
        public int ProductId { get; set; }
        public int ProductType { get; set; }

        public UserFavoriteProductC(int productId,int productType)
        {
            ProductId= productId;
            ProductType= productType;
        }
    }
}
