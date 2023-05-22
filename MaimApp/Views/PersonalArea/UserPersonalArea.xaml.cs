using MaimApp.Class.User;
using MaimApp.Views.PersonalArea.UpdateUserData;
using MaimApp.Views.PersonalArea.UserPersonalAreaGrid;
using MaimApp.Views.Treaty;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace MaimApp.Views.PersonalArea
{
    /// <summary>
    /// Логика взаимодействия для UserPersonalArea.xaml
    /// </summary>
    public partial class UserPersonalArea : Page
    {
        //Блок с временными переменными которые отслеживают нажатые кнопки
        static Grid SecondGrid;
        static object senderNowLeftP, senderSecondLeftP;




        public UserPersonalArea()
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

        private void Consideration_Click(object sender, RoutedEventArgs e)
        {
            Consideration consideration = new Consideration();

            ConsiderationFrame.Content = consideration;

            Senderar(sender, ConsiderationGFrame);
        }

        private void UnderConsideration_Click(object sender, RoutedEventArgs e)
        {
            UnderConsideration underConsideration = new UnderConsideration();

            UnderConsiderationFrame.Content = underConsideration;

            Senderar(sender, UnderConsiderationGFrame);
        }

        private void MyOrder_Click(object sender, RoutedEventArgs e)
        {
            MyOrder myOrder = new MyOrder();

            MyOrderFrame.Content = myOrder;

            Senderar(sender, MyOrderGFrame);
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

        private void RedactProfile_Click(object sender, RoutedEventArgs e)
        {
            UpdateData updateData = new UpdateData();
            updateData.ShowDialog();
        }

        public void FillFIO()
        {
            AuthUser authUser = new AuthUser();
            FIO.Content = authUser.UserFIO();

            Status.Content += authUser.GetStatus();
        }
    }
}
