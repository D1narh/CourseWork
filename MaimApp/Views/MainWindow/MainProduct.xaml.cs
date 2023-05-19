using DataModels;
using LinqToDB;
using MaimApp.Class;
using MaimApp.Class.MainProductC;
using MaimApp.Class.Translator;
using MaimApp.Class.User;
using MaimApp.Views.MainWindow;
using MaimApp.Views.MessageView;
using MaimApp.Views.PersonalArea;
using MaimApp.Views.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        static object senderNowLeftP, senderSecondLeftP, senderNowCou, senderSecondCou, senderNowReg, senderSecondReg, senderNowCity, senderSecondCity, senderNowSort, senderSecondSort;
        private static BrushConverter brushConverter = new BrushConverter();
        int NowSort = 0;
        private ListView ChangeListNow;
        List<string> RegionL = new List<string>();
        ViewProduct viewProduct = new ViewProduct();
        AuthUser authUser = new AuthUser();

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartMethod();
        }
        async void StartMethod()
        {

            //При открытии всегда заполняется первым этот лист
            ChangeListNow = list;

            //Проверим стоит ли показывать приветствии при открытии окна 
            if (authUser.AuthOrNo())
            {
                HelloPanel.Visibility = Visibility.Hidden;
            }

                UserFavoriteProduct();
            new Task(() => ChangeCounty()).Start();
            Sortierung();
            ButtonBackgroung();
            getCityClient();
            await LoadProduct();
            NumberStroke();
            GarbageClean();

            animation.Visibility = Visibility.Hidden;
            SearchText.Visibility = Visibility.Hidden;
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (ChangeSortGrid.Visibility == Visibility.Hidden)
            {
                ChangeSortGrid.Visibility = Visibility.Visible;
                anim.To = 120;
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

        public async void ChangeSort_Click(object sender, RoutedEventArgs e)
        {
            senderSecondSort = senderNowSort;
            senderNowSort = sender;

            if (senderSecondSort == senderNowSort)
            {
                return;
            }
            else
            {
                NowSort = int.Parse(((Button)sender).Name.ElementAt(((Button)sender).Name.Length - 1).ToString());

                ChangeListNow.ItemsSource = null;
                ChangeListNow.ItemsSource = await viewProduct.SortButtonClick(NowSort);
                Country_Click(senderNowSort, senderSecondSort);
            }
        }

        public void County_Click(object sender, RoutedEventArgs e)//Используется в методе ChangeSitys
        {
            //Запись в глобальные переменные кнопок в выподающем списке городов
            senderSecondCou = senderNowCou;
            senderNowCou = sender;

            if (senderSecondCou == senderNowCou)
            {
                return;
            }
            else
            {
                Country_Click(senderNowCou, senderSecondCou);

                //Загружает регионы данной области 
                RegionSP.Children.Clear();
                LoadRegion(sender);
            }
        }

        public void Region_Click(object sender, RoutedEventArgs e)//Используется в методе ChangeSitys
        {
            //Запись в глобальные переменные кнопок в выподающем списке городов
            senderSecondReg = senderNowReg;
            senderNowReg = sender;

            if (senderSecondReg == senderNowReg)
            {
                return;
            }
            else
            {
                Country_Click(senderNowReg, senderSecondReg);
            }
        }


        public void City_Click(object sender, RoutedEventArgs e)//Используется в методе ChangeSitys
        {
            //Запись в глобальные переменные кнопок в выподающем списке городов
            senderSecondCou = senderNowCou;
            senderNowCou = sender;

            if (senderSecondCou == senderNowCou)
            {
                return;
            }
            else
            {
                Country_Click(senderNowCou, senderSecondCou);

                //Загружает регионы данной области 
                RegionSP.Children.Clear();
                LoadRegion(sender);
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
            if (((Button)sender).Name.ToString() == "Number" + viewProduct.NowPage)
            {
                return;
            }
            else
            {
                ChangeListNow.ItemsSource = null;
                viewProduct.NowPage = Convert.ToInt32(((Button)sender).Content.ToString());

                ChangeListNow.ItemsSource = await Task.Run(() => viewProduct.Load40Product(NowSort));


                StrokeNumber.Children.Clear();
                NumberStroke();
                GC.Collect();
            }
        }

        private void ChangeCity_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (ChangeCityGrid.Visibility == Visibility.Hidden)
            {

                ChangeCityGrid.Visibility = Visibility.Visible;
                anim.To = 305;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                ChangeCityGrid.BeginAnimation(HeightProperty, anim);
                IsEnabled(false);
                ChangeCityGrid.IsEnabled = true;
                SearchCity.IsEnabled = true;
            }
            else
            {
                SearchSity.Text.Trim();
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                ChangeCityGrid.BeginAnimation(HeightProperty, anim);
                ChangeCityGrid.Visibility = Visibility.Hidden;
                IsEnabled(true);
            }
        }

        private void IsEnabled(bool status)
        {
            SearchBox.IsEnabled = status;
            Sorting.IsEnabled = status;
            SearchCity.IsEnabled = status;
            VerticalBor.IsEnabled = status;
            CubeBor.IsEnabled = status;
            CubeBor.IsEnabled = status;
            list.IsEnabled = status;
            Cubelist.IsEnabled = status;
            Cubelist.IsEnabled = status;
            StrokeNumber.IsEnabled = status;
            ChangeSortGrid.IsEnabled = status;
            ChangeCityGrid.IsEnabled = status;
        }

        private void PersonalArea_Click(object sender, RoutedEventArgs e)
        {
            PersonalAreaLoaded(sender);
        }

        private void Adventures_Click(object sender, RoutedEventArgs e)
        {
            Senderar(sender, AdventuresG);
        }

        private void BusTickets_Click(object sender, RoutedEventArgs e)
        {
            Senderar(sender, BusTicketsG);
        }

        private void Hotels_Click(object sender, RoutedEventArgs e)
        {
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
                var IdProduct = TypeDescriptor.GetProperties(a)["ID"].GetValue(a);
                InProduct product = new InProduct(viewProduct.GetInfoHotel(int.Parse(IdProduct.ToString())));
                product.Show();
                this.Close();
            }
        }

        private void CubePrB_Click(object sender, RoutedEventArgs e)
        {
            ChangeListNow = Cubelist;

            VerticalPrB.Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
            list.Visibility = Visibility.Hidden;

            CubePrB.Background = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            Cubelist.Visibility = Visibility.Visible;

            ChangeListNow.ItemsSource = list.ItemsSource;
            list.ItemsSource = null;
        }

        private void VerticalPrB_Click(object sender, RoutedEventArgs e)
        {
            ChangeListNow = list;

            CubePrB.Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
            Cubelist.Visibility = Visibility.Hidden;

            VerticalPrB.Background = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            list.Visibility = Visibility.Visible;

            ChangeListNow.ItemsSource = Cubelist.ItemsSource;
            Cubelist.ItemsSource = null;
        }

        private async void SearchSity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchSity.Text.Trim() == "")
            {
                CityInDBSP.Visibility = Visibility.Hidden;
                CityInDBSP.Children.Clear();
                return;
            }
            else
            {
                CityInDBSP.Visibility = Visibility.Visible;
                CityInDBSP.Children.Clear();
                CityInDBSP.Background = (Brush)brushConverter.ConvertFrom("#EDEDED");

                using (var db = new DbA96b40MaimfDB())
                {
                    if (RegionL.Count == 0)
                    {
                        foreach (var i in db.Regions)
                        {
                            RegionL.Add(i.Name);
                        }
                    }
                    foreach (var i in RegionL.Where(x => x.Contains($"{((TextBox)sender).Text}")))
                    {
                        Label label = new Label
                        {
                            Content = i,
                            // Style = (Style)FindResource("LableMouse")
                        };
                        label.MouseEnter += IsMouseEnter;
                        label.MouseLeave += IsMouseLeave;
                        CityInDBSP.Children.Add(label);
                    }
                }
            }
        }

        private void View_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
        }

        private void View_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = (Brush)brushConverter.ConvertFrom("#E0E0E0");
        }


        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
           FavoriteClick(sender);
        }


        //Учтасток кода с логикой =(


        //Метод для убирания лишней ширины
        public void Senderar(object sender, Grid gridName)
        {
            HelloPanel.Visibility = Visibility.Hidden;
            gridName.Visibility = Visibility.Visible;

            //Запись в глобальные переменные кнопок
            senderSecondLeftP = senderNowLeftP;
            senderNowLeftP = sender;

            if (senderSecondLeftP != null)//Если жмякнули на другую кнопку, где не было ВЫДВИНУТО
            {
                LeaveFromButton(senderSecondLeftP, gridName);
            }//Т.к. анимация сворачивания не сработает, у кнопки будет Width = 140
            SecondGrid = gridName;
        }

        //Анимация выдвижения с верху в низ
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

        //Получает Город пользователя по ip
        public void getCityClient()
        {
            Translator translator = new Translator();

            var ip = viewProduct.GetUserCountryByIp();
            var City = translator.Translate(ip.City.Trim());
            UserCity.Content = $"г.{City}";
            CityL.Content = $"Ваш город : {City}";
        }

        //Метод загрузки товаров
        public async Task LoadProduct()
        {
            ChangeListNow.ItemsSource = null;
            ChangeListNow.ItemsSource = await Task.Run(() => viewProduct.Load40Product(NowSort));
        }
        //Метод для заполнения каталога

        //Метод для заполнения кнопками Грид ChangeSort
        public void Sortierung()
        {
            var i = new List<string> { "Близко к центру", "Лучшие оценки", "Цена: Сначала дешевле", "Цена: Сначала дороже" };
            var count = 0;
            foreach (var item in i)
            {
                Button button = new Button
                {
                    Name = "Sort" + count,
                    Content = item,
                    Height = 30,
                    FontSize = 14,
                    Style = (Style)FindResource("ComboBoxButton"),
                };
                button.Click += ChangeSort_Click;
                button.MouseEnter += IsMouseEnter;
                button.MouseLeave += IsMouseLeave;
                ChangeSort.Children.Add(button);
                count++;
            }
        }
        //Метод для заполнения кнопками Грид ChangeSity
        private void ChangeCounty()
        {
            using (var db = new DbA96b40MaimfDB())
            {
                foreach (var County in db.Counties)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Button button = new Button
                        {
                            Name = "County" + County.Id,
                            Content = County.Name,
                            FontSize = 13,
                            Height = 25.5,
                            Style = (Style)FindResource("ComboBoxButton"),

                        };
                        button.Click += County_Click;
                        button.MouseEnter += IsMouseEnter;
                        button.MouseLeave += IsMouseLeave;
                        CountySP.Children.Add(button);
                    });
                }
            }
        }

        //Выводит все регионы в зависимости от выбранной области
        private void LoadRegion(object sender)
        {
            using (var db = new DbA96b40MaimfDB())
            {
                foreach (var Region in db.Regions.Where(x => x.CountyId == int.Parse(string.Join("", ((Button)sender).Name.Where(c => char.IsDigit(c))))))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Button button = new Button
                        {
                            Name = "Region" + Region.Id,
                            Content = Region.Name,
                            FontSize = 13,
                            Height = 25.5,
                            Style = (Style)FindResource("ComboBoxButton")
                        };
                        button.Click += Region_Click;
                        button.MouseEnter += IsMouseEnter;
                        button.MouseLeave += IsMouseLeave;
                        RegionSP.Children.Add(button);
                    });
                }
            }
        }

        //Для вывода нумерации товаров
        public void NumberStroke()
        {
            int? nowNumber = 0;
            int count = viewProduct.NowPage;
            if (viewProduct.NowPage == 1)//Проверяется, только ли мы открыли программу или нет
            {
                if (viewProduct.CountLine < viewProduct.NowPage + 8)
                {
                    nowNumber = viewProduct.CountLine;
                }
                else
                {
                    nowNumber = viewProduct.NowPage + 8;
                }
            }
            else if (viewProduct.NowPage >= 5)
            {
                count = viewProduct.NowPage - 4;
                nowNumber = count + 8;
                if (viewProduct.CountLine <= viewProduct.NowPage + 8)
                {
                    if (viewProduct.NowPage + 4 < viewProduct.CountLine)
                    {
                        count = viewProduct.NowPage - 4;
                        nowNumber = viewProduct.NowPage + 4;
                    }
                    else
                    {
                        nowNumber = viewProduct.CountLine;
                        count = viewProduct.CountLine - 8;
                    }
                }
            }
            else
            {
                if (viewProduct.NowPage <= 4)
                {
                    count = 1;
                    nowNumber = count + 8;
                }
            }
            while (count <= nowNumber)
            {
                Button button = new Button
                {
                    Name = "Number" + count,
                    Content = count,
                    FontSize = 19,
                    Height = 30,
                    Width = 30,
                    Style = (Style)FindResource("CornerButton"),
                    Margin = new Thickness(5, 2, 0, 0),
                };
                button.Click += Button2_Click;
                count++;
                StrokeNumber.Children.Add(button);
            }
            GC.Collect();
        }

        //Очистка мусора каждые 5 секунд
        private async void GarbageClean()
        {
            Func<Task> Func = async () =>
            {
                GC.Collect();
                await Task.Delay(5000);
            };
            while (true)
            {
                await Func();
            }
        }

        //При наведении на элемент
        public void IsMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name.ToString() == "Button")
            {
                ((Button)sender).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            }
            else
            {
                ((Label)sender).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            }
        }

        //При выходе с элемента
        public void IsMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name.ToString() == "Button")
            {
                if (((Button)senderNowCou)?.Content == ((Button)sender).Content || ((Button)senderNowReg)?.Content == ((Button)sender).Content || ((Button)senderNowCity)?.Content == ((Button)sender).Content || ((Button)senderNowSort)?.Content == ((Button)sender).Content)
                {
                    return;
                }
                else
                {
                    ((Button)sender).Foreground = (Brush)brushConverter.ConvertFrom("#000000");
                }
            }
            else
            {
                ((Label)sender).Foreground = (Brush)brushConverter.ConvertFrom("#000000");
            }
        }

        //При выборе закрашивает выбранную кнопку
        public void Country_Click(object now, object second)
        {
            if (second != null)
            {
                if (second == now)
                {
                    return;
                }
                else
                {
                    ((Button)now).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
                    ((Button)second).Foreground = (Brush)brushConverter.ConvertFrom("#000000");
                }
            }
            else
            {
                ((Button)now).Foreground = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            }
        }

        //Вызывается при нажатии на кнопку Профиль
        public void PersonalAreaLoaded(object sender)
        {
            HelloPanel.Visibility = Visibility.Hidden;

            if (authUser.AuthOrNo())
            {
                if (authUser.GetUserRole() == 2)
                {
                    ManagerPersonalArea managerPersonalArea = new ManagerPersonalArea();
                    managerPersonalArea.FillFIO();

                    ManagerPersonalAreaFrame.Content = managerPersonalArea;

                    Senderar(sender, ManagerAreaGFrame);
                }
                else if (authUser.GetUserRole() == 3)
                {

                }
                else
                {
                    UserPersonalArea userPersonalArea = new UserPersonalArea();
                    userPersonalArea.FillFIO();

                    UserPersonalAreaFrame.Content = userPersonalArea;

                    Senderar(sender, UserAreaGFrame);
                }
            }
            else
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                this.Close();
            }
        }

        public async Task FavoriteClick(object sender) // При нажатии кнопки Сердца (добавление в избранное)
        {
            var a = ((Button)sender).DataContext;
            var IdProduct = TypeDescriptor.GetProperties(a)["ID"].GetValue(a);
            if (authUser.AuthOrNo())
            {
                await Task.Run(() => viewProduct.DelOrIns(Convert.ToInt32(IdProduct)));
                ChangeListNow.ItemsSource = await Task.Run(() => viewProduct.Load40Product(NowSort));
            }
            else
            {
                NoAuthUserMessageBox boxView = new NoAuthUserMessageBox();
                boxView.ShowDialog();
                if (boxView.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                {
                    return;
                }
                else //Если пользователь захотел авторизироваться
                {
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
                }
            }
        }

        public void UserFavoriteProduct()
        {
            if (authUser.AuthOrNo())
            {
                using (var db = new DbA96b40MaimfDB())
                {
                    foreach (var i in db.UserFavProducts)
                    {
                        viewProduct.userFavorite.Add(new UserFavoriteProductC(i.ProductId, i.ProductType)
                        {
                            ProductId = i.ProductId,
                            ProductType = i.ProductType
                        });
                    }
                }
            }
            else
                return;
        }
    }
}

