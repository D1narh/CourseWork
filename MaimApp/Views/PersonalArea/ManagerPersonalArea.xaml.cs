using DataModels;
using MaimApp.Class.User;
using MaimApp.Views.PersonalArea.ManagerPersonalAreaFrame;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaimApp.Views.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для ManagerPersonalArea.xaml
    /// </summary>
    public partial class ManagerPersonalArea : Page
    {
        static Grid SecondGrid;
        static object senderNowLeftP, senderSecondLeftP;
        AuthUser authUser = new AuthUser();

        public ManagerPersonalArea()
        {
            InitializeComponent();
        }
        private void LeftPMouseEnter(object sender, MouseEventArgs e)
        {
            AnimationEnter((Button)sender);
        }

        private void LeftPMouseLeave(object sender, MouseEventArgs e)
        {
            LeaveFromButton(sender);
        }

        public void Senderar(object sender, Grid gridName)
        {
            gridName.Visibility = Visibility.Visible;

            //Запись в глобальные переменные кнопок
            senderSecondLeftP = senderNowLeftP;
            senderNowLeftP = sender;

            if (senderSecondLeftP != null)//Если жмякнули на другую кнопку, где не было ВЫДВИНУТО
            {
                LeaveFromButton(senderSecondLeftP, gridName);
            }//Т.к. анимация сворачивания не сработает, у кнопки будет Width = 140
            SecondGrid = gridName;
        }

        //Анимация выдвижения с верху в низ
        public void AnimationEnter(Button name)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 50;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            name.BeginAnimation(HeightProperty, anim);
        }

        //Общий функционал для двух методов (анимация задвигания*)
        public void Leave(object sender)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 40;
            anim.Duration = TimeSpan.FromSeconds(0.1);
            ((Button)sender).BeginAnimation(HeightProperty, anim);
        }

        //Метод для проверки стоит ли делать Грид невидимым при нажатии кнопки
        public void LeaveFromButton(object sender, Grid gridName)
        {
            //Идет проверка на то, стоит ли выполнять сворачивание кнопки ?
            if (((Button)senderNowLeftP).Name + "GFrame" != SecondGrid.Name) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                Leave(sender);
                SecondGrid.Visibility = Visibility.Hidden;
            }
        }

        //Метод когда просто навелись на кнопку (без нажатия на нее сделали focus) и вышли , т.е. потеряли focus
        public void LeaveFromButton(object sender)//Используется в методах с название MouseLeave
        {
            if (senderNowLeftP == sender) //Если нажал два раза на одну и туже кнопку то сработает это <-
            {
                return;
            }
            else
            {
                Leave(sender);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void FillFIO()
        {
            AuthUser authUser = new AuthUser();
            FIO.Content = authUser.UserFIO();
        }

        private void VaitingApproval_Click(object sender, RoutedEventArgs e)
        {
            ApprovalF approval = new ApprovalF();
            ApprovalFrame.Content = approval;

            Senderar(sender, ApprovalVaitGFrame);
        }
    }
}
