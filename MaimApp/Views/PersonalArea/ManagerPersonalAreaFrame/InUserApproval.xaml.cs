using DataModels;
using LinqToDB;
using Microsoft.Kiota.Abstractions;
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

namespace MaimApp.Views.PersonalArea.ManagerPersonalAreaFrame
{
    /// <summary>
    /// Логика взаимодействия для InUserApproval.xaml
    /// </summary>
    public partial class InUserApproval : Window
    {

        int UserID;
        int ApprovalID;
        public InUserApproval(int userID,int approvalID)
        {
            InitializeComponent();
            UserID = userID;
            ApprovalID = approvalID;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadData()
        {
            using (var db = new DbA96b40MaimfDB())
            {
                var PersonalData = db.UserPrData.FirstOrDefault(x => x.UserId == UserID);
                var UserData = db.Users.FirstOrDefault(x => x.Id == UserID);

                NameTB.Text = PersonalData.Name;
                SureNameTB.Text = PersonalData.Surname;
                SecondNameTB.Text = PersonalData.LastName;

                SeriesTB.Text = PersonalData.SeriesPasport;
                NumberTB.Text = PersonalData.NumberPasport;
                IssuedTB.Text = PersonalData.IssuedPasport;
                IssuedDateDP.Text = PersonalData.IssuedDatePasport.ToString();

                MailTB.Text = UserData.Mail;

                if (PersonalData.TelNumber == null)
                {
                    PersonalData.TelNumber = "";
                }
                PhoneTB.Text = PersonalData.TelNumber;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void RejectB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            using (var db = new DbA96b40MaimfDB())
            {
                db.Approvals
                .Where(x => x.ApprovalRequestId == ApprovalID)
                .Set(x => x.IsOk, -1)
                .Update();
            }
        }

        private void ApproveB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            using (var db = new DbA96b40MaimfDB())
            {
                db.Approvals
                .Where(x => x.ApprovalRequestId == ApprovalID)
                .Set(x => x.IsOk, 1)
                .Update();


                db.Users
                .Where(x => x.Id == UserID)
                .Set(x => x.Status, 2)
                .Update();
            }
        }
    }
}
