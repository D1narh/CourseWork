using MaimApp.Parser.Class;
using MaimApp.Views.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaimApp.Views.Treaty
{
    /// <summary>
    /// Логика взаимодействия для SettingNumPeople.xaml
    /// </summary>
    public partial class SettingNumPeople : Window
    {
        HotelInf Hotel;
        public SettingNumPeople(HotelInf a)
        {
            InitializeComponent();

            Hotel = a;
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
                anim.To = 134;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
                SelNumPeople.Visibility = Visibility.Hidden;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            InProduct inProduct = new InProduct(Hotel);
            inProduct.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Content = Hotel.Name;
            PeopleCount.Content = "Колличество людей: " + (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())).ToString();
        }

        private void OldPlus_Click(object sender, RoutedEventArgs e)
        {
            CountOld.Content = int.Parse(CountOld.Content.ToString()) + 1;
            PeopleCount.Content = "Колличество людей: " + (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())).ToString();
        }

        private void OldMinus_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(CountOld.Content.ToString()) - 1 <= 0)
            {
                return;
            }
            CountOld.Content = int.Parse(CountOld.Content.ToString()) - 1;
            PeopleCount.Content = "Колличество людей: " + (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())).ToString();
        }

        private void ChildPlus_Click(object sender, RoutedEventArgs e)
        {
            CountChild.Content = int.Parse(CountChild.Content.ToString()) + 1;
            PeopleCount.Content = "Колличество людей: " + (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())).ToString();
        }

        private void ChildMinus_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountChild.Content.ToString()) - 1 < 0)
            {
                return;
            }
            CountChild.Content = int.Parse(CountChild.Content.ToString()) -1;
            PeopleCount.Content = "Колличество людей: " + (int.Parse(CountChild.Content.ToString()) + int.Parse(CountOld.Content.ToString())).ToString();
        }

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
