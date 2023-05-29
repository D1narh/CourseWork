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
        private static BrushConverter brushConverter = new BrushConverter();
        AuthUser authUser = new AuthUser();
        TicketsLoader loader = new TicketsLoader();

        public BusTicketsF()
        {
            InitializeComponent();
        }

        private async void View_Click(object sender, MouseButtonEventArgs e)
        {
            if (authUser.AuthOrNo())
            {
                if (sender != null)
                {
                    var a = ((Border)sender).DataContext;
                    var IdProduct = int.Parse(TypeDescriptor.GetProperties(a)["ID"].GetValue(a).ToString());
                    TicketBooking tickets = new TicketBooking(loader.GetTicket(IdProduct));
                    tickets.ShowDialog();

                    if(tickets.DialogResult == true)
                    {
                        await LoadApproval();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                NoAuthUserMessageBox messageBox = new NoAuthUserMessageBox("Вы не можете перейти к покупке без авторизации");
                messageBox.ShowDialog();
                if (messageBox.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                {
                    return;
                }
                else //Если пользователь захотел авторизироваться
                {
                    Authorization authorization = new Authorization();
                    authorization.ShowDialog();
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           await LoadApproval();
        }

        public async Task LoadApproval()
        {
            list.ItemsSource = await Task.Run(async () => await loader.Load());
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderThickness = new Thickness(1, 1, 1, 1);
            ((Border)sender).BorderBrush = (Brush)brushConverter.ConvertFrom("#B0BDE9");
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderThickness = new Thickness(0, 0, 0, 0);
            ((Border)sender).BorderBrush = (Brush)brushConverter.ConvertFrom("#E0E0E0");
        }
    }
}
