using MaimApp.Class.MainProductC;
using MaimApp.Parser.Class;
using MaimApp.Views.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
            NumberStroke(null);
            await LoadProduct();

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
            PersonalAreaG.Visibility = Visibility.Visible;
            Senderar(sender, PersonalAreaG);
        }

        private void Adventures_Click(object sender, RoutedEventArgs e)
        {
            AdventuresG.Visibility = Visibility.Visible;
            Senderar(sender, AdventuresG);
        }

        private void BusTickets_Click(object sender, RoutedEventArgs e)
        {
            BusTicketsG.Visibility = Visibility.Visible;
            Senderar(sender, BusTicketsG);
        }

        private void Hotels_Click(object sender, RoutedEventArgs e)
        {
            HotelsG.Visibility = Visibility.Visible;
            Senderar(sender, HotelsG);
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
        //Используется в методах с название MouseLeave
        public void LeaveFromButton(object sender)
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
            Products = await Task.Run(() => viewProduct.Load40Product(1));
            list.ItemsSource= Products;
        }

        private void View_MouseEnter(object sender, MouseEventArgs e)
        {
            Color color = Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0);
            SolidColorBrush brush = new SolidColorBrush(color);
            ((Button)sender).Background = brush;
        }

        private void View_MouseLeave(object sender, MouseEventArgs e)
        {
            Color color = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
            SolidColorBrush brush = new SolidColorBrush(color);
            ((Button)sender).Background = brush;
        }

        private void CubePrB_Click(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0);
            SolidColorBrush brush = new SolidColorBrush(color);
            VerticalPrB.Background = brush;
            list.Visibility = Visibility.Hidden;

            Color color2 = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
            SolidColorBrush brush2 = new SolidColorBrush(color2);
            CubePrB.Background = brush2;
            Cubelist.Visibility = Visibility.Visible;

            list.ItemsSource = null;
            Cubelist.ItemsSource = Products;
        }

        private void VerticalPrB_Click(object sender, RoutedEventArgs e)
        {
            Color color = Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0);
            SolidColorBrush brush = new SolidColorBrush(color);
            CubePrB.Background = brush;
            Cubelist.Visibility = Visibility.Hidden;

            Color color2 = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
            SolidColorBrush brush2 = new SolidColorBrush(color2);
            VerticalPrB.Background = brush2;
            list.Visibility = Visibility.Visible;

            Cubelist.ItemsSource = null;
            list.ItemsSource = Products;
        }

        //private void ImageEl_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    Button img = (Button)sender;
        //    img.Height = img.ActualHeight * 1.1;
        //    img.Width = img.ActualHeight * 1.1;
        //}

        //private void ImageEl_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    Button img = ((Button)sender);
        //    img.Height /= 1.1;
        //    img.Width /= 1.1;
        //}

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

        public void Button_Click(object sender, RoutedEventArgs e)
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
                    Color color = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
                    SolidColorBrush brush = new SolidColorBrush(color);
                    ((Button)sender).Foreground = brush;

                    Color color2 = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    SolidColorBrush brush2 = new SolidColorBrush(color2);
                    ((Button)senderSecondCou).Foreground = brush2;
                }
            }
            else
            {
                Color color = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
                SolidColorBrush brush = new SolidColorBrush(color);
                ((Button)sender).Foreground = brush;
            }
        }

        public void NumberStroke(int? a)
        {
            if (a == null)//Проверяется, только ли мы открыли программу или нет
            {
                a = 1;
            }

            var nowNumber = a;
            while (nowNumber < a + 10)
            {
                Button button = new Button
                {
                    Name = "Number" + nowNumber,
                    Content = nowNumber,
                    FontSize = 19,
                    Height = 30,
                    Width= 30,
                    Style = (Style)FindResource("CornerButton"),
                    Margin = new Thickness(5, 2, 0, 0),
                };
                if (nowNumber == a + 8)
                {
                    button.Content = "...";
                }
                button.Click += Button2_Click;
                nowNumber++;
                StrokeNumber.Children.Add(button);
            }
        }

        public async void Button2_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = null;
            Cubelist.ItemsSource = null;

            ViewProduct viewProduct = new ViewProduct();
            var i = Convert.ToInt32(((Button)sender).Content.ToString());
            Products = await Task.Run(() => viewProduct.Load40Product(i));

            list.ItemsSource = Products;
            Cubelist.ItemsSource = Products;
        }
    }
}
