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
using System.Windows.Shapes;

namespace MaimApp.Views.PersonalArea
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private static BrushConverter brushConverter = new BrushConverter();

        public Authorization()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            AuthClick();
        }
        public void AuthClick()
        {
            AuthUser authUser = new AuthUser(tb_login.Text, tb_password.Password);
            if (authUser.AuthOrNo())
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не верный логин или пароль", "Внимание");
            }
        }

        private void l_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        private void l_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(137, 131, 255));
            ((Label)sender).Foreground = brush;
        }

        private void l_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            Brush brush = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            ((Label)sender).Foreground = brush;
        }

        private void Registr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            if(registration.DialogResult == false)
            {
                this.Close();
            }
        }
    }
}
