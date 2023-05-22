using MaimApp.Class.Approval;
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

namespace MaimApp.Views.PersonalArea.ManagerPersonalAreaFrame
{
    /// <summary>
    /// Логика взаимодействия для ApprovalF.xaml
    /// </summary>
    public partial class ApprovalF : Page
    {
        public ApprovalF()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadApproval();
        }

        public async Task LoadApproval()
        {
            ApprovalLoader loader = new ApprovalLoader();
            list.ItemsSource = await Task.Run(async () => await loader.Load());
        }

        private void View_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                var a = ((Border)sender).DataContext;
                var IdUser = TypeDescriptor.GetProperties(a)["UserId"].GetValue(a);
                var IdApproval = TypeDescriptor.GetProperties(a)["ApprovalRequestID"].GetValue(a);

                InUserApproval userApproval = new InUserApproval(Convert.ToInt32(IdUser), Convert.ToInt32(IdApproval));
                userApproval.ShowDialog();
            }
        }
    }
}
