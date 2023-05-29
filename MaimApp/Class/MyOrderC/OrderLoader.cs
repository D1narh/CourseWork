using DataModels;
using MaimApp.Class.BusTickets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaimApp.Class.MyOrderC
{
    public class OrderLoader
    {
        public static ObservableCollection<MyOrder> OrderList = new ObservableCollection<MyOrder>();
        public async Task<ObservableCollection<MyOrder>> Load()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                OrderList.Clear();

                using (var db = new DbA99dc4MaimfDB())
                {
                    var data = db.Baskets.Join(db.BasketLines,
                        b => b.Id,
                        bl => bl.BasketId,
                        (b, bl) => new
                        {
                            BasketId = b.Id,
                            Product = bl.ProductId,
                            Category = bl.ProductType,
                            bl.Count
                        });

                    foreach (var i in data)
                    {
                        var name = NameProduct(i.Product, i.Category);
                        var price = PriceP(i.Product, i.Category, i.Count);
                        var category = GetCategory(i.Category);
                        OrderList.Add(new MyOrder(i.BasketId, name, category, i.Count, price)
                        {
                            Id = i.BasketId,
                            Name = name,
                            Category = category,
                            Count = i.Count,
                            TotalPrice = price
                        });
                    }
                }
            });
            return OrderList;
        }

        public string NameProduct(int Id, int? Type)
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                switch (Type)
                {
                    case 1:
                        return db.HotelProducts.FirstOrDefault(x => x.Id == Id).Name;

                    case 2:
                        return db.BusTickets.FirstOrDefault(x => x.Id == Id).Name;

                    case 3:
                        return "NET";
                }
                return "Ничего";
            }
        }

        public double PriceP(int Id, int? Type, int? Count)
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                switch (Type)
                {
                    case 1:
                        return db.HotelProducts.FirstOrDefault(x => x.Id == Id).Price * Count ?? 0;

                    case 2:

                        var a = db.BusTickets.FirstOrDefault(x => x.Id == Id).Price * Count ?? 0;
                        return (double)a;

                    case 3:
                        return 0;
                }
                return 0;
            }
        }

        public string GetCategory(int? Category)
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                return db.ProductCategories.FirstOrDefault(x => x.Id == Category).Name;
            }
        }
    }
}
