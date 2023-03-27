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
            Animation();
        }
        public void Animation()
        {
            DoubleAnimation anim = new DoubleAnimation();
            if (SelNumPeople.Visibility == Visibility.Hidden)
            {
                SelNumPeople.Visibility = Visibility.Visible;
                anim.To = 134;
                anim.Duration = TimeSpan.FromSeconds(0.25);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
            }
            else
            {
                anim.To = 0;
                anim.Duration = TimeSpan.FromSeconds(0.1);
                SelNumPeople.BeginAnimation(HeightProperty, anim);
                SelNumPeople.Visibility = Visibility.Hidden;
            }
        }
    }
}
