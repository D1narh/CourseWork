﻿using System;
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

namespace MaimApp.Views.MessageView
{
    /// <summary>
    /// Логика взаимодействия для MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView : Window
    {
        string MailCode;
        public MessageBoxView(string code)
        {
            InitializeComponent();
            MailCode = code;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (emailCode.Text.Trim() == MailCode)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Вы ввели не верный код, попробуйте снова","Внимание!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}