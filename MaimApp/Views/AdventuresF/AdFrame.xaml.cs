using MaimApp.Class.AdventuresC;
using MaimApp.Class.MainProductC;
using MaimApp.Class.User;
using MaimApp.Views.MessageView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MaimApp.Views.AdventuresF
{
    /// <summary>
    /// Логика взаимодействия для AdFrame.xaml
    /// </summary>
    public partial class AdFrame : Page
    {
        private static readonly BrushConverter brushConverter = new BrushConverter();
        private readonly AuthUser authUser = new AuthUser();
        private readonly AdLoader loader = new AdLoader();
        ViewProduct viewProduct = new ViewProduct();

        public AdFrame()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
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

        private void View_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
