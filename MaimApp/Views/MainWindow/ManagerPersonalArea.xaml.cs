using DataModels;
using MaimApp.Class.User;
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

namespace MaimApp.Views.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для ManagerPersonalArea.xaml
    /// </summary>
    public partial class ManagerPersonalArea : Page
    {
        public ManagerPersonalArea()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void FillFIO()
        {
            AuthUser authUser = new AuthUser();
            FIO.Content = authUser.UserFIO();
        }
    }
}
