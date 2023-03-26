using MaimApp.Class.MainProductC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaimApp.BLL;
using System.Linq;
using System.Threading;
using MaimApp.DataModels;
using System.Data;
using System.Data.Entity;
using System.Runtime.InteropServices;

namespace MaimApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainProduct.xaml
    /// </summary>
    public partial class MainProduct : Window
    {
        private readonly Formatter _formatter = new Formatter();

        //Блок с временными переменными которые отслеживают нажатые кнопки
        object senderNowLeftP, senderSecondLeftP, senderNowCou, senderSecondCou;



        //Основной блок программы
        public MainProduct()
        {
            InitializeComponent();
        }
        public void buttonBackgroung()
        {
            Hotels.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            BusTickets.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            PersonalArea.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            Adventures.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
        }

        private void Hotels_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter(Hotels);
        }

        private void Hotels_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLeave(Hotels);
        }

        private void BusTickets_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter(BusTickets);
        }

        private void BusTickets_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLeave(BusTickets);
        }

        private void Adventures_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter(Adventures);
        }

        private void Adventures_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLeave(Adventures);
        }

        private void PersonalArea_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter(PersonalArea);
        }

        private void PersonalArea_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationLeave(PersonalArea);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeSitys();
            Sortierung();
            await LoadProduct();

            animation.Visibility = Visibility.Hidden;
            SearchText.Visibility = Visibility.Hidden;

            buttonBackgroung();

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
            Senderar(sender);
        }

        private void Adventures_Click(object sender, RoutedEventArgs e)
        {
            Senderar(sender);
        }

        private void BusTickets_Click(object sender, RoutedEventArgs e)
        {
            Senderar(sender);
        }

        private void Hotels_Click(object sender, RoutedEventArgs e)
        {
            Senderar(sender);
        }


        //Учтасток кода с логикой =(


        //Метод для убирания лишней ширины
        public void Senderar(object sender)
        {
            //Запись в глобальные переменные кнопок
            senderSecondLeftP = senderNowLeftP;
            senderNowLeftP = sender;

            if (senderSecondLeftP != null)//Если жмякнули на другую кнопку, где не было ВЫДВИНУТО
            {
                AnimationLeave(senderSecondLeftP);
            }//Т.к. анимация сворачивания не сработает, у кнопки будет Width = 140
        }

        //Анимация выдвижения
        public void AnimationEnter(Button name)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 140;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            name.BeginAnimation(WidthProperty, anim);
            Color color = Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0);
            SolidColorBrush brush = new SolidColorBrush(color);
            name.Background = brush;
        }

        //Анимация сворачивания
        public void AnimationLeave(object sender)
        {
            //Идет проверка на то, стоит ли выполнять сворачивание кнопки ?
            if (senderNowLeftP == sender) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                return;
            }
            else
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.To = 130;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                ((Button)sender).BeginAnimation(WidthProperty, anim);
                Color color = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
                SolidColorBrush brush = new SolidColorBrush(color);
                ((Button)sender).Background = brush;
            }
        }

        //Метод загрузки товаров
        public async Task LoadProduct()
        {
            ViewProduct viewProduct = new ViewProduct();
            list.ItemsSource = await Task.Run(() => viewProduct.FillCatalog());
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
                    FontFamily = new FontFamily("Merienda One"),
                    Padding = new Thickness(10),
                    Style = (Style)FindResource("ComboBoxButton")
                };
                ChangeSort.Children.Add(button);
            }
        }

        //Метод для заполнения кнопками Грид ChangeSity
        private void ChangeSitys() //Какаха, нужно править асинхронщину !!!!
        {
            new Thread(() =>
            {
                Dispatcher.InvokeAsync((Action) (() =>
                {
                    var count = 0;
                    var County = new List<string> { "Дальневосточный", "Приволжский", "Северо - Западный", "Северо - Кавказский", "Сибирский", "Уральский", "Центральный", "Южный" };
                    foreach (var item in App.Entity.County)
                    {
                        Button button = new Button
                        {
                            Name = "County" + count,
                            Content = item.Name,
                            FontSize = 15,
                            Height = 25.5,
                            FontFamily = new FontFamily("Merienda One"),
                            Style = (Style)FindResource("ComboBoxButton"),

                        };
                        button.Click += Button_Click;
                        count++;
                        CountySP.Children.Add(button);
                    }
                }));
            }).Start();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //Запись в глобальные переменные кнопок
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
    }
}
