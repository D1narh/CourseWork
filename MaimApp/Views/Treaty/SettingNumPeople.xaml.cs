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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaimApp.Views.Treaty
{
    /// <summary>
    /// Логика взаимодействия для SettingNumPeople.xaml
    /// </summary>
    public partial class SettingNumPeople : Window
    {
        public SettingNumPeople()
        {
            InitializeComponent();
        }

        private void People_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (NumPeople.Visibility == Visibility.Hidden)
            {
                NumPeople.Visibility = Visibility.Visible;
                anim.To = 156;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                NumPeople.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                NumPeople.BeginAnimation(HeightProperty, anim);
                NumPeople.Visibility = Visibility.Hidden;
            }
        }
    }
}
