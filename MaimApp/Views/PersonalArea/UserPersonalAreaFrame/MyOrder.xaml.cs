using MaimApp.Class.Approval;
using MaimApp.Class.MyOrderC;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaimApp.Views.PersonalArea.UserPersonalAreaGrid
{
    /// <summary>
    /// Логика взаимодействия для MyOrderFrame.xaml
    /// </summary>
    public partial class MyOrder : Page
    {
        public MyOrder()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OrderLoader loader = new OrderLoader();
            list.ItemsSource = await loader.Load();
        }
    }
}
