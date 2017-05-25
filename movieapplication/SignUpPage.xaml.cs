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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Pages.MainPage.UpdateSearchState();
            Pages.ChangeFrameSize(500, 750);
            NavigationService.Navigate(Pages.MainPage);
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show("Необходимо ввести логин", "Ошибка!");
                textBoxUsername.Focus();
                return;
            }

            if (!Data.checkUsernameValidity(textBoxUsername.Text))
            {
                MessageBox.Show("Использованы запрещенные символы. Разрешены: латинские символы, цифры и нижнее подчёркивание", "Ошибка!");
                textBoxUsername.Focus();
                return;
            }

            if (!Data.checkIfUsernameIsUsed(textBoxUsername.Text))
            {
                MessageBox.Show("Данный логин занят. Необходимо использовать другой логин", "Ошибка!");
                textBoxUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Необходимо ввести имя", "Ошибка!");
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                MessageBox.Show("Необходимо ввести фамилию", "Ошибка!");
                textBoxSurname.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Необходимо ввести пароль", "Ошибка!");
                passwordBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordBoxRepeat.Password))
            {
                MessageBox.Show("Необходимо повторно ввести пароль", "Ошибка!");
                passwordBoxRepeat.Focus();
                return;
            }
            else if (passwordBox.Password != passwordBoxRepeat.Password)
            {
                MessageBox.Show("Введенные пароли не совпадают", "Ошибка!");
                return;
            }

            Data.ReadUsersData();
            Data.Users.Add(new User(textBoxUsername.Text, textBoxName.Text, textBoxSurname.Text, passwordBox.Password));
            Data.LoggedUser = Data.Users.LastOrDefault();
            MessageBox.Show($"{textBoxName.Text}, вы были успешно зарегистрированы в системе, как \"{textBoxUsername.Text}\"", "Успешно!");
            Data.UpdateUsersData();
            Pages.ChangeFrameSize(500, 750);
            Pages.MainPageAsUser.UpdateSearchState();
            Logger.Log($"Регистрация и вход пользователя: \"{textBoxUsername.Text}\"");
            NavigationService.Navigate(Pages.MainPageAsUser);
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Pages.ChangeFrameSize(220, 220);
            NavigationService.Navigate(Pages.LoginPage);
        }

        private void CheckUsername(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                if (Data.checkUsernameValidity(textBoxUsername.Text))
                {
                    if (Data.checkIfUsernameIsUsed(textBoxUsername.Text))
                    {
                        textBlockUsernameCheck.Text = "Данный логин доступен";
                        textBlockUsernameCheck.Foreground = Brushes.DarkGreen;
                    }
                else
                    {
                        textBlockUsernameCheck.Text = "Данный логин занят";
                        textBlockUsernameCheck.Foreground = Brushes.Firebrick;
                    }
                }
                else
                {
                    textBlockUsernameCheck.Text = "Разрешены: латинские символы, цифры и нижнее подчёркивание";
                    textBlockUsernameCheck.Foreground = Brushes.Firebrick;
                }
            }
            else
            {
                textBlockUsernameCheck.Text = "Для проверки введите логин";
                textBlockUsernameCheck.Foreground = Brushes.Black;
            }
        }
    }
}
