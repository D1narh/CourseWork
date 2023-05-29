using DataModels;
using LinqToDB;
using MaimApp.Class.RegistrC;
using MaimApp.Class.User;
using MaimApp.Parser.Models;
using MaimApp.Views.MessageView;
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
        private static BrushConverter brushConverter = new BrushConverter();
        public Registration()
        {
            InitializeComponent();
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(137, 131, 255));
            ((Label)sender).Foreground = brush;
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Brush brush = (Brush)brushConverter.ConvertFrom("#B0BDE9");
            ((Label)sender).Foreground = brush;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (((Label)sender).Content.ToString() == "X")
            {
                DialogResult = false;
                this.Close();
            }
            else
            {
                DialogResult = true;
                this.Close();
            }
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            Registr();
        }

        public void Registr()
        {
            if (name.Text.Trim() == "" || sname.Text.Trim() == "" || email.Text.Trim() == "" || password.Password.Trim() == "" || password2.Password.Trim() == "" || login.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели некоторые параметры", "Внимание");
            }
            else
            {
                if (password.Password.Trim() != password2.Password.Trim())
                {
                    MessageBox.Show("Пароли не свопладают, проверьте корректность ввода", "Внимание");
                }
                else
                {
                    using (var db = new DbA99dc4MaimfDB())
                    {
                        var user = db.Users.FirstOrDefault(x => x.Login == login.Text.Trim() || x.Mail == email.Text.Trim());
                        if (user == null)
                        {
                            MessageSend message = new MessageSend(email.Text.Trim());
                            var code = message.SendMessage();
                            if (code == null)
                            {
                                return;
                            }
                            else
                            {
                                MessageBoxView boxView = new MessageBoxView(code);
                                boxView.ShowDialog();
                                if (boxView.DialogResult == false) // Если пользователь вышел из окна подтверждения почты
                                {
                                    return;
                                }
                                else //Если пользователь смог ввести корректный код с почты
                                {
                                    db.Insert(new User
                                    {
                                        Login = login.Text.Trim(),
                                        Password = password.Password.Trim(),
                                        Mail = email.Text.Trim(),
                                        DateReg = DateTime.Now,
                                        RoleId = 1
                                    });

                                    var id = db.Users.FirstOrDefault(x => x.Login == login.Text.Trim()).Id;
                                    db.Insert(new UserPrData
                                    {
                                        UserId = id,
                                        Name = name.Text.Trim(),
                                        LastName = sname.Text.Trim(),
                                    });
                                    new AuthUser(login.Text.Trim(), password.Password.Trim());
                                    DialogResult = true;
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином или почтой уже существует", "Внимание!");
                        }
                    }
                }
            }
        }
    }
}
