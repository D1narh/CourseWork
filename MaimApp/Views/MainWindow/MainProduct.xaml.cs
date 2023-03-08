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
using MaimApp.Class.RegistrC;

namespace MaimApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainProduct.xaml
    /// </summary>
    public partial class MainProduct : Window
    {
        private readonly Formatter _formatter = new Formatter();

        object senderNow;
        object senderSecond;

        public MainProduct()
        {
            InitializeComponent();

            Sortierung();
            ChangeSitys();
            buttonBackgroung();
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
            senderSecond = senderNow;
            senderNow = sender;

            if (senderSecond != null)//Если жмякнули на другую кнопку, где не было ВЫДВИНУТО
            {
                AnimationLeave(senderSecond);
            }//Т.к. анимация сворачивания не сработает, у кнопки будет Width = 140
        }

        //Анимация выдвижения
        public void AnimationEnter(Button name)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 140;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            name.BeginAnimation(WidthProperty, anim);
        }

        //Анимация сворачивания
        public void AnimationLeave(object sender)
        {
            //Идет проверка на то, стоит ли выполнять сворачивание кнопки ?
            if (senderNow == sender) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                return;
            }
            else
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.To = 130;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                (sender as Button).BeginAnimation(WidthProperty, anim);
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
        public void ChangeSitys()
        {
            var County = new List<string> { "Дальневосточный", "Приволжский", "Северо - Западный", "Северо - Кавказский", "Сибирский", "Уральский", "Центральный", "Южный" };
            foreach(var item in County)
            {
                Button button = new Button
                {
                    Content = item,
                    FontSize = 15,
                    Height = 25.5,
                    FontFamily = new FontFamily("Merienda One"),
                    Style = (Style)FindResource("ComboBoxButton")
                };
                CountySP.Children.Add(button);
            }


            //var count = 0;
            //var gg = 1;

            //var i = new List<string> { "Абакан", "Азов", "Александров", "Алексин", "Альметьевск", "Анапа", "Ангарск", "Анжеро-Судженск", "Апатиты", "Арзамас", "Армавир", "Арсеньев", "Артем", "Архангельск", "Асбест", "Астрахань", "Ачинск", "Балаково", "Балахна", "Балашиха", "Балашов", "Барнаул", " Батайск", "Белгород", "Белебей", "Белово", "Белогорск (Амурская область)", "Белорецк,Белореченск", "Бердск", "Березники", "Березовский (Свердловская область)", "Бийск", "Биробиджан", "Благовещенск (Амурская область)", "Бор", "Борисоглебск", "Боровичи", "Братск", "Брянск", "Бугульма", "Буденновск", "Бузулук", "Буйнакск", "Великие Луки", "Великий Новгород", "Верхняя Пышма", "Видное", "Владивосток", "Владикавказ", "Владимир", "Волгоград", "Волгодонск", "Волжск", "Волжский", "Вологда", "Вольск", "Воркута", "Воронеж", "Воскресенск", "Воткинск", "Всеволожск", "Выборг", "Выкса", "Вязьма", "Гатчина", "Геленджик", "Георгиевск", "Глазов", "Горно-Алтайск", "Грозный", "Губкин", "Гудермес", "Гуково", "Гусь-Хрустальный", "Дербент", "Дзержинск", "Димитровград", "Дмитров", "Долгопрудный", "Домодедово", "Донской", "Дубна", "Евпатория", "Егорьевск", "Ейск", "Екатеринбург", "Елабуга", "Елец", "Ессентуки", "Железногорск (Красноярский край)", "Железногорск (Курская область)", "Жигулевск", "Жуковский,Заречный", "Зеленогорск", "Зеленодольск", "Златоуст", "Иваново", "Ивантеевка", "Ижевск", "Избербаш", "Иркутск", "Искитим", "Ишим", "Ишимбай", "Йошкар-Ола", "Казань,Калининград", "Калуга", "Каменск-Уральский", "Каменск-Шахтинский", "Камышин", "Канск", "Каспийск", "Кемерово", "Керчь", "Кинешма", "Кириши", "Киров (Кировская область)", "Кирово-Чепецк", "Киселевск", "Кисловодск", "Клин", "Клинцы", "Ковров", "Когалым", "Коломна", "Комсомольск-на-Амуре", "Копейск", "Королев", "Кострома", "Котлас", "Красногорск", "Краснодар", "Краснокаменск", "Краснокамск", "Краснотурьинск", "Красноярск", "Кропоткин", "Крымск", "Кстово", "Кузнецк", "Кумертау", "Кунгур", "Курган", "Курск", "Кызыл", "Лабинск", "Лениногорск", "Ленинск-Кузнецкий", "Лесосибирск", "Липецк", "Лиски", "Лобня", "Лысьва", "Лыткарино", "Люберцы", "Магадан", "Магнитогорск", "Майкоп", "Махачкала", "Междуреченск", "Мелеуз", "Миасс", "Минеральные Воды", "Минусинск", "Михайловка", "Михайловск (Ставропольский край)", "Мичуринск", "Москва", "Мурманск", "Муром", "Мытищи", "Набережные Челны", "Назарово", "Назрань", "Нальчик", "Наро-Фоминск", "Находка", "Невинномысск", "Нерюнгри", "Нефтекамск", "Нефтеюганск", "Нижневартовск", "Нижнекамск", "Нижний Новгород", "Нижний Тагил", "Новоалтайск", "Новокузнецк", "Новокуйбышевск", "Новомосковск", "Новороссийск", "Новосибирск", "Новотроицк", "Новоуральск", "Новочебоксарск", "Новочеркасск", "Новошахтинск", "Новый Уренгой", "Ногинск", "Норильск", "Ноябрьск", "Нягань", "Обнинск", "Одинцово", "Озерск (Челябинская область)", "Октябрьский", "Омск", "Орел", "Оренбург", "Орехово-Зуево", "Орск", "Павлово,Павловский Посад", "Пенза", "Первоуральск", "Пермь", "Петрозаводск", "Петропавловск-Камчатский", "Подольск", "Полевской", "Прокопьевск", "Прохладный", "Псков", "Пушкино", "Пятигорск", "Раменское", "Ревда", "Реутов", "Ржев", "Рославль", "Россошь", "Ростов-на-Дону", "Рубцовск", "Рыбинск", "Рязань", "Салават", "Сальск", "Самара", "Санкт-Петербург", "Саранск", "Сарапул", "Саратов", "Саров", "Свободный", "Севастополь", "Северодвинск", "Северск", "Сергиев Посад", "Серов,Серпухов", "Сертолово", "Сибай", "Симферополь", "Славянск-на-Кубани", "Смоленск", "Соликамск", "Солнечногорск", "Сосновый Бор", "Сочи", "Ставрополь", "Старый Оскол", "Стерлитамак", "Ступино", "Сургут", "Сызрань", "Сыктывкар", "Таганрог", "Тамбов", "Тверь", "Тимашевск", "Тихвин", "Тихорецк", "Тобольск", "Тольятти", "Томск", "Троицк", "Туапсе", "Туймазы", "Тула", "Тюмень", "Узловая", "Улан-Удэ", "Ульяновск", "Урус-Мартан", "Усолье-Сибирское", "Уссурийск", "Усть-Илимск", "Уфа", "Ухта", "Феодосия", "Фрязино", "Хабаровск", "Ханты-Мансийск", "Хасавюрт", "Химки", "Чайковский", "Чапаевск", "Чебоксары", "Челябинск", "Черемхово", "Череповец", "Черкесск", "Черногорск", "Чехов", "Чистополь", "Чита", "Шадринск", "Шали", "Шахты", "Шуя", "Щекино", "Щелково", "Электросталь", "Элиста", "Энгельс", "Южно-Сахалинск", "Юрга", "Якутск", "Ялта", "Ярославль" };
            //var ir = new List<StackPanel> { Stack1,Stack2,Stack3 };

            //foreach (var item in i)
            //{
            //    if(gg % 70 == 0)
            //    {
            //        count++;
            //    }
            //    Button button = new Button
            //    {
            //        Content = item,
            //        FontSize = 12,
            //        FontFamily = new FontFamily("Merienda One"),
            //        Padding = new Thickness(3),
            //        Style = (Style)FindResource("ComboBoxButton")
            //    };
            //    gg++;
            //    ir[count].Children.Add(button);
            //}
        }
    }
}
