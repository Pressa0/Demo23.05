using Demo23._05.DataBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Demo23._05.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        private DemkaEntities db;
        public RegPage()
        {
            db = new DemkaEntities();
            InitializeComponent();
            cmbBox_Role.ItemsSource = db.Role.ToList();
        }

        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = txt_Login.Text;
            string pass = txt_Pass.Password;
            string firstName = txt_FirstName.Text;
            string lastName = txt_LastName.Text;
            string patronymic = txt_Patronymic.Text;
            Role role = (Role)cmbBox_Role.SelectedItem;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic) || role == null)
                MessageBox.Show("Заполните все поля");
            else
            {
                User user = db.User.Where(u=>u.Login==login).FirstOrDefault();

                if(user!=null)
                    MessageBox.Show("Пользователь уже создан");
                else
                {
                    User newUser = new User()
                    {
                        Login = login,
                        Password = pass,
                        FirstName = firstName,
                        LastName = lastName,
                        Patronymic = patronymic,
                        RoleID= role.ID
                    };
                    db.User.Add(newUser);
                    db.SaveChanges();
                    MessageBox.Show("Пользователь создан");
                    App.AuthUser = newUser;
                    NavigationService.Navigate(new MainPage());
                }
            }

        }

        private void txt_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Login.Text != null)
                hintLogin.Visibility = Visibility.Collapsed;
            else hintLogin.Visibility = Visibility.Visible;
        }

        private void txt_Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txt_Pass.Password != null)
                hintPass.Visibility = Visibility.Collapsed;
            else hintPass.Visibility = Visibility.Visible;
        }

        private void txt_LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_LastName.Text != null)
                hintLastName.Visibility = Visibility.Collapsed;
            else hintLastName.Visibility = Visibility.Visible;
        }

        private void txt_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_FirstName.Text != null)
                hintFirstName.Visibility = Visibility.Collapsed;
            else hintFirstName.Visibility = Visibility.Visible;
        }

        private void txt_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Patronymic.Text != null)
                hintPatronymic.Visibility = Visibility.Collapsed;
            else hintPatronymic.Visibility = Visibility.Visible;
        }


    }
}
