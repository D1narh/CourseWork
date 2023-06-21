using MaimApp.Class.BusTickets;
using MaimApp.Class.MainProductC;
using MaimApp.Class.User;
using MaimApp.Views.MessageView;
using MaimApp.Views.PersonalArea;
using MaimApp.Views.Product;
using MaimApp.Views.Treaty.Bus_Tickets;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaimApp.Views.TicketsF
{
    /// <summary>
    /// Логика взаимодействия для BusTicketsF.xaml
    /// </summary>
    public partial class BusTicketsF : Page
    {
        private static readonly BrushConverter brushConverter = new BrushConverter();
        private readonly AuthUser authUser = new AuthUser();
        private readonly TicketsLoader loader = new TicketsLoader();

        public BusTicketsF()
        {
            InitializeComponent();
        }

        private void View_Click(object sender, MouseButtonEventArgs e)
        {
            if (authUser.AuthOrNo())
            {
                if (sender is Border clickedBorder)
                {
                    var a = clickedBorder.DataContext;
                    var IdProduct = int.Parse(TypeDescriptor.GetProperties(a)["ID"].GetValue(a).ToString());
                    TicketBooking tickets = new TicketBooking(loader.GetTicket(IdProduct));
                    tickets.ShowDialog();

                    if (tickets.DialogResult == true)
                    {
                        LoadApproval();
                    }
                }
            }
            else
            {
                NoAuthUserMessageBox messageBox = new NoAuthUserMessageBox("Вы не можете перейти к покупке без авторизации");
                messageBox.ShowDialog();

                if (messageBox.DialogResult == true) // Если пользователь захотел авторизироваться
                {
                    Authorization authorization = new Authorization();
                    authorization.ShowDialog();
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Loaders();
        }

        public async void Loaders()
        {
            await LoadApproval();
            NumberStroke();
        }

        public async Task LoadApproval()
        {
            list.ItemsSource = await loader.Load21Product();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                border.BorderBrush = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.BorderThickness = new Thickness(0, 0, 0, 0);
                border.BorderBrush = (Brush)brushConverter.ConvertFrom("#E0E0E0");
            }
        }

        //Для вывода нумерации товаров
        public void NumberStroke()
        {
            StrokeNumber.Children.Clear();

            int count, nowNumber;

            if (loader.NowPage <= 4 || loader.NowPage == 1)
            {
                count = 1;
                nowNumber = 9 <= loader.CountLine ? 9 : loader.CountLine;
            }
            else
            {
                count = loader.NowPage - 4;
                nowNumber = loader.CountLine <= loader.NowPage + 4 ? loader.CountLine : loader.NowPage + 4;
                if (loader.NowPage + 4 > loader.CountLine)
                {
                    nowNumber = loader.CountLine;
                    count = loader.CountLine - 8;
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

        public async void Button2_Click(object sender, RoutedEventArgs e)//Используется в методе NumberStroke
        {
            if (((Button)sender).Name.ToString() == "Number" + loader.NowPage)
            {
                return;
            }
            else
            {
                loader.NowPage = Convert.ToInt32(((Button)sender).Content.ToString());

                await loader.Load21Product();

                StrokeNumber.Children.Clear();
                NumberStroke();
                GC.Collect();
            }
        }
    }
}
