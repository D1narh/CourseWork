using DataModels;
using LinqToDB;
using MaimApp.Class.User;
using MaimApp.Parser.Class;
using MaimApp.Parser.Models;
using MaimApp.Views.Treaty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaimApp.Views.Product
{
    /// <summary>
    /// Логика взаимодействия для ViewProduct.xaml
    /// </summary>
    public partial class InProduct : Window
    {
        HotelInf Hotel;
        static int number;
        public InProduct(HotelInf a)
        {
            InitializeComponent();

            Hotel = a;
        }

        private void l_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void l_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            if (((Label)sender).Name.ToString() == "l_exit")
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(246, 62, 62));
                ((Label)sender).Background = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 250, 250));
                ((Label)sender).Background = brush;
            }
        }

        private void l_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Background = Brushes.Gray;
        }

        private void l_exit_minimize(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public void SelImage()
        {
            var hh = Hotel.Images;
            var imagepath = hh[number].path;
        }

        public void ButtonBackgroung()
        {
            BackImage.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            NextImage.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonBackgroung();
            LoadProductView();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void LoadProductView()
        {
            ImageBrush br = new ImageBrush();
            br.ImageSource = new BitmapImage(new Uri(Hotel.ImagePath));
            ImageBorder.Background = br;

            HotelName.Content = Hotel.Name;
            Grade.Content += Hotel.Reviews;
            Price.Content = Hotel.Price;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainProduct mainProduct = new MainProduct();
            mainProduct.Show(); 
            this.Close();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush br = new ImageBrush();
            number++;
            if(number >= Hotel.Images.Length - 1)
            {
                number = -1;
                br.ImageSource = new BitmapImage(new Uri(Hotel.ImagePath));
                ImageBorder.Background = br;
                return;
            }
            br.ImageSource = new BitmapImage(new Uri(Hotel.Images[number].path));
            ImageBorder.Background = br;
        }

        private void BackImage_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush br = new ImageBrush();
            number--;
            if(number <= 0)
            {
                br.ImageSource = new BitmapImage(new Uri(Hotel.ImagePath));
                ImageBorder.Background = br;
                number = Hotel.Images.Length - 1;
                return;
            }
            br.ImageSource = new BitmapImage(new Uri(Hotel.Images[number].path));
            ImageBorder.Background = br;
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            AuthUser authUser = new AuthUser();
            if (authUser.AuthOrNo())
            {
                SettingNumPeople settingNumPeople = new SettingNumPeople(Hotel);
                settingNumPeople.Show();
                this.Close();
            }
        }
    }
}
