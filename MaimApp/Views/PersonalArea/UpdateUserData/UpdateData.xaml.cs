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
            if (IssuedTB.Text.Trim() == "" || SeriesTB.Text.Trim() == "" || NumberTB.Text.Trim() == "")
            {
                MessageBox.Show("Внимание", "Введите паспортные данные чтобы отправить на одобрение");
            }
            else
            {

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
            using (var db = new DbA96b40MaimfDB())
            {
                if (NameTB.Text.Trim().Length >= 2 || SureNameTB.Text.Trim().Length >= 4)
                {
                    var userData = db.Users.FirstOrDefault(x => x.Id == authUser.GetUserId());
                    if (EmailTB.Text.Trim() != userData.Mail) //Если пользователь менял почту в TextBox
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
            using (var db = new DbA96b40MaimfDB())
            {
                db.UserPrData
                .Where(x => x.UserId == userData.Id)
                .Set(x => x.IssuedPasport, IssuedTB.Text.Trim())
                //.Set(x => x.IssuedDatePasport, IssuedDateDP.SelectedDate)
                .Set(x => x.NumberPasport, NumberTB.Text.Trim())
                .Set(x => x.SeriesPasport, SeriesTB.Text.Trim())
                .Set(x => x.Name, NameTB.Text.Trim())
                .Set(x => x.Surname, SureNameTB.Text.Trim())
                .Set(x => x.LastName, SecondNameTB.Text.Trim())
                //.Set(x => x.TelNumber, int.Parse(PhoneTB.Text.Trim()))
                .Update();

                db.Users.Where(x => x.Id == userData.Id)
                .Set(x => x.Mail, EmailTB.Text.Trim())
                .Update();
            }
        }
        private void NameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(NameTB.Text == "")
            {
                return;
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(NameTB.Text, "^[а-яА-Я ]"))
                {
                    if (NameTB.Text.Length - 1 == -1)
                    {
                        NameTB.Text = "";
                        MessageBox.Show("Можно вводить только буквы");
                    }
                    else
                    {
                        NameTB.Text = NameTB.Text.Remove(NameTB.Text.Length - 1);
                        MessageBox.Show("Можно вводить только буквы");
                    }
                }
                else
                {
                    return;
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
            using (var db = new DbA96b40MaimfDB())
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
                PhoneTB.Text = PersonalData.TelNumber.ToString();
            }
        }
    }
}
