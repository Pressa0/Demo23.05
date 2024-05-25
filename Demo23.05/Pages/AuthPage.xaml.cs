using Demo23._05.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Demo23._05.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private DemkaEntities db;
        public AuthPage()
        {
            db = new DemkaEntities();
            InitializeComponent();
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txt_Login.Text!=null)
                hintLogin.Visibility = Visibility.Collapsed;
            else hintLogin.Visibility = Visibility.Visible;
        }

        private void txt_Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txt_Pass.Password != null)
                hintPass.Visibility = Visibility.Collapsed;
            else hintPass.Visibility = Visibility.Visible;
        }

        private void btn_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = txt_Login.Text;
            string pass = txt_Pass.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
                MessageBox.Show("Заполните все поля!");
            else
            {
                User uesr = db.User.Where(u=>u.Login== login && u.Password==pass).FirstOrDefault();
                if (uesr != null)
                {
                    MessageBox.Show($"Добро пожаловать, {uesr.LastName} {uesr.FirstName}");
                    App.AuthUser = uesr;
                    NavigationService.GoBack();
                }
                else
                    MessageBox.Show("Данный пользователь не найден");
            }
        }

        private void btn_ToReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
