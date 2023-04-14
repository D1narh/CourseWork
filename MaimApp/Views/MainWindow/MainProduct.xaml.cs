using DataModels;
using MaimApp.Class.MainProductC;
using MaimApp.Class.User;
using MaimApp.Parser.Class;
using MaimApp.Views.MessageView;
using MaimApp.Views.PersonalArea;
using MaimApp.Views.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace MaimApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainProduct.xaml
    /// </summary>
    public partial class MainProduct : Window
    {

        //Блок с временными переменными которые отслеживают нажатые кнопки
        static Grid SecondGrid;
        object senderNowLeftP, senderSecondLeftP, senderNowCou, senderSecondCou;
        public ObservableCollection<HotelInf> Products = new ObservableCollection<HotelInf>();
        private static BrushConverter brushConverter = new BrushConverter();
        int LineCount = 0;



        //Основной блок программы
        public MainProduct()
        {
            InitializeComponent();
        }

        private void LeftPMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter((Button)sender);
        }

        private void LeftPMouseLeave(object sender, MouseEventArgs e)
        {
            LeaveFromButton(sender);
        }

        public void ButtonBackgroung()
        {
            Hotels.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            BusTickets.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            PersonalArea.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            Adventures.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeSitys();
            Sortierung();
            ButtonBackgroung();
            getCityClient();
            await LoadProduct();
            LineCount = Line_count_hotel(Products);
            NumberStroke(null, LineCount);
            await FillCatalog();

            animation.Visibility = Visibility.Hidden;
            SearchText.Visibility = Visibility.Hidden;

            GC.Collect();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (ChangeSortGrid.Visibility == Visibility.Hidden)
            {
                ChangeSortGrid.Visibility = Visibility.Visible;
                anim.To = 145;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                ChangeSortGrid.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                ChangeSortGrid.BeginAnimation(HeightProperty, anim);
                ChangeSortGrid.Visibility = Visibility.Hidden;
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)//Используется в методе ChangeSitys
        {
            //Запись в глобальные переменные кнопок в выподающем списке городов
            senderSecondCou = senderNowCou;
            senderNowCou = sender;

            if (senderSecondCou != null)
            {
                if (senderSecondCou == sender)
                {
                    return;
                }
                else
                {
                    ((Button)sender).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
                    ((Button)senderSecondCou).Foreground = (Brush)brushConverter.ConvertFrom("#000000");
                }
            }
            else
            {
                ((Button)sender).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            }
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

        public async void Button2_Click(object sender, RoutedEventArgs e)//Используется в методе NumberStroke
        {

            list.ItemsSource = null;
            Cubelist.ItemsSource = null;

            ViewProduct viewProduct = new ViewProduct();
            var i = Convert.ToInt32(((Button)sender).Content.ToString());
            Products = await Task.Run(() => viewProduct.Load40Product(i));

            list.ItemsSource = Products;
            Cubelist.ItemsSource = Products;

            StrokeNumber.Children.Clear();
            NumberStroke(i, LineCount);
            GC.Collect();
        }

        private void City_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (ChangeCityGrid.Visibility == Visibility.Hidden)
            {
                ChangeCityGrid.Visibility = Visibility.Visible;
                anim.To = 305;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                ChangeCityGrid.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                ChangeCityGrid.BeginAnimation(HeightProperty, anim);
                ChangeCityGrid.Visibility = Visibility.Hidden;
            }
        }

        private void PersonalArea_Click(object sender, RoutedEventArgs e)
        {
            HelloPanel.Visibility = Visibility.Hidden;
            PersonalAreaG.Visibility = Visibility.Visible;
            AuthUser authUser = new AuthUser();
            if (authUser.AuthOrNo())
            {
                Senderar(sender, PersonalAreaG);
                TakePersonInfo();
            }
            else
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                this.Close();
            }
        }

        private void Adventures_Click(object sender, RoutedEventArgs e)
        {
            HelloPanel.Visibility = Visibility.Hidden;
            AdventuresG.Visibility = Visibility.Visible;
            Senderar(sender, AdventuresG);
        }

        private void BusTickets_Click(object sender, RoutedEventArgs e)
        {
            HelloPanel.Visibility = Visibility.Hidden;
            BusTicketsG.Visibility = Visibility.Visible;
            Senderar(sender, BusTicketsG);
        }

        private void Hotels_Click(object sender, RoutedEventArgs e)
        {
            HelloPanel.Visibility = Visibility.Hidden;
            HotelsG.Visibility = Visibility.Visible;
            Senderar(sender, HotelsG);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void View_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                var a = ((Grid)sender).DataContext;
                var myValue = TypeDescriptor.GetProperties(a)["ID"].GetValue(a);
                InProduct product = new InProduct(myValue.ToString());
                product.Show();
                this.Close();
            }
        }

        private void CubePrB_Click(object sender, RoutedEventArgs e)
        {
            VerticalPrB.Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
            list.Visibility = Visibility.Hidden;

            CubePrB.Background = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            Cubelist.Visibility = Visibility.Visible;

            list.ItemsSource = null;
            Cubelist.ItemsSource = Products;
        }

        private void VerticalPrB_Click(object sender, RoutedEventArgs e)
        {
            CubePrB.Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
            Cubelist.Visibility = Visibility.Hidden;

            VerticalPrB.Background = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            list.Visibility = Visibility.Visible;

            Cubelist.ItemsSource = null;
            list.ItemsSource = Products;
        }




        //Учтасток кода с логикой =(


        //Метод для убирания лишней ширины
        public void Senderar(object sender, Grid gridName)
        {
            //Запись в глобальные переменные кнопок
            senderSecondLeftP = senderNowLeftP;
            senderNowLeftP = sender;
            gridName.Visibility = Visibility.Visible;

            if (senderSecondLeftP != null)//Если жмякнули на другую кнопку, где не было ВЫДВИНУТО
            {
                LeaveFromButton(senderSecondLeftP, gridName);
                SecondGrid = gridName;

            }//Т.к. анимация сворачивания не сработает, у кнопки будет Width = 140
            else
            {
                SecondGrid = gridName;//Для дальнейшей проверки на то, какой Grid был visible
            }
        }

        //Анимация выдвижения
        public void AnimationEnter(Button name)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 140;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            name.BeginAnimation(WidthProperty, anim);
        }

        //Общий функционал для двух методов (анимация задвигания*)
        public void Leave(object sender)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 130;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            ((Button)sender).BeginAnimation(WidthProperty, anim);
        }

        //Метод для проверки стоит ли делать Грид невидимым при нажатии кнопки
        public void LeaveFromButton(object sender, Grid gridName)
        {
            //Идет проверка на то, стоит ли выполнять сворачивание кнопки ?
            if (((Button)senderNowLeftP).Name + "G" != SecondGrid.Name) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                Leave(sender);
                SecondGrid.Visibility = Visibility.Hidden;
            }
        }

        //Метод когда просто навелись на кнопку (без нажатия на нее сделали focus) и вышли , т.е. потеряли focus
        public void LeaveFromButton(object sender)//Используется в методах с название MouseLeave
        {
            if (senderNowLeftP == sender) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                return;
            }
            else
            {
                Leave(sender);
            }
        }

        public void getCityClient()
        {
            ViewProduct viewProduct = new ViewProduct();
            var ip = viewProduct.GetUserCountryByIp();
            City.Content = $"Ваш город : {ip.City}";
        }

        //Метод загрузки товаров
        public async Task LoadProduct()
        {
            ViewProduct viewProduct = new ViewProduct();
            Products = await Task.Run(() => viewProduct.FillCatalog());
        }
        //Метод для заполнения каталога
        public async Task FillCatalog()
        {
            ViewProduct viewProduct = new ViewProduct();
            Products = await Task.Run(() => viewProduct.Load40Product(1));
            list.ItemsSource = Products;
        }

        private void View_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
        }

        private void View_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
        }

        //Метод для заполнения кнопками Грид ChangeSort
        public void Sortierung()
        {
            var i = new List<string> { "По скидке", "Лучшие отзывы", "Цена: Сначала дешевле", "Цена: Сначала дороже" };
            foreach (var item in i)
            {
                Button button = new Button
                {
                    Content = item,
                    FontSize = 18,
                    Height= 30,
                    Padding = new Thickness(10),
                    Style = (Style)FindResource("ComboBoxButton")
                };
                ChangeSort.Children.Add(button);
            }
        }

        //Метод для заполнения кнопками Грид ChangeSity
        private void ChangeSitys()
        {
            var count = 0;
            var County = new List<string> { "Дальневосточный", "Приволжский", "Северо - Западный", "Северо - Кавказский", "Сибирский", "Уральский", "Центральный", "Южный" };
            foreach (var item in County)
            {
                Button button = new Button
                {
                    Name = "County" + count,
                    Content = item,
                    FontSize = 15,
                    Height = 25.5,
                    Style = (Style)FindResource("ComboBoxButton"),

                };
                button.Click += Button_Click;
                count++;
                CountySP.Children.Add(button);
            }
        }

        //Для вывода нумерации товаров
        public void NumberStroke(int? a, int? Stroke)
        {
            int? nowNumber = 1;
            if (a == null)//Проверяется, только ли мы открыли программу или нет
            {
                if (Stroke < a + 8)
                {
                    a = Stroke;
                }
                else
                {
                    a = 1;
                }
            }
            else if (a >= 5)
            {
                nowNumber = a - 4;
                a = a - 4;
                if (Stroke <= a + 8)
                {
                    a = Stroke - 8;
                    nowNumber = a;
                }
            }
            else
            {
                a = 1;
            }
            while (nowNumber < a + 8)
            {
                Button button = new Button
                {
                    Name = "Number" + nowNumber,
                    Content = nowNumber,
                    FontSize = 19,
                    Height = 30,
                    Width = 30,
                    Style = (Style)FindResource("CornerButton"),
                    Margin = new Thickness(5, 2, 0, 0),
                };
                button.Click += Button2_Click;
                nowNumber++;
                StrokeNumber.Children.Add(button);
            }
        }
        public int Line_count_hotel(ObservableCollection<HotelInf> values)
        {
            var count = values.Count / 40;
            return count;
        }

        public void TakePersonInfo()
        {
            AuthUser authUser = new AuthUser();
            using (var db = new DbA96b40MaimfDB())
            {
               var i = db.UserPrData.FirstOrDefault(x => x.User.Login == authUser.GetUserLogin());
                FIO.Content = i.Name + " " + i.LastName;
            }
        }
    }
}
