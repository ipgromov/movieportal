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
using System.Security.Cryptography;

namespace movieapplication
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = textBoxUsername.Text;
            if (!string.IsNullOrWhiteSpace(username))
            {
                string password = CalculateHash(passwordBox.Password);
                if (!string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    bool isFound = false;
                    Data.ReadUsersData();
                    foreach (User user in Data.Users)
                    {
                        if (user.Username == username)
                        {
                            isFound = true;
                            if (user.Password == password)
                            {
                                Pages.ChangeFrameSize(500, 750);
                                Data.LoggedUser = user;
                                Logger.Log($"Успешный вход пользователя: \"{username}\" ({(user.IsAdmin ? "администратор" : "обычный пользователь")})");
                                if (user.IsAdmin)
                                {
                                    Pages.MainPageAsAdmin.UpdateSearchState();
                                    NavigationService.Navigate(Pages.MainPageAsAdmin);
                                }
                                else
                                {
                                    Pages.MainPageAsUser.UpdateSearchState();
                                    NavigationService.Navigate(Pages.MainPageAsUser);
                                }
                                textBoxUsername.Text = "";
                            }
                            else
                            {
                                passwordBox.Password = "";
                                MessageBox.Show("Неверный пароль", "Ошибка!");
                                passwordBox.Focus();
                            }
                        }
                    }
                    if (!isFound)
                    {
                        MessageBox.Show("Пользователя с таким псевдонимом не существует", "Ошибка!");
                        textBoxUsername.Focus();
                    }
                    else
                    {
                        isFound = false;
                    }

                }
                else
                {
                    MessageBox.Show("Необходимо ввести пароль", "Ошибка!");
                    passwordBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Необходимо ввести псевдоним", "Ошибка!");
                textBoxUsername.Focus();
            }
        }

        private string CalculateHash(string password)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Pages.MainPage.UpdateSearchState();
            Pages.ChangeFrameSize(500, 750);
            NavigationService.Navigate(Pages.MainPage);
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            Pages.ChangeFrameSize(400, 370);
            NavigationService.Navigate(Pages.SignUpPage);
        }
    }
}
