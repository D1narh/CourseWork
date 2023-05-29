using Catharsis.Commons;
using DataModels;
using LinqToDB;
using MaimApp.Class.BusTickets;
using MaimApp.Class.User;
using Microsoft.Kiota.Abstractions;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using static Azure.Core.HttpHeader;


namespace MaimApp.Views.Treaty.Bus_Tickets
{
    /// <summary>
    /// Логика взаимодействия для TicketBooking.xaml
    /// </summary>
    public partial class TicketBooking : Window
    {
        AuthUser user = new AuthUser();
        Tickets tick;

        public TicketBooking(Tickets ticket)
        {
            InitializeComponent();
            tick= ticket;
        }

        private void People_Click(object sender, RoutedEventArgs e)
        {
            Animation();
        }
        public void Animation()
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (SelNumPeople.Visibility == Visibility.Hidden)
            {
                SelNumPeople.Visibility = Visibility.Visible;
                anim.From= 0;
                anim.To = 134;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.From = 134;
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
                SelNumPeople.Visibility = Visibility.Hidden;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePeopleCount(int.Parse(CountChild.Content.ToString()), int.Parse(CountOld.Content.ToString()));

            DateL.Content += tick.DateStart;
            StartTimeL.Content = tick.TimeStart;
            TotalTimeL.Content = tick.TravelTime;
            EndTimeL.Content = tick.TimeEnd;
            StartCityTB.Text = tick.StartCity;
            EndCityTB.Text = tick.EndCity;
            Name.Content += tick.Name;


        }

        private void OldPlus_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString()) >= tick.NumberSeats)
            {
                return;
            }
            CountOld.Content = int.Parse(CountOld.Content.ToString()) + 1;
            UpdatePeopleCount(int.Parse(CountChild.Content.ToString()), int.Parse(CountOld.Content.ToString()));
        }

        private void OldMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountOld.Content.ToString()) - 1 <= 0)
            {
                return;
            }
            CountOld.Content = int.Parse(CountOld.Content.ToString()) - 1;
            UpdatePeopleCount(int.Parse(CountChild.Content.ToString()), int.Parse(CountOld.Content.ToString()));
        }

        private void ChildPlus_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString()) >= tick.NumberSeats)
            {
                return;
            }
            CountChild.Content = int.Parse(CountChild.Content.ToString()) + 1;
            UpdatePeopleCount(int.Parse(CountChild.Content.ToString()), int.Parse(CountOld.Content.ToString()));
        }

        private void ChildMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountChild.Content.ToString()) - 1 < 0)
            {
                return;
            }
            CountChild.Content = int.Parse(CountChild.Content.ToString()) - 1;
            UpdatePeopleCount(int.Parse(CountChild.Content.ToString()), int.Parse(CountOld.Content.ToString()));
        }

        private void UpdatePeopleCount(int countChild, int countOld)
        {
            int totalCount = countChild + countOld;
            PeopleCount.Content = "Количество людей: " + totalCount.ToString();
            CountPeople.Content = $"Осталось мест : {tick.NumberSeats - totalCount}";
        }

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                db.Insert(new Basket
                {
                   UserId = user.GetUserId(),
                   DateIns = DateTime.Now
                });
                var basket = db.Baskets.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == user.GetUserId());
                db.Insert(new BasketLine
                {
                    BasketId = basket.Id,
                    ProductId = tick.ID,
                    ProductType = 2,
                    Count = int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())
                });

                db.BusTickets
                    .Where(x => x.Id == tick.ID)
                    .Set(x => x.NumberSeats, tick.NumberSeats - (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())))
                    .Update();
            }
            DialogResult = true;
        }
    }
}
