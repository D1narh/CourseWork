using MaimApp.Parser.Models;
using Microsoft.Win32;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            buttonBackgroung();
        }
        public void buttonBackgroung()
        {
            Color color = Color.FromArgb(0xFF, 0xB0, 0xBD, 0xE9);
            Login.Background = new SolidColorBrush(color) { Opacity = 0.5 };
            Passwork.Background = new SolidColorBrush(color) { Opacity = 0.5 };
            Mail.Background = new SolidColorBrush(color) { Opacity = 0.5 };
        }
    }
}
