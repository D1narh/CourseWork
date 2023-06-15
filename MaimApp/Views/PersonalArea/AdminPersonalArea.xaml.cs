using MaimApp.Class.User;
using MaimApp.Views.PersonalArea.AdminPanel;
using MaterialDesignThemes.Wpf;
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

namespace MaimApp.Views.PersonalArea
{
    /// <summary>
    /// Логика взаимодействия для AdminPersonalArea.xaml
    /// </summary>
    public partial class AdminPersonalArea : Page
    {
        public AdminPersonalArea()
        {
            InitializeComponent();
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProductW product = new NewProductW();
            product.ShowDialog();

        }

        public void FillFIO()
        {
            AuthUser authUser = new AuthUser();
            FIO.Content = authUser.UserFIO();
        }
    }
}
