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

        private List<User> dynamicUsersSearch = new List<User>();

        private List<Genre> dynamicGenresSearch = new List<Genre>();

        private List<User> otherUsers = new List<User>();

        public ControlPage()
        {
            InitializeComponent();
            Data.ReadUsersData();
            Data.ReadGenresData();
            RefreshListBoxUsers();
            RefreshListBoxGenres();
        }

        private void RefreshListBoxUsers()
        {
            otherUsers = new List<User>();
            otherUsers = Data.Users;
            UpdateOtherUsers();
            listBoxUsers.ItemsSource = null;
            listBoxUsers.ItemsSource = otherUsers;
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
            Pages.MainPageAsAdmin.RefreshListBox();
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.LoggedUser != null)
                textBlockGreeting.Text = $"Здравствуйте, {Data.LoggedUser.Username}!";
            RefreshListBoxUsers();
            textBoxSearchUsers.Text = "";
            textBoxSearchGenres.Text = "";
        }

        private void textBoxSearchUsers_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dynamicSearchQuery = textBoxSearchUsers.Text;
            dynamicUsersSearch = new List<User>();
            foreach (User user in otherUsers)
            {
                if (user.Username.ToLower().Contains(dynamicSearchQuery.ToLower()))
                {
                    dynamicUsersSearch.Add(user);
                }
            }
            listBoxUsers.ItemsSource = null;
            listBoxUsers.ItemsSource = dynamicUsersSearch;
        }

        private void buttonChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUsers.SelectedIndex != -1)
            {
                Data.ReadUsersData();
                User selectedUser = (User)listBoxUsers.SelectedItem;
                int selectedListIndex = Data.Users.FindIndex(user => user.Username == selectedUser.Username);
                Data.Users[selectedListIndex].IsAdmin = !Data.Users[selectedListIndex].IsAdmin;
                Data.UpdateUsersData();
                RefreshListBoxUsers();
                Logger.Log($"\"{Data.LoggedUser.Username}\" изменил права пользователя {selectedUser.Username}\" на \"{(Data.Users[selectedListIndex].IsAdmin ? "Администратор" : "Обычный пользователь")}\"");
            }
        }

        private void listBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonDeleteUser.IsEnabled = listBoxUsers.SelectedIndex != -1;
            buttonChangeUser.IsEnabled = listBoxUsers.SelectedIndex != -1;
        }

        private void listBoxGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxGenres.SelectedIndex != -1)
            {
                Genre selectedGenre = (Genre)listBoxGenres.SelectedItem;
                if (selectedGenre.Id == 0)
                {
                    buttonDeleteGenre.IsEnabled = false;
                    buttonChangeGenre.IsEnabled = false;
                }
                else
                {
                    UpdateButtonsGenres();
                }
            }
        }

        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUsers.SelectedIndex != -1)
            {
                User selectedUser = (User)listBoxUsers.SelectedItem;
                Data.Users.RemoveAll(user => user.Username == selectedUser.Username);
                textBoxSearchUsers.Text = "";
                Data.UpdateUsersData();
                RefreshListBoxUsers();
                Logger.Log($"\"{Data.LoggedUser.Username}\" удалил пользователя: \"{selectedUser.Name}\"");
            }
        }

        private void buttonAddGenre_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddGenreWindow();
            if (window.ShowDialog().Value)
            {
                Data.ReadGenresData();
                Data.Genres.Add(window.NewGenre);
                Data.UpdateGenresData();
                Data.IsSearched = false;
                UpdateButtonsGenres();
                RefreshListBoxGenres();
                Logger.Log($"Добавлен новый жанр: \"{window.NewGenre.Name}\"");
            }
        }

        private void buttonChangeGenre_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxGenres.SelectedIndex != -1)
            {
                Genre selectedGenre = (Genre)listBoxGenres.SelectedItem;
                int selectedListIndex = Data.Genres.FindIndex(genre => genre.Id == selectedGenre.Id);

                var window = new ChangeGenreWindow(Data.Genres[selectedListIndex]);
                if (window.ShowDialog().Value)
                {
                    string oldName = Data.Genres[selectedListIndex].Name;
                    Data.Genres[selectedListIndex].Name = window.ChangedName;
                    Data.UpdateGenresData();
                    RefreshListBoxGenres();
                    Logger.Log(oldName == window.ChangedName ?
                        $"Изменён жанр: \"{window.ChangedName}\"" :
                        $"Изменён жанр: \"{oldName}\" -> \"{window.ChangedName}\"");
                }
            }
        }

        private void buttonDeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxGenres.SelectedIndex != -1)
            {
                Genre selectedGenre = (Genre)listBoxGenres.SelectedItem;
                var window = new DeleteGenreWindow(selectedGenre);
                if (window.ShowDialog().Value)
                {
                    int originalId = selectedGenre.Id;
                    int replacementId = window.Replacement.Id;

                    Data.ReadGenresData();
                    foreach (Movie movie in Data.Movies)
                    {
                        if (movie.GenreId == originalId)
                            movie.GenreId = replacementId;
                    }

                    Data.Genres.RemoveAll(genre => genre.Id == originalId);
                    Data.UpdateGenresData();
                    RefreshListBoxGenres();
                    textBoxSearchGenres.Text = "";
                    Logger.Log($"Удалён жанр \"{selectedGenre.Name}\" с заменой на \"{window.Replacement.Name}\"");
                }
            }
        }

        private void RefreshListBoxGenres()
        {
            listBoxGenres.ItemsSource = null;
            listBoxGenres.ItemsSource = Data.Genres;
        }

        private void UpdateButtonsGenres()
        {
            buttonDeleteGenre.IsEnabled = listBoxGenres.SelectedIndex != -1;
            buttonChangeGenre.IsEnabled = listBoxGenres.SelectedIndex != -1;
        }

        private void textBoxSearchGenres_TextChanged(object sender, TextChangedEventArgs e)
        {
            string dynamicSearchQuery = textBoxSearchGenres.Text;
            dynamicGenresSearch = new List<Genre>();
            foreach (Genre genre in Data.Genres)
            {
                if (genre.Name.ToLower().Contains(dynamicSearchQuery.ToLower()))
                {
                    dynamicGenresSearch.Add(genre);
                }
            }
            listBoxGenres.ItemsSource = null;
            listBoxGenres.ItemsSource = dynamicGenresSearch;
        }
    }
}
