using DataModels;
using LinqToDB;
using MaimApp.Class.RegistrC;
using MaimApp.Class.User;
using MaimApp.Views.MessageView;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MaimApp.Views.PersonalArea.UpdateUserData
{
    /// <summary>
    /// Логика взаимодействия для UpdateData.xaml
    /// </summary>
    public partial class UpdateData : Window
    {
        AuthUser authUser = new AuthUser();
        public UpdateData()
        {
            InitializeComponent();
        }

        private void AcceptB_Click(object sender, RoutedEventArgs e) //|| DateIssuedTB.Text.Trim() == "" 
        {
            if (RadioB.IsChecked.Value == true)
            {
                DateTime? date = IssuedDateDP.SelectedDate;
                if (IssuedTB.Text.Trim() == "" || date == null || SeriesTB.Text.Trim() == "" || NumberTB.Text.Trim() == "")
                {
                    MessageBox.Show("Внимание", "Введите паспортные данные чтобы отправить на одобрение");
                }
                else
                {
                    using (var db = new DbA99dc4MaimfDB())
                    {
                        var Data = from u in db.UserPrData
                                   join ar in db.ApprovalRequests on u.UserId equals ar.UserId
                                   join a in db.Approvals on ar.Id equals a.ApprovalRequestId
                                   where a.IsOk == 0 && u.UserId == authUser.GetUserId()
                                   select u.UserId;
                        if (Data.Count() >= 1)
                        {
                            MessageBox.Show("Вы не можете отправить заяву на одобрение\nКогда у вас уже есть активная заявка ", "Внимание");
                            return;
                        }

                        db.Insert(new ApprovalRequest
                        {
                            UserId = Convert.ToInt32(authUser.GetUserId()),
                            Date = DateTime.Now
                        });

                        var ApprovalRequestID = db.ApprovalRequests.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.UserId == authUser.GetUserId());

                        db.Insert(new Approval
                        {
                            ApprovalRequestId = ApprovalRequestID.Result.Id,
                            IsOk = 0
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы не можете отправить на одобрение\nНе дав согласие на обработку пер.данных","Внимание");
            }
        }

        private void SaveChangeB_Click(object sender, RoutedEventArgs e)
        {
            SaveChange();
            DialogResult = true;
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private void SaveChange()
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                if (NameTB.Text.Trim().Length >= 2 || SureNameTB.Text.Trim().Length >= 4)
                {
                    var userData = db.Users.FirstOrDefault(x => x.Id == authUser.GetUserId());
                    if (EmailTB.Text.Trim() != userData.Mail) //Если пользователь менял почту в TextBox
                    {
                        if (EmailTB.Text.Trim().Length > 0)
                        {
                            MessageSend message = new MessageSend(EmailTB.Text.Trim());
                            var code = message.SendMessage();
                            if (code == null)//Если код для сообщение не был сгенирирован
                            {
                                MessageBox.Show("Внимание", "Код для подтверждения не был сгенирирован\nНажмите кнопку сохранения еще раз");
                            }
                            else
                            {
                                MessageBoxView boxView = new MessageBoxView(code);
                                boxView.ShowDialog();
                                if (boxView.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                                {
                                    MessageBox.Show("Внимание", "Вы не ввели код\nПоэтому сохранятся все изменения кроме почты");
                                    EmailTB.Text = userData.Mail;
                                    Save(userData);
                                }
                            }
                        }
                    }
                    else //Если пользователь не менял почту в TextBox
                    {
                        Save(userData);
                    }
                }
                else
                {
                    MessageBox.Show("Внимание", "Имя или фамилия не может быть меньше двух знаков имя, и 4 знаков фамилия");
                }
            }
        }

        private void Save(User userData)
        {
            if (PhoneTB.Text.Trim().Length > 11 || SeriesTB.Text.Trim().Length > 4 || NumberTB.Text.Trim().Length > 6)
            {
                MessageBox.Show("Внимание", "Перепроверьте паспортные данные\nНекоторые значения выходят за рамки диапозона");
            }
            else
            {
                DateTime? date = IssuedDateDP.SelectedDate;
                using (var db = new DbA99dc4MaimfDB())
                {
                    db.UserPrData
                    .Where(x => x.UserId == userData.Id)
                    .Set(x => x.IssuedPasport, IssuedTB.Text.Trim())
                    .Set(x => x.IssuedDatePasport, date)
                    .Set(x => x.NumberPasport, NumberTB.Text.Trim())
                    .Set(x => x.SeriesPasport, SeriesTB.Text.Trim())
                    .Set(x => x.Name, NameTB.Text.Trim())
                    .Set(x => x.Surname, SureNameTB.Text.Trim())
                    .Set(x => x.LastName, SecondNameTB.Text.Trim())
                    .Set(x => x.TelNumber, PhoneTB.Text.Trim())
                    .Update(); 

                    db.Users.Where(x => x.Id == userData.Id)
                    .Set(x => x.Mail, EmailTB.Text.Trim())
                    .Update();
                }
            }
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInfClient();
        }

        private void LoadInfClient()
        {
            using (var db = new DbA99dc4MaimfDB())
            {
                var PersonalData = db.UserPrData.FirstOrDefault(x => x.UserId == authUser.GetUserId());

                NameTB.Text = PersonalData.Name;
                SureNameTB.Text = PersonalData.Surname;
                SecondNameTB.Text = PersonalData.LastName;

                SeriesTB.Text = PersonalData.SeriesPasport;
                NumberTB.Text = PersonalData.NumberPasport;
                IssuedTB.Text = PersonalData.IssuedPasport;
                IssuedDateDP.Text = PersonalData.IssuedDatePasport.ToString();

                EmailTB.Text = authUser.GetEmail();

                if (PersonalData.TelNumber == null)
                {
                    PersonalData.TelNumber = "";
                }
                PhoneTB.Text = PersonalData.TelNumber;
            }
        }
    }
}
