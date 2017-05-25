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
    /// Логика взаимодействия для ControlPage.xaml
    /// </summary>
    public partial class ControlPage : Page
    {

        private List<User> dynamicMoviesSearch = new List<User>();

        private List<User> otherUsers = new List<User>();

        public ControlPage()
        {
            InitializeComponent();
            Data.ReadUsersData();
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            otherUsers = new List<User>();
            otherUsers = Data.Users;
            UpdateOtherUsers();
            listBox.ItemsSource = null;
            listBox.ItemsSource = otherUsers;
        }

        private void UpdateOtherUsers()
        {
            otherUsers = new List<User>();
            Data.ReadUsersData();
            foreach (User user in Data.Users)
            {
                if (Data.LoggedUser == null || user.Username != Data.LoggedUser.Username)
                    otherUsers.Add(user);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.LoggedUser != null)
                textBlockGreeting.Text = $"Здравствуйте, {Data.LoggedUser.Username}!";
            RefreshListBox();
            textBoxSearch.Text = "";
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dynamicSearchQuery = textBoxSearch.Text;
            dynamicMoviesSearch = new List<User>();
            foreach (User user in otherUsers)
            {
                if (user.Username.ToLower().Contains(dynamicSearchQuery.ToLower()))
                {
                    dynamicMoviesSearch.Add(user);
                }
            }
            listBox.ItemsSource = null;
            listBox.ItemsSource = dynamicMoviesSearch;
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                Data.ReadUsersData();
                User selectedUser = (User)listBox.SelectedItem;
                int selectedListIndex = Data.Users.FindIndex(user => user.Username == selectedUser.Username);
                Data.Users[selectedListIndex].IsAdmin = !Data.Users[selectedListIndex].IsAdmin;
                Data.UpdateUsersData();
                RefreshListBox();
                Logger.Log($"\"{Data.LoggedUser.Username}\" изменил права пользователя {selectedUser.Username}\" на \"{(Data.Users[selectedListIndex].IsAdmin ? "Администратор" : "Обычный пользователь")}\"");
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDelete.IsEnabled = listBox.SelectedIndex != -1;
            buttonChange.IsEnabled = listBox.SelectedIndex != -1;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                User selectedUser = (User)listBox.SelectedItem;
                Data.Users.RemoveAll(user => user.Username == selectedUser.Username);
                textBoxSearch.Text = "";
                Data.UpdateUsersData();
                RefreshListBox();
                Logger.Log($"\"{Data.LoggedUser.Username}\" удалил пользователя: \"{selectedUser.Name}\"");
            }
        }
    }
}
