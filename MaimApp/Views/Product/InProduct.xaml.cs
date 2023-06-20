using DataModels;
using LinqToDB;
using MaimApp.Class.MainProductC;
using MaimApp.Class.User;
using MaimApp.Parser.Class;
using MaimApp.Parser.Models;
using MaimApp.Views.MessageView;
using MaimApp.Views.PersonalArea;
using MaimApp.Views.Treaty;
using Newtonsoft.Json.Linq;
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

            CountStar.Content += Hotel.CountStars.ToString();
            HotelName.Content = Hotel.Name;
            Grade.Content += Hotel.Reviews;
            Price.Content = Hotel.Price;
            BreakeF.Content = Hotel.Breakefast;

            if (Hotel.IsFavorite != "\\Image\\heart-shape.png")
            {
                IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart.png") as ImageSource;
            }
            else
            {
                IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart-shape.png") as ImageSource;
            }
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
            if (number >= Hotel.Images.Length - 1)
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
            if (number <= 0)
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
                using (var db = new DbA99dc4MaimfDB())
                {
                    db.Insert(new HotelProduct
                    {
                        Name = Hotel.Name,
                        Price = Convert.ToDouble(Hotel.Price),
                        CityId = db.Cities.FirstOrDefault(x => x.Name == "Москва").Id,
                        DistanceToCenter = Convert.ToDouble(string.Join("", Hotel.DistanceToCenter.Where(c => char.IsDigit(c)))),
                        MainImage = Hotel.ImagePath
                    });

                    db.Insert(new Basket
                    {
                        UserId = authUser.GetUserId(),
                        DateIns = DateTime.Now
                    });
                    var basket = db.Baskets.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == authUser.GetUserId());
                    db.Insert(new BasketLine
                    {
                        BasketId = basket.Id,
                        ProductId = Hotel.ID,
                        ProductType = 1,
                        Count = 1
                    });
                    MessageBox.Show("Вы забронировали номер в отеле", "Поздравляем");
                }
                MainProduct mainProduct = new MainProduct();
                mainProduct.Show();
                this.Close();
                //SettingNumPeople settingNumPeople = new SettingNumPeople(Hotel);
                //settingNumPeople.Show();
                //this.Close();
            }
            else
            {
                NoAuthUserMessageBox boxView = new NoAuthUserMessageBox("Вы не можете оформить заявку пока не авторизируетесь");
                boxView.ShowDialog();
                if (boxView.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                {
                    return;
                }
                else //Если пользователь захотел авторизироваться
                {
                    Authorization authorization = new Authorization();
                    this.Hide();
                    authorization.ShowDialog();
                    if (authorization.DialogResult == true)
                    {
                        this.Show();
                        return;
                    }
                    else
                    {
                        this.Show();
                        return;
                    }
                }
            }
        }

        private async void Favorites_Click(object sender, RoutedEventArgs e)
        {
            AuthUser authUser = new AuthUser();
            ViewProduct viewProduct = new ViewProduct();
            if (authUser.AuthOrNo())
            {
                if (await Task.Run(() => viewProduct.DelOrIns(Convert.ToInt32(Hotel.ID),1)))
                {
                    IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart.png") as ImageSource;
                }
                else
                {
                    IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart-shape.png") as ImageSource;
                }
            }
            else
            {
                NoAuthUserMessageBox boxView = new NoAuthUserMessageBox("Вы не можете добавить в избранное не авторизировавшись");
                boxView.ShowDialog();
                if (boxView.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                {
                    return;
                }
                else //Если пользователь захотел авторизироваться
                {
                    this.Hide();
                    Authorization authorization = new Authorization();
                    authorization.ShowDialog();
                    this.Show();

                    if (await Task.Run(() => viewProduct.DelOrIns(Convert.ToInt32(Hotel.ID), 1)))
                    {
                        IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart.png") as ImageSource;
                    }
                    else
                    {
                        IsFavorite.Source = new ImageSourceConverter().ConvertFromString("pack://application:,,,/Image/heart-shape.png") as ImageSource;
                    }
                }
            }
        }
    }
}
