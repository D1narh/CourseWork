using DataModels;
using LinqToDB;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MaimApp.Views.PersonalArea.AdminPanel
{
    /// <summary>
    /// Логика взаимодействия для NewProductW.xaml
    /// </summary>
    public partial class NewProductW : Window
    {
        public NewProductW()
        {
            InitializeComponent();
        }

        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Trim() == "" || DescroptionTB.Text.Trim() == "" || ShortDescroptionTB.Text.Trim() == "" || int.Parse(Price.Text.ToString()) <= 0 || ImagePath.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели некоторые параметры", "Внимание");
            }
            else
            {
                if (NameTB.Text.Trim().Length > 100 || DescroptionTB.Text.Trim().Length > 1000 || ShortDescroptionTB.Text.Trim().Length > 500)
                {
                    MessageBox.Show("Вы привысили лимит на допустимое колличество символов", "Внимание");
                }
                else
                {
                    if (NameTB.Text.Trim().Length == 1 || DescroptionTB.Text.Trim().Length == 1 || ShortDescroptionTB.Text.Trim().Length == 1)
                    {
                        MessageBox.Show("Вы ввели только один символ в одно из полей ", "Внимание");
                    }
                    else
                    {
                        using (var db = new DbA99dc4MaimfDB())
                        {
                            db.Insert(new CompanyProduct
                            {
                                Name = NameTB.Text.Trim(),
                                Description = DescroptionTB.Text.Trim(),
                                ShorDescription = ShortDescroptionTB.Text.Trim(),
                                Price = int.Parse(Price.Text.ToString().Trim()),
                                Image = ImagePath.Text.Trim(),
                                CategoriId = 3
                            });
                            DialogResult = true;
                        }
                    }
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
